using ModBusRTU;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace EquipDriver
{
    public static class CEquipServer
    {
        public static CEquipInfo equipinfo = new CEquipInfo();
        public static ConcurrentDictionary<string, CEquipInfo> DicEquipInfo = new ConcurrentDictionary<string, CEquipInfo>();//unid---equip
        public static CSerialDriver serialdriver = new CSerialDriver();


        public static string connmode = "serial";//tcp
        public static string command ="";
        public static string ip = "";//要操作的设备IP地址
        public static string remoteIP = "";//远端设备ip信息
        /// <summary>
        /// 关闭所有
        /// </summary>
        public static void CloseAll()
        {
            serialdriver.Close();
            connmode = "";
            CSysDelegateEvent.ShowStatusInfo("端口已断开");
        }
  
        /// <summary>
        /// 连接串口
        /// </summary>
        /// <param name="com"></param>
        /// <param name="baud"></param>
        /// <param name="parity"></param>
        /// <param name="dataBits"></param>
        /// <param name="stopBits"></param>
        public static void ConnSerial(string com,int baud, int parity, int dataBits, int stopBits)
        {
            equipinfo.InitEvent = serialdriver.Init;
            equipinfo.IsOnlineEvent = serialdriver.IsOnline;
            equipinfo.SendByteEvent = serialdriver.SendByte;
            equipinfo.SendStringEvent = serialdriver.SendString;
            equipinfo.dealDriverEvent = serialdriver.dealDriver;
            serialdriver.RcvInfoEvent = equipinfo.RcvInfo;
            equipinfo.InitEvent("", string.Format("{0};{1};{2};{3};{4}", com, baud,parity,dataBits,stopBits));
        }
        #region 设备维护线程
        private static Thread m_txthread;
        public static void InitEvent()
        {
            m_txthread = new Thread(ServerTxThreadStart) { IsBackground = true };
            m_txthread.Start();
        }

        private static void ServerTxThreadStart()
        {
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
    }
}
