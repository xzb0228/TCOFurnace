using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using TCOFurnace.Models;
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
        private Button butYHL1;
        private Button butYHL2;
        private Button butCHL1;
        private Button butCHL2;
        private Label labDCF1Light;
        private Label labDCF3Light;
        private Label labYHL1Light;
        private Label labCHL1Light;
        private Label labDCF2Light;
        private Label labDCF4Light;
        private Label labYHL2Light;
        private Label labCHL2Light;
        private Label labBox1Light;
        private TextBox textBox1;
        private Label label8;
        private Button butBack;

        private ToggleSwitch toggleSwitch1;
        private Label labBox2Light;
        private Label label10;
        private TextBox textBox2;
        private Label label1;
        private Label label2;
        private ToggleSwitch toggleSwitch2;

        public FormHandMode()
        {
            InitializeComponent();
            //AddMyControls();
        }

        private void AddMyControls() {

            // 创建开关控件
            toggleSwitch1 = new ToggleSwitch();
            toggleSwitch1.Location = new System.Drawing.Point(190, 310);
            toggleSwitch1.ToggleChanged += ToggleSwitch1_ToggleChanged;
            this.Controls.Add(toggleSwitch1);

            // 创建开关控件
            toggleSwitch2 = new ToggleSwitch();
            toggleSwitch2.Location = new System.Drawing.Point(390, 310);
            toggleSwitch2.ToggleChanged += ToggleSwitch2_ToggleChanged;
            this.Controls.Add(toggleSwitch2);
        }
        // 开关状态改变事件处理
        private void ToggleSwitch1_ToggleChanged(object sender, EventArgs e)
        {
            var toggle = sender as ToggleSwitch;
            if (toggle != null)
            {
                // 这里可以添加更多状态改变后的逻辑
                if (toggle.IsOn)
                {
                    // 开关打开时的操作

                }
                else
                {
                    // 开关关闭时的操作
                }
            }
        }

        // 开关状态改变事件处理
        private void ToggleSwitch2_ToggleChanged(object sender, EventArgs e)
        {
            var toggle = sender as ToggleSwitch;
            if (toggle != null)
            {
                // 这里可以添加更多状态改变后的逻辑
                if (toggle.IsOn)
                {
                    // 开关打开时的操作
                   
                }
                else
                {
                    // 开关关闭时的操作
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
            this.butYHL1 = new System.Windows.Forms.Button();
            this.butYHL2 = new System.Windows.Forms.Button();
            this.butCHL1 = new System.Windows.Forms.Button();
            this.butCHL2 = new System.Windows.Forms.Button();
            this.labCHL2Light = new System.Windows.Forms.Label();
            this.labYHL2Light = new System.Windows.Forms.Label();
            this.labDCF4Light = new System.Windows.Forms.Label();
            this.labDCF2Light = new System.Windows.Forms.Label();
            this.labCHL1Light = new System.Windows.Forms.Label();
            this.labYHL1Light = new System.Windows.Forms.Label();
            this.labDCF3Light = new System.Windows.Forms.Label();
            this.labDCF1Light = new System.Windows.Forms.Label();
            this.labBox1Light = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.labBox2Light = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.toggleSwitch1 = new TCOFurnace.UserControls.ToggleSwitch();
            this.toggleSwitch2 = new TCOFurnace.UserControls.ToggleSwitch();
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
            this.labTCO.Location = new System.Drawing.Point(12, 24);
            this.labTCO.Name = "labTCO";
            this.labTCO.Size = new System.Drawing.Size(472, 29);
            this.labTCO.TabIndex = 1;
            this.labTCO.Text = "有机氚碳氧化系统";
            this.labTCO.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // butDCF1
            // 
            this.butDCF1.Location = new System.Drawing.Point(120, 100);
            this.butDCF1.Name = "butDCF1";
            this.butDCF1.Size = new System.Drawing.Size(75, 35);
            this.butDCF1.TabIndex = 2;
            this.butDCF1.Text = "电磁阀1";
            this.butDCF1.UseVisualStyleBackColor = true;
            this.butDCF1.Click += new System.EventHandler(this.butDCF1_Click);
            // 
            // butDCF2
            // 
            this.butDCF2.Location = new System.Drawing.Point(320, 100);
            this.butDCF2.Name = "butDCF2";
            this.butDCF2.Size = new System.Drawing.Size(75, 35);
            this.butDCF2.TabIndex = 3;
            this.butDCF2.Text = "电磁阀1";
            this.butDCF2.UseVisualStyleBackColor = true;
            // 
            // butDCF3
            // 
            this.butDCF3.Location = new System.Drawing.Point(120, 150);
            this.butDCF3.Name = "butDCF3";
            this.butDCF3.Size = new System.Drawing.Size(75, 35);
            this.butDCF3.TabIndex = 4;
            this.butDCF3.Text = "电磁阀2";
            this.butDCF3.UseVisualStyleBackColor = true;
            // 
            // butDCF4
            // 
            this.butDCF4.Location = new System.Drawing.Point(320, 150);
            this.butDCF4.Name = "butDCF4";
            this.butDCF4.Size = new System.Drawing.Size(75, 35);
            this.butDCF4.TabIndex = 5;
            this.butDCF4.Text = "电磁阀2";
            this.butDCF4.UseVisualStyleBackColor = true;
            // 
            // butYHL1
            // 
            this.butYHL1.Location = new System.Drawing.Point(120, 200);
            this.butYHL1.Name = "butYHL1";
            this.butYHL1.Size = new System.Drawing.Size(75, 35);
            this.butYHL1.TabIndex = 6;
            this.butYHL1.Text = "氧化炉";
            this.butYHL1.UseVisualStyleBackColor = true;
            // 
            // butYHL2
            // 
            this.butYHL2.Location = new System.Drawing.Point(320, 205);
            this.butYHL2.Name = "butYHL2";
            this.butYHL2.Size = new System.Drawing.Size(75, 35);
            this.butYHL2.TabIndex = 7;
            this.butYHL2.Text = "氧化炉";
            this.butYHL2.UseVisualStyleBackColor = true;
            // 
            // butCHL1
            // 
            this.butCHL1.Location = new System.Drawing.Point(120, 250);
            this.butCHL1.Name = "butCHL1";
            this.butCHL1.Size = new System.Drawing.Size(75, 35);
            this.butCHL1.TabIndex = 8;
            this.butCHL1.Text = "催化炉";
            this.butCHL1.UseVisualStyleBackColor = true;
            // 
            // butCHL2
            // 
            this.butCHL2.Location = new System.Drawing.Point(320, 255);
            this.butCHL2.Name = "butCHL2";
            this.butCHL2.Size = new System.Drawing.Size(75, 35);
            this.butCHL2.TabIndex = 9;
            this.butCHL2.Text = "催化炉";
            this.butCHL2.UseVisualStyleBackColor = true;
            // 
            // labCHL2Light
            // 
            this.labCHL2Light.Image = global::TCOFurnace.Properties.Resources.NoSelectLight;
            this.labCHL2Light.Location = new System.Drawing.Point(280, 255);
            this.labCHL2Light.Name = "labCHL2Light";
            this.labCHL2Light.Size = new System.Drawing.Size(25, 25);
            this.labCHL2Light.TabIndex = 17;
            // 
            // labYHL2Light
            // 
            this.labYHL2Light.Image = global::TCOFurnace.Properties.Resources.NoSelectLight;
            this.labYHL2Light.Location = new System.Drawing.Point(280, 205);
            this.labYHL2Light.Name = "labYHL2Light";
            this.labYHL2Light.Size = new System.Drawing.Size(25, 25);
            this.labYHL2Light.TabIndex = 16;
            // 
            // labDCF4Light
            // 
            this.labDCF4Light.Image = global::TCOFurnace.Properties.Resources.NoSelectLight;
            this.labDCF4Light.Location = new System.Drawing.Point(280, 155);
            this.labDCF4Light.Name = "labDCF4Light";
            this.labDCF4Light.Size = new System.Drawing.Size(25, 25);
            this.labDCF4Light.TabIndex = 15;
            // 
            // labDCF2Light
            // 
            this.labDCF2Light.Image = global::TCOFurnace.Properties.Resources.NoSelectLight;
            this.labDCF2Light.Location = new System.Drawing.Point(280, 105);
            this.labDCF2Light.Name = "labDCF2Light";
            this.labDCF2Light.Size = new System.Drawing.Size(25, 25);
            this.labDCF2Light.TabIndex = 14;
            // 
            // labCHL1Light
            // 
            this.labCHL1Light.Image = global::TCOFurnace.Properties.Resources.NoSelectLight;
            this.labCHL1Light.Location = new System.Drawing.Point(80, 255);
            this.labCHL1Light.Name = "labCHL1Light";
            this.labCHL1Light.Size = new System.Drawing.Size(25, 25);
            this.labCHL1Light.TabIndex = 13;
            // 
            // labYHL1Light
            // 
            this.labYHL1Light.Image = global::TCOFurnace.Properties.Resources.NoSelectLight;
            this.labYHL1Light.Location = new System.Drawing.Point(80, 205);
            this.labYHL1Light.Name = "labYHL1Light";
            this.labYHL1Light.Size = new System.Drawing.Size(25, 25);
            this.labYHL1Light.TabIndex = 12;
            // 
            // labDCF3Light
            // 
            this.labDCF3Light.Image = global::TCOFurnace.Properties.Resources.NoSelectLight;
            this.labDCF3Light.Location = new System.Drawing.Point(80, 155);
            this.labDCF3Light.Name = "labDCF3Light";
            this.labDCF3Light.Size = new System.Drawing.Size(25, 25);
            this.labDCF3Light.TabIndex = 11;
            // 
            // labDCF1Light
            // 
            this.labDCF1Light.Image = global::TCOFurnace.Properties.Resources.NoSelectLight;
            this.labDCF1Light.Location = new System.Drawing.Point(80, 105);
            this.labDCF1Light.Name = "labDCF1Light";
            this.labDCF1Light.Size = new System.Drawing.Size(25, 25);
            this.labDCF1Light.TabIndex = 10;
            // 
            // labBox1Light
            // 
            this.labBox1Light.Image = global::TCOFurnace.Properties.Resources.NoSelectLight;
            this.labBox1Light.Location = new System.Drawing.Point(80, 305);
            this.labBox1Light.Name = "labBox1Light";
            this.labBox1Light.Size = new System.Drawing.Size(25, 25);
            this.labBox1Light.TabIndex = 18;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(120, 310);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(26, 21);
            this.textBox1.TabIndex = 19;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(150, 315);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 12);
            this.label8.TabIndex = 20;
            this.label8.Text = "L/min";
            // 
            // labBox2Light
            // 
            this.labBox2Light.Image = global::TCOFurnace.Properties.Resources.NoSelectLight;
            this.labBox2Light.Location = new System.Drawing.Point(280, 305);
            this.labBox2Light.Name = "labBox2Light";
            this.labBox2Light.Size = new System.Drawing.Size(25, 25);
            this.labBox2Light.TabIndex = 21;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(350, 315);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(35, 12);
            this.label10.TabIndex = 23;
            this.label10.Text = "L/min";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(320, 310);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(26, 21);
            this.textBox2.TabIndex = 22;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(137, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 19);
            this.label1.TabIndex = 24;
            this.label1.Text = "1#";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(339, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 19);
            this.label2.TabIndex = 25;
            this.label2.Text = "2#";
            // 
            // toggleSwitch1
            // 
            this.toggleSwitch1.IsOn = false;
            this.toggleSwitch1.Location = new System.Drawing.Point(190, 310);
            this.toggleSwitch1.Name = "toggleSwitch1";
            this.toggleSwitch1.OffBackColor = System.Drawing.Color.LightGray;
            this.toggleSwitch1.OffText = "OFF";
            this.toggleSwitch1.OffTextColor = System.Drawing.Color.DimGray;
            this.toggleSwitch1.OnBackColor = System.Drawing.Color.LimeGreen;
            this.toggleSwitch1.OnText = "ON";
            this.toggleSwitch1.OnTextColor = System.Drawing.Color.White;
            this.toggleSwitch1.Size = new System.Drawing.Size(53, 20);
            this.toggleSwitch1.SwitchColor = System.Drawing.Color.White;
            this.toggleSwitch1.TabIndex = 0;
            // 
            // toggleSwitch2
            // 
            this.toggleSwitch2.IsOn = false;
            this.toggleSwitch2.Location = new System.Drawing.Point(390, 310);
            this.toggleSwitch2.Name = "toggleSwitch2";
            this.toggleSwitch2.OffBackColor = System.Drawing.Color.LightGray;
            this.toggleSwitch2.OffText = "OFF";
            this.toggleSwitch2.OffTextColor = System.Drawing.Color.DimGray;
            this.toggleSwitch2.OnBackColor = System.Drawing.Color.LimeGreen;
            this.toggleSwitch2.OnText = "ON";
            this.toggleSwitch2.OnTextColor = System.Drawing.Color.White;
            this.toggleSwitch2.Size = new System.Drawing.Size(53, 20);
            this.toggleSwitch2.SwitchColor = System.Drawing.Color.White;
            this.toggleSwitch2.TabIndex = 1;
            // 
            // FormHandMode
            // 
            this.ClientSize = new System.Drawing.Size(501, 357);
            this.Controls.Add(this.toggleSwitch1);
            this.Controls.Add(this.toggleSwitch2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.labBox2Light);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.labBox1Light);
            this.Controls.Add(this.labCHL2Light);
            this.Controls.Add(this.labYHL2Light);
            this.Controls.Add(this.labDCF4Light);
            this.Controls.Add(this.labDCF2Light);
            this.Controls.Add(this.labCHL1Light);
            this.Controls.Add(this.labYHL1Light);
            this.Controls.Add(this.labDCF3Light);
            this.Controls.Add(this.labDCF1Light);
            this.Controls.Add(this.butCHL2);
            this.Controls.Add(this.butCHL1);
            this.Controls.Add(this.butYHL2);
            this.Controls.Add(this.butYHL1);
            this.Controls.Add(this.butDCF4);
            this.Controls.Add(this.butDCF3);
            this.Controls.Add(this.butDCF2);
            this.Controls.Add(this.butDCF1);
            this.Controls.Add(this.labTCO);
            this.Controls.Add(this.butBack);
            this.Name = "FormHandMode";
            this.Text = "手动模式";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void butDCF1_Click(object sender, EventArgs e)
        {
            //打开串口Com3 串口
        }
    }

}
