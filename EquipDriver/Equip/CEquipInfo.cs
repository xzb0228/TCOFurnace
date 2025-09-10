
using ModBusRTU;
using System;
using System.Collections.Generic;

namespace EquipDriver
{
    public class CEquipInfo
    {
        public sys_EquipNowModel m_model = new sys_EquipNowModel();
        public EquipInfoLog damlog = new EquipInfoLog();
        public double[] regai = new double[32];
        
        public string[] regaistr = new string[32];
        
        public int IsDODIValueChange = 0;
        public int IsAIValueChange = 0;

        public int[] DODIValueChange = new int[10];
        public int[] AIValueChange = new int[10];

        public bool moniflag = false;
        public bool IDFlag = false;

        public CEquipInfo()
        {
          
        }

        #region 驱动层参数
        public int mbaddr = 0;
        private bool IsWait = false;//解析到一半
        private bool IsRcvOk = false;
        public int RqTimeout = 3000;//超时请求时间
        public int RqInterval = 30;

        private DateTime LastRcvByteTime = DateTime.Now;
        private DateTime LastRcvTime = DateTime.Now;
        private DateTime PrevSndTime = DateTime.Now;
        private DynamicBufferManager ReceiveBuffer = new DynamicBufferManager(4096);
        private CModbusReg m_prevRqMreg = null;

        #endregion

        #region 跑马灯 流水灯
        private int doworkindex = 0;
        private int doworkmode = 0;
        private int doworkmodeparam = 0;
        
        private DateTime prevworktime = DateTime.Now;
        #endregion

        #region  定时读取SN  AI DIDO 
        
        private DateTime preRqSN = DateTime.Now;
        private DateTime preRqAI = DateTime.Now;
        private DateTime preSetTiming = DateTime.Now.AddHours(-1);
        public int IsneedRqSN = 1;//请求SN的次数
        public int IsneedRqAI = 1;//请求AI的次数

        public int TimingRd = 2000;//手动请求
        public int SNCycleTime = 10000;//定时循环请求DODI
        public int AICycleTime = 10000;//定时循环请求AI
        #endregion

        private Queue<COprParam> qOprParam = new Queue<COprParam>();//外部控制参数  30秒钟控制有效
        public Queue<CModbusReg> qMBSndInfo = new Queue<CModbusReg>();

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
        public delegate void SendByteDelegate(string param, byte[] buffer, int offset, int count);
        public SendByteDelegate SendByteEvent;

        //声明一个delegate（委托）类型 和 声明一个testDelegate类型的对象
        public delegate void dealDriverDelegate();
        public dealDriverDelegate dealDriverEvent;

        private void ShowDebugInfo(string info)
        {
            CSysDelegateEvent.ShowDebugInfo(info);
            CSysDelegateEvent.LogInfoThread?.Invoke(info);
        }

