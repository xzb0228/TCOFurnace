using Common;
using EquipDriver.Model;
using ModBusRTU;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web.UI.WebControls;

namespace EquipDriver
{
    /// <summary>
    /// 管理一个串口与下面所有板子的通讯 支持串口，TCP,UDP
    /// </summary>
    public class Equipment : IDisposable
    {
        private Equipment()
        {
        }
        public Equipment(IEquipDriver iEquipDriver, SerialParamets paramets)
        {
            equipDriver = iEquipDriver;
            portPar = paramets;
            ConnSerial(paramets);
        }
        public Equipment(IEquipDriver iEquipDriver, TCPParamets paramets)
        {
            equipDriver = iEquipDriver;
            portPar = paramets;
            ConnTCP(paramets);
        }
        public Equipment(IEquipDriver iEquipDriver, UdpParamets paramets)
        {
            equipDriver = iEquipDriver;
            portPar = paramets;
            ConnUDP(paramets);
        }
        //串口信息
        private IEquipDriver equipDriver;

        public EquipInfo equipinfo = new EquipInfo();


        public ParametsBase portPar;

        public PortType portType = PortType.None;//tcp  serial 
        public string command = "";
        public string ip = "";//要操作的设备IP地址
        public string remoteIP = "";//远端设备ip信息
        /// <summary>
        /// 关闭所有
        /// </summary>
        public void CloseAll()
        {
            equipDriver.Close("");
            portType = PortType.None;
            SysDelegateEvent.ShowStatusInfo("端口已断开");
        }
        /// <summary>
        /// 连接TCP端口
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        public void ConnTCP(TCPParamets paramets)
        {
            portType = PortType.TCP;
            equipinfo.InitEvent = equipDriver.Init;
            equipinfo.IsOnlineEvent = equipDriver.IsOnline;
            equipinfo.SendByteEvent = equipDriver.SendByte;
            equipinfo.SendStringEvent = equipDriver.SendString;
            equipinfo.dealDriverEvent = equipDriver.dealDriver;

            //tcpdriver.RcvInfoEvent = equipinfo.RcvInfo;
            //仿真模式就不打开串口
            if (!EquipmentManager.IsEmulatorMode)
                equipinfo.InitEvent("", paramets);
        }
        /// <summary>
        /// 连接UDP端口
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        public void ConnUDP(UdpParamets paramets)
        {
            portType = PortType.UDP;
            equipinfo.InitEvent = equipDriver.Init;
            equipinfo.IsOnlineEvent = equipDriver.IsOnline;
            equipinfo.SendByteEvent = equipDriver.SendByte;
            equipinfo.SendStringEvent = equipDriver.SendString;
            equipinfo.dealDriverEvent = equipDriver.dealDriver;

            //udpdriver.RcvInfoEvent = equipinfo.RcvInfo;
            //仿真模式就不打开串口
            if (!EquipmentManager.IsEmulatorMode)
                equipinfo.InitEvent("", paramets);
        }
        /// <summary>
        /// 连接串口
        /// </summary>
        public void ConnSerial(SerialParamets paramets)
        {
            portType = PortType.Serial;
            equipinfo.InitEvent = equipDriver.Init;
            equipinfo.IsOnlineEvent = equipDriver.IsOnline;
            equipinfo.SendByteEvent = equipDriver.SendByte;
            equipinfo.SendStringEvent = equipDriver.SendString;
            equipinfo.dealDriverEvent = equipDriver.dealDriver;

            //仿真模式就不打开串口
            if (!EquipmentManager.IsEmulatorMode)
                equipinfo.InitEvent("", paramets);
        }

        #region 设备维护线程
        private Thread m_txthread;
        public void InitEvent()
        {
            m_txthread = new Thread(ServerTxThreadStart) { IsBackground = true };
            m_txthread.Start();
        }

        private void ServerTxThreadStart()
        {
            //某台设备是否在线
            bool isonline = false;
            while (m_txthread.IsAlive)
            {
                try
                {
                    if ((portType != PortType.None && equipinfo.IsOnlineEvent != null))
                    {
                        //仿真模式直接往下走
                        if (EquipmentManager.IsEmulatorMode || equipinfo.IsOnlineEvent("") == true)
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

                    if (command == "CLOSE")
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
                    Loger.Error(e.Message, exc: e);
                }
                Thread.Sleep(5);
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
