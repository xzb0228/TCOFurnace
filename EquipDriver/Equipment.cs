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
    /// 表示一套完整的协议
    /// </summary>
    public  class Equipment : IDisposable
    {
        public CEquipInfo equipinfo = new CEquipInfo();

        //该串口下所有板子信息
        public SerialPortConfig serialPortConfig = new SerialPortConfig();
        
        //串口信息
        public  CSerialDriver serialdriver = new CSerialDriver();
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
            serialdriver.Close();
            connmode = "";
            CSysDelegateEvent.ShowStatusInfo("端口已断开");
        }
  
        /// <summary>
        /// 连接串口
        /// </summary>
        public  void ConnSerial()
        {
            connmode = "serial";
            equipinfo.InitEvent = serialdriver.Init;
            equipinfo.IsOnlineEvent = serialdriver.IsOnline;
            equipinfo.SendByteEvent = serialdriver.SendByte;
            equipinfo.SendStringEvent = serialdriver.SendString;
            equipinfo.dealDriverEvent = serialdriver.dealDriver;
            serialdriver.RcvInfoEvent = equipinfo.RcvInfo;
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
                                CSysDelegateEvent.ShowStatusInfo("数据通讯端口已经打开\r\n");
                                CSysDelegateEvent.ShowDebugInfo("");
                            }
                            equipinfo.DealTiming();
                        }
                        else if (isonline == true)
                        {
                            isonline = false;
                            CSysDelegateEvent.ShowStatusInfo("数据通讯端口已断开\r\n");
                            CSysDelegateEvent.ShowDebugInfo("");
                        }
                        else equipinfo.dealDriverEvent?.Invoke();
                    }
                    else isonline = false;

                    if(command=="CLOSE")
                    {
                        isonline = false;
                        command = "";
                        CloseAll();
                        CSysDelegateEvent.ShowStatusInfo("数据通讯端口已手动关闭");
                        CSysDelegateEvent.ShowDebugInfo("");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
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
