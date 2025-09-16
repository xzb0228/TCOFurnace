using ModBusRTU;
using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace EquipDriver
{
    public class UDPClient : IEquipDriver, IDisposable
    {
        private string initparam = null;
        public byte[] buffer = new byte[4096];
        UdpClient m_client;
        UdpClient local_client;
        public string hostName = "";
        public int port = 0;
        public int localport = 0;
        #region 初始化
        public void Dispose()
        {
            Close();
        }

        public void Close()
        {
            try
            {
                if (m_client != null)
                {
                    if (m_client.Client != null)
                    {
                        if (m_client.Client.Connected) m_client.Close();
                        try
                        {
                            m_client.Client.Disconnect(true);
                            m_client.Client.Close();
                            local_client.Client.Disconnect(true);
                            local_client.Client.Close();
                            m_client.Client = null;
                        }
                        catch (Exception)
                        {

                        }
                    }
                    m_client = null;
                }
            }
            catch (Exception)
            {

            }
        }

        public bool Connect(string ip, Int32 Port, Int32 localPort)
        {
            try
            {
                Close();
                m_client = new UdpClient(0);
                m_client.Connect(ip, Port);
                hostName = ip;
                port = Port;
                localport = localPort;
                local_client = new UdpClient(localport);
                if (m_client.Client.Connected)
                {
                    SysDelegateEvent.ShowStatusInfo("网络连接成功");
                    local_client.BeginReceive(new AsyncCallback(ReceiveCallback), null);
                }
                else
                {
                    SysDelegateEvent.ShowStatusInfo("网络连接失败");
                    m_client = null;
                }
                //根据服务器的IP地址和端口    异步连接服务器
            }
            catch (Exception)
            {
            }
            return true;
        }

        public void Receive()
        {
            if (m_client.Client.Connected == false) return;
            try
            {
                IPEndPoint remoteIpep = new IPEndPoint(IPAddress.Parse(hostName), port);
                //byte[] bytRecv = m_client.Receive(ref remoteIpep);
                UdpState s = new UdpState();
                s.e = remoteIpep;
                s.u = m_client;
                //byte[] udpReceiveResult = m_client.Receive(ref remoteIpep);
                
            }
            catch (Exception e)
            {
                SysDelegateEvent.ShowDebugInfo("Network IO problem " + e.ToString());
            }

        }
        public class UdpState
        {
            public UdpClient u;
            public IPEndPoint e;
        }
        #endregion

        #region Write


        public bool IsConn()
        {
            if (m_client == null) return false;
            if (m_client.Client == null) return false;
            return m_client.Client.Connected;
        }
        public void Write(string text)
        {
            if (IsConn() == false) return;
            // Convert the string data to byte data using ASCII encoding.
            byte[] byteData = Encoding.ASCII.GetBytes(text);

            try
            {
                IPEndPoint remoteIpep = new IPEndPoint(IPAddress.Parse(hostName), port);
                local_client.Send(byteData, byteData.Length, remoteIpep);
                //m_client.BeginSend(byteData, byteData.Length, new AsyncCallback(SendCallback), remoteIpep);                
            }
            catch (Exception)
            {

            }
        }
        public void Write(byte[] buffer, int offset, int count)
        {
            if (IsConn() == false) return;
            try
            {
                IPEndPoint remoteIpep = new IPEndPoint(IPAddress.Parse(hostName), port);
                local_client.Send(buffer, count, remoteIpep);
                //m_client.BeginSend(buffer, count, new AsyncCallback(SendCallback), remoteIpep);
            }
            catch (Exception)
            {

            }

        }
        #endregion

        #region 处理接收到的数据的委托
        private void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                UdpClient udpclient = (UdpClient)ar.AsyncState;
                udpclient.EndSend(ar);
                // Complete the connection.     
                if (udpclient.Client.Connected)
                {
                    SysDelegateEvent.ShowStatusInfo("网络连接成功");
                    Receive();
                }
                else
                {
                    SysDelegateEvent.ShowStatusInfo("网络连接失败");
                    m_client = null;
                }
            }
            catch (Exception)
            {
                SysDelegateEvent.ShowStatusInfo("网络连接失败");
                Close();
            }
        }
        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.     
                NetworkStream stream = (NetworkStream)ar.AsyncState;
                // Complete sending the data to the remote device.     
                stream.EndWrite(ar);
                //Console.WriteLine("Sent {0} bytes to server.", bytesSent);
            }
            catch (Exception)
            {
            }
        }
        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(hostName), port);
                byte[] receiveData = local_client.EndReceive(ar, ref iPEndPoint);
                if (receiveData != null)
                {                      
                    SysDelegateEvent.SerialRcvThread?.Invoke(receiveData, 0, receiveData.Length);
                    RcvInfoEvent?.Invoke("", receiveData, 0, receiveData.Length);
                    local_client.BeginReceive(new AsyncCallback(ReceiveCallback), null);   
                }
            }
            catch (Exception)
            {
            }
        }
        #endregion

        #region 对外接口
        public void Init(string param, string info)
        {
            initparam = info;
            string[] strparam = initparam.Split(';');
            Connect(strparam[0], Convert.ToInt32(strparam[1]), Convert.ToInt32(strparam[2]));
            SysDelegateEvent.ShowDebugInfo("正在连接服务器" + initparam);
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
        public void SendByte(string param, byte[] buffer, int offset, int count)
        {
            Write(buffer, offset, count);
        }
        public void SendByte(ModbusReg reg)
        {
            try
            {
                byte[] buffer = Methord.DealMasterSnd(reg);
                reg.IsSuccess = false;
                //发送数据委托
                SysDelegateEvent.SerialSendThread?.Invoke(buffer);

                Write(buffer, 0, buffer.Length);
            }
            catch (Exception ex)
            {


            }
            if (reg.IsSuccess)
            {
            }
        }
        DateTime prevLinkTime = DateTime.Now;
        public void dealDriver()
        {
            if ((m_client == null) && (prevLinkTime.AddSeconds(2) < DateTime.Now))
            {
                Init("", initparam);
                prevLinkTime = DateTime.Now;
                return;
            }
            if (m_client != null)
            {
                if ((m_client.Client.Connected == false) && (prevLinkTime.AddSeconds(2) < DateTime.Now))
                {
                    prevLinkTime = DateTime.Now;
                    Init("", initparam);
                    return;
                }

                Receive();
            }


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
