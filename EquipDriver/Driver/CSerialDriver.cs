using Common;
using Modbus.Device;
using ModBusRTU;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Timers;
using System.Web.ModelBinding;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace EquipDriver
{
    public class CSerialDriver : IEquipDriver, IDisposable
    {
        private SerialPort Comm = null;
        private DateTime prevRqTime = DateTime.Now;
        public IModbusMaster master;
        /// <summary>
        /// 读取超时时间
        /// </summary>

        // 重试配置
        public const int MaxRetries = 3;

        private string initparam = null;

        #region 初始化
        public void Dispose()
        {
            Close();
            if (Comm != null)
            {
                Comm.Dispose();
                Comm = null;
            }
        }
        public void Close()
        {
            try
            {
                if (Comm != null)
                {
                    if (Comm.IsOpen) Comm.Close();
                    Comm = null;
                }
            }
            catch (Exception)
            {

            }
        }

        public bool Open(string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits)
        {
            try
            {
                Close();
                Comm = new SerialPort(portName, baudRate, parity, dataBits, stopBits);
                //初始化SerialPort对象
                Comm.ParityReplace = 0xFF;
                Comm.NewLine = Environment.NewLine;
                Comm.RtsEnable = true;//根据实际情况吧。

                Comm.ReadTimeout = 1000;
                Comm.WriteTimeout = 1500;
                Comm.ReceivedBytesThreshold = 1;
                // Comm.DataReceived += ReceiveCallback;
                Comm.ReadBufferSize = 4096;

                Comm.Open();
                if (master == null)
                {
                    master = ModbusSerialMaster.CreateRtu(Comm);
                    master.Transport.ReadTimeout = 1000;
                    master.Transport.WriteTimeout = 1500;
                }

                return true;
            }
            catch (Exception e)
            {
                CSysDelegateEvent.ShowDebugInfo("串口打开失败：" + e.Message);
                CSysDelegateEvent.ShowStatusInfo("串口打开失败：" + e.Message);
            }
            return false;
        }
        #endregion

        #region Write
        public bool IsConn()
        {
            if (Comm == null) return false;
            return Comm.IsOpen;
        }
        public void Write(string text)
        {
            if (IsConn() == false) return;
            Comm.Write(text);
        }
        public void Write(byte[] buffer, int offset, int count)
        {
            //if (IsConn() == false) return;
            //try
            //{
            //    Comm.Write(buffer, offset, count);
            //}
            //catch (Exception)
            //{
            //    Close();
            //    CSysDelegateEvent.ShowDebugInfo("串口异常关闭");
            //}

        }
        #endregion

        #region 处理接收到的数据的委托

        //委托的实现
        //串口接收数据事件响应
        public void ReceiveCallback(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {

                if (Comm.BytesToRead == 0) return;

                int n = Comm.BytesToRead;//先记录下来，避免某种原因，人为的原因，操作几次之间时间长，缓存不一致
                byte[] newbyte = new byte[n];//声明一个临时数组存储当前来的串口数据

                int readlen = Comm.Read(newbyte, 0, n);//读取缓冲数据


                ////对应发送命令
                ////解析响应帧的关键信息（从站地址、功能码）
                //if (newbyte.Length < 2) return; // 无效帧
                //byte slaveAddress = newbyte[0];
                //CModbusCode function = (CModbusCode)newbyte[1];

                //// 处理错误响应（功能码最高位为1表示错误）
                //if ((newbyte[1] & 0x80) != 0)
                //{
                //    function = (CModbusCode)(newbyte[1] & 0x7F); // 提取原始功能码
                //    Log.Error($"收到错误响应，功能码：{function}，错误码：{newbyte[2]}");
                //}

                //// 3. 匹配待处理命令
                //var key = (slaveAddress, function);
                //if (_pendingCommands.TryGetValue(key, out var command))
                //{
                //    // 4. 关联响应数据
                //    command.ResponseData = CMethord.ConvertToCoreDataList(newbyte[1], newbyte);
                //    command.IsSuccess = (newbyte[1] & 0x80) == 0; // 无错误标识
                //    if (!command.IsSuccess || command.ResponseData == null || command.ResponseData.Count == 0)
                //    {
                //        command.Error = $"错误码：{newbyte[2]}";
                //    }

                //    // 5. 唤醒等待的发送线程
                //    command.WaitHandle.Set();

                //接收数据
                CSysDelegateEvent.SerialRcvThread?.Invoke(newbyte, 0, readlen);
                RcvInfoEvent?.Invoke("", newbyte, 0, readlen);
            }
            catch (Exception)
            {
                Loger.Error("数据接收报错");
            }
            finally
            {

            }
        }


        #endregion

        #region 对外接口

        public void Init(string param, string info)
        {
            initparam = info;
            string[] strparam = initparam.Split(';');

            CSysDelegateEvent.ShowDebugInfo("正在打开串口" + initparam);

            Open(strparam[0],
                Convert.ToInt32(strparam[1]),
                Convert.ToInt16(strparam[2]) == 1 ? Parity.Even : (Convert.ToInt16(strparam[2]) == 2 ? Parity.Odd : Parity.None),
                Convert.ToInt16(strparam[3]),
                Convert.ToInt16(strparam[4]) == 0 ? StopBits.One : StopBits.Two);
        }

        public void Close(string param)
        {
            Close();
        }
        public bool IsOnline(string param)
        {
            return IsConn();
        }
        public void SendString(string param, string info)
        {
            Write(info);
        }
        public void SendByte(CModbusReg reg)
        {
            try
            {
                
                byte[] buffer = CModbus.DealMasterSnd(reg);
                reg.IsSuccess = false;
                //发送数据委托
                CSysDelegateEvent.SerialSendThread?.Invoke(buffer);

                int TempCount = 0;
                while (TempCount < MaxRetries)
                {
                    TempCount++;
                    try
                    {
                        StopwatchHelper.Lap(reg.name, "开始发送");
                        // 根据功能码执行不同操作
                        switch (reg.code)
                        {
                            case CModbusCode.ReadCoil: // 读线圈状态
                                bool[] bls = master.ReadCoils((byte)reg.addr, (ushort)reg.regstart, (ushort)reg.regnum);
                                int[] ibls = new int[bls.Length];
                                for (int i = 0; i < bls.Length; i++)
                                {
                                    // 映射规则：可根据需求修改（如 true→255，false→0）
                                    ibls[i] = bls[i] ? 1 : 0;
                                }
                                reg.ResponseData = ibls;
                                reg.IsSuccess = true;
                                break;
                            case CModbusCode.ReadDI: // 读离散输入
                                bool[] bls1 = master.ReadInputs((byte)reg.addr, (ushort)reg.regstart, (ushort)reg.regnum);
                                int[] ibls1 = new int[bls1.Length];
                                for (int i = 0; i < bls1.Length; i++)
                                {
                                    // 映射规则：可根据需求修改（如 true→255，false→0）
                                    ibls1[i] = bls1[i] ? 1 : 0;
                                }
                                reg.ResponseData = ibls1;
                                reg.IsSuccess = true;
                                break;
                            case CModbusCode.ReadHolding: // 读保持寄存器
                                ushort[] ush = master.ReadHoldingRegisters((byte)reg.addr, (ushort)reg.regstart, (ushort)reg.regnum);
                                int[] ush1 = new int[ush.Length];
                                for (int i = 0; i < ush.Length; i++)
                                {
                                    // 映射规则：可根据需求修改（如 true→255，false→0）
                                    ush1[i] = (int)ush[i];
                                }
                                reg.ResponseData = ush1;
                                reg.IsSuccess = true;
                                break;
                            case CModbusCode.ReadInput: // 读输入寄存器

                                ushort[] ush2 = master.ReadInputRegisters((byte)reg.addr, (ushort)reg.regstart, (ushort)reg.regnum);
                                int[] bush2 = new int[ush2.Length];
                                for (int i = 0; i < ush2.Length; i++)
                                {
                                    // 映射规则：可根据需求修改（如 true→255，false→0）
                                    bush2[i] = (int)ush2[i];
                                }
                                reg.ResponseData = bush2;
                                reg.IsSuccess = true;
                                break;
                            case CModbusCode.WriteCoil: // 写单个线圈
                                master.WriteSingleCoil((byte)reg.addr, (ushort)reg.regstart, reg.vbyte[0] == 0xff);
                                reg.ResponseData = new int[1] { reg.vbyte[0] == 0xff ? 1 : 0 };
                                reg.IsSuccess = true;
                                break;
                            case CModbusCode.WriteReg: // 写单个寄存器
                                ushort uReg = CMethord.bytetou16(reg.vbyte, 0);
                                master.WriteSingleRegister((byte)reg.addr, (ushort)reg.regstart, uReg);
                                reg.ResponseData = new int[1] { uReg };
                                reg.IsSuccess = true;
                                break;
                            case CModbusCode.WriteCoils: // 写多个线圈
                                bool[] bCoils = CMethord.BytesToBools(reg.vbyte, reg.regnum);
                                master.WriteMultipleCoils((byte)reg.addr, (ushort)reg.regstart, bCoils);
                                int[] bCoils1 = new int[bCoils.Length];
                                for (int i = 0; i < bCoils.Length; i++)
                                {
                                    bCoils1[i] = bCoils[i] ? 1 : 0;
                                }
                                reg.ResponseData = bCoils1;
                                reg.IsSuccess = true;
                                break;
                            case CModbusCode.WriteRegs: // 写多个寄存器
                                ushort[] uRegs = CMethord.BytesToRegisters(reg.vbyte);
                                master.WriteMultipleRegisters((byte)reg.addr, (ushort)reg.regstart, uRegs);
                                int[] uRegs1 = new int[uRegs.Length];
                                for (int i = 0; i < uRegs.Length; i++)
                                {
                                    uRegs1[i] = (int)uRegs[i];
                                }
                                reg.ResponseData = uRegs1;
                                reg.IsSuccess = true;
                                break;
                            default: // 未知功能码
                                throw new NotSupportedException($"不支持的功能码: 0x");
                        }

                        StopwatchHelper.Lap(reg.name, "发送结束");
                    }
                    catch (Exception ex)
                    {

                        // 可根据异常类型过滤是否重试（如只重试超时，不重试设备异常）
                        if (!IsRetryableException(ex))
                        {
                            break;
                        }

                        Thread.Sleep(500);
                    }
                    if (reg.IsSuccess)
                    {
                        break;
                    }
                }
            }
            finally
            {
                reg.IsCompleted = true;
                //接收到数据后发布出去 
                if (reg.IsSuccess)
                    CSysDelegateEvent.ReciveCModbusRegThread?.Invoke(reg);
            }

        }

        /// <summary>
        /// 判断异常是否可重试
        /// </summary>
        private static bool IsRetryableException(Exception ex)
        {
            // 可重试的异常类型（根据实际需求调整）
            return ex is TimeoutException ||               // 通信超时
                   ex.Message.Contains("CRC") ||           // CRC校验失败
                   ex.Message.Contains("未找到响应");       // 未收到响应
        }

        public void dealDriver()
        {
            if (IsOnline("") == false) Init("", initparam);
            RcvInfoEvent?.Invoke("", null, 0, 0);
        }
        #endregion

        #region 声明委托
        //声明一个delegate（委托）类型 和 声明一个testDelegate类型的对象
        public delegate void RcvInfoDelegate(string param, byte[] buffer, int offset, int count);
        public RcvInfoDelegate RcvInfoEvent;
        #endregion

    }
}
