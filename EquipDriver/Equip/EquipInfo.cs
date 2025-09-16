
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
    public class EquipInfo
    {
        /// <summary>
        /// 循环执行命令集
        /// </summary>
        public List<TimesModbusReg> timesModbusReg = new List<TimesModbusReg>();
        /// <summary>
        /// 定点时间执行命令 只执行一次
        /// </summary>
        public List<OneTimeModbusReg> oneTimeModbusReg = new List<OneTimeModbusReg>();


        public EquipInfo()
        {

        }

        #region 驱动层参数
        public int mbaddr = 0;
        private bool IsWait = false;//解析到一半
        public int RqTimeout = 3000;//超时请求时间
        public int RqInterval = 30;//命令发送间隔至少30毫秒

        private DateTime LastRcvByteTime = DateTime.Now;
        private DateTime PrevSndTime = DateTime.Now;
        private DynamicBuffer ReceiveBuffer = new DynamicBuffer(4096);

        #endregion

        //主程序中的使用的队列 状体机中传入的队列
        public ConcurrentQueue<ModbusReg> mainQueue = new ConcurrentQueue<ModbusReg>();

        //次级队列  用户手动模式传入
        public ConcurrentQueue<ModbusReg> secondaryQueue = new ConcurrentQueue<ModbusReg>();

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
        public delegate void SendByteDelegate(ModbusReg reg);
        public SendByteDelegate SendByteEvent;

        //声明一个delegate（委托）类型 和 声明一个testDelegate类型的对象
        public delegate void dealDriverDelegate();
        public dealDriverDelegate dealDriverEvent;

        private void ShowDebugInfo(string info)
        {
            SysDelegateEvent.ShowDebugInfo(info);
            SysDelegateEvent.LogInfoThread?.Invoke(info);
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


        #region 定时查询
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
                        AddMainQueue(cmd);
                    cmd.LastSendTime = now; // 更新发送时间
                }
            }

            //定点时间执行命令 只执行一次
            foreach (var cmd in oneTimeModbusReg)
            {
                //没有之心过，且到了执行时间
                if (cmd.LastSendTime == null && cmd.SendTime < DateTime.Now)
                {
                    AddMainQueue(cmd);
                    cmd.LastSendTime = now; // 更新上次发送时间

                    Loger.Info($"定点时间执行命令 {cmd.name} 的执行时间为{now::yyyy-MM-dd HH:mm:ss}");
                }
            }
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

        private ModbusReg RqRealParamCode()
        {
            //CModbusReg mbreg = GetModbusReg();
            //if (mbreg == null) return null;
            //return CModbus.DealMasterSnd(mbreg);
            TimingGetModbusReg();
            ModbusReg mbreg = GetModbusReg();
            return mbreg;
        }

        private void DealTimingSend()
        {
            //没有连接就不发送
            if (IsOnlineEvent == null) return;
            if (IsOnlineEvent("") == false) return;

            if ((PrevSndTime.AddMilliseconds(RqInterval) > DateTime.Now)) return;

            ModbusReg reg = RqRealParamCode();

            if (reg == null) return;

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
