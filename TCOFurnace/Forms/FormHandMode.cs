using Common;
using EquipDriver;
using ModBusRTU;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using TCOFurnace.Common;
using TCOFurnace.InstrumentServices;
using TCOFurnace.InstrumentsServices;
using TCOFurnace.UserControls;


namespace TCOFurnace.Forms
{
    public partial class FormHandMode : BaseForm
    {
       
        public FormHandMode()
        {
            InitializeComponent();


            SysDelegateEvent.ReciveModbusRegThread += ListenceReciveCModbusReg;

            toggleSwitchWriteReg1_1.ToggleChanged += toggleSwitchWriteReg1_1_ToggleChanged;
            toggleSwitchWriteReg1_2.ToggleChanged += toggleSwitchWriteReg1_2_ToggleChanged;
            toggleSwitchWriteReg1_5.ToggleChanged += toggleSwitchWriteReg1_5_ToggleChanged;
            toggleSwitchWriteReg2_3.ToggleChanged += toggleSwitchWriteReg2_3_ToggleChanged;
            toggleSwitchWriteReg2_4.ToggleChanged += toggleSwitchWriteReg2_4_ToggleChanged;
            toggleSwitchWriteReg2_6.ToggleChanged += toggleSwitchWriteReg2_6_ToggleChanged;

            EquipmentManager.AddTimesModbusReg(GlobalPara.Regs["1流量计流量读"] as TimesModbusReg);
            EquipmentManager.AddTimesModbusReg(GlobalPara.Regs["2流量计流量读"] as TimesModbusReg);
            EquipmentManager.AddTimesModbusReg(GlobalPara.Regs["1路温度回传"] as TimesModbusReg);
            EquipmentManager.AddTimesModbusReg(GlobalPara.Regs["2路温度回传"] as TimesModbusReg);


        }

