using EquipDriver.Model;
using ModBusRTU;
using System;
using System.Net.Sockets;
using System.Text;
using System.Web.UI.WebControls;

namespace EquipDriver
{
    public class TCPDriver : IEquipDriver, IDisposable
    {
        private TCPParamets initparam = null;
        public byte[] buffer = new byte[4096];
        TcpClient m_client;
        NetworkStream m_sendStream = null;
        NetworkStream m_recvStream = null;

        #region 初始化
        public void Dispose()
        {
            Close();
        }

        public void Close()
        {
            try
            {
                if (m_sendStream != null)
                {
                    m_sendStream.Close();
                    m_sendStream = null;
                }
                if (m_recvStream != null)
                {
                    m_recvStream.Close();
                    m_recvStream = null;
                }
                if (m_client != null)
                {
                    if (m_client.Connected) m_client.Close();
                    if (m_client.Client != null)
                    {
                        try
                        {
                            m_client.Client.Disconnect(true);
                            m_client.Client.Close();
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

        public bool Connect(string ip, Int32 port)
        {
            try
            {
                Close();
                m_sendStream = null;
                m_recvStream = null;
                m_client = new TcpClient();
                m_client.SendTimeout = 300;
                m_client.ReceiveTimeout = 0;
                m_client.BeginConnect(ip, port, new AsyncCallback(ConnectCallback), m_client);

                //m_client.Connect(ip, port);
                //根据服务器的IP地址和端口    异步连接服务器
            }
            catch (Exception)
            {
            }
            return true;
        }

        public void Receive()
        {
            if (m_client.Connected == false) return;
            if (m_recvStream != null) return;
            m_recvStream = m_client.GetStream();

            if (m_recvStream.CanRead)
            {
                try
                {
                    m_recvStream.BeginRead(buffer, 0, buffer.Length,
                            new AsyncCallback(ReceiveCallback), m_recvStream);
                }
                catch (Exception e)
                {
                    SysDelegateEvent.ShowDebugInfo("Network IO problem " + e.ToString());
                }
            }
        }
        #endregion

        #region Write


        public bool IsConn()
        {
            if (m_client == null) return false;
            if (m_client.Client == null) return false;
            if (m_client.Connected)
            {
                if (m_sendStream == null) m_sendStream = m_client.GetStream();
                return true;
            }
            else
            {
                return false;
            }
        }
        public void Write(string text)
        {
            if (IsConn() == false) return;
            // Convert the string data to byte data using ASCII encoding.
            byte[] byteData = Encoding.ASCII.GetBytes(text);

            try
            {
                if (m_sendStream == null) m_sendStream = m_client.GetStream();
                m_sendStream.Write(byteData, 0, byteData.Length);
                //m_sendStream.BeginWrite(byteData, 0, byteData.Length, new AsyncCallback(SendCallback), m_sendStream);//异步发送数据
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
                if (m_sendStream == null) m_sendStream = m_client.GetStream();
                m_sendStream.Write(buffer, offset, count);
                //m_sendStream.BeginWrite(buffer, offset, count, new AsyncCallback(SendCallback), m_sendStream);//异步发送数据
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
                TcpClient tcpclient = (TcpClient)ar.AsyncState;
                tcpclient.EndConnect(ar);
                // Complete the connection.     
                if (tcpclient.Connected)
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
                NetworkStream ns = (NetworkStream)ar.AsyncState;

                int bytesRead = ns.EndRead(ar);
                if (bytesRead > 0)
                {
                    byte[] rcvbuffer = new byte[bytesRead];
                    Array.Copy(buffer, 0, rcvbuffer, 0, bytesRead);
                    SysDelegateEvent.SerialRcvThread?.Invoke(rcvbuffer, 0, bytesRead);
                    RcvInfoEvent?.Invoke("", rcvbuffer, 0, bytesRead);
                    ns.BeginRead(buffer, 0, buffer.Length,
                            new AsyncCallback(ReceiveCallback), ns);
                }
            }
            catch (Exception)
            {
            }
        }
        #endregion

        #region 对外接口
        public void Init(string param, ParametsBase paramets)
        {
            initparam = paramets as TCPParamets;
           
            Connect(initparam.IP, initparam.PortNum);
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
                if (EquipmentManager.IsEmulatorMode)
                {
                    reg.IsSuccess = true;
                }
                else
                {
                    Write(buffer, 0, buffer.Length);
                }


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
                if ((m_client.Connected == false) && (prevLinkTime.AddSeconds(2) < DateTime.Now))
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
