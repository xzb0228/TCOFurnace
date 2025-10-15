using Common;
using EquipDriver;
using log4net.Repository.Hierarchy;
using ModBusRTU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TCOFurnace.InstrumentServices;
using TCOFurnace.Models;
using static EquipDriver.SysDelegateEvent;

namespace TCOFurnace.InstrumentsServices
{
    // 状态机核心类
    public class StateInstrument
    {
        //所需要的命令参数
        public InstrumentMBReg equipmentMBReg = new InstrumentMBReg();
        //所需要的业务参数
        public TCOFRunningMode tCOFRunningMode = new TCOFRunningMode();

        //当前反应步骤
        public int currentStep = 0;

        //当前仪器所有端口的状体信息
        public MonitoringData monitoringData = new MonitoringData();

        //用户处理命令的发送
        public System.Threading.Timer timer;

        //温度回传参数
        public ReciveCModbusRegDelegate reciveModbusRegThread;

        //当前状态
        public IState CurrentState { get; private set; }

        // 初始化时进入初始化状态 并且注入协议类
        public StateInstrument()
        {
           
            CurrentState = new InitializingState();
            // MessageBox.Show($"初始状态: {CurrentState.GetType().Name}");

            //注册事件到 串口管理类
            SysDelegateEvent.ReciveModbusRegThread += ListenceReciveCModbusReg;
        }

        public void ListenceReciveCModbusReg(ModbusReg reg)
        {
            //流量计回踩
            if (reg.name == equipmentMBReg.FlowRed.name)
            {
                if (reg.ResponseData != null && reg.ResponseData.Count() > 0)
                    monitoringData.Lab3_5 = UnitConverter.ElectricToFlow(reg.ResponseData[0]).ToString();
            }

            //温度计回踩
            if (reg.name == equipmentMBReg.TempRed.name)
            {
                if (reg.ResponseData != null && reg.ResponseData.Count() > 1)
                {
                    monitoringData.Lab4_5 = reg.ResponseData[0].ToString();
                    monitoringData.Lab5_5 = reg.ResponseData[1].ToString();

                    ////状态机运行模式的时候 过温保持
                    //if (CurrentState is RunningState)
                    //{
                    //    //氧化区 温度保护处理
                    //    int temp = 0;
                    //    int.TryParse(monitoringData.Lab4_4, out temp);    
                    //    if (reg.ResponseData[0] > temp + 5) //超过温度断电
                    //    {
                    //        ModbusReg OVol = equipmentMBReg.OVol.Clone();
                    //        OVol.vbyte = MBRTU.U16tou8((ushort)(0 * 1000));//氧调压 为 0
                    //        EquipmentManager.AddMainQueue(OVol);
                    //    }
                    //    else if (reg.ResponseData[0] < temp) //低于温度加压
                    //    {
                    //        ModbusReg OVol = equipmentMBReg.OVol.Clone();
                    //        OVol.vbyte = MBRTU.U16tou8((ushort)(tCOFRunningMode.RunningSteps[currentStep-1].OVol * 1000));//氧调压 为 0
                    //        EquipmentManager.AddMainQueue(OVol);
                    //    }

                    //    int.TryParse(monitoringData.Lab5_4, out temp);
                    //    //催化区 温度保护处理
                    //    if (reg.ResponseData[1] > temp + 5)
                    //    {
                    //        ModbusReg CVol = equipmentMBReg.CVol.Clone();
                    //        CVol.vbyte = MBRTU.U16tou8((ushort)(0 * 1000));//氧调压 为 0
                    //        EquipmentManager.AddMainQueue(CVol);
                    //    }
                    //    else if (reg.ResponseData[1] < temp)
                    //    {
                    //        ModbusReg CVol = equipmentMBReg.CVol.Clone();
                    //        CVol.vbyte = MBRTU.U16tou8((ushort)(tCOFRunningMode.RunningSteps[currentStep - 1].CVol * 1000));//氧调压 为 0
                    //        EquipmentManager.AddMainQueue(CVol);
                    //    }
                    //}
                }
            }
        }
        // 切换状态
        public void SetState(IState newState)
        {
            CurrentState = newState;
            Loger.Info($"状态已切换至: {CurrentState.GetType().Name}");
        }

        //将某台仪器所有命令 置于最初始状态 
        public void InitPort()
        {
            //从循环执行队列中去掉 流量计流量读  路温度回传 
            ModbusReg FlowRed = equipmentMBReg.FlowRed.Clone();
            EquipmentManager.RemoveTimesModbusReg(FlowRed.name, "Port1");
            ModbusReg TempRed = equipmentMBReg.TempRed.Clone();
            EquipmentManager.RemoveTimesModbusReg(TempRed.name, "Port1");

            #region 将某台仪器所有命令 置于最初始状态
            ModbusReg k1 = equipmentMBReg.K1.Clone();
            k1.vbyte = new byte[2] { 0xff, 0x00 };//一路常开
            EquipmentManager.AddMainQueue(k1);

            ModbusReg k2 = equipmentMBReg.K2.Clone();
            k2.vbyte = new byte[2] { 0x00, 0x00 };//二路常闭
            EquipmentManager.AddMainQueue(k2);

            ModbusReg OVol = equipmentMBReg.OVol.Clone();
            OVol.vbyte = MBRTU.U16tou8((ushort)(UnitConverter.VoltageToElectric(0)));//氧调压 为 0
            Loger.Info("关闭系统-氧调压信息入队列" + UnitConverter.VoltageToElectric(0));
            EquipmentManager.AddMainQueue(OVol);

            ModbusReg CVol = equipmentMBReg.CVol.Clone();
            CVol.vbyte = MBRTU.U16tou8((ushort)UnitConverter.VoltageToElectric(0));//催调压 为 0
            Loger.Info("关闭系统-催调压信息入队列" + UnitConverter.VoltageToElectric(0));
            EquipmentManager.AddMainQueue(CVol);

            ModbusReg Flow = equipmentMBReg.Flow.Clone();
            Flow.vbyte = MBRTU.U16tou8((ushort)UnitConverter.FlowToElectric(0));//流量计4到20mA流量设定 为 0
            EquipmentManager.AddMainQueue(Flow);

            #endregion
        }

        /// <summary>
        /// 清空当前状态机的过程状态数据
        /// </summary>
        public void InitData()
        {
            #region 设备状态初始化
            string Lab3_6 = monitoringData.Lab3_6;
            string LabModel = monitoringData.LabModel;
            monitoringData.InitializeDefaults();
            monitoringData.Lab3_6 = Lab3_6;
            monitoringData.LabModel = LabModel;
            #endregion

            timer = null;
        }

        // 暴露外部操作接口
        public void Initialize() => CurrentState.Initialize(this);
        public void Start() => CurrentState.Start(this);
        public void Stop() => CurrentState.Stop(this);
    }
}