        public void ListenceReciveCModbusReg(ModbusReg reg)
        {
            // 由于事件可能从非UI线程触发，需要检查InvokeRequired
            if (this.InvokeRequired)
            {
                // 跨线程调用UI
                this.Invoke(new Action<ModbusReg>(ListenceReciveCModbusReg), reg);
                return;
            }
            StopwatchHelper.Lap(reg.name, "接收到广播");

            switch (reg.name)
            {
                case "1路常开":
                    labDCF1Light.Image = (reg.ResponseData == null || reg.ResponseData[0] == 0) ? global::TCOFurnace.Properties.Resources.NoSelectLight : global::TCOFurnace.Properties.Resources.SelectLight;
                    break;
                case "1路常闭":
                    labDCF3Light.Image = (reg.ResponseData == null || reg.ResponseData[0] == 0) ? global::TCOFurnace.Properties.Resources.NoSelectLight : global::TCOFurnace.Properties.Resources.SelectLight;
                    break;
                case "2路常开":
                    labDCF2Light.Image = (reg.ResponseData == null || reg.ResponseData[0] == 0) ? global::TCOFurnace.Properties.Resources.NoSelectLight : global::TCOFurnace.Properties.Resources.SelectLight;
                    break;
                case "2路常闭":
                    labDCF4Light.Image = (reg.ResponseData == null || reg.ResponseData[0] == 0) ? global::TCOFurnace.Properties.Resources.NoSelectLight : global::TCOFurnace.Properties.Resources.SelectLight;
                    break;
                case "1氧调压":
                    labYHL1Light.Image = (reg.ResponseData == null || reg.ResponseData[0] == 0) ? global::TCOFurnace.Properties.Resources.NoSelectLight : global::TCOFurnace.Properties.Resources.SelectLight;
                    break;
                case "1催调压":
                    labCHL1Light.Image = (reg.ResponseData == null || reg.ResponseData[0] == 0) ? global::TCOFurnace.Properties.Resources.NoSelectLight : global::TCOFurnace.Properties.Resources.SelectLight;
                    break;
                case "2氧调压":
                    labYHL2Light.Image = (reg.ResponseData == null || reg.ResponseData[0] == 0) ? global::TCOFurnace.Properties.Resources.NoSelectLight : global::TCOFurnace.Properties.Resources.SelectLight;
                    break;
                case "2催调压":
                    labCHL2Light.Image = (reg.ResponseData == null || reg.ResponseData[0] == 0) ? global::TCOFurnace.Properties.Resources.NoSelectLight : global::TCOFurnace.Properties.Resources.SelectLight;
                    break;
                case "1流量计4到20mA流量设定":
                    labBox1Light.Image = (reg.ResponseData == null || reg.ResponseData[0] == 0) ? global::TCOFurnace.Properties.Resources.NoSelectLight : global::TCOFurnace.Properties.Resources.SelectLight;
                    break;
                case "2流量计4到20mA流量设定":
                    labBox2Light.Image = (reg.ResponseData == null || reg.ResponseData[0] == 0) ? global::TCOFurnace.Properties.Resources.NoSelectLight : global::TCOFurnace.Properties.Resources.SelectLight;
                    break;
                case "1流量计流量读":
                    textReadHolding1_1.Text = UnitConverter.ElectricToFlow(reg.ResponseData[0]).ToString();
                    break;
                case "2流量计流量读":
                    textReadHolding2_2.Text = UnitConverter.ElectricToFlow(reg.ResponseData[0]).ToString();
                    break;
                case "1路温度回传":
                    //第一路 氧化区 温度保护处理
                    int temp = 0;
                    if (int.TryParse(textWriteReg1_1_C.Text, out temp))
                    {
                        if (reg.ResponseData[0] > temp + 5)
                        {
                            //过温保护
                            OperWriteReg1_1(false);
                        }
                        else if (reg.ResponseData[0] < temp)
                        {
                            //过温保护
                            OperWriteReg1_1(true);
                        }

                        //1路氧化区温度实时显示
                        this.textWriteReg1_1_C_Back.Text = reg.ResponseData[0].ToString();
                    }

                    //第一路 催化区 温度保护处理
                    if (int.TryParse(textWriteReg1_2_C.Text, out temp) && reg.ResponseData.Length > 1)
                    {
                        if (reg.ResponseData[1] > temp + 5)
                        {
                            //过温保护
                            OperWriteReg1_2(false);
                        }
                        else if (reg.ResponseData[1] < temp)
                        {
                            //过温保护
                            OperWriteReg1_2(true);
                        }

                        //1路崔化区温度实时显示
                        this.textWriteReg1_2_C_Back.Text = reg.ResponseData[1].ToString();
                    }

                    break;
                case "2路温度回传":
                    //第二路 氧化区 温度保护处理
                    if (int.TryParse(textWriteReg2_3_C.Text, out temp))
                    {
                        if (reg.ResponseData[0] > temp + 5)
                        {
                            //过温保护
                            OperWriteReg2_3(false);
                        }
                        else if (reg.ResponseData[0] < temp)
                        {
                            //过温保护
                            OperWriteReg2_3(true);
                        }

                        //2路氧化区温度实时显示
                        this.textWriteReg2_3_C_Back.Text = reg.ResponseData[0].ToString();

                    }

                    //第二路 催化区 温度保护处理
                    if (int.TryParse(textWriteReg2_3_C.Text, out temp) && reg.ResponseData.Length > 1)
                    {
                        if (reg.ResponseData[1] > temp + 5)
                        {
                            //过温保护
                            OperWriteReg2_4(false);
                        }
                        else if (reg.ResponseData[1] < temp)
                        {
                            //过温保护
                            OperWriteReg2_4(true);
                        }

                        //2路崔化区温度实时显示
                        this.textWriteReg2_4_C_Back.Text = reg.ResponseData[1].ToString();

                    }
                    break;
            }
            StopwatchHelper.Lap(reg.name, "手动模式界面 接收到广播-结束");
        }
        // 窗口关闭时取消订阅，避免内存泄漏
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            SysDelegateEvent.ReciveModbusRegThread -= ListenceReciveCModbusReg;
        }

