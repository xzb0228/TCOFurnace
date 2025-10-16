using Common;
using EquipDriver;
using ModBusRTU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TCOFurnace.Common;
using TCOFurnace.InstrumentServices;
using TCOFurnace.Models;
using static EquipDriver.SysDelegateEvent;

namespace TCOFurnace.InstrumentsServices
{
    internal class InitializingState : IState
    {
        //氧化区加热与恒温控制
        public TemperatureController oIntegratedTemperature = new TemperatureController();

        //催化区加热与恒温控制
        public TemperatureController cIntegratedTemperature = new TemperatureController();

        public void Initialize(StateInstrument machine)
        {
            //Console.WriteLine("已在初始化状态，正在加载配置...");
            //// 模拟初始化完成
            //Console.WriteLine("初始化完成！");
            if (machine.tCOFRunningMode == null)
            {
                MessageBox.Show(LanguageManager.GetMsg("10014"));
                return;
            }
            machine.InitPort();

            machine.InitData();
        }

        public void Start(StateInstrument machine)
        {
            //转为运行状态
            machine.SetState(new RunningState());

            //仪器开始运行
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
            EquipmentManager.AddTimesModbusReg(FlowRed);
            TimesModbusReg TempRed = machine.equipmentMBReg.TempRed.Clone();
            EquipmentManager.AddTimesModbusReg(TempRed);

            //触发催区与氧化区的温度控制
            machine.reciveModbusRegThread = new ReciveCModbusRegDelegate(reg =>
            {
                if (reg.name == machine.equipmentMBReg.TempRed.name && reg.ResponseData != null && reg.ResponseData.Count() > 1)
                {
                    oIntegratedTemperature.OnTemperatureReceived(reg.ResponseData[0]);
                    cIntegratedTemperature.OnTemperatureReceived(reg.ResponseData[1]);
                }
            });

            //温度控制:温度采集 
            SysDelegateEvent.ReciveModbusRegThread += machine.reciveModbusRegThread;
            //氧化区温度控制 
            oIntegratedTemperature.SetSendPower(power =>
            {
                ModbusReg OVol = machine.equipmentMBReg.OVol.Clone();
                //存在电压与流量的转换
                OVol.vbyte = MBRTU.U16tou8((ushort)((ushort)(UnitConverter.VoltageToElectric(power))));//氧调压 为 0 
                Loger.Info("温控算法-氧调压信息入队列" + power);
                EquipmentManager.AddMainQueue(OVol);
            });

            //催化区温度控制
            cIntegratedTemperature.SetSendPower(power =>
            {
                ModbusReg CVol = machine.equipmentMBReg.CVol.Clone();
                //存在电压与流量的转换
                CVol.vbyte = MBRTU.U16tou8((ushort)((UnitConverter.VoltageToElectric(power))));//催调压 为 0
                Loger.Info("温控算法-催调压信息入队列" + power);
                EquipmentManager.AddMainQueue(CVol);
            });


            //一分钟执行一次
            machine.timer = new System.Threading.Timer((state) =>
            {
                countMin++;
                machine.monitoringData.Lab6_5 = countMin.ToString();

                //没到时间继续循环
                if (tCOF != null && tCOF.Times > countMin) return;

                if (tCOF == null || tCOFRunningMode.RunningSteps.Count >= machine.currentStep)
                {
                    tCOF = tCOFRunningMode.RunningSteps[machine.currentStep - 1];

                    machine.currentStep++;
                    //重新计时
                    countMin = 0;
                    machine.monitoringData.Lab6_5 = countMin.ToString();

                    //当前仪器状态改变
                    machine.monitoringData.Lab1_2 = tCOF.StepNum.ToString();//当前步骤
                    machine.monitoringData.Lab3_4 = tCOF.Flow.ToString();//设定值 流量(L/min) 
                    machine.monitoringData.Lab4_4 = tCOF.Otemp.ToString();//设定值 氧化区温度(℃)
                    machine.monitoringData.Lab5_4 = tCOF.CTemp.ToString();//设定值 催化区温度(℃)
                    machine.monitoringData.Lab6_3 = tCOF.Times.ToString();//设定值 设置时间(min)

                    //电磁阀1
                    ModbusReg K1 = machine.equipmentMBReg.K1.Clone();
                    K1.vbyte = (tCOF.K1 == 1 ? new byte[2] { 0xff, 0x00 } : new byte[2] { 0x00, 0x00 });
                    EquipmentManager.AddMainQueue(K1);

                    //电磁阀2
                    ModbusReg K2 = machine.equipmentMBReg.K2.Clone();
                    K2.vbyte = (tCOF.K2 == 1 ? new byte[2] { 0xff, 0x00 } : new byte[2] { 0x00, 0x00 });
                    EquipmentManager.AddMainQueue(K2);

                    //氧化区调压
                    ModbusReg OVol = machine.equipmentMBReg.OVol.Clone();
                    OVol.vbyte = MBRTU.U16tou8((ushort)((UnitConverter.VoltageToElectric(tCOF.OVol))));
                    EquipmentManager.AddMainQueue(OVol);

                    //催化区调压
                    ModbusReg CVol = machine.equipmentMBReg.CVol.Clone();
                    CVol.vbyte = MBRTU.U16tou8((ushort)((UnitConverter.VoltageToElectric(tCOF.CVol))));
                    EquipmentManager.AddMainQueue(CVol);

                    //氧化去 催化区域 温度调控 传递入参
                    oIntegratedTemperature.SetPara(tCOF.Otemp, tCOF.OVol*0.7, tCOF.OVol);
                    cIntegratedTemperature.SetPara(tCOF.CTemp, tCOF.CVol*0.7, tCOF.CVol);

                    //流量计
                    ModbusReg Flow = machine.equipmentMBReg.Flow.Clone();
                    Flow.vbyte = MBRTU.U16tou8((ushort)(UnitConverter.FlowToElectric(tCOF.Flow)));
                    EquipmentManager.AddMainQueue(Flow);
                }
                else
                {
                    machine.Stop();
                }
            }, null, 0, 1000 * 60);

            // MessageBox.Show(LanguageManager.GetMsg("10015")); 
        }

        public void Stop(StateInstrument machine)
        {
            MessageBox.Show(LanguageManager.GetMsg("10016"));
            machine.SetState(new StoppedState());
        }
    }
}
