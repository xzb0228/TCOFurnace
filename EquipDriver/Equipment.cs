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
        //不允许使用无参构造函数
        public Equipment() {
        }
        public Equipment(IEquipDriver iEquipDriver)
        {
            //equipDriver = iEquipDriver;
        }

        public EquipInfo equipinfo = new EquipInfo();

        //该串口下所有板子信息
        public SerialPortConfig serialPortConfig = new SerialPortConfig();
        
        //串口信息
        private SerialDriver equipDriver = new SerialDriver();
        public string Name = "";
        public string connmode = "";//tcp  serial 
        public string command ="";
        public string ip = "";//要操作的设备IP地址
        public string remoteIP = "";//远端设备ip信息
        /// <summary>
        /// 关闭所有
        /// </summary>
        public  void CloseAll()
        {
            equipDriver.Close("");
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
            equipinfo.InitEvent = equipDriver.Init;
            equipinfo.IsOnlineEvent = equipDriver.IsOnline;
            equipinfo.SendByteEvent = equipDriver.SendByte;
            equipinfo.SendStringEvent = equipDriver.SendString;
            equipinfo.dealDriverEvent = equipDriver.dealDriver;

            //tcpdriver.RcvInfoEvent = equipinfo.RcvInfo;

            equipinfo.InitEvent("", string.Format("{0};{1}", ip, port));
        }
        /// <summary>
        /// 连接UDP端口
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        public  void ConnUDP(string ip, int port, int localport)
        {
            connmode = "udp";
            equipinfo.InitEvent = equipDriver.Init;
            equipinfo.IsOnlineEvent = equipDriver.IsOnline;
            equipinfo.SendByteEvent = equipDriver.SendByte;
            equipinfo.SendStringEvent = equipDriver.SendString;
            equipinfo.dealDriverEvent = equipDriver.dealDriver;

            //udpdriver.RcvInfoEvent = equipinfo.RcvInfo;

            equipinfo.InitEvent("", string.Format("{0};{1};{2}", ip, port, localport));
        }
        /// <summary>
        /// 连接串口
        /// </summary>
        public  void ConnSerial()
        {
            connmode = "serial";
            equipinfo.InitEvent = equipDriver.Init;
            equipinfo.IsOnlineEvent = equipDriver.IsOnline;
            equipinfo.SendByteEvent = equipDriver.SendByte;
            equipinfo.SendStringEvent = equipDriver.SendString;
            equipinfo.dealDriverEvent = equipDriver.dealDriver;
            equipinfo.InitEvent("", string.Format("{0};{1};{2};{3};{4}", serialPortConfig.Com, serialPortConfig.BaudRate, serialPortConfig.Parity, serialPortConfig.DataBits , serialPortConfig.StopBits));
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
                    if (connmode != "" && equipinfo.IsOnlineEvent != null)
                    {
                        if (equipinfo.IsOnlineEvent("") == true)
                        {
                            if (isonline == false)
                            {
                                isonline = true;
                                SysDelegateEvent.ShowStatusInfo("数据通讯端口已经打开\r\n");
                                SysDelegateEvent.ShowDebugInfo("");
                            }
                            equipinfo.DealTiming();
                        }
                        else if (isonline == true)
                        {
                            isonline = false;
                            SysDelegateEvent.ShowStatusInfo("数据通讯端口已断开\r\n");
                            SysDelegateEvent.ShowDebugInfo("");
                        }
                        else equipinfo.dealDriverEvent?.Invoke();
                    }
                    else isonline = false;

                    if(command=="CLOSE")
                    {
                        isonline = false;
                        command = "";
                        CloseAll();
                        SysDelegateEvent.ShowStatusInfo("数据通讯端口已手动关闭");
                        SysDelegateEvent.ShowDebugInfo("");
                    }
                }
                catch (Exception e)
                {
                    Loger.Error(e.Message);
                }
                Thread.Sleep(3);
            }
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

            equipinfo = null;
        }
    }
}
