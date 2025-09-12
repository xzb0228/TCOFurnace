
using Common;
using ModBusRTU;
using ModBusRTU.Model;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace EquipDriver
{
    /// <summary>
    /// 一台仪器的信息类
    /// </summary>
    public class CEquipInfo
    {

        //表示该实例串口对用串口下面所有板子的 离散输入 与 输入寄存器
        public List<PortInfo> m_model = new List<PortInfo>();

        //定时发送命令集
        public List<CScheduledModbusReg> cScheduledModbusReg = new List<CScheduledModbusReg>();
        public CEquipInfo()
        {
          
        }

        #region 驱动层参数
        public int mbaddr = 0;
        private bool IsWait = false;//解析到一半
        private bool IsRcvOk = false;
        public int RqTimeout = 3000;//超时请求时间
        public int RqInterval = 30;//命令发送间隔至少30毫秒

        private DateTime LastRcvByteTime = DateTime.Now;
        private DateTime PrevSndTime = DateTime.Now;
        private DynamicBufferManager ReceiveBuffer = new DynamicBufferManager(4096);

        #endregion

        //主程序中的使用的队列 状体机中传入的队列
        public ConcurrentQueue<CModbusReg> mainQueue = new ConcurrentQueue<CModbusReg>();

        //次级队列  用户手动模式传入
        public ConcurrentQueue<CModbusReg> secondaryQueue = new ConcurrentQueue<CModbusReg>();

        #region 声明委托
        //声明一个delegate（委托）类型 和 声明一个testDelegate类型的对象
        public delegate void InitDelegate(string param, string info);
        public InitDelegate InitEvent;

        //声明一个delegate（委托）类型 和 声明一个testDelegate类型的对象
        public delegate void CloseDelegate(string param);
        public CloseDelegate CloseEvent;

        //声明一个delegate（委托）类型 和 声明一个testDelegate类型的对象
        public delegate bool IsOnlineDelegate(string param);
        public IsOnlineDelegate IsOnlineEvent;

        //声明一个delegate（委托）类型 和 声明一个testDelegate类型的对象
        public delegate void SendStringDelegate(string param, string info);
        public SendStringDelegate SendStringEvent;

        //声明一个delegate（委托）类型 和 声明一个testDelegate类型的对象
        public delegate void SendByteDelegate(CModbusReg reg);
        public SendByteDelegate SendByteEvent;

        //声明一个delegate（委托）类型 和 声明一个testDelegate类型的对象
        public delegate void dealDriverDelegate();
        public dealDriverDelegate dealDriverEvent;

        private void ShowDebugInfo(string info)
        {
            CSysDelegateEvent.ShowDebugInfo(info);
            CSysDelegateEvent.LogInfoThread?.Invoke(info);
        }
        #endregion

        #region 状态机发送过来的命令
        public string AddMainQueue(CModbusReg mreg)
        {
            if(mainQueue.FirstOrDefault(c=>c.name== mreg.name)==null)
             mainQueue.Enqueue(mreg);
            return "";
        }
        #endregion

        #region 用户手摸模式发送过来的命令
        public string AddSecondaryQueue(CModbusReg mreg)
        {
            if (secondaryQueue.FirstOrDefault(c => c.name == mreg.name) == null)
                secondaryQueue.Enqueue(mreg);
            return "";
        }
        #endregion

        #region 接收数据处理

        private int DealRF(byte[] src)
        {
            try
            {
                return -1;
            }
            catch(Exception)
            {
            }                        
            return 1;
        }

        private int DealModbus(byte[] src)
        {
            try
            {
                CModbusReg mreg = CModbus.DealMasterRcv(src, mbaddr);

                if (mreg == null) return -1;
                else if (mreg.code == CModbusCode.Wait)
                {
                    if (IsWait == false)
                    {
                        IsWait = true;
                    }
                    else if (LastRcvByteTime.AddMilliseconds(RqTimeout) < DateTime.Now) //超过3s的命令
                    {
                        return -1;
                    }
                    return 0;
                }
                else
                {
                    IsWait = false;
                    ReceiveBuffer.Clear(mreg.strinfo.Length / 2);
                    RcvModbusReg(mreg);
                    IsRcvOk = true;

                    return 1;
                }
            }
            catch (Exception)
            {
            }
            return -1;
        }
        public void RcvInfo(string param, byte[] buffer, int offset, int count)
        {
            if (buffer != null && count != 0)
            {
                LastRcvByteTime = DateTime.Now;
                ReceiveBuffer.WriteBuffer(buffer, offset, count);
            }

            int rst = 0;
            while (ReceiveBuffer.DataCount > 5)
            {
                byte[] src = ReceiveBuffer.GetBytes();
                
                if ((rst=DealRF(src)) >= 0)
                {
                    if (rst == 0) return;
                }
                else if ((rst = DealModbus(src)) >= 0)
                {
                    if (rst == 0) return;
                }
                else
                {
                    IsWait = false;
                    ReceiveBuffer.Clear(1);
                }
            }
        }

        /// <summary>
        /// 解析回参命令
        /// </summary>
        /// <param name="mreg"></param>
        /// <returns></returns>
        public string RcvModbusReg(CModbusReg mreg)
        {
            switch (mreg.code)
            {
                case CModbusCode.ReadCoil:
                    return "ReadCoil";
                case CModbusCode.ReadDI:
                    return "ReadCoil";
                case CModbusCode.ReadHolding:
                    return "ReadHolding";
                case CModbusCode.ReadInput:     
                    return "ReadInput";
                case CModbusCode.WriteCoil:
                    //if (mreg.regstart < m_model.donum)
                    //{
                    //    if (mreg.vbyte[0] != 0)
                    //    {
                    //        m_model.regdo[mreg.regstart >> 3] |= 1 << (mreg.regstart & 0x07);
                    //    }
                    //    else
                    //        m_model.regdo[mreg.regstart >> 3] &= ~(1 << (mreg.regstart & 0x07));

                    //    m_model.DOTime = DateTime.Now;
                    //    ShowDebugInfo("操作单DO成功");
                    //    return "OK";
                    //}
                    return "WriteCoil";
                case CModbusCode.WriteCoils:
                    return "WriteCoils";
                case CModbusCode.WriteReg:
                    ShowDebugInfo("写AO成功");
                    return "WriteReg";
                case CModbusCode.WriteRegs:
                    return "WriteRegs";
            }

            return "";
        }
        #endregion

        #region 定时查询
        public void TimingGetModbusReg()
        {
            DateTime now = DateTime.Now;
            foreach (var cmd in cScheduledModbusReg)
            {
                if ((now - cmd.LastSendTime).TotalMilliseconds >= cmd.IntervalMs)
                {
                    AddMainQueue(cmd);
                    cmd.LastSendTime = now; // 更新上次发送时间
                }
            }
        }
        #endregion

        #region 请求发送的modbus指令

        public CModbusReg GetModbusReg()
        {
            CModbusReg mbreg;
            //优先发送队列里面的modbus命令
            if (mainQueue.TryDequeue(out mbreg)) return mbreg;

            //手动参数
            if (secondaryQueue.TryDequeue(out mbreg)) return mbreg;
            
            return null;
        }

        private CModbusReg RqRealParamCode()
        {
            //CModbusReg mbreg = GetModbusReg();
            //if (mbreg == null) return null;
            //return CModbus.DealMasterSnd(mbreg);
            TimingGetModbusReg();
            CModbusReg mbreg = GetModbusReg();
            return mbreg;
        }

        private int errcnt = 0;
        private void DealTimingSend()
        {
            //没有连接就不发送
            if (IsOnlineEvent == null) return;
            if (IsOnlineEvent("") == false) return;

            if ((PrevSndTime.AddMilliseconds(RqInterval) > DateTime.Now)) return;

            CModbusReg reg = RqRealParamCode();

            if (reg == null) return;

            IsRcvOk = false;
            SendByteEvent(reg);
            PrevSndTime = DateTime.Now;
            ReceiveBuffer.Clear(0);//清空数据
        }
        public void DealTiming()
        {
            DealTimingSend();
        }
        #endregion
    }
}
