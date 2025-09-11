using ModBusRTU;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;

namespace EquipDriver
{
    public class CSerialDriver: IEquipDriver, IDisposable
    {
        private SerialPort Comm = null;
        private DateTime prevRqTime = DateTime.Now;
        private readonly ConcurrentDictionary<string, CModbusReg> _pendingCommands = new ConcurrentDictionary<string, CModbusReg>();

        /// <summary>
        /// 读取超时时间
        /// </summary>
        private const int READTIMEOUT = 1000;

        private string initparam=null;

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
                Comm = new SerialPort(portName, baudRate, parity,  dataBits, stopBits);
                //初始化SerialPort对象
                Comm.ParityReplace = 0xFF;
                Comm.NewLine = Environment.NewLine;
                Comm.RtsEnable = true;//根据实际情况吧。

                Comm.ReadTimeout = 1000;
                Comm.WriteTimeout = 1500;
                Comm.ReceivedBytesThreshold = 1;
                Comm.DataReceived += ReceiveCallback;

                Comm.ReadBufferSize = 4096;
                
                Comm.Open();

                return true;
            }
            catch(Exception e)
            {
                CSysDelegateEvent.ShowDebugInfo("串口打开失败："+e.Message);
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
            if (IsConn() == false) return;
            try
            {
                Comm.Write(buffer, offset, count);
            }
            catch(Exception)
            {
                Close();
                CSysDelegateEvent.ShowDebugInfo("串口异常关闭");
            }
            
        }
        #endregion

        #region 处理接收到的数据的委托

        //委托的实现
        //串口接收数据事件响应
        public void ReceiveCallback(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                while (Comm.BytesToRead > 0)
                {
                    int n = Comm.BytesToRead;//先记录下来，避免某种原因，人为的原因，操作几次之间时间长，缓存不一致
                    byte[] newbyte = new byte[n];//声明一个临时数组存储当前来的串口数据

                    int readlen = Comm.Read(newbyte, 0, n);//读取缓冲数据

                    

                    CSysDelegateEvent.SerialRcvThread?.Invoke(newbyte, 0,readlen);
                    RcvInfoEvent?.Invoke("", newbyte, 0, readlen);
                }
            }
            catch (Exception)
            {

            }
            finally
            {
            }
        }
        #endregion

        #region 对外接口

        public void Init(string param,string info)
        {
            initparam = info;
            string[] strparam = initparam.Split(';');

            CSysDelegateEvent.ShowDebugInfo("正在打开串口"+ initparam);

            Open(strparam[0],
                Convert.ToInt32(strparam[1]),
                Convert.ToInt16(strparam[2])==1? Parity.Even:(Convert.ToInt16(strparam[2]) == 2 ? Parity.Odd:Parity.None),
                Convert.ToInt16(strparam[3]), 
                Convert.ToInt16(strparam[4])==0? StopBits.One: StopBits.Two);            
        }
        
        public void Close(string param)
        {
            Close();
        }
        public bool IsOnline(string param)
        {
            return IsConn();
        }
        public void SendString(string param,string info)
        {
            Write(info);
        }
        public void SendByte(string param, byte[] buffer, int offset, int count)
        {
            Write(buffer,offset,count);
        }

       
        public void dealDriver()
        {
            if(IsOnline("")==false)Init("", initparam);
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