        private void ShowStatusInfo(string info)
        {
            CSysDelegateEvent.ShowStatusInfo(info);
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
                    else if (LastRcvByteTime.AddMilliseconds(RqTimeout) < DateTime.Now)
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

        int rcvaddr = 0;

        public string RcvModbusReg(CModbusReg mreg)
        {
            LastRcvTime = DateTime.Now;
            switch (mreg.code)
            {
                case CModbusCode.ReadCoil:
                    if (mreg.regnum == ((m_model.donum + 7) / 8))
                    {
                        for (int i = 0; i < mreg.regnum; i++)
                        {
                            if (m_model.regdo[i] != mreg.vbyte[i])
                            {
                                IsDODIValueChange++;
                                m_model.regdo[i] = mreg.vbyte[i];
                            }
                        }
                        m_model.DOTime = DateTime.Now;
                        ShowDebugInfo("读取DO成功");
                        return "OK";
                    }
                    return "ReadCoil";
                case CModbusCode.ReadDI:
                    if (mreg.regnum == ((m_model.dinum + 7) / 8))
                    {
                        for (int i = 0; i < mreg.regnum; i++)
                        {
                            if (m_model.regdi[i] != mreg.vbyte[i])
                            {
                                IsDODIValueChange++;
                                m_model.regdi[i] = mreg.vbyte[i];
                            }
                        }
                        m_model.DITime = DateTime.Now;
                        ShowDebugInfo("读取DI成功");
                        return "OK";
                    }
                    return "ReadCoil";
                case CModbusCode.ReadHolding:
                    if (m_prevRqMreg == null) return "";
                    if((m_prevRqMreg.regstart==1000)&&(mreg.regnum==6))
                    {
                        CEquipDelegateEvent.ParamInfoThread?.Invoke("reg1000_6",mreg.vbyte);
                    }
                    else if ((m_prevRqMreg.regstart == 1006)&& (mreg.regnum == 3))
                    {
                        CEquipDelegateEvent.ParamInfoThread?.Invoke("reg1006_3", mreg.vbyte);
                    }
                    else if ((m_prevRqMreg.regstart == 1010) && (mreg.regnum == 1))
                    {
                        CEquipDelegateEvent.ParamInfoThread?.Invoke("reg1010_1", mreg.vbyte);
                    }

                    ShowDebugInfo("读取AO成功");
                    return "ReadHolding";
                case CModbusCode.ReadInput:     
                    if(m_prevRqMreg != null && m_prevRqMreg.regstart==1000)
                    {
                        if ((mreg.regnum == 20)&&(moniflag==false))
                        {
                            sys_EquipUNIDModel model = new sys_EquipUNIDModel();
                            if (model.UpdateByte(mreg.vbyte))
                            {
                                for (int i = 0; i < 4; i++)
                                {
                                    if (m_model.regdi[i] != model.regdi[i]) IsDODIValueChange++;
                                    if (m_model.regdo[i] != model.regdo[i]) IsDODIValueChange++;
                                }
                                if (model.sn != m_model.SN)
                                {
                                    IsDODIValueChange++;
                                    IsAIValueChange++;
                                }
                                m_model.Update(model);
                                mbaddr = m_model.Addr;
                                if (rcvaddr != mbaddr)
                                {
                                    IsDODIValueChange++;
                                    IsAIValueChange++;

                                    rcvaddr = mbaddr;
                                }

                                IDFlag = true;
                                ShowDebugInfo("读设备ID、DO、DI状态成功");
                                return "OK";
                            }
                        }
                        else if ((mreg.regnum == 6) && (moniflag == true))
                        {
                            m_model.Addr = CMethord.bytetos16(mreg.vbyte, 0);
                            for (int i = 0; i < 4; i++)
                            {
                                if (m_model.regdo[i] != mreg.vbyte[4 + i]) {
                                    m_model.regdo[i] = mreg.vbyte[4 + i];
                                    IsDODIValueChange++;
                                }
                                if (m_model.regdi[i] != mreg.vbyte[8 + i])
                                {
                                    m_model.regdi[i] = mreg.vbyte[8 + i];
                                    IsDODIValueChange++;
                                }
                            }
                            mbaddr = m_model.Addr;
                            if (rcvaddr != mbaddr)
                            {
                                IsDODIValueChange++;
                                IsAIValueChange++;

                                rcvaddr = mbaddr;
                            }
                            IDFlag = true;
                            return "OK";
                        }
                    }                    
                    else if (mreg.regnum == ((m_model.aimode == 0) ? (m_model.ainum) : (2 * m_model.ainum)))
                    {
                        double val = 0;
                        if (m_model.aimode == 0)
                        {
                            for (int i = 0; i < m_model.ainum; i++)
                            {
                                val = ModBusRTU.CMethord.bytetos16(mreg.vbyte, i * 2);
                                if (m_model.regai[i] != val)
                                {
                                    m_model.regai[i] = val;
                                }
                            }
                        }
                        else if (m_model.aimode == 1)
                        {
                            for (int i = 0; i < m_model.ainum; i++)
                            {
                                val = ModBusRTU.CMethord.bytetolong(mreg.vbyte, i * 4, m_model.IsInverse == 0 ? false : true);
                                if (m_model.regai[i] != val)
                                {
                                    m_model.regai[i] = val;
                                }
                            }
                        }
                        else if ((m_model.aimode == 2) || (m_model.aimode == 3))
                        {
                            for (int i = 0; i < m_model.ainum; i++)
                            {
                                val = ModBusRTU.CMethord.bytetofloat(mreg.vbyte, i * 4, m_model.IsInverse == 0 ? false : true);
                                if (m_model.regai[i] != val)
                                {
                                    m_model.regai[i] = val;
                                }
                            }
                        }                    
                        else return "ReadInput";
                        string AIStr = "";
                        for (int i = 0; i < m_model.ainum; i++)
                        {
                            AIStr += m_model.regai[i] + ",";
                        }
                        IsAIValueChange++;
                        ShowDebugInfo("采集AI数据成功 " + AIStr);
                        return "OK";
                    }
                    return "ReadInput";
                case CModbusCode.WriteCoil:
                    if (mreg.regstart < m_model.donum)
                    {
                        if (mreg.vbyte[0] != 0)
                        {
                            m_model.regdo[mreg.regstart >> 3] |= 1 << (mreg.regstart & 0x07);
                        }
                        else
                            m_model.regdo[mreg.regstart >> 3] &= ~(1 << (mreg.regstart & 0x07));

                        IsDODIValueChange++;
                        m_model.DOTime = DateTime.Now;
                        ShowDebugInfo("操作单DO成功");
                        return "OK";
                    }
                    return "WriteCoil";
                case CModbusCode.WriteCoils:
                    ShowDebugInfo("写多DO成功");
                    return "WriteCoils";
                case CModbusCode.WriteReg:
                    ShowDebugInfo("写AO成功");
                    return "WriteReg";
                case CModbusCode.WriteRegs:
                    if ((m_prevRqMreg.regstart == 1000) && (mreg.regnum == 5))
                    {
                        CEquipDelegateEvent.ParamInfoThread?.Invoke("wreg1000_5", mreg.vbyte);
                    }
                    else if ((m_prevRqMreg.regstart == 1006) && (mreg.regnum == 3))
                    {
                        CEquipDelegateEvent.ParamInfoThread?.Invoke("wreg1006_3", mreg.vbyte);
                    }
                    else if ((m_prevRqMreg.regstart == 1010) && (mreg.regnum == 1))
                    {
                        CEquipDelegateEvent.ParamInfoThread?.Invoke("wreg1010_1", mreg.vbyte);
                    }
                    ShowDebugInfo(string.Format("写多AO成功 地址 {0},数量 {1}", m_prevRqMreg.regstart, m_prevRqMreg.regnum));
                    return "WriteRegs";
            }

            return "";
        }
        #endregion

        #region 外部请求命令


        public void RqNewInfo(int cnt = 1, int tmrInverse = 100)
        {
            IsneedRqAI = cnt;
            IsneedRqSN = cnt;
            TimingRd = tmrInverse;
            preRqSN = DateTime.Now;
            preRqAI = DateTime.Now;
        }

        public void RqAIInfo(int cnt = 1, int tmrInverse = 100)
        {
            IsneedRqAI = cnt;
            TimingRd = tmrInverse;
        }


        public string AddOprParam(string oprtype, string unid, string reginfo)
        {
            qOprParam.Enqueue(new COprParam(oprtype, unid, reginfo));
            return string.Format("手动操作:{0} [{1}]  {2} ", oprtype, unid, reginfo);
        }


        public string AddModebusReg(CModbusReg mreg)
        {
            qMBSndInfo.Enqueue(mreg);
            return "";
        }

        public void AddOprParam(int workmode,int workparam)
        {
            this.doworkmode = workmode;
            this.doworkmodeparam = workparam;
            this.doworkindex = m_model.donum * 2;
        }
        #endregion


        #region 定时查询
        public CModbusReg TimingGetDOModbusReg()
        {
            if (doworkmode == 0) return null;

            if ((prevworktime < DateTime.Now) && (prevworktime.AddMilliseconds(doworkmodeparam*10) > DateTime.Now))
                return null;
            prevworktime = DateTime.Now;


            if (doworkindex < m_model.donum * 2 - 1) doworkindex++;
            else
                doworkindex = 0;

            byte[] src = new byte[2];
            int io = 0;
            if (doworkmode == 1)
            {
                src[0] = (byte)(((doworkindex & 0x01) == 0x00) ? 0xff : 0x00);
                src[1] = 0x00;
                io = doworkindex / 2;
                return new CModbusReg(mbaddr, CModbusCode.WriteCoil, io, 1, src);
            }
            else if (doworkmode == 2)
            {
                src[0] = (byte)((doworkindex < m_model.donum) ? 0xff : 0x00);
                src[1] = 0x00;
                io = (doworkindex < m_model.donum) ? doworkindex : (doworkindex - m_model.donum);
                return new CModbusReg(mbaddr, CModbusCode.WriteCoil, io, 1, src);
            }

            return null;
        }
        public CModbusReg TimingGetModbusReg()
        {
            //优先跑马灯模式
            CModbusReg mreg = TimingGetDOModbusReg();
            if (mreg != null) return mreg;

            if ((m_model.dinum == 0) && (m_model.donum == 0)) IsneedRqSN = 0;
            if (m_model.ainum == 0) IsneedRqAI = 0;

            if (IDFlag==false) IsneedRqSN = 1;

            int timingcycleSN = (IsneedRqSN > 0) ? TimingRd : SNCycleTime;
            int timingcycleAI = (IsneedRqAI > 0) ? TimingRd : AICycleTime;

            //if ((preRqSN > DateTime.Now) || (preRqSN.AddMilliseconds(timingcycleSN) < DateTime.Now))
            //{
            //    IsneedRqSN = (IsneedRqSN > 0) ? (IsneedRqSN - 1) : 0;
            //    preRqSN = DateTime.Now;
            //    ShowDebugInfo("定时读取设备ID、DO、DI状态");
            //    if (moniflag) return new CModbusReg(mbaddr, CModbusCode.ReadInput, 1000, 6);
            //    else return new CModbusReg(mbaddr, CModbusCode.ReadInput, 1000, 20);
            //}

            //if (m_model.ainum > 0)
            //{
            //    if ((preRqAI > DateTime.Now) || (preRqAI.AddMilliseconds(timingcycleAI) < DateTime.Now))
            //    {
            //        IsneedRqAI = (IsneedRqAI > 0) ? (IsneedRqAI - 1) : 0;
            //        preRqAI = DateTime.Now;
            //        ShowDebugInfo("定时读取设备AI状态");
            //        return new CModbusReg(mbaddr, CModbusCode.ReadInput, (m_model.aimode == 3) ? 50 : 0, (m_model.aimode == 0) ? m_model.ainum : (m_model.ainum * 2));
            //    }
            //}

            if (m_model.IsTiming > 0)
            {
                if ((preSetTiming > DateTime.Now) || (preSetTiming.AddMinutes(10) < DateTime.Now))
                {
                    preSetTiming = DateTime.Now;
                    ushort[] srcs16 = new ushort[6]; ;
                    srcs16[0] = (ushort)DateTime.Now.Year;
                    srcs16[1] = (ushort)DateTime.Now.Month;
                    srcs16[2] = (ushort)DateTime.Now.Day;
                    srcs16[3] = (ushort)DateTime.Now.Hour;
                    srcs16[4] = (ushort)DateTime.Now.Minute;
                    srcs16[5] = (ushort)DateTime.Now.Second;
                    byte[] src = new byte[srcs16.Length * 2];
                    for (int i = 0; i < srcs16.Length; i++)
                        ModBusRTU.CMethord.Convertu16Tobyte(ref src, 2 * i, srcs16[i]);

                    //ShowDebugInfo("定时配置时间");
                    return new CModbusReg(mbaddr, CModbusCode.WriteRegs, 1100, srcs16.Length, src);
                }
            }
            return null;
        }
        #endregion


        #region 请求发送的modbus指令

        public CModbusReg GetModbusReg()
        {
            CModbusReg mbreg;

            //其他相求
            while (qMBSndInfo.Count > 0)
            {
                mbreg = qMBSndInfo.Dequeue();
                if (mbreg != null) return mbreg;
            }
            return TimingGetModbusReg();
        }

        private byte[] RqOprParamCode()
        {
            CModbusReg mbreg = GetModbusReg();
            if (mbreg == null) return null;
            m_prevRqMreg = mbreg;
            return CModbus.DealMasterSnd(mbreg);
        }

        private void DealTimingRecv()
        {
            dealDriverEvent?.Invoke();

            if (IsOnlineEvent == null) return;
            if (IsOnlineEvent("") == false) return;

            if(IsDODIValueChange != DODIValueChange[0])
            {
                DODIValueChange[0] = IsDODIValueChange;
                CEquipDelegateEvent.DIDOInfoThread?.Invoke("");
                CEquipDelegateEvent.AIInfoThread?.Invoke("adc");

                damlog.WriteFile();
            }
            if (IsAIValueChange != AIValueChange[0])
            {
                AIValueChange[0] = IsAIValueChange;
                CEquipDelegateEvent.AIInfoThread?.Invoke("adc");
                damlog.WriteFile();
            }
        }

        private int errcnt = 0;
        private void DealTimingSend()
        {
            if (IsOnlineEvent == null) return;
            if (IsOnlineEvent("") == false) return;

            if ((PrevSndTime.AddMilliseconds(RqInterval) > DateTime.Now)) return;

            if (IsRcvOk == true)
            {               
                if(errcnt>0)
               // CSoundHelper.playalarmstop();
                errcnt = 0;
            }
            else if (PrevSndTime.AddMilliseconds(RqTimeout) > DateTime.Now) return;
            else
            {
                errcnt++;
                if(errcnt>=3)
                {
                  //  CSoundHelper.playalarm();
                }
            }  
            byte[] sndinfo = RqOprParamCode();
            if (sndinfo == null) return;

            IsRcvOk = false;
            SendByteEvent("", sndinfo, 0, sndinfo.Length);
            CSysDelegateEvent.SerialSendThread?.Invoke(sndinfo);
            PrevSndTime = DateTime.Now;
            ReceiveBuffer.Clear(0);//清空数据
        }
        public void DealTiming()
        {
            DealTimingRecv();
            DealTimingSend();
        }
        #endregion
    }
}