        // 开关状态改变事件处理
        private void toggleSwitchWriteReg1_1_ToggleChanged(object sender, EventArgs e)
        {
            var toggle = sender as ToggleSwitch;
            if (toggle != null)
            {
                // 这里可以添加更多状态改变后的逻辑
                if (toggle.IsOn)
                {
                    // 开关打开时的操作
                    if (int.TryParse(textWriteReg1_1.Text, out int val) || val < 0 || val > 20)
                    {
                        OperWriteReg1_1(true);
                        this.textWriteReg1_1.Enabled = false;
                        this.textWriteReg1_1_C.Enabled = false;
                    }
                    else
                    {
                        MessageBox.Show(LanguageManager.GetMsg("10011"), LanguageManager.GetMsg("10000"), MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                }
                else
                {
                    OperWriteReg1_1(false);
                    this.textWriteReg1_1.Enabled = true;
                    this.textWriteReg1_1_C.Enabled = true;
                }
            }
        }

        private void OperWriteReg1_1(bool isOpen)
        {
            if (isOpen)
            {
                int.TryParse(textWriteReg1_1.Text, out int val);
                ModbusReg WriteReg1_1 = GlobalPara.Regs["1氧调压"];
                WriteReg1_1.vbyte = MBRTU.U16tou8((ushort)((ushort)(UnitConverter.VoltageToElectric(val))));
                EquipmentManager.AddSecondaryQueue(WriteReg1_1);

            }
            else
            {
                // 开关关闭时的操作
                ModbusReg WriteReg1_1 = GlobalPara.Regs["1氧调压"];
                WriteReg1_1.vbyte = MBRTU.U16tou8((ushort)((ushort)(UnitConverter.VoltageToElectric(0))));
                EquipmentManager.AddSecondaryQueue(WriteReg1_1);
            }
        }

        // 开关状态改变事件处理
        private void toggleSwitchWriteReg1_2_ToggleChanged(object sender, EventArgs e)
        {
            var toggle = sender as ToggleSwitch;
            if (toggle != null)
            {
                // 这里可以添加更多状态改变后的逻辑
                if (toggle.IsOn)
                {
                    // 开关打开时的操作
                    if (int.TryParse(textWriteReg1_2.Text, out int val) || val < 0 || val > 20)
                    {
                        OperWriteReg1_2(true);
                        this.textWriteReg1_2.Enabled = false;
                        this.textWriteReg1_2_C.Enabled = false;
                    }
                    else
                    {
                        MessageBox.Show(LanguageManager.GetMsg("10011"), LanguageManager.GetMsg("10000"), MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                }
                else
                {
                    OperWriteReg1_2(false);
                    this.textWriteReg1_2.Enabled = true;
                    this.textWriteReg1_2_C.Enabled = true;
                }
            }
        }
        private void OperWriteReg1_2(bool isOpen)
        {
            if (isOpen)
            {
                int.TryParse(textWriteReg1_2.Text, out int val);
                ModbusReg WriteReg1_2 = GlobalPara.Regs["1催调压"];
                WriteReg1_2.vbyte = MBRTU.U16tou8((ushort)((ushort)(UnitConverter.VoltageToElectric(val))));
                EquipmentManager.AddSecondaryQueue(WriteReg1_2);

            }
            else
            {
                // 开关关闭时的操作
                ModbusReg WriteReg1_2 = GlobalPara.Regs["1催调压"];
                WriteReg1_2.vbyte = MBRTU.U16tou8((ushort)(UnitConverter.VoltageToElectric(0)));
                EquipmentManager.AddSecondaryQueue(WriteReg1_2);
            }
        }

        // 开关状态改变事件处理
        private void toggleSwitchWriteReg1_5_ToggleChanged(object sender, EventArgs e)
        {
            var toggle = sender as ToggleSwitch;
            if (toggle != null)
            {
                // 这里可以添加更多状态改变后的逻辑
                if (toggle.IsOn)
                {
                    // 开关打开时的操作
                    if (float.TryParse(textWriteReg1_5.Text, out float val) && val > 0 && val < 5)
                    {
                        ModbusReg WriteReg1_5 = GlobalPara.Regs["1流量计4到20mA流量设定"];
                        WriteReg1_5.vbyte = MBRTU.U16tou8((ushort)(UnitConverter.FlowToElectric(val)));
                        EquipmentManager.AddSecondaryQueue(WriteReg1_5);
                    }
                    else
                    {
                        MessageBox.Show(LanguageManager.GetMsg("10011"), LanguageManager.GetMsg("10000"), MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                }
                else
                {
                    // 开关关闭时的操作
                    ModbusReg WriteReg1_5 = GlobalPara.Regs["1流量计4到20mA流量设定"];
                    WriteReg1_5.vbyte = MBRTU.U16tou8((ushort)UnitConverter.FlowToElectric(0f));
                    EquipmentManager.AddSecondaryQueue(WriteReg1_5);
                }
            }
        }

        // 开关状态改变事件处理
        private void toggleSwitchWriteReg2_3_ToggleChanged(object sender, EventArgs e)
        {
            var toggle = sender as ToggleSwitch;
            if (toggle != null)
            { // 这里可以添加更多状态改变后的逻辑
                if (toggle.IsOn)
                {
                    // 开关打开时的操作
                    if (int.TryParse(textWriteReg2_3.Text, out int val) || val < 0 || val > 20)
                    {
                        OperWriteReg2_3(true);
                        this.textWriteReg2_3.Enabled = false;
                        this.textWriteReg2_3_C.Enabled = false;
                    }
                    else
                    {
                        MessageBox.Show(LanguageManager.GetMsg("10011"), LanguageManager.GetMsg("10000"), MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                }
                else
                {
                    OperWriteReg2_3(false);
                    this.textWriteReg2_3.Enabled = true;
                    this.textWriteReg2_3_C.Enabled = true;
                }
            }
        }
        private void OperWriteReg2_3(bool isOpen)
        {
            if (isOpen)
            {
                int.TryParse(textWriteReg2_3.Text, out int val);
                ModbusReg WriteReg2_3 = GlobalPara.Regs["2氧调压"];
                WriteReg2_3.vbyte = MBRTU.U16tou8((ushort)((ushort)(UnitConverter.VoltageToElectric(val))));
                EquipmentManager.AddSecondaryQueue(WriteReg2_3.Clone());

            }
            else
            {

                // 开关关闭时的操作
                ModbusReg WriteReg2_3 = GlobalPara.Regs["2氧调压"];
                WriteReg2_3.vbyte = MBRTU.U16tou8((ushort)((ushort)(UnitConverter.VoltageToElectric(0))));
                EquipmentManager.AddSecondaryQueue(WriteReg2_3);
            }
        }

        // 开关状态改变事件处理
        private void toggleSwitchWriteReg2_4_ToggleChanged(object sender, EventArgs e)
        {
            var toggle = sender as ToggleSwitch;
            if (toggle != null)
            {
                if (toggle != null)
                { // 这里可以添加更多状态改变后的逻辑
                    if (toggle.IsOn)
                    {
                        // 开关打开时的操作
                        if (int.TryParse(textWriteReg2_4.Text, out int val) || val < 0 || val > 20)
                        {
                            OperWriteReg2_4(true);
                            this.textWriteReg2_4.Enabled = false;
                            this.textWriteReg2_4_C.Enabled = false;
                        }
                        else
                        {
                            MessageBox.Show(LanguageManager.GetMsg("10011"), LanguageManager.GetMsg("10000"), MessageBoxButtons.OK, MessageBoxIcon.None);
                        }
                    }
                    else
                    {
                        OperWriteReg2_4(false);
                        this.textWriteReg2_4.Enabled = true;
                        this.textWriteReg2_4_C.Enabled = true;
                    }
                }
            }
        }

        private void OperWriteReg2_4(bool isOpen)
        {
            if (isOpen)
            {

                int.TryParse(textWriteReg2_4.Text, out int val);
                ModbusReg WriteReg2_4 = GlobalPara.Regs["2催调压"];
                WriteReg2_4.vbyte = MBRTU.U16tou8((ushort)(UnitConverter.VoltageToElectric(val)));
                EquipmentManager.AddSecondaryQueue(WriteReg2_4);
            }
            else
            {
                // 开关关闭时的操作
                ModbusReg WriteReg2_4 = GlobalPara.Regs["2催调压"];
                WriteReg2_4.vbyte = MBRTU.U16tou8((ushort)(UnitConverter.VoltageToElectric(0)));
                EquipmentManager.AddSecondaryQueue(WriteReg2_4);
            }
        }

        // 开关状态改变事件处理
        private void toggleSwitchWriteReg2_6_ToggleChanged(object sender, EventArgs e)
        {
            var toggle = sender as ToggleSwitch;
            if (toggle != null)
            {
                if (toggle != null)
                { // 这里可以添加更多状态改变后的逻辑
                    if (toggle.IsOn)
                    {

                        // 开关打开时的操作
                        if (float.TryParse(textWriteReg2_6.Text, out float val) && val > 0 && val < 5)
                        {
                            ModbusReg WriteReg2_6 = GlobalPara.Regs["2流量计4到20mA流量设定"];
                            WriteReg2_6.vbyte = MBRTU.U16tou8((ushort)(UnitConverter.FlowToElectric(val)));
                            EquipmentManager.AddSecondaryQueue(WriteReg2_6);
                        }
                        else
                        {
                            MessageBox.Show(LanguageManager.GetMsg("10011"), LanguageManager.GetMsg("10000"), MessageBoxButtons.OK, MessageBoxIcon.None);
                        }
                    }
                    else
                    {
                        // 开关关闭时的操作
                        ModbusReg WriteReg2_6 = GlobalPara.Regs["2流量计4到20mA流量设定"];
                        WriteReg2_6.vbyte = MBRTU.U16tou8(0x0000);
                        EquipmentManager.AddSecondaryQueue(WriteReg2_6);
                    }
                }
            }
        }
        

        bool bButDCF1 = false;
        private void butDCF1_Click(object sender, EventArgs e)
        {
            bButDCF1 = !bButDCF1;
            ModbusReg WriteCoil1_1 = GlobalPara.Regs["1路常开"];
            WriteCoil1_1.vbyte = (bButDCF1 ? new byte[2] { 0xff, 0x00 } : new byte[2] { 0x00, 0x00 });
            StopwatchHelper.Start(WriteCoil1_1.name);
            StopwatchHelper.Lap(WriteCoil1_1.name, "开始入栈");
            EquipmentManager.AddSecondaryQueue(WriteCoil1_1);
            StopwatchHelper.Lap(WriteCoil1_1.name, "结束入栈");
        }

        bool bButDCF3 = false;
        private void butDCF3_Click(object sender, EventArgs e)
        {
            bButDCF3 = !bButDCF3;
            ModbusReg WriteCoil1_2 = GlobalPara.Regs["1路常闭"];
            WriteCoil1_2.vbyte = (bButDCF3 ? new byte[2] { 0xff, 0x00 } : new byte[2] { 0x00, 0x00 });
            StopwatchHelper.Start(WriteCoil1_2.name);
            StopwatchHelper.Lap(WriteCoil1_2.name, "开始入栈");
            EquipmentManager.AddSecondaryQueue(WriteCoil1_2);
            StopwatchHelper.Lap(WriteCoil1_2.name, "结束入栈");

        }
        bool bButDCF2 = false;
        private void butDCF2_Click(object sender, EventArgs e)
        {
            bButDCF2 = !bButDCF2;
            ModbusReg WriteCoil2_3 = GlobalPara.Regs["2路常开"];
            WriteCoil2_3.vbyte = (bButDCF2 ? new byte[2] { 0xff, 0x00 } : new byte[2] { 0x00, 0x00 });
            StopwatchHelper.Start(WriteCoil2_3.name);
            StopwatchHelper.Lap(WriteCoil2_3.name, "开始入栈");
            EquipmentManager.AddSecondaryQueue(WriteCoil2_3);
            StopwatchHelper.Lap(WriteCoil2_3.name, "结束入栈");

        }
        bool bButDCF4 = false;
        private void butDCF4_Click(object sender, EventArgs e)
        {
            bButDCF4 = !bButDCF4;
            var WriteCoil2_4 = GlobalPara.Regs["2路常闭"];
            WriteCoil2_4.vbyte = (bButDCF4 ? new byte[2] { 0xff, 0x00 } : new byte[2] { 0x00, 0x00 });
            StopwatchHelper.Start(WriteCoil2_4.name);
            StopwatchHelper.Lap(WriteCoil2_4.name, "开始入栈");
            EquipmentManager.AddSecondaryQueue(WriteCoil2_4);
            StopwatchHelper.Lap(WriteCoil2_4.name, "结束入栈");

        }

        private void FormHandMode_Load(object sender, EventArgs e)
        {
            //仪器在运行时，要控制对应的反应管的手动模式不能操作
            if (FormEquipRunMain.stateInstruments.Count > 0)
            {
                if (!(FormEquipRunMain.stateInstruments[0].CurrentState == null || FormEquipRunMain.stateInstruments[0].CurrentState is InitializingState))
                {
                    this.butDCF1.Enabled = false;
                    this.butDCF3.Enabled = false;
                    this.textWriteReg1_1.Enabled = false;
                    this.textWriteReg1_1_C.Enabled = false;
                    this.toggleSwitchWriteReg1_1.Enabled = false;
                    this.textWriteReg1_2.Enabled = false;
                    this.textWriteReg1_2_C.Enabled = false;
                    this.toggleSwitchWriteReg1_2.Enabled = false;
                    this.textWriteReg1_5.Enabled = false;
                    this.toggleSwitchWriteReg1_5.Enabled = false;
                }

                if (!(FormEquipRunMain.stateInstruments[1].CurrentState == null || FormEquipRunMain.stateInstruments[1].CurrentState is InitializingState))
                {
                    butDCF2.Enabled = false;
                    butDCF4.Enabled = false;
                    textWriteReg2_3.Enabled = false;
                    textWriteReg2_3_C.Enabled = false;
                    toggleSwitchWriteReg2_3.Enabled = false;
                    textWriteReg2_4.Enabled = false;
                    textWriteReg2_4_C.Enabled = false;
                    toggleSwitchWriteReg2_4.Enabled = false;
                    textWriteReg2_6.Enabled = false;
                    toggleSwitchWriteReg2_6.Enabled = false;
                }
            }
        }

        // FormClosing事件处理方法
        private void FormHandMode_FormClosing(object sender, FormClosingEventArgs e)
        {
            //将所有反应管置于初始态避免主页面关闭反应管还在加热
            FormEquipRunMain.stateInstruments.ForEach(instr =>
            {
                if (instr.CurrentState is InitializingState)
                {
                    instr.InitPort();
                }
            });
        }
    }

}
