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
    public class FormHandMode : BaseForm
    {
        private Label labTCO;
        private Button butDCF1;
        private Button butDCF2;
        private Button butDCF3;
        private Button butDCF4;
        private Label labDCF1Light;
        private Label labDCF3Light;
        private Label labYHL1Light;
        private Label labCHL1Light;
        private Label labDCF2Light;
        private Label labDCF4Light;
        private Label labYHL2Light;
        private Label labCHL2Light;
        private Label labBox1Light;
        private TextBox textWriteReg1_5;
        private Label label8;
        private Button butBack;

        private ToggleSwitch toggleSwitchWriteReg1_5;
        private Label labBox2Light;
        private Label label10;
        private TextBox textWriteReg2_6;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private TextBox textReadHolding1_1;
        private Label label7;
        private TextBox textReadHolding2_2;
        private Label label9;
        private Label label11;
        private Label label12;
        private ToggleSwitch toggleSwitchWriteReg1_1;
        private Label label13;
        private TextBox textWriteReg1_1;
        private ToggleSwitch toggleSwitchWriteReg1_2;
        private Label label14;
        private TextBox textWriteReg1_2;
        private ToggleSwitch toggleSwitchWriteReg2_3;
        private Label label15;
        private TextBox textWriteReg2_3;
        private ToggleSwitch toggleSwitchWriteReg2_4;
        private Label label16;
        private TextBox textWriteReg2_4;
        private Label label17;
        private Label label18;
        private Label label19;
        private Label label20;
        private TextBox textWriteReg1_1_C;
        private Label label21;
        private Label label22;
        private TextBox textWriteReg1_2_C;
        private Label label23;
        private TextBox textWriteReg2_3_C;
        private Label label24;
        private TextBox textWriteReg2_4_C;
        private ToggleSwitch toggleSwitchWriteReg2_6;

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
                ModbusReg WriteReg1_1 = Smess.WriteReg1_1.Clone();
                WriteReg1_1.vbyte = MBRTU.U16tou8((ushort)(val * 1000));
                GlobalPara.deviceProtocol.equipinfo.AddMainQueue(WriteReg1_1);

            }
            else
            {
                // 开关关闭时的操作
                ModbusReg WriteReg1_1 = Smess.WriteReg1_1.Clone();
                WriteReg1_1.vbyte = MBRTU.U16tou8(0x0000);
                GlobalPara.deviceProtocol.equipinfo.AddMainQueue(WriteReg1_1);
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
                ModbusReg WriteReg1_2 = Smess.WriteReg1_2.Clone();
                WriteReg1_2.vbyte = MBRTU.U16tou8((ushort)(val * 1000));
                GlobalPara.deviceProtocol.equipinfo.AddMainQueue(WriteReg1_2);

            }
            else
            {
                // 开关关闭时的操作
                ModbusReg WriteReg1_2 = Smess.WriteReg1_2.Clone();
                WriteReg1_2.vbyte = MBRTU.U16tou8(0x0000);
                GlobalPara.deviceProtocol.equipinfo.AddMainQueue(WriteReg1_2);
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
                    if (float.TryParse(textWriteReg1_5.Text, out float val) || val < 0 || val > 5)
                    {
                        ModbusReg WriteReg1_5 = Smess.WriteReg1_5.Clone();
                        WriteReg1_5.vbyte = MBRTU.U16tou8((ushort)(UnitConverter.FlowToElectric(val)));
                        GlobalPara.deviceProtocol.equipinfo.AddMainQueue(WriteReg1_5);
                    }
                    else
                    {
                        MessageBox.Show(LanguageManager.GetMsg("10011"), LanguageManager.GetMsg("10000"), MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                }
                else
                {
                    // 开关关闭时的操作
                    ModbusReg WriteReg1_5 = Smess.WriteReg1_5.Clone();
                    WriteReg1_5.vbyte = MBRTU.U16tou8((ushort)UnitConverter.FlowToElectric(0f));
                    GlobalPara.deviceProtocol.equipinfo.AddMainQueue(WriteReg1_5);
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
                ModbusReg WriteReg2_3 = Smess.WriteReg2_3.Clone();
                WriteReg2_3.vbyte = MBRTU.U16tou8((ushort)(val * 1000));
                GlobalPara.deviceProtocol.equipinfo.AddMainQueue(WriteReg2_3.Clone());

            }
            else
            {

                // 开关关闭时的操作
                ModbusReg WriteReg2_3 = Smess.WriteReg2_3.Clone();
                WriteReg2_3.vbyte = MBRTU.U16tou8(0x0000);
                GlobalPara.deviceProtocol.equipinfo.AddMainQueue(WriteReg2_3);
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
                ModbusReg WriteReg2_4 = Smess.WriteReg2_4.Clone();
                WriteReg2_4.vbyte = MBRTU.U16tou8((ushort)(val * 1000));
                GlobalPara.deviceProtocol.equipinfo.AddMainQueue(WriteReg2_4);
            }
            else
            {
                // 开关关闭时的操作
                ModbusReg WriteReg2_4 = Smess.WriteReg2_4.Clone();
                WriteReg2_4.vbyte = MBRTU.U16tou8(0x0000);
                GlobalPara.deviceProtocol.equipinfo.AddMainQueue(WriteReg2_4);
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
                        if (float.TryParse(textWriteReg2_6.Text, out float val) || val < 0 || val > 5)
                        {
                            ModbusReg WriteReg2_6 = Smess.WriteReg2_6.Clone();
                            WriteReg2_6.vbyte = MBRTU.U16tou8((ushort)(UnitConverter.FlowToElectric(val)));
                            GlobalPara.deviceProtocol.equipinfo.AddMainQueue(WriteReg2_6);
                        }
                        else
                        {
                            MessageBox.Show(LanguageManager.GetMsg("10011"), LanguageManager.GetMsg("10000"), MessageBoxButtons.OK, MessageBoxIcon.None);
                        }
                    }
                    else
                    {
                        // 开关关闭时的操作
                        ModbusReg WriteReg2_6 = Smess.WriteReg2_6.Clone();
                        WriteReg2_6.vbyte = MBRTU.U16tou8(0x0000);
                        GlobalPara.deviceProtocol.equipinfo.AddMainQueue(WriteReg2_6);
                    }
                }
            }
        }
        private void InitializeComponent()
        {
            this.butBack = new System.Windows.Forms.Button();
            this.labTCO = new System.Windows.Forms.Label();
            this.butDCF1 = new System.Windows.Forms.Button();
            this.butDCF2 = new System.Windows.Forms.Button();
            this.butDCF3 = new System.Windows.Forms.Button();
            this.butDCF4 = new System.Windows.Forms.Button();
            this.labCHL2Light = new System.Windows.Forms.Label();
            this.labYHL2Light = new System.Windows.Forms.Label();
            this.labDCF4Light = new System.Windows.Forms.Label();
            this.labDCF2Light = new System.Windows.Forms.Label();
            this.labCHL1Light = new System.Windows.Forms.Label();
            this.labYHL1Light = new System.Windows.Forms.Label();
            this.labDCF3Light = new System.Windows.Forms.Label();
            this.labDCF1Light = new System.Windows.Forms.Label();
            this.labBox1Light = new System.Windows.Forms.Label();
            this.textWriteReg1_5 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.labBox2Light = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.textWriteReg2_6 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.toggleSwitchWriteReg1_5 = new TCOFurnace.UserControls.ToggleSwitch();
            this.toggleSwitchWriteReg2_6 = new TCOFurnace.UserControls.ToggleSwitch();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textReadHolding1_1 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textReadHolding2_2 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.toggleSwitchWriteReg1_1 = new TCOFurnace.UserControls.ToggleSwitch();
            this.label13 = new System.Windows.Forms.Label();
            this.textWriteReg1_1 = new System.Windows.Forms.TextBox();
            this.toggleSwitchWriteReg1_2 = new TCOFurnace.UserControls.ToggleSwitch();
            this.label14 = new System.Windows.Forms.Label();
            this.textWriteReg1_2 = new System.Windows.Forms.TextBox();
            this.toggleSwitchWriteReg2_3 = new TCOFurnace.UserControls.ToggleSwitch();
            this.label15 = new System.Windows.Forms.Label();
            this.textWriteReg2_3 = new System.Windows.Forms.TextBox();
            this.toggleSwitchWriteReg2_4 = new TCOFurnace.UserControls.ToggleSwitch();
            this.label16 = new System.Windows.Forms.Label();
            this.textWriteReg2_4 = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.textWriteReg1_1_C = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.textWriteReg1_2_C = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.textWriteReg2_3_C = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.textWriteReg2_4_C = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // butBack
            // 
            this.butBack.Location = new System.Drawing.Point(-1, -2);
            this.butBack.Name = "butBack";
            this.butBack.Size = new System.Drawing.Size(75, 23);
            this.butBack.TabIndex = 0;
            this.butBack.Text = "返回";
            this.butBack.UseVisualStyleBackColor = true;
            // 
            // labTCO
            // 
            this.labTCO.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labTCO.Location = new System.Drawing.Point(12, 55);
            this.labTCO.Name = "labTCO";
            this.labTCO.Size = new System.Drawing.Size(680, 29);
            this.labTCO.TabIndex = 1;
            this.labTCO.Text = "有机氚碳氧化系统";
            this.labTCO.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // butDCF1
            // 
            this.butDCF1.Location = new System.Drawing.Point(163, 130);
            this.butDCF1.Name = "butDCF1";
            this.butDCF1.Size = new System.Drawing.Size(87, 35);
            this.butDCF1.TabIndex = 2;
            this.butDCF1.Text = "电磁阀1";
            this.butDCF1.UseVisualStyleBackColor = true;
            this.butDCF1.Click += new System.EventHandler(this.butDCF1_Click);
            // 
            // butDCF2
            // 
            this.butDCF2.Location = new System.Drawing.Point(496, 130);
            this.butDCF2.Name = "butDCF2";
            this.butDCF2.Size = new System.Drawing.Size(87, 35);
            this.butDCF2.TabIndex = 3;
            this.butDCF2.Text = "电磁阀1";
            this.butDCF2.UseVisualStyleBackColor = true;
            this.butDCF2.Click += new System.EventHandler(this.butDCF2_Click);
            // 
            // butDCF3
            // 
            this.butDCF3.Location = new System.Drawing.Point(163, 180);
            this.butDCF3.Name = "butDCF3";
            this.butDCF3.Size = new System.Drawing.Size(87, 35);
            this.butDCF3.TabIndex = 4;
            this.butDCF3.Text = "电磁阀2";
            this.butDCF3.UseVisualStyleBackColor = true;
            this.butDCF3.Click += new System.EventHandler(this.butDCF3_Click);
            // 
            // butDCF4
            // 
            this.butDCF4.Location = new System.Drawing.Point(496, 180);
            this.butDCF4.Name = "butDCF4";
            this.butDCF4.Size = new System.Drawing.Size(87, 35);
            this.butDCF4.TabIndex = 5;
            this.butDCF4.Text = "电磁阀2";
            this.butDCF4.UseVisualStyleBackColor = true;
            this.butDCF4.Click += new System.EventHandler(this.butDCF4_Click);
            // 
            // labCHL2Light
            // 
            this.labCHL2Light.Image = global::TCOFurnace.Properties.Resources.NoSelectLight;
            this.labCHL2Light.Location = new System.Drawing.Point(456, 285);
            this.labCHL2Light.Name = "labCHL2Light";
            this.labCHL2Light.Size = new System.Drawing.Size(25, 25);
            this.labCHL2Light.TabIndex = 17;
            // 
            // labYHL2Light
            // 
            this.labYHL2Light.Image = global::TCOFurnace.Properties.Resources.NoSelectLight;
            this.labYHL2Light.Location = new System.Drawing.Point(456, 235);
            this.labYHL2Light.Name = "labYHL2Light";
            this.labYHL2Light.Size = new System.Drawing.Size(25, 25);
            this.labYHL2Light.TabIndex = 16;
            // 
            // labDCF4Light
            // 
            this.labDCF4Light.Image = global::TCOFurnace.Properties.Resources.NoSelectLight;
            this.labDCF4Light.Location = new System.Drawing.Point(456, 185);
            this.labDCF4Light.Name = "labDCF4Light";
            this.labDCF4Light.Size = new System.Drawing.Size(25, 25);
            this.labDCF4Light.TabIndex = 15;
            // 
            // labDCF2Light
            // 
            this.labDCF2Light.Image = global::TCOFurnace.Properties.Resources.NoSelectLight;
            this.labDCF2Light.Location = new System.Drawing.Point(456, 135);
            this.labDCF2Light.Name = "labDCF2Light";
            this.labDCF2Light.Size = new System.Drawing.Size(25, 25);
            this.labDCF2Light.TabIndex = 14;
            // 
            // labCHL1Light
            // 
            this.labCHL1Light.Image = global::TCOFurnace.Properties.Resources.NoSelectLight;
            this.labCHL1Light.Location = new System.Drawing.Point(123, 285);
            this.labCHL1Light.Name = "labCHL1Light";
            this.labCHL1Light.Size = new System.Drawing.Size(25, 25);
            this.labCHL1Light.TabIndex = 13;
            // 
            // labYHL1Light
            // 
            this.labYHL1Light.Image = global::TCOFurnace.Properties.Resources.NoSelectLight;
            this.labYHL1Light.Location = new System.Drawing.Point(123, 235);
            this.labYHL1Light.Name = "labYHL1Light";
            this.labYHL1Light.Size = new System.Drawing.Size(25, 25);
            this.labYHL1Light.TabIndex = 12;
            // 
            // labDCF3Light
            // 
            this.labDCF3Light.Image = global::TCOFurnace.Properties.Resources.NoSelectLight;
            this.labDCF3Light.Location = new System.Drawing.Point(123, 185);
            this.labDCF3Light.Name = "labDCF3Light";
            this.labDCF3Light.Size = new System.Drawing.Size(25, 25);
            this.labDCF3Light.TabIndex = 11;
            // 
            // labDCF1Light
            // 
            this.labDCF1Light.Image = global::TCOFurnace.Properties.Resources.NoSelectLight;
            this.labDCF1Light.Location = new System.Drawing.Point(123, 135);
            this.labDCF1Light.Name = "labDCF1Light";
            this.labDCF1Light.Size = new System.Drawing.Size(25, 25);
            this.labDCF1Light.TabIndex = 10;
            // 
            // labBox1Light
            // 
            this.labBox1Light.Image = global::TCOFurnace.Properties.Resources.NoSelectLight;
            this.labBox1Light.Location = new System.Drawing.Point(123, 335);
            this.labBox1Light.Name = "labBox1Light";
            this.labBox1Light.Size = new System.Drawing.Size(25, 25);
            this.labBox1Light.TabIndex = 18;
            // 
            // textWriteReg1_5
            // 
            this.textWriteReg1_5.Location = new System.Drawing.Point(163, 340);
            this.textWriteReg1_5.Name = "textWriteReg1_5";
            this.textWriteReg1_5.Size = new System.Drawing.Size(85, 21);
            this.textWriteReg1_5.TabIndex = 19;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(249, 345);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 12);
            this.label8.TabIndex = 20;
            this.label8.Text = "L/min";
            // 
            // labBox2Light
            // 
            this.labBox2Light.Image = global::TCOFurnace.Properties.Resources.NoSelectLight;
            this.labBox2Light.Location = new System.Drawing.Point(456, 335);
            this.labBox2Light.Name = "labBox2Light";
            this.labBox2Light.Size = new System.Drawing.Size(25, 25);
            this.labBox2Light.TabIndex = 21;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(586, 343);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(35, 12);
            this.label10.TabIndex = 23;
            this.label10.Text = "L/min";
            // 
            // textWriteReg2_6
            // 
            this.textWriteReg2_6.Location = new System.Drawing.Point(496, 340);
            this.textWriteReg2_6.Name = "textWriteReg2_6";
            this.textWriteReg2_6.Size = new System.Drawing.Size(87, 21);
            this.textWriteReg2_6.TabIndex = 22;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(180, 106);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 19);
            this.label1.TabIndex = 24;
            this.label1.Text = "1#";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(515, 106);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 19);
            this.label2.TabIndex = 25;
            this.label2.Text = "2#";
            // 
            // toggleSwitchWriteReg1_5
            // 
            this.toggleSwitchWriteReg1_5.IsOn = false;
            this.toggleSwitchWriteReg1_5.Location = new System.Drawing.Point(285, 341);
            this.toggleSwitchWriteReg1_5.Name = "toggleSwitchWriteReg1_5";
            this.toggleSwitchWriteReg1_5.OffBackColor = System.Drawing.Color.LightGray;
            this.toggleSwitchWriteReg1_5.OffText = "OFF";
            this.toggleSwitchWriteReg1_5.OffTextColor = System.Drawing.Color.DimGray;
            this.toggleSwitchWriteReg1_5.OnBackColor = System.Drawing.Color.LimeGreen;
            this.toggleSwitchWriteReg1_5.OnText = "ON";
            this.toggleSwitchWriteReg1_5.OnTextColor = System.Drawing.Color.White;
            this.toggleSwitchWriteReg1_5.Size = new System.Drawing.Size(53, 20);
            this.toggleSwitchWriteReg1_5.SwitchColor = System.Drawing.Color.White;
            this.toggleSwitchWriteReg1_5.TabIndex = 0;
            // 
            // toggleSwitchWriteReg2_6
            // 
            this.toggleSwitchWriteReg2_6.IsOn = false;
            this.toggleSwitchWriteReg2_6.Location = new System.Drawing.Point(624, 340);
            this.toggleSwitchWriteReg2_6.Name = "toggleSwitchWriteReg2_6";
            this.toggleSwitchWriteReg2_6.OffBackColor = System.Drawing.Color.LightGray;
            this.toggleSwitchWriteReg2_6.OffText = "OFF";
            this.toggleSwitchWriteReg2_6.OffTextColor = System.Drawing.Color.DimGray;
            this.toggleSwitchWriteReg2_6.OnBackColor = System.Drawing.Color.LimeGreen;
            this.toggleSwitchWriteReg2_6.OnText = "ON";
            this.toggleSwitchWriteReg2_6.OnTextColor = System.Drawing.Color.White;
            this.toggleSwitchWriteReg2_6.Size = new System.Drawing.Size(53, 20);
            this.toggleSwitchWriteReg2_6.SwitchColor = System.Drawing.Color.White;
            this.toggleSwitchWriteReg2_6.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 340);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 26;
            this.label3.Text = "流量计";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(33, 241);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 27;
            this.label4.Text = "氧调压";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(33, 296);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 28;
            this.label5.Text = "催调压";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(250, 389);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 12);
            this.label6.TabIndex = 30;
            this.label6.Text = "L/min";
            // 
            // textReadHolding1_1
            // 
            this.textReadHolding1_1.Enabled = false;
            this.textReadHolding1_1.Location = new System.Drawing.Point(163, 385);
            this.textReadHolding1_1.Name = "textReadHolding1_1";
            this.textReadHolding1_1.Size = new System.Drawing.Size(84, 21);
            this.textReadHolding1_1.TabIndex = 29;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(589, 390);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 12);
            this.label7.TabIndex = 32;
            this.label7.Text = "L/min";
            // 
            // textReadHolding2_2
            // 
            this.textReadHolding2_2.Enabled = false;
            this.textReadHolding2_2.Location = new System.Drawing.Point(496, 385);
            this.textReadHolding2_2.Name = "textReadHolding2_2";
            this.textReadHolding2_2.Size = new System.Drawing.Size(87, 21);
            this.textReadHolding2_2.TabIndex = 31;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(367, 243);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 33;
            this.label9.Text = "氧调压";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(367, 293);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(41, 12);
            this.label11.TabIndex = 34;
            this.label11.Text = "催调压";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(367, 340);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(41, 12);
            this.label12.TabIndex = 35;
            this.label12.Text = "流量计";
            // 
            // toggleSwitchWriteReg1_1
            // 
            this.toggleSwitchWriteReg1_1.IsOn = false;
            this.toggleSwitchWriteReg1_1.Location = new System.Drawing.Point(285, 240);
            this.toggleSwitchWriteReg1_1.Name = "toggleSwitchWriteReg1_1";
            this.toggleSwitchWriteReg1_1.OffBackColor = System.Drawing.Color.LightGray;
            this.toggleSwitchWriteReg1_1.OffText = "OFF";
            this.toggleSwitchWriteReg1_1.OffTextColor = System.Drawing.Color.DimGray;
            this.toggleSwitchWriteReg1_1.OnBackColor = System.Drawing.Color.LimeGreen;
            this.toggleSwitchWriteReg1_1.OnText = "ON";
            this.toggleSwitchWriteReg1_1.OnTextColor = System.Drawing.Color.White;
            this.toggleSwitchWriteReg1_1.Size = new System.Drawing.Size(53, 20);
            this.toggleSwitchWriteReg1_1.SwitchColor = System.Drawing.Color.White;
            this.toggleSwitchWriteReg1_1.TabIndex = 36;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(201, 245);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(17, 12);
            this.label13.TabIndex = 38;
            this.label13.Text = "mA";
            // 
            // textWriteReg1_1
            // 
            this.textWriteReg1_1.Location = new System.Drawing.Point(163, 240);
            this.textWriteReg1_1.Name = "textWriteReg1_1";
            this.textWriteReg1_1.Size = new System.Drawing.Size(35, 21);
            this.textWriteReg1_1.TabIndex = 37;
            // 
            // toggleSwitchWriteReg1_2
            // 
            this.toggleSwitchWriteReg1_2.IsOn = false;
            this.toggleSwitchWriteReg1_2.Location = new System.Drawing.Point(285, 290);
            this.toggleSwitchWriteReg1_2.Name = "toggleSwitchWriteReg1_2";
            this.toggleSwitchWriteReg1_2.OffBackColor = System.Drawing.Color.LightGray;
            this.toggleSwitchWriteReg1_2.OffText = "OFF";
            this.toggleSwitchWriteReg1_2.OffTextColor = System.Drawing.Color.DimGray;
            this.toggleSwitchWriteReg1_2.OnBackColor = System.Drawing.Color.LimeGreen;
            this.toggleSwitchWriteReg1_2.OnText = "ON";
            this.toggleSwitchWriteReg1_2.OnTextColor = System.Drawing.Color.White;
            this.toggleSwitchWriteReg1_2.Size = new System.Drawing.Size(53, 20);
            this.toggleSwitchWriteReg1_2.SwitchColor = System.Drawing.Color.White;
            this.toggleSwitchWriteReg1_2.TabIndex = 39;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(200, 296);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(17, 12);
            this.label14.TabIndex = 41;
            this.label14.Text = "mA";
            // 
            // textWriteReg1_2
            // 
            this.textWriteReg1_2.Location = new System.Drawing.Point(163, 290);
            this.textWriteReg1_2.Name = "textWriteReg1_2";
            this.textWriteReg1_2.Size = new System.Drawing.Size(35, 21);
            this.textWriteReg1_2.TabIndex = 40;
            // 
            // toggleSwitchWriteReg2_3
            // 
            this.toggleSwitchWriteReg2_3.IsOn = false;
            this.toggleSwitchWriteReg2_3.Location = new System.Drawing.Point(624, 243);
            this.toggleSwitchWriteReg2_3.Name = "toggleSwitchWriteReg2_3";
            this.toggleSwitchWriteReg2_3.OffBackColor = System.Drawing.Color.LightGray;
            this.toggleSwitchWriteReg2_3.OffText = "OFF";
            this.toggleSwitchWriteReg2_3.OffTextColor = System.Drawing.Color.DimGray;
            this.toggleSwitchWriteReg2_3.OnBackColor = System.Drawing.Color.LimeGreen;
            this.toggleSwitchWriteReg2_3.OnText = "ON";
            this.toggleSwitchWriteReg2_3.OnTextColor = System.Drawing.Color.White;
            this.toggleSwitchWriteReg2_3.Size = new System.Drawing.Size(53, 20);
            this.toggleSwitchWriteReg2_3.SwitchColor = System.Drawing.Color.White;
            this.toggleSwitchWriteReg2_3.TabIndex = 42;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(533, 245);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(17, 12);
            this.label15.TabIndex = 44;
            this.label15.Text = "mA";
            // 
            // textWriteReg2_3
            // 
            this.textWriteReg2_3.Location = new System.Drawing.Point(496, 240);
            this.textWriteReg2_3.Name = "textWriteReg2_3";
            this.textWriteReg2_3.Size = new System.Drawing.Size(35, 21);
            this.textWriteReg2_3.TabIndex = 43;
            // 
            // toggleSwitchWriteReg2_4
            // 
            this.toggleSwitchWriteReg2_4.IsOn = false;
            this.toggleSwitchWriteReg2_4.Location = new System.Drawing.Point(624, 290);
            this.toggleSwitchWriteReg2_4.Name = "toggleSwitchWriteReg2_4";
            this.toggleSwitchWriteReg2_4.OffBackColor = System.Drawing.Color.LightGray;
            this.toggleSwitchWriteReg2_4.OffText = "OFF";
            this.toggleSwitchWriteReg2_4.OffTextColor = System.Drawing.Color.DimGray;
            this.toggleSwitchWriteReg2_4.OnBackColor = System.Drawing.Color.LimeGreen;
            this.toggleSwitchWriteReg2_4.OnText = "ON";
            this.toggleSwitchWriteReg2_4.OnTextColor = System.Drawing.Color.White;
            this.toggleSwitchWriteReg2_4.Size = new System.Drawing.Size(53, 20);
            this.toggleSwitchWriteReg2_4.SwitchColor = System.Drawing.Color.White;
            this.toggleSwitchWriteReg2_4.TabIndex = 45;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(535, 292);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(17, 12);
            this.label16.TabIndex = 47;
            this.label16.Text = "mA";
            // 
            // textWriteReg2_4
            // 
            this.textWriteReg2_4.Location = new System.Drawing.Point(496, 290);
            this.textWriteReg2_4.Name = "textWriteReg2_4";
            this.textWriteReg2_4.Size = new System.Drawing.Size(35, 21);
            this.textWriteReg2_4.TabIndex = 46;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(33, 141);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(29, 12);
            this.label17.TabIndex = 48;
            this.label17.Text = "常开";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(33, 191);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(29, 12);
            this.label18.TabIndex = 49;
            this.label18.Text = "常关";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(367, 141);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(29, 12);
            this.label19.TabIndex = 50;
            this.label19.Text = "常开";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(367, 191);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(29, 12);
            this.label20.TabIndex = 51;
            this.label20.Text = "常关";
            // 
            // textWriteReg1_1_C
            // 
            this.textWriteReg1_1_C.Location = new System.Drawing.Point(222, 239);
            this.textWriteReg1_1_C.Name = "textWriteReg1_1_C";
            this.textWriteReg1_1_C.Size = new System.Drawing.Size(28, 21);
            this.textWriteReg1_1_C.TabIndex = 52;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(253, 244);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(17, 12);
            this.label21.TabIndex = 53;
            this.label21.Text = "℃";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(251, 294);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(17, 12);
            this.label22.TabIndex = 55;
            this.label22.Text = "℃";
            // 
            // textWriteReg1_2_C
            // 
            this.textWriteReg1_2_C.Location = new System.Drawing.Point(220, 290);
            this.textWriteReg1_2_C.Name = "textWriteReg1_2_C";
            this.textWriteReg1_2_C.Size = new System.Drawing.Size(28, 21);
            this.textWriteReg1_2_C.TabIndex = 54;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(584, 244);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(17, 12);
            this.label23.TabIndex = 57;
            this.label23.Text = "℃";
            // 
            // textWriteReg2_3_C
            // 
            this.textWriteReg2_3_C.Location = new System.Drawing.Point(554, 240);
            this.textWriteReg2_3_C.Name = "textWriteReg2_3_C";
            this.textWriteReg2_3_C.Size = new System.Drawing.Size(28, 21);
            this.textWriteReg2_3_C.TabIndex = 56;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(586, 294);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(17, 12);
            this.label24.TabIndex = 59;
            this.label24.Text = "℃";
            // 
            // textWriteReg2_4_C
            // 
            this.textWriteReg2_4_C.Location = new System.Drawing.Point(555, 290);
            this.textWriteReg2_4_C.Name = "textWriteReg2_4_C";
            this.textWriteReg2_4_C.Size = new System.Drawing.Size(28, 21);
            this.textWriteReg2_4_C.TabIndex = 58;
            // 
            // FormHandMode
            // 
            this.ClientSize = new System.Drawing.Size(719, 455);
            this.Controls.Add(this.toggleSwitchWriteReg2_4);
            this.Controls.Add(this.toggleSwitchWriteReg2_3);
            this.Controls.Add(this.toggleSwitchWriteReg1_1);
            this.Controls.Add(this.toggleSwitchWriteReg1_5);
            this.Controls.Add(this.toggleSwitchWriteReg2_6);
            this.Controls.Add(this.toggleSwitchWriteReg1_2);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.textWriteReg2_4_C);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.textWriteReg2_3_C);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.textWriteReg1_2_C);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.textWriteReg1_1_C);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.textWriteReg2_4);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.textWriteReg2_3);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.textWriteReg1_2);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.textWriteReg1_1);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textReadHolding2_2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textReadHolding1_1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.textWriteReg2_6);
            this.Controls.Add(this.labBox2Light);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textWriteReg1_5);
            this.Controls.Add(this.labBox1Light);
            this.Controls.Add(this.labCHL2Light);
            this.Controls.Add(this.labYHL2Light);
            this.Controls.Add(this.labDCF4Light);
            this.Controls.Add(this.labDCF2Light);
            this.Controls.Add(this.labCHL1Light);
            this.Controls.Add(this.labYHL1Light);
            this.Controls.Add(this.labDCF3Light);
            this.Controls.Add(this.labDCF1Light);
            this.Controls.Add(this.butDCF4);
            this.Controls.Add(this.butDCF3);
            this.Controls.Add(this.butDCF2);
            this.Controls.Add(this.butDCF1);
            this.Controls.Add(this.labTCO);
            this.Controls.Add(this.butBack);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FormHandMode";
            this.Text = "手动模式";
            this.Load += new System.EventHandler(this.FormHandMode_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        bool bButDCF1 = false;
        private void butDCF1_Click(object sender, EventArgs e)
        {
            bButDCF1 = !bButDCF1;
            ModbusReg WriteCoil1_1 = Smess.WriteCoil1_1.Clone();
            WriteCoil1_1.vbyte = (bButDCF1 ? new byte[2] { 0xff, 0x00 } : new byte[2] { 0x00, 0x00 });
            StopwatchHelper.Start(WriteCoil1_1.name);
            StopwatchHelper.Lap(WriteCoil1_1.name, "开始入栈");
            GlobalPara.deviceProtocol.equipinfo.AddMainQueue(WriteCoil1_1);
            StopwatchHelper.Lap(WriteCoil1_1.name, "结束入栈");
        }

        bool bButDCF3 = false;
        private void butDCF3_Click(object sender, EventArgs e)
        {
            bButDCF3 = !bButDCF3;
            ModbusReg WriteCoil1_2 = Smess.WriteCoil1_2.Clone();
            WriteCoil1_2.vbyte = (bButDCF3 ? new byte[2] { 0xff, 0x00 } : new byte[2] { 0x00, 0x00 });
            StopwatchHelper.Start(WriteCoil1_2.name);
            StopwatchHelper.Lap(WriteCoil1_2.name, "开始入栈");
            GlobalPara.deviceProtocol.equipinfo.AddMainQueue(WriteCoil1_2);
            StopwatchHelper.Lap(WriteCoil1_2.name, "结束入栈");

        }
        bool bButDCF2 = false;
        private void butDCF2_Click(object sender, EventArgs e)
        {
            bButDCF2 = !bButDCF2;
            ModbusReg WriteCoil2_3 = Smess.WriteCoil2_3.Clone();
            WriteCoil2_3.vbyte = (bButDCF2 ? new byte[2] { 0xff, 0x00 } : new byte[2] { 0x00, 0x00 });
            StopwatchHelper.Start(WriteCoil2_3.name);
            StopwatchHelper.Lap(WriteCoil2_3.name, "开始入栈");
            GlobalPara.deviceProtocol.equipinfo.AddMainQueue(WriteCoil2_3);
            StopwatchHelper.Lap(WriteCoil2_3.name, "结束入栈");

        }
        bool bButDCF4 = false;
        private void butDCF4_Click(object sender, EventArgs e)
        {
            bButDCF4 = !bButDCF4;
            var WriteCoil2_4 = Smess.WriteCoil2_4.Clone();
            WriteCoil2_4.vbyte = (bButDCF4 ? new byte[2] { 0xff, 0x00 } : new byte[2] { 0x00, 0x00 });
            StopwatchHelper.Start(WriteCoil2_4.name);
            StopwatchHelper.Lap(WriteCoil2_4.name, "开始入栈");
            GlobalPara.deviceProtocol.equipinfo.AddMainQueue(WriteCoil2_4);
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
    }

}
