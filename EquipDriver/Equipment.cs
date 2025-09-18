using Common;
using ModBusRTU;
using ModBusRTU.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace EquipDriver
{
    /// <summary>
    /// 管理一个串口与下面所有板子的通讯 支持串口，TCP,UDP
    /// </summary>
    public class Equipment : IDisposable
    {
        //连接名称
        public string Name = "";

        #region 命令与队列
        /// <summary>
        /// 循环执行命令集
        /// </summary>
        public List<TimesModbusReg> timesModbusReg = new List<TimesModbusReg>();
        /// <summary>
        /// 定点时间执行命令 只执行一次
        /// </summary>
        public List<OneTimeModbusReg> oneTimeModbusReg = new List<OneTimeModbusReg>();

        //主程序中的使用的队列 状体机中传入的队列
        private ConcurrentQueue<ModbusReg> mainQueue = new ConcurrentQueue<ModbusReg>();

        //次级队列  用户手动模式传入
        private ConcurrentQueue<ModbusReg> secondaryQueue = new ConcurrentQueue<ModbusReg>();
        #endregion

        #region 控制命令发送间隔

        //串口信息
        private IEquipDriver equipDriver = null;

        public string connmode = "";//tcp  serial 

        public string ip = "";//要操作的设备IP地址
        public string remoteIP = "";//远端设备ip信息
        #endregion

        //该串口下所有板子信息
        public SerialPortConfig serialPortConfig = new SerialPortConfig();

        //不允许使用无参构造函数
        public Equipment() {
        }
        public Equipment(IEquipDriver iEquipDriver)
        {
            equipDriver = iEquipDriver;
        }

        /// <summary>
        /// 关闭所有
        /// </summary>
        public void Close()
        {
            equipDriver?.Close("");
            connmode = "";
            SysDelegateEvent.ShowStatusInfo("端口已断开");
        }

        /// <summary>
        /// 连接TCP端口
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        public  void ConnTCP(string ip, int port)
        {
            connmode = "tcp";
            equipDriver.Init("", string.Format("{0};{1}", ip, port));
        }
        /// <summary>
        /// 连接UDP端口
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        public  void ConnUDP(string ip, int port, int localport)
        {
            connmode = "udp";
            equipDriver.Init("", string.Format("{0};{1};{2}", ip, port, localport));
        }
        /// <summary>
        /// 连接串口
        /// </summary>
        public  void ConnSerial()
        {
            connmode = "serial";
            equipDriver.Init("", string.Format("{0};{1};{2};{3};{4}", serialPortConfig.Com, serialPortConfig.BaudRate, serialPortConfig.Parity, serialPortConfig.DataBits , serialPortConfig.StopBits));
        }


        #region 设备维护线程
        private Thread m_txthread;
        public  void InitEvent()
        {
            m_txthread = new Thread(ServerTxThreadStart) { IsBackground = true };
            m_txthread.Start();
        }

        private  void ServerTxThreadStart()
        {
            //某台设备是否在线
            bool isonline = false;
            while (m_txthread.IsAlive)
            {
                try
                {
                    if (connmode != "" && equipDriver != null)
                    {
                        if (equipDriver.IsOnline("") == true)
                        {
                            if (isonline == false)
                            {
                                isonline = true;
                                SysDelegateEvent.ShowStatusInfo("数据通讯端口已经打开\r\n");
                            }
                            DealTiming();
                        }
                        else if (isonline == true)
                        {
                            isonline = false;
                            SysDelegateEvent.ShowStatusInfo("数据通讯端口已断开\r\n");
                        }
                        else equipDriver.dealDriver();
                    }
                    else isonline = false;

                }
                catch (Exception e)
                {
                    Loger.Error("循环执行命令报错："+e.Message);
                }

                //30ms执行一次命令
                Thread.Sleep(30);
            }
        }
        #endregion



        #region 定时命令
        public void TimingGetModbusReg()
        {
            DateTime now = DateTime.Now;

            //循环执行命令
            foreach (var cmd in timesModbusReg)
            {
                if ((now - cmd.LastSendTime).TotalMilliseconds >= cmd.IntervalMs)
                {
                    //循环执行命令 如果不在队列中才加入
                    if (mainQueue.FirstOrDefault(c => c.name == cmd.name) == null)
                        AddMainQueue(cmd.Clone());
                    cmd.LastSendTime = now; // 更新发送时间
                }
            }

            //定点时间执行命令 只执行一次
            foreach (var cmd in oneTimeModbusReg)
            {
                //没有之心过，且到了执行时间
                if (cmd.LastSendTime == null && cmd.SendTime < DateTime.Now)
                {
                    AddMainQueue(cmd.Clone());
                    cmd.LastSendTime = now; // 更新上次发送时间

                    Loger.Info($"定点时间执行命令 {cmd.name} 的执行时间为{now::yyyy-MM-dd HH:mm:ss}");
                }
            }
        }
        #endregion

        #region 状态机发送过来的命令
        public string AddMainQueue(ModbusReg mreg)
        {
            mainQueue.Enqueue(mreg);
            return "";
        }
        #endregion

        #region 用户手摸模式发送过来的命令
        public string AddSecondaryQueue(ModbusReg mreg)
        {
            secondaryQueue.Enqueue(mreg);
            return "";
        }
        #endregion

        #region 请求发送的modbus指令

        public ModbusReg GetModbusReg()
        {
            ModbusReg mbreg;
            //优先发送队列里面的modbus命令
            if (mainQueue.TryDequeue(out mbreg)) return mbreg;

            //手动参数
            if (secondaryQueue.TryDequeue(out mbreg)) return mbreg;

            return null;
        }



        public void DealTiming()
        {
            TimingGetModbusReg();
            ModbusReg mbreg = GetModbusReg();

            if (mbreg == null) return;

            equipDriver.SendByte(mbreg);
        }
        #endregion

        public void Dispose()
        {
            if (m_txthread != null && m_txthread.IsAlive)
            {
                // 最多等待0.5秒让线程自行终止
                bool isThreadTerminated = m_txthread.Join(500);
            }
            m_txthread = null;

            equipDriver = null;
        }
    }
}
