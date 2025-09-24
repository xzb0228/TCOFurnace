using EquipDriver;
using ModBusRTU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TCOFurnace.Models;

namespace TCOFurnace.InstrumentsServices
{
    // 状态机核心类
    public class StateInstrument
    {
        public Equipment equipment;
        //所需要的命令参数
        public InstrumentMBReg equipmentMBReg = new InstrumentMBReg();
        //所需要的业务参数
        public TCOFRunningMode tCOFRunningMode = new TCOFRunningMode();
        //当前仪器所有端口的状体信息
        public MonitoringData monitoringData = new MonitoringData();

        //用户处理命令的发送
        public System.Threading.Timer timer;

        //当前状态
        public IState CurrentState { get; private set; }

        // 初始化时进入初始化状态 并且注入协议类
        public StateInstrument(Equipment equip)
        {
            equipment = equip;
            CurrentState = new InitializingState();
            // MessageBox.Show($"初始状态: {CurrentState.GetType().Name}");
        }

        // 切换状态
        public void SetState(IState newState)
        {
            CurrentState = newState;
            MessageBox.Show($"状态已切换至: {CurrentState.GetType().Name}");
        }

        //将某台仪器所有命令 置于最初始状态 ,设备状态初始化
        public void Init()
        {
            #region 将某台仪器所有命令 置于最初始状态
            ModbusReg k1 = equipmentMBReg.K1.Clone();
            k1.vbyte = new byte[2] { 0xff, 0x00 };//一路常开
            equipment.equipinfo.AddMainQueue(k1);

            ModbusReg k2 = equipmentMBReg.K2.Clone();
            k2.vbyte = new byte[2] { 0x00, 0x00 };//二路常闭
            equipment.equipinfo.AddMainQueue(k2);

            ModbusReg OVol = equipmentMBReg.OVol.Clone();
            OVol.vbyte = MBRTU.U16tou8((ushort)(0 * 1000));//氧调压 为 0
            equipment.equipinfo.AddMainQueue(OVol);

            ModbusReg CVol = equipmentMBReg.CVol.Clone();
            CVol.vbyte = MBRTU.U16tou8((ushort)(0 * 1000));//催调压 为 0
            equipment.equipinfo.AddMainQueue(CVol);

            ModbusReg Flow = equipmentMBReg.Flow.Clone();
            Flow.vbyte = MBRTU.U16tou8((ushort)(0 * 1000));//流量计4到20mA流量设定 为 0
            equipment.equipinfo.AddMainQueue(Flow);

            //从循环执行队列中去掉 流量计流量读  路温度回传 
            ModbusReg FlowRed = equipmentMBReg.FlowRed.Clone();
            equipment.equipinfo.RemoveoneTimeModbusReg(FlowRed.name);
            ModbusReg TempRed = equipmentMBReg.TempRed.Clone();
            equipment.equipinfo.RemoveoneTimeModbusReg(TempRed.name);
            #endregion

            #region 设备状态初始化
            string Lab3_6 = monitoringData.Lab3_6;
            string LabModel = monitoringData.LabModel;
            monitoringData.InitializeDefaults();
            monitoringData.Lab3_6 = Lab3_6;
            monitoringData.LabModel = LabModel;
            #endregion
        }

        // 暴露外部操作接口
        public void Initialize() => CurrentState.Initialize(this);
        public void Start() => CurrentState.Start(this);
        public void Stop() => CurrentState.Stop(this);
    }
}
