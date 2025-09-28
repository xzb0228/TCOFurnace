using EquipDriver;
using ModBusRTU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TCOFurnace.Common;
using TCOFurnace.InstrumentServices;
using TCOFurnace.Models;
using static System.Windows.Forms.AxHost;

namespace TCOFurnace.InstrumentsServices
{
    public class RunningState : IState
    {
        public void Initialize(StateInstrument machine)
        {
            MessageBox.Show(LanguageManager.GetMsg("10017"));
        }

        public void Start(StateInstrument machine)
        {
            //记录每一步执行了多长时间
            int countMin = 0;
            machine.currentStep = 1;
            TCOFRunningMode tCOFRunningMode = machine.tCOFRunningMode;

            TCOFRunningStep tCOF = null;

            //改变仪器状态信息
            machine.monitoringData.Lab2_2 = ComColor.EquipRunColor;
            machine.monitoringData.Lab3_2 = ComColor.EquipRunColor;
            machine.monitoringData.Lab4_2 = ComColor.EquipRunColor;
            machine.monitoringData.Lab5_2 = ComColor.EquipRunColor;
            machine.monitoringData.ButSysRun = ComColor.EquipRunColor;

            //添加循环发送命令 流量计流量读  温度回传 
            TimesModbusReg FlowRed = machine.equipmentMBReg.FlowRed.Clone();
            machine.equipment.equipinfo.AddTimesModbusReg(FlowRed);
            TimesModbusReg TempRed = machine.equipmentMBReg.TempRed.Clone();
            machine.equipment.equipinfo.AddTimesModbusReg(TempRed);

            //一分钟执行一次
            machine.timer = new System.Threading.Timer((state) =>
            {
                machine.monitoringData.Lab6_5 = countMin.ToString();
                countMin++;

                //没到时间继续循环
                if (tCOF != null && tCOF.Times > countMin) return;

                if (tCOF == null || tCOFRunningMode.RunningSteps.Count >= machine.currentStep)
                {
                    tCOF = tCOFRunningMode.RunningSteps[machine.currentStep - 1];

                    machine.currentStep++;
                    //重新计时
                    countMin = 0;

                    //当前仪器状态改变
                    machine.monitoringData.Lab1_2 = tCOF.StepNum.ToString();//当前步骤
                    machine.monitoringData.Lab3_4 = tCOF.Flow.ToString();//设定值 流量(L/min) 
                    machine.monitoringData.Lab4_4 = tCOF.Otemp.ToString();//设定值 氧化区温度(℃)
                    machine.monitoringData.Lab5_4 = tCOF.CTemp.ToString();//设定值 催化区温度(℃)
                    machine.monitoringData.Lab6_3 = tCOF.Times.ToString();//设定值 设置时间(min)

                    //电磁阀1
                    ModbusReg K1 = machine.equipmentMBReg.K1.Clone();
                    K1.vbyte = (tCOF.K1==1 ? new byte[2] { 0xff, 0x00 } : new byte[2] { 0x00, 0x00 });
                    machine.equipment.equipinfo.AddMainQueue(K1);

                    //电磁阀2
                    ModbusReg K2 = machine.equipmentMBReg.K2.Clone();
                    K2.vbyte = (tCOF.K2 == 1 ? new byte[2] { 0xff, 0x00 } : new byte[2] { 0x00, 0x00 });
                    machine.equipment.equipinfo.AddMainQueue(K2);

                    //氧化区调压
                    ModbusReg OVol = machine.equipmentMBReg.OVol.Clone();
                    OVol.vbyte = MBRTU.U16tou8((ushort)(tCOF.OVol));
                    machine.equipment.equipinfo.AddMainQueue(OVol);

                    //催化区调压
                    ModbusReg CVol = machine.equipmentMBReg.CVol.Clone();
                    CVol.vbyte = MBRTU.U16tou8((ushort)(tCOF.CVol ));
                    machine.equipment.equipinfo.AddMainQueue(CVol);

                    //流量计
                    ModbusReg Flow = machine.equipmentMBReg.Flow.Clone();
                    Flow.vbyte = MBRTU.U16tou8((ushort)(UnitConverter.FlowToElectric(tCOF.Flow)));
                    machine.equipment.equipinfo.AddMainQueue(Flow);
                }
                else {

                    //所有步骤都执行完了
                    machine.timer.Change(Timeout.Infinite, Timeout.Infinite);

                    //将某台仪器所有命令 置于最初始状态
                    machine.InitPort();

                    machine.SetState(new StoppedState());
                }
            }, null, 0, 1000 * 60);

        }

        public void Stop(StateInstrument machine)
        {
            // 模拟停止过程
            MessageBox.Show(LanguageManager.GetMsg("10018"));
            return;
            machine.SetState(new StoppedState());
        }
    }
}
