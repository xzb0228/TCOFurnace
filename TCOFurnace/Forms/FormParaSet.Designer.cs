using Common;
using EquipDriver;
using ModBusRTU;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using TCOFurnace.UserControls;

namespace TCOFurnace.Forms
{
    public partial class FormParaSet
    {
        
        #region Windows 窗体设计器生成的代码

        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.textFlow = new System.Windows.Forms.TextBox();
            this.label38 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.textTStep4T = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.textTStep4C = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.textTStep4O = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.textTStep3T = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.textTStep3C = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.textTStep3O = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textTStep2T = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.textTStep2C = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.textTStep2O = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textTStep1T = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textTStep1C = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textTStep1O = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label31 = new System.Windows.Forms.Label();
            this.labTCO = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.butSave = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label57 = new System.Windows.Forms.Label();
            this.textVStep5C = new System.Windows.Forms.TextBox();
            this.label58 = new System.Windows.Forms.Label();
            this.label61 = new System.Windows.Forms.Label();
            this.textVStep5O = new System.Windows.Forms.TextBox();
            this.label62 = new System.Windows.Forms.Label();
            this.label63 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.textVStep9C = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.textVStep9O = new System.Windows.Forms.TextBox();
            this.label34 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.textVStep8C = new System.Windows.Forms.TextBox();
            this.label37 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.textVStep8O = new System.Windows.Forms.TextBox();
            this.label41 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.textVStep7C = new System.Windows.Forms.TextBox();
            this.label44 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.textVStep7O = new System.Windows.Forms.TextBox();
            this.label48 = new System.Windows.Forms.Label();
            this.label49 = new System.Windows.Forms.Label();
            this.label50 = new System.Windows.Forms.Label();
            this.textVStep6C = new System.Windows.Forms.TextBox();
            this.label51 = new System.Windows.Forms.Label();
            this.label54 = new System.Windows.Forms.Label();
            this.textVStep6O = new System.Windows.Forms.TextBox();
            this.label55 = new System.Windows.Forms.Label();
            this.label56 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label64 = new System.Windows.Forms.Label();
            this.textVStep4C = new System.Windows.Forms.TextBox();
            this.label65 = new System.Windows.Forms.Label();
            this.label68 = new System.Windows.Forms.Label();
            this.textVStep4O = new System.Windows.Forms.TextBox();
            this.label69 = new System.Windows.Forms.Label();
            this.label70 = new System.Windows.Forms.Label();
            this.label71 = new System.Windows.Forms.Label();
            this.textVStep3C = new System.Windows.Forms.TextBox();
            this.label72 = new System.Windows.Forms.Label();
            this.label75 = new System.Windows.Forms.Label();
            this.textVStep3O = new System.Windows.Forms.TextBox();
            this.label76 = new System.Windows.Forms.Label();
            this.label77 = new System.Windows.Forms.Label();
            this.label78 = new System.Windows.Forms.Label();
            this.textVStep2C = new System.Windows.Forms.TextBox();
            this.label79 = new System.Windows.Forms.Label();
            this.label82 = new System.Windows.Forms.Label();
            this.textVStep2O = new System.Windows.Forms.TextBox();
            this.label83 = new System.Windows.Forms.Label();
            this.label84 = new System.Windows.Forms.Label();
            this.label85 = new System.Windows.Forms.Label();
            this.textVStep1C = new System.Windows.Forms.TextBox();
            this.label86 = new System.Windows.Forms.Label();
            this.label89 = new System.Windows.Forms.Label();
            this.textVStep1O = new System.Windows.Forms.TextBox();
            this.label90 = new System.Windows.Forms.Label();
            this.label91 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label66 = new System.Windows.Forms.Label();
            this.textTStep5T = new System.Windows.Forms.TextBox();
            this.label67 = new System.Windows.Forms.Label();
            this.label73 = new System.Windows.Forms.Label();
            this.textTStep5C = new System.Windows.Forms.TextBox();
            this.label74 = new System.Windows.Forms.Label();
            this.label80 = new System.Windows.Forms.Label();
            this.textTStep5O = new System.Windows.Forms.TextBox();
            this.label81 = new System.Windows.Forms.Label();
            this.label87 = new System.Windows.Forms.Label();
            this.label88 = new System.Windows.Forms.Label();
            this.textTStep9T = new System.Windows.Forms.TextBox();
            this.label92 = new System.Windows.Forms.Label();
            this.label93 = new System.Windows.Forms.Label();
            this.textTStep9C = new System.Windows.Forms.TextBox();
            this.label94 = new System.Windows.Forms.Label();
            this.label95 = new System.Windows.Forms.Label();
            this.textTStep9O = new System.Windows.Forms.TextBox();
            this.label96 = new System.Windows.Forms.Label();
            this.label97 = new System.Windows.Forms.Label();
            this.label98 = new System.Windows.Forms.Label();
            this.textTStep8T = new System.Windows.Forms.TextBox();
            this.label99 = new System.Windows.Forms.Label();
            this.label100 = new System.Windows.Forms.Label();
            this.textTStep8C = new System.Windows.Forms.TextBox();
            this.label101 = new System.Windows.Forms.Label();
            this.label102 = new System.Windows.Forms.Label();
            this.textTStep8O = new System.Windows.Forms.TextBox();
            this.label103 = new System.Windows.Forms.Label();
            this.label104 = new System.Windows.Forms.Label();
            this.label105 = new System.Windows.Forms.Label();
            this.textTStep7T = new System.Windows.Forms.TextBox();
            this.label106 = new System.Windows.Forms.Label();
            this.label107 = new System.Windows.Forms.Label();
            this.textTStep7C = new System.Windows.Forms.TextBox();
            this.label108 = new System.Windows.Forms.Label();
            this.label109 = new System.Windows.Forms.Label();
            this.textTStep7O = new System.Windows.Forms.TextBox();
            this.label110 = new System.Windows.Forms.Label();
            this.label111 = new System.Windows.Forms.Label();
            this.label112 = new System.Windows.Forms.Label();
            this.textTStep6T = new System.Windows.Forms.TextBox();
            this.label113 = new System.Windows.Forms.Label();
            this.label114 = new System.Windows.Forms.Label();
            this.textTStep6C = new System.Windows.Forms.TextBox();
            this.label115 = new System.Windows.Forms.Label();
            this.label116 = new System.Windows.Forms.Label();
            this.textTStep6O = new System.Windows.Forms.TextBox();
            this.label117 = new System.Windows.Forms.Label();
            this.label118 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textFlow);
            this.panel1.Controls.Add(this.label38);
            this.panel1.Controls.Add(this.label22);
            this.panel1.Controls.Add(this.textTStep4T);
            this.panel1.Controls.Add(this.label23);
            this.panel1.Controls.Add(this.label24);
            this.panel1.Controls.Add(this.textTStep4C);
            this.panel1.Controls.Add(this.label25);
            this.panel1.Controls.Add(this.label26);
            this.panel1.Controls.Add(this.textTStep4O);
            this.panel1.Controls.Add(this.label27);
            this.panel1.Controls.Add(this.label28);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.textTStep3T);
            this.panel1.Controls.Add(this.label16);
            this.panel1.Controls.Add(this.label17);
            this.panel1.Controls.Add(this.textTStep3C);
            this.panel1.Controls.Add(this.label18);
            this.panel1.Controls.Add(this.label19);
            this.panel1.Controls.Add(this.textTStep3O);
            this.panel1.Controls.Add(this.label20);
            this.panel1.Controls.Add(this.label21);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.textTStep2T);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.textTStep2C);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.textTStep2O);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.textTStep1T);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.textTStep1C);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.textTStep1O);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(3, 14);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(660, 200);
            this.panel1.TabIndex = 0;
            // 
            // textFlow
            // 
            this.textFlow.Location = new System.Drawing.Point(343, 5);
            this.textFlow.Name = "textFlow";
            this.textFlow.Size = new System.Drawing.Size(44, 25);
            this.textFlow.TabIndex = 43;
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(169, 7);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(137, 15);
            this.label38.TabIndex = 42;
            this.label38.Text = "气体流量（L/min）";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(605, 171);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(31, 15);
            this.label22.TabIndex = 41;
            this.label22.Text = "min";
            // 
            // textTStep4T
            // 
            this.textTStep4T.Location = new System.Drawing.Point(554, 167);
            this.textTStep4T.Name = "textTStep4T";
            this.textTStep4T.Size = new System.Drawing.Size(45, 25);
            this.textTStep4T.TabIndex = 40;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(506, 170);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(37, 15);
            this.label23.TabIndex = 39;
            this.label23.Text = "时间";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(393, 171);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(22, 15);
            this.label24.TabIndex = 38;
            this.label24.Text = "℃";
            // 
            // textTStep4C
            // 
            this.textTStep4C.Location = new System.Drawing.Point(342, 168);
            this.textTStep4C.Name = "textTStep4C";
            this.textTStep4C.Size = new System.Drawing.Size(45, 25);
            this.textTStep4C.TabIndex = 37;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(282, 170);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(52, 15);
            this.label25.TabIndex = 36;
            this.label25.Text = "催化区";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(169, 170);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(22, 15);
            this.label26.TabIndex = 35;
            this.label26.Text = "℃";
            // 
            // textTStep4O
            // 
            this.textTStep4O.Location = new System.Drawing.Point(118, 166);
            this.textTStep4O.Name = "textTStep4O";
            this.textTStep4O.Size = new System.Drawing.Size(45, 25);
            this.textTStep4O.TabIndex = 34;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(60, 170);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(52, 15);
            this.label27.TabIndex = 33;
            this.label27.Text = "氧化区";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(18, 170);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(47, 15);
            this.label28.TabIndex = 32;
            this.label28.Text = "Step4";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(605, 125);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(31, 15);
            this.label15.TabIndex = 31;
            this.label15.Text = "min";
            // 
            // textTStep3T
            // 
            this.textTStep3T.Location = new System.Drawing.Point(554, 121);
            this.textTStep3T.Name = "textTStep3T";
            this.textTStep3T.Size = new System.Drawing.Size(45, 25);
            this.textTStep3T.TabIndex = 30;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(506, 125);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(37, 15);
            this.label16.TabIndex = 29;
            this.label16.Text = "时间";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(393, 125);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(22, 15);
            this.label17.TabIndex = 28;
            this.label17.Text = "℃";
            // 
            // textTStep3C
            // 
            this.textTStep3C.Location = new System.Drawing.Point(342, 122);
            this.textTStep3C.Name = "textTStep3C";
            this.textTStep3C.Size = new System.Drawing.Size(45, 25);
            this.textTStep3C.TabIndex = 27;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(282, 124);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(52, 15);
            this.label18.TabIndex = 26;
            this.label18.Text = "催化区";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(169, 124);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(22, 15);
            this.label19.TabIndex = 25;
            this.label19.Text = "℃";
            // 
            // textTStep3O
            // 
            this.textTStep3O.Location = new System.Drawing.Point(118, 120);
            this.textTStep3O.Name = "textTStep3O";
            this.textTStep3O.Size = new System.Drawing.Size(45, 25);
            this.textTStep3O.TabIndex = 24;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(60, 124);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(52, 15);
            this.label20.TabIndex = 23;
            this.label20.Text = "氧化区";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(18, 124);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(47, 15);
            this.label21.TabIndex = 22;
            this.label21.Text = "Step3";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(605, 84);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 15);
            this.label6.TabIndex = 21;
            this.label6.Text = "min";
            // 
            // textTStep2T
            // 
            this.textTStep2T.Location = new System.Drawing.Point(554, 80);
            this.textTStep2T.Name = "textTStep2T";
            this.textTStep2T.Size = new System.Drawing.Size(45, 25);
            this.textTStep2T.TabIndex = 20;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(506, 84);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(37, 15);
            this.label9.TabIndex = 19;
            this.label9.Text = "时间";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(393, 84);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(22, 15);
            this.label10.TabIndex = 18;
            this.label10.Text = "℃";
            // 
            // textTStep2C
            // 
            this.textTStep2C.Location = new System.Drawing.Point(342, 81);
            this.textTStep2C.Name = "textTStep2C";
            this.textTStep2C.Size = new System.Drawing.Size(45, 25);
            this.textTStep2C.TabIndex = 17;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(282, 83);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(52, 15);
            this.label11.TabIndex = 16;
            this.label11.Text = "催化区";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(169, 83);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(22, 15);
            this.label12.TabIndex = 15;
            this.label12.Text = "℃";
            // 
            // textTStep2O
            // 
            this.textTStep2O.Location = new System.Drawing.Point(118, 79);
            this.textTStep2O.Name = "textTStep2O";
            this.textTStep2O.Size = new System.Drawing.Size(45, 25);
            this.textTStep2O.TabIndex = 14;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(60, 83);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(52, 15);
            this.label13.TabIndex = 13;
            this.label13.Text = "氧化区";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(18, 83);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(47, 15);
            this.label14.TabIndex = 12;
            this.label14.Text = "Step2";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(605, 41);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 15);
            this.label7.TabIndex = 11;
            this.label7.Text = "min";
            // 
            // textTStep1T
            // 
            this.textTStep1T.Location = new System.Drawing.Point(554, 37);
            this.textTStep1T.Name = "textTStep1T";
            this.textTStep1T.Size = new System.Drawing.Size(45, 25);
            this.textTStep1T.TabIndex = 10;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(506, 39);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 15);
            this.label8.TabIndex = 9;
            this.label8.Text = "时间";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(393, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(22, 15);
            this.label4.TabIndex = 7;
            this.label4.Text = "℃";
            // 
            // textTStep1C
            // 
            this.textTStep1C.Location = new System.Drawing.Point(342, 38);
            this.textTStep1C.Name = "textTStep1C";
            this.textTStep1C.Size = new System.Drawing.Size(45, 25);
            this.textTStep1C.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(282, 40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 15);
            this.label5.TabIndex = 5;
            this.label5.Text = "催化区";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(169, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(22, 15);
            this.label3.TabIndex = 3;
            this.label3.Text = "℃";
            // 
            // textTStep1O
            // 
            this.textTStep1O.Location = new System.Drawing.Point(118, 36);
            this.textTStep1O.Name = "textTStep1O";
            this.textTStep1O.Size = new System.Drawing.Size(45, 25);
            this.textTStep1O.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(60, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "氧化区";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Step1";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label31);
            this.panel2.Controls.Add(this.labTCO);
            this.panel2.Location = new System.Drawing.Point(12, 39);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(677, 68);
            this.panel2.TabIndex = 1;
            // 
            // label31
            // 
            this.label31.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label31.Location = new System.Drawing.Point(7, 38);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(660, 25);
            this.label31.TabIndex = 3;
            this.label31.Text = "模式界面";
            this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labTCO
            // 
            this.labTCO.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labTCO.Location = new System.Drawing.Point(7, 6);
            this.labTCO.Name = "labTCO";
            this.labTCO.Size = new System.Drawing.Size(660, 29);
            this.labTCO.TabIndex = 2;
            this.labTCO.Text = "有机氚碳氧化系统";
            this.labTCO.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.butSave);
            this.panel3.Location = new System.Drawing.Point(12, 367);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(677, 30);
            this.panel3.TabIndex = 2;
            // 
            // butSave
            // 
            this.butSave.Location = new System.Drawing.Point(294, 3);
            this.butSave.Name = "butSave";
            this.butSave.Size = new System.Drawing.Size(100, 23);
            this.butSave.TabIndex = 0;
            this.butSave.Text = "保  存";
            this.butSave.UseVisualStyleBackColor = true;
            this.butSave.Click += new System.EventHandler(this.butSave_Click);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label57);
            this.panel4.Controls.Add(this.textVStep5C);
            this.panel4.Controls.Add(this.label58);
            this.panel4.Controls.Add(this.label61);
            this.panel4.Controls.Add(this.textVStep5O);
            this.panel4.Controls.Add(this.label62);
            this.panel4.Controls.Add(this.label63);
            this.panel4.Controls.Add(this.label29);
            this.panel4.Controls.Add(this.textVStep9C);
            this.panel4.Controls.Add(this.label30);
            this.panel4.Controls.Add(this.label33);
            this.panel4.Controls.Add(this.textVStep9O);
            this.panel4.Controls.Add(this.label34);
            this.panel4.Controls.Add(this.label35);
            this.panel4.Controls.Add(this.label36);
            this.panel4.Controls.Add(this.textVStep8C);
            this.panel4.Controls.Add(this.label37);
            this.panel4.Controls.Add(this.label40);
            this.panel4.Controls.Add(this.textVStep8O);
            this.panel4.Controls.Add(this.label41);
            this.panel4.Controls.Add(this.label42);
            this.panel4.Controls.Add(this.label43);
            this.panel4.Controls.Add(this.textVStep7C);
            this.panel4.Controls.Add(this.label44);
            this.panel4.Controls.Add(this.label47);
            this.panel4.Controls.Add(this.textVStep7O);
            this.panel4.Controls.Add(this.label48);
            this.panel4.Controls.Add(this.label49);
            this.panel4.Controls.Add(this.label50);
            this.panel4.Controls.Add(this.textVStep6C);
            this.panel4.Controls.Add(this.label51);
            this.panel4.Controls.Add(this.label54);
            this.panel4.Controls.Add(this.textVStep6O);
            this.panel4.Controls.Add(this.label55);
            this.panel4.Controls.Add(this.label56);
            this.panel4.Location = new System.Drawing.Point(6, 6);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(657, 200);
            this.panel4.TabIndex = 42;
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.Location = new System.Drawing.Point(519, 7);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(15, 15);
            this.label57.TabIndex = 51;
            this.label57.Text = "V";
            // 
            // textVStep5C
            // 
            this.textVStep5C.Location = new System.Drawing.Point(468, 3);
            this.textVStep5C.Name = "textVStep5C";
            this.textVStep5C.Size = new System.Drawing.Size(45, 25);
            this.textVStep5C.TabIndex = 50;
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.Location = new System.Drawing.Point(406, 8);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(52, 15);
            this.label58.TabIndex = 49;
            this.label58.Text = "催化区";
            // 
            // label61
            // 
            this.label61.AutoSize = true;
            this.label61.Location = new System.Drawing.Point(250, 6);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(15, 15);
            this.label61.TabIndex = 45;
            this.label61.Text = "V";
            // 
            // textVStep5O
            // 
            this.textVStep5O.Location = new System.Drawing.Point(199, 2);
            this.textVStep5O.Name = "textVStep5O";
            this.textVStep5O.Size = new System.Drawing.Size(45, 25);
            this.textVStep5O.TabIndex = 44;
            // 
            // label62
            // 
            this.label62.AutoSize = true;
            this.label62.Location = new System.Drawing.Point(141, 6);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(52, 15);
            this.label62.TabIndex = 43;
            this.label62.Text = "氧化区";
            // 
            // label63
            // 
            this.label63.AutoSize = true;
            this.label63.Location = new System.Drawing.Point(99, 6);
            this.label63.Name = "label63";
            this.label63.Size = new System.Drawing.Size(47, 15);
            this.label63.TabIndex = 42;
            this.label63.Text = "Step5";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(519, 174);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(15, 15);
            this.label29.TabIndex = 41;
            this.label29.Text = "V";
            // 
            // textVStep9C
            // 
            this.textVStep9C.Location = new System.Drawing.Point(468, 170);
            this.textVStep9C.Name = "textVStep9C";
            this.textVStep9C.Size = new System.Drawing.Size(45, 25);
            this.textVStep9C.TabIndex = 40;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(406, 176);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(52, 15);
            this.label30.TabIndex = 39;
            this.label30.Text = "催化区";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(250, 173);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(15, 15);
            this.label33.TabIndex = 35;
            this.label33.Text = "V";
            // 
            // textVStep9O
            // 
            this.textVStep9O.Location = new System.Drawing.Point(199, 169);
            this.textVStep9O.Name = "textVStep9O";
            this.textVStep9O.Size = new System.Drawing.Size(45, 25);
            this.textVStep9O.TabIndex = 34;
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(141, 173);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(52, 15);
            this.label34.TabIndex = 33;
            this.label34.Text = "氧化区";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(99, 173);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(47, 15);
            this.label35.TabIndex = 32;
            this.label35.Text = "Step9";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(519, 128);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(15, 15);
            this.label36.TabIndex = 31;
            this.label36.Text = "V";
            // 
            // textVStep8C
            // 
            this.textVStep8C.Location = new System.Drawing.Point(468, 124);
            this.textVStep8C.Name = "textVStep8C";
            this.textVStep8C.Size = new System.Drawing.Size(45, 25);
            this.textVStep8C.TabIndex = 30;
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(406, 131);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(52, 15);
            this.label37.TabIndex = 29;
            this.label37.Text = "催化区";
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(250, 127);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(15, 15);
            this.label40.TabIndex = 25;
            this.label40.Text = "V";
            // 
            // textVStep8O
            // 
            this.textVStep8O.Location = new System.Drawing.Point(199, 123);
            this.textVStep8O.Name = "textVStep8O";
            this.textVStep8O.Size = new System.Drawing.Size(45, 25);
            this.textVStep8O.TabIndex = 24;
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(141, 127);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(52, 15);
            this.label41.TabIndex = 23;
            this.label41.Text = "氧化区";
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(99, 127);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(47, 15);
            this.label42.TabIndex = 22;
            this.label42.Text = "Step8";
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Location = new System.Drawing.Point(519, 87);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(15, 15);
            this.label43.TabIndex = 21;
            this.label43.Text = "V";
            // 
            // textVStep7C
            // 
            this.textVStep7C.Location = new System.Drawing.Point(468, 83);
            this.textVStep7C.Name = "textVStep7C";
            this.textVStep7C.Size = new System.Drawing.Size(45, 25);
            this.textVStep7C.TabIndex = 20;
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Location = new System.Drawing.Point(406, 90);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(52, 15);
            this.label44.TabIndex = 19;
            this.label44.Text = "催化区";
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(250, 86);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(15, 15);
            this.label47.TabIndex = 15;
            this.label47.Text = "V";
            // 
            // textVStep7O
            // 
            this.textVStep7O.Location = new System.Drawing.Point(199, 82);
            this.textVStep7O.Name = "textVStep7O";
            this.textVStep7O.Size = new System.Drawing.Size(45, 25);
            this.textVStep7O.TabIndex = 14;
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Location = new System.Drawing.Point(141, 86);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(52, 15);
            this.label48.TabIndex = 13;
            this.label48.Text = "氧化区";
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Location = new System.Drawing.Point(99, 86);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(47, 15);
            this.label49.TabIndex = 12;
            this.label49.Text = "Step7";
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Location = new System.Drawing.Point(519, 44);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(15, 15);
            this.label50.TabIndex = 11;
            this.label50.Text = "V";
            // 
            // textVStep6C
            // 
            this.textVStep6C.Location = new System.Drawing.Point(468, 40);
            this.textVStep6C.Name = "textVStep6C";
            this.textVStep6C.Size = new System.Drawing.Size(45, 25);
            this.textVStep6C.TabIndex = 10;
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.Location = new System.Drawing.Point(406, 45);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(52, 15);
            this.label51.TabIndex = 9;
            this.label51.Text = "催化区";
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Location = new System.Drawing.Point(250, 43);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(15, 15);
            this.label54.TabIndex = 3;
            this.label54.Text = "V";
            // 
            // textVStep6O
            // 
            this.textVStep6O.Location = new System.Drawing.Point(199, 39);
            this.textVStep6O.Name = "textVStep6O";
            this.textVStep6O.Size = new System.Drawing.Size(45, 25);
            this.textVStep6O.TabIndex = 2;
            // 
            // label55
            // 
            this.label55.AutoSize = true;
            this.label55.Location = new System.Drawing.Point(141, 43);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(52, 15);
            this.label55.TabIndex = 1;
            this.label55.Text = "氧化区";
            // 
            // label56
            // 
            this.label56.AutoSize = true;
            this.label56.Location = new System.Drawing.Point(99, 43);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(47, 15);
            this.label56.TabIndex = 0;
            this.label56.Text = "Step6";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.label64);
            this.panel5.Controls.Add(this.textVStep4C);
            this.panel5.Controls.Add(this.label65);
            this.panel5.Controls.Add(this.label68);
            this.panel5.Controls.Add(this.textVStep4O);
            this.panel5.Controls.Add(this.label69);
            this.panel5.Controls.Add(this.label70);
            this.panel5.Controls.Add(this.label71);
            this.panel5.Controls.Add(this.textVStep3C);
            this.panel5.Controls.Add(this.label72);
            this.panel5.Controls.Add(this.label75);
            this.panel5.Controls.Add(this.textVStep3O);
            this.panel5.Controls.Add(this.label76);
            this.panel5.Controls.Add(this.label77);
            this.panel5.Controls.Add(this.label78);
            this.panel5.Controls.Add(this.textVStep2C);
            this.panel5.Controls.Add(this.label79);
            this.panel5.Controls.Add(this.label82);
            this.panel5.Controls.Add(this.textVStep2O);
            this.panel5.Controls.Add(this.label83);
            this.panel5.Controls.Add(this.label84);
            this.panel5.Controls.Add(this.label85);
            this.panel5.Controls.Add(this.textVStep1C);
            this.panel5.Controls.Add(this.label86);
            this.panel5.Controls.Add(this.label89);
            this.panel5.Controls.Add(this.textVStep1O);
            this.panel5.Controls.Add(this.label90);
            this.panel5.Controls.Add(this.label91);
            this.panel5.Location = new System.Drawing.Point(6, 16);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(642, 221);
            this.panel5.TabIndex = 42;
            // 
            // label64
            // 
            this.label64.AutoSize = true;
            this.label64.Location = new System.Drawing.Point(548, 144);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(15, 15);
            this.label64.TabIndex = 41;
            this.label64.Text = "V";
            // 
            // textVStep4C
            // 
            this.textVStep4C.Location = new System.Drawing.Point(497, 140);
            this.textVStep4C.Name = "textVStep4C";
            this.textVStep4C.Size = new System.Drawing.Size(45, 25);
            this.textVStep4C.TabIndex = 40;
            // 
            // label65
            // 
            this.label65.AutoSize = true;
            this.label65.Location = new System.Drawing.Point(436, 144);
            this.label65.Name = "label65";
            this.label65.Size = new System.Drawing.Size(52, 15);
            this.label65.TabIndex = 39;
            this.label65.Text = "催化区";
            // 
            // label68
            // 
            this.label68.AutoSize = true;
            this.label68.Location = new System.Drawing.Point(248, 142);
            this.label68.Name = "label68";
            this.label68.Size = new System.Drawing.Size(15, 15);
            this.label68.TabIndex = 35;
            this.label68.Text = "v";
            // 
            // textVStep4O
            // 
            this.textVStep4O.Location = new System.Drawing.Point(197, 138);
            this.textVStep4O.Name = "textVStep4O";
            this.textVStep4O.Size = new System.Drawing.Size(45, 25);
            this.textVStep4O.TabIndex = 34;
            // 
            // label69
            // 
            this.label69.AutoSize = true;
            this.label69.Location = new System.Drawing.Point(139, 142);
            this.label69.Name = "label69";
            this.label69.Size = new System.Drawing.Size(52, 15);
            this.label69.TabIndex = 33;
            this.label69.Text = "氧化区";
            // 
            // label70
            // 
            this.label70.AutoSize = true;
            this.label70.Location = new System.Drawing.Point(97, 142);
            this.label70.Name = "label70";
            this.label70.Size = new System.Drawing.Size(47, 15);
            this.label70.TabIndex = 32;
            this.label70.Text = "Step4";
            // 
            // label71
            // 
            this.label71.AutoSize = true;
            this.label71.Location = new System.Drawing.Point(548, 98);
            this.label71.Name = "label71";
            this.label71.Size = new System.Drawing.Size(15, 15);
            this.label71.TabIndex = 31;
            this.label71.Text = "V";
            // 
            // textVStep3C
            // 
            this.textVStep3C.Location = new System.Drawing.Point(497, 94);
            this.textVStep3C.Name = "textVStep3C";
            this.textVStep3C.Size = new System.Drawing.Size(45, 25);
            this.textVStep3C.TabIndex = 30;
            // 
            // label72
            // 
            this.label72.AutoSize = true;
            this.label72.Location = new System.Drawing.Point(436, 99);
            this.label72.Name = "label72";
            this.label72.Size = new System.Drawing.Size(52, 15);
            this.label72.TabIndex = 29;
            this.label72.Text = "催化区";
            // 
            // label75
            // 
            this.label75.AutoSize = true;
            this.label75.Location = new System.Drawing.Point(248, 96);
            this.label75.Name = "label75";
            this.label75.Size = new System.Drawing.Size(15, 15);
            this.label75.TabIndex = 25;
            this.label75.Text = "V";
            // 
            // textVStep3O
            // 
            this.textVStep3O.Location = new System.Drawing.Point(197, 92);
            this.textVStep3O.Name = "textVStep3O";
            this.textVStep3O.Size = new System.Drawing.Size(45, 25);
            this.textVStep3O.TabIndex = 24;
            // 
            // label76
            // 
            this.label76.AutoSize = true;
            this.label76.Location = new System.Drawing.Point(139, 96);
            this.label76.Name = "label76";
            this.label76.Size = new System.Drawing.Size(52, 15);
            this.label76.TabIndex = 23;
            this.label76.Text = "氧化区";
            // 
            // label77
            // 
            this.label77.AutoSize = true;
            this.label77.Location = new System.Drawing.Point(97, 96);
            this.label77.Name = "label77";
            this.label77.Size = new System.Drawing.Size(47, 15);
            this.label77.TabIndex = 22;
            this.label77.Text = "Step3";
            // 
            // label78
            // 
            this.label78.AutoSize = true;
            this.label78.Location = new System.Drawing.Point(548, 57);
            this.label78.Name = "label78";
            this.label78.Size = new System.Drawing.Size(15, 15);
            this.label78.TabIndex = 21;
            this.label78.Text = "V";
            // 
            // textVStep2C
            // 
            this.textVStep2C.Location = new System.Drawing.Point(497, 53);
            this.textVStep2C.Name = "textVStep2C";
            this.textVStep2C.Size = new System.Drawing.Size(45, 25);
            this.textVStep2C.TabIndex = 20;
            // 
            // label79
            // 
            this.label79.AutoSize = true;
            this.label79.Location = new System.Drawing.Point(436, 58);
            this.label79.Name = "label79";
            this.label79.Size = new System.Drawing.Size(52, 15);
            this.label79.TabIndex = 19;
            this.label79.Text = "催化区";
            // 
            // label82
            // 
            this.label82.AutoSize = true;
            this.label82.Location = new System.Drawing.Point(248, 55);
            this.label82.Name = "label82";
            this.label82.Size = new System.Drawing.Size(15, 15);
            this.label82.TabIndex = 15;
            this.label82.Text = "V";
            // 
            // textVStep2O
            // 
            this.textVStep2O.Location = new System.Drawing.Point(197, 51);
            this.textVStep2O.Name = "textVStep2O";
            this.textVStep2O.Size = new System.Drawing.Size(45, 25);
            this.textVStep2O.TabIndex = 14;
            // 
            // label83
            // 
            this.label83.AutoSize = true;
            this.label83.Location = new System.Drawing.Point(139, 55);
            this.label83.Name = "label83";
            this.label83.Size = new System.Drawing.Size(52, 15);
            this.label83.TabIndex = 13;
            this.label83.Text = "氧化区";
            // 
            // label84
            // 
            this.label84.AutoSize = true;
            this.label84.Location = new System.Drawing.Point(97, 55);
            this.label84.Name = "label84";
            this.label84.Size = new System.Drawing.Size(47, 15);
            this.label84.TabIndex = 12;
            this.label84.Text = "Step2";
            // 
            // label85
            // 
            this.label85.AutoSize = true;
            this.label85.Location = new System.Drawing.Point(548, 14);
            this.label85.Name = "label85";
            this.label85.Size = new System.Drawing.Size(15, 15);
            this.label85.TabIndex = 11;
            this.label85.Text = "V";
            // 
            // textVStep1C
            // 
            this.textVStep1C.Location = new System.Drawing.Point(497, 10);
            this.textVStep1C.Name = "textVStep1C";
            this.textVStep1C.Size = new System.Drawing.Size(45, 25);
            this.textVStep1C.TabIndex = 10;
            // 
            // label86
            // 
            this.label86.AutoSize = true;
            this.label86.Location = new System.Drawing.Point(436, 13);
            this.label86.Name = "label86";
            this.label86.Size = new System.Drawing.Size(52, 15);
            this.label86.TabIndex = 9;
            this.label86.Text = "催化区";
            // 
            // label89
            // 
            this.label89.AutoSize = true;
            this.label89.Location = new System.Drawing.Point(248, 12);
            this.label89.Name = "label89";
            this.label89.Size = new System.Drawing.Size(15, 15);
            this.label89.TabIndex = 3;
            this.label89.Text = "V";
            // 
            // textVStep1O
            // 
            this.textVStep1O.Location = new System.Drawing.Point(197, 8);
            this.textVStep1O.Name = "textVStep1O";
            this.textVStep1O.Size = new System.Drawing.Size(45, 25);
            this.textVStep1O.TabIndex = 2;
            // 
            // label90
            // 
            this.label90.AutoSize = true;
            this.label90.Location = new System.Drawing.Point(139, 12);
            this.label90.Name = "label90";
            this.label90.Size = new System.Drawing.Size(52, 15);
            this.label90.TabIndex = 1;
            this.label90.Text = "氧化区";
            // 
            // label91
            // 
            this.label91.AutoSize = true;
            this.label91.Location = new System.Drawing.Point(97, 12);
            this.label91.Name = "label91";
            this.label91.Size = new System.Drawing.Size(47, 15);
            this.label91.TabIndex = 0;
            this.label91.Text = "Step1";
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.label66);
            this.panel6.Controls.Add(this.textTStep5T);
            this.panel6.Controls.Add(this.label67);
            this.panel6.Controls.Add(this.label73);
            this.panel6.Controls.Add(this.textTStep5C);
            this.panel6.Controls.Add(this.label74);
            this.panel6.Controls.Add(this.label80);
            this.panel6.Controls.Add(this.textTStep5O);
            this.panel6.Controls.Add(this.label81);
            this.panel6.Controls.Add(this.label87);
            this.panel6.Controls.Add(this.label88);
            this.panel6.Controls.Add(this.textTStep9T);
            this.panel6.Controls.Add(this.label92);
            this.panel6.Controls.Add(this.label93);
            this.panel6.Controls.Add(this.textTStep9C);
            this.panel6.Controls.Add(this.label94);
            this.panel6.Controls.Add(this.label95);
            this.panel6.Controls.Add(this.textTStep9O);
            this.panel6.Controls.Add(this.label96);
            this.panel6.Controls.Add(this.label97);
            this.panel6.Controls.Add(this.label98);
            this.panel6.Controls.Add(this.textTStep8T);
            this.panel6.Controls.Add(this.label99);
            this.panel6.Controls.Add(this.label100);
            this.panel6.Controls.Add(this.textTStep8C);
            this.panel6.Controls.Add(this.label101);
            this.panel6.Controls.Add(this.label102);
            this.panel6.Controls.Add(this.textTStep8O);
            this.panel6.Controls.Add(this.label103);
            this.panel6.Controls.Add(this.label104);
            this.panel6.Controls.Add(this.label105);
            this.panel6.Controls.Add(this.textTStep7T);
            this.panel6.Controls.Add(this.label106);
            this.panel6.Controls.Add(this.label107);
            this.panel6.Controls.Add(this.textTStep7C);
            this.panel6.Controls.Add(this.label108);
            this.panel6.Controls.Add(this.label109);
            this.panel6.Controls.Add(this.textTStep7O);
            this.panel6.Controls.Add(this.label110);
            this.panel6.Controls.Add(this.label111);
            this.panel6.Controls.Add(this.label112);
            this.panel6.Controls.Add(this.textTStep6T);
            this.panel6.Controls.Add(this.label113);
            this.panel6.Controls.Add(this.label114);
            this.panel6.Controls.Add(this.textTStep6C);
            this.panel6.Controls.Add(this.label115);
            this.panel6.Controls.Add(this.label116);
            this.panel6.Controls.Add(this.textTStep6O);
            this.panel6.Controls.Add(this.label117);
            this.panel6.Controls.Add(this.label118);
            this.panel6.Location = new System.Drawing.Point(6, 14);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(636, 200);
            this.panel6.TabIndex = 52;
            // 
            // label66
            // 
            this.label66.AutoSize = true;
            this.label66.Location = new System.Drawing.Point(607, 8);
            this.label66.Name = "label66";
            this.label66.Size = new System.Drawing.Size(31, 15);
            this.label66.TabIndex = 51;
            this.label66.Text = "min";
            // 
            // textTStep5T
            // 
            this.textTStep5T.Location = new System.Drawing.Point(556, 4);
            this.textTStep5T.Name = "textTStep5T";
            this.textTStep5T.Size = new System.Drawing.Size(45, 25);
            this.textTStep5T.TabIndex = 50;
            // 
            // label67
            // 
            this.label67.AutoSize = true;
            this.label67.Location = new System.Drawing.Point(508, 6);
            this.label67.Name = "label67";
            this.label67.Size = new System.Drawing.Size(37, 15);
            this.label67.TabIndex = 49;
            this.label67.Text = "时间";
            // 
            // label73
            // 
            this.label73.AutoSize = true;
            this.label73.Location = new System.Drawing.Point(397, 6);
            this.label73.Name = "label73";
            this.label73.Size = new System.Drawing.Size(22, 15);
            this.label73.TabIndex = 48;
            this.label73.Text = "℃";
            // 
            // textTStep5C
            // 
            this.textTStep5C.Location = new System.Drawing.Point(346, 3);
            this.textTStep5C.Name = "textTStep5C";
            this.textTStep5C.Size = new System.Drawing.Size(45, 25);
            this.textTStep5C.TabIndex = 47;
            // 
            // label74
            // 
            this.label74.AutoSize = true;
            this.label74.Location = new System.Drawing.Point(284, 7);
            this.label74.Name = "label74";
            this.label74.Size = new System.Drawing.Size(52, 15);
            this.label74.TabIndex = 46;
            this.label74.Text = "催化区";
            // 
            // label80
            // 
            this.label80.AutoSize = true;
            this.label80.Location = new System.Drawing.Point(171, 7);
            this.label80.Name = "label80";
            this.label80.Size = new System.Drawing.Size(22, 15);
            this.label80.TabIndex = 45;
            this.label80.Text = "℃";
            // 
            // textTStep5O
            // 
            this.textTStep5O.Location = new System.Drawing.Point(120, 3);
            this.textTStep5O.Name = "textTStep5O";
            this.textTStep5O.Size = new System.Drawing.Size(45, 25);
            this.textTStep5O.TabIndex = 44;
            // 
            // label81
            // 
            this.label81.AutoSize = true;
            this.label81.Location = new System.Drawing.Point(62, 7);
            this.label81.Name = "label81";
            this.label81.Size = new System.Drawing.Size(52, 15);
            this.label81.TabIndex = 43;
            this.label81.Text = "氧化区";
            // 
            // label87
            // 
            this.label87.AutoSize = true;
            this.label87.Location = new System.Drawing.Point(20, 8);
            this.label87.Name = "label87";
            this.label87.Size = new System.Drawing.Size(47, 15);
            this.label87.TabIndex = 42;
            this.label87.Text = "Step5";
            // 
            // label88
            // 
            this.label88.AutoSize = true;
            this.label88.Location = new System.Drawing.Point(607, 175);
            this.label88.Name = "label88";
            this.label88.Size = new System.Drawing.Size(31, 15);
            this.label88.TabIndex = 41;
            this.label88.Text = "min";
            // 
            // textTStep9T
            // 
            this.textTStep9T.Location = new System.Drawing.Point(556, 171);
            this.textTStep9T.Name = "textTStep9T";
            this.textTStep9T.Size = new System.Drawing.Size(45, 25);
            this.textTStep9T.TabIndex = 40;
            // 
            // label92
            // 
            this.label92.AutoSize = true;
            this.label92.Location = new System.Drawing.Point(508, 174);
            this.label92.Name = "label92";
            this.label92.Size = new System.Drawing.Size(37, 15);
            this.label92.TabIndex = 39;
            this.label92.Text = "时间";
            // 
            // label93
            // 
            this.label93.AutoSize = true;
            this.label93.Location = new System.Drawing.Point(397, 173);
            this.label93.Name = "label93";
            this.label93.Size = new System.Drawing.Size(22, 15);
            this.label93.TabIndex = 38;
            this.label93.Text = "℃";
            // 
            // textTStep9C
            // 
            this.textTStep9C.Location = new System.Drawing.Point(346, 170);
            this.textTStep9C.Name = "textTStep9C";
            this.textTStep9C.Size = new System.Drawing.Size(45, 25);
            this.textTStep9C.TabIndex = 37;
            // 
            // label94
            // 
            this.label94.AutoSize = true;
            this.label94.Location = new System.Drawing.Point(284, 174);
            this.label94.Name = "label94";
            this.label94.Size = new System.Drawing.Size(52, 15);
            this.label94.TabIndex = 36;
            this.label94.Text = "催化区";
            // 
            // label95
            // 
            this.label95.AutoSize = true;
            this.label95.Location = new System.Drawing.Point(171, 174);
            this.label95.Name = "label95";
            this.label95.Size = new System.Drawing.Size(22, 15);
            this.label95.TabIndex = 35;
            this.label95.Text = "℃";
            // 
            // textTStep9O
            // 
            this.textTStep9O.Location = new System.Drawing.Point(120, 170);
            this.textTStep9O.Name = "textTStep9O";
            this.textTStep9O.Size = new System.Drawing.Size(45, 25);
            this.textTStep9O.TabIndex = 34;
            // 
            // label96
            // 
            this.label96.AutoSize = true;
            this.label96.Location = new System.Drawing.Point(62, 174);
            this.label96.Name = "label96";
            this.label96.Size = new System.Drawing.Size(52, 15);
            this.label96.TabIndex = 33;
            this.label96.Text = "氧化区";
            // 
            // label97
            // 
            this.label97.AutoSize = true;
            this.label97.Location = new System.Drawing.Point(20, 174);
            this.label97.Name = "label97";
            this.label97.Size = new System.Drawing.Size(47, 15);
            this.label97.TabIndex = 32;
            this.label97.Text = "Step9";
            // 
            // label98
            // 
            this.label98.AutoSize = true;
            this.label98.Location = new System.Drawing.Point(607, 129);
            this.label98.Name = "label98";
            this.label98.Size = new System.Drawing.Size(31, 15);
            this.label98.TabIndex = 31;
            this.label98.Text = "min";
            // 
            // textTStep8T
            // 
            this.textTStep8T.Location = new System.Drawing.Point(556, 125);
            this.textTStep8T.Name = "textTStep8T";
            this.textTStep8T.Size = new System.Drawing.Size(45, 25);
            this.textTStep8T.TabIndex = 30;
            // 
            // label99
            // 
            this.label99.AutoSize = true;
            this.label99.Location = new System.Drawing.Point(508, 129);
            this.label99.Name = "label99";
            this.label99.Size = new System.Drawing.Size(37, 15);
            this.label99.TabIndex = 29;
            this.label99.Text = "时间";
            // 
            // label100
            // 
            this.label100.AutoSize = true;
            this.label100.Location = new System.Drawing.Point(397, 127);
            this.label100.Name = "label100";
            this.label100.Size = new System.Drawing.Size(22, 15);
            this.label100.TabIndex = 28;
            this.label100.Text = "℃";
            // 
            // textTStep8C
            // 
            this.textTStep8C.Location = new System.Drawing.Point(346, 124);
            this.textTStep8C.Name = "textTStep8C";
            this.textTStep8C.Size = new System.Drawing.Size(45, 25);
            this.textTStep8C.TabIndex = 27;
            // 
            // label101
            // 
            this.label101.AutoSize = true;
            this.label101.Location = new System.Drawing.Point(284, 128);
            this.label101.Name = "label101";
            this.label101.Size = new System.Drawing.Size(52, 15);
            this.label101.TabIndex = 26;
            this.label101.Text = "催化区";
            // 
            // label102
            // 
            this.label102.AutoSize = true;
            this.label102.Location = new System.Drawing.Point(171, 128);
            this.label102.Name = "label102";
            this.label102.Size = new System.Drawing.Size(22, 15);
            this.label102.TabIndex = 25;
            this.label102.Text = "℃";
            // 
            // textTStep8O
            // 
            this.textTStep8O.Location = new System.Drawing.Point(120, 124);
            this.textTStep8O.Name = "textTStep8O";
            this.textTStep8O.Size = new System.Drawing.Size(45, 25);
            this.textTStep8O.TabIndex = 24;
            // 
            // label103
            // 
            this.label103.AutoSize = true;
            this.label103.Location = new System.Drawing.Point(62, 128);
            this.label103.Name = "label103";
            this.label103.Size = new System.Drawing.Size(52, 15);
            this.label103.TabIndex = 23;
            this.label103.Text = "氧化区";
            // 
            // label104
            // 
            this.label104.AutoSize = true;
            this.label104.Location = new System.Drawing.Point(20, 128);
            this.label104.Name = "label104";
            this.label104.Size = new System.Drawing.Size(47, 15);
            this.label104.TabIndex = 22;
            this.label104.Text = "Step8";
            // 
            // label105
            // 
            this.label105.AutoSize = true;
            this.label105.Location = new System.Drawing.Point(607, 88);
            this.label105.Name = "label105";
            this.label105.Size = new System.Drawing.Size(31, 15);
            this.label105.TabIndex = 21;
            this.label105.Text = "min";
            // 
            // textTStep7T
            // 
            this.textTStep7T.Location = new System.Drawing.Point(556, 84);
            this.textTStep7T.Name = "textTStep7T";
            this.textTStep7T.Size = new System.Drawing.Size(45, 25);
            this.textTStep7T.TabIndex = 20;
            // 
            // label106
            // 
            this.label106.AutoSize = true;
            this.label106.Location = new System.Drawing.Point(508, 88);
            this.label106.Name = "label106";
            this.label106.Size = new System.Drawing.Size(37, 15);
            this.label106.TabIndex = 19;
            this.label106.Text = "时间";
            // 
            // label107
            // 
            this.label107.AutoSize = true;
            this.label107.Location = new System.Drawing.Point(397, 86);
            this.label107.Name = "label107";
            this.label107.Size = new System.Drawing.Size(22, 15);
            this.label107.TabIndex = 18;
            this.label107.Text = "℃";
            // 
            // textTStep7C
            // 
            this.textTStep7C.Location = new System.Drawing.Point(346, 83);
            this.textTStep7C.Name = "textTStep7C";
            this.textTStep7C.Size = new System.Drawing.Size(45, 25);
            this.textTStep7C.TabIndex = 17;
            // 
            // label108
            // 
            this.label108.AutoSize = true;
            this.label108.Location = new System.Drawing.Point(284, 87);
            this.label108.Name = "label108";
            this.label108.Size = new System.Drawing.Size(52, 15);
            this.label108.TabIndex = 16;
            this.label108.Text = "催化区";
            // 
            // label109
            // 
            this.label109.AutoSize = true;
            this.label109.Location = new System.Drawing.Point(171, 87);
            this.label109.Name = "label109";
            this.label109.Size = new System.Drawing.Size(22, 15);
            this.label109.TabIndex = 15;
            this.label109.Text = "℃";
            // 
            // textTStep7O
            // 
            this.textTStep7O.Location = new System.Drawing.Point(120, 83);
            this.textTStep7O.Name = "textTStep7O";
            this.textTStep7O.Size = new System.Drawing.Size(45, 25);
            this.textTStep7O.TabIndex = 14;
            // 
            // label110
            // 
            this.label110.AutoSize = true;
            this.label110.Location = new System.Drawing.Point(62, 87);
            this.label110.Name = "label110";
            this.label110.Size = new System.Drawing.Size(52, 15);
            this.label110.TabIndex = 13;
            this.label110.Text = "氧化区";
            // 
            // label111
            // 
            this.label111.AutoSize = true;
            this.label111.Location = new System.Drawing.Point(20, 87);
            this.label111.Name = "label111";
            this.label111.Size = new System.Drawing.Size(47, 15);
            this.label111.TabIndex = 12;
            this.label111.Text = "Step7";
            // 
            // label112
            // 
            this.label112.AutoSize = true;
            this.label112.Location = new System.Drawing.Point(607, 45);
            this.label112.Name = "label112";
            this.label112.Size = new System.Drawing.Size(31, 15);
            this.label112.TabIndex = 11;
            this.label112.Text = "min";
            // 
            // textTStep6T
            // 
            this.textTStep6T.Location = new System.Drawing.Point(556, 41);
            this.textTStep6T.Name = "textTStep6T";
            this.textTStep6T.Size = new System.Drawing.Size(45, 25);
            this.textTStep6T.TabIndex = 10;
            // 
            // label113
            // 
            this.label113.AutoSize = true;
            this.label113.Location = new System.Drawing.Point(508, 43);
            this.label113.Name = "label113";
            this.label113.Size = new System.Drawing.Size(37, 15);
            this.label113.TabIndex = 9;
            this.label113.Text = "时间";
            // 
            // label114
            // 
            this.label114.AutoSize = true;
            this.label114.Location = new System.Drawing.Point(397, 43);
            this.label114.Name = "label114";
            this.label114.Size = new System.Drawing.Size(22, 15);
            this.label114.TabIndex = 7;
            this.label114.Text = "℃";
            // 
            // textTStep6C
            // 
            this.textTStep6C.Location = new System.Drawing.Point(346, 40);
            this.textTStep6C.Name = "textTStep6C";
            this.textTStep6C.Size = new System.Drawing.Size(45, 25);
            this.textTStep6C.TabIndex = 6;
            // 
            // label115
            // 
            this.label115.AutoSize = true;
            this.label115.Location = new System.Drawing.Point(284, 44);
            this.label115.Name = "label115";
            this.label115.Size = new System.Drawing.Size(52, 15);
            this.label115.TabIndex = 5;
            this.label115.Text = "催化区";
            // 
            // label116
            // 
            this.label116.AutoSize = true;
            this.label116.Location = new System.Drawing.Point(171, 44);
            this.label116.Name = "label116";
            this.label116.Size = new System.Drawing.Size(22, 15);
            this.label116.TabIndex = 3;
            this.label116.Text = "℃";
            // 
            // textTStep6O
            // 
            this.textTStep6O.Location = new System.Drawing.Point(120, 40);
            this.textTStep6O.Name = "textTStep6O";
            this.textTStep6O.Size = new System.Drawing.Size(45, 25);
            this.textTStep6O.TabIndex = 2;
            // 
            // label117
            // 
            this.label117.AutoSize = true;
            this.label117.Location = new System.Drawing.Point(62, 44);
            this.label117.Name = "label117";
            this.label117.Size = new System.Drawing.Size(52, 15);
            this.label117.TabIndex = 1;
            this.label117.Text = "氧化区";
            // 
            // label118
            // 
            this.label118.AutoSize = true;
            this.label118.Location = new System.Drawing.Point(20, 44);
            this.label118.Name = "label118";
            this.label118.Size = new System.Drawing.Size(47, 15);
            this.label118.TabIndex = 0;
            this.label118.Text = "Step6";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(12, 113);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(677, 248);
            this.tabControl1.TabIndex = 53;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(669, 219);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "温度设置Step1-4";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panel6);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(669, 219);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "温度设置Step5-9";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.panel5);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(669, 219);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "电压设置Step1-4";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.panel4);
            this.tabPage4.Location = new System.Drawing.Point(4, 25);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(669, 219);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "电压设置5-9";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // FormParaSet
            // 
            this.ClientSize = new System.Drawing.Size(717, 456);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FormParaSet";
            this.Text = "自定义模式界面";
            this.Load += new System.EventHandler(this.FormParaSet_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private Label label7;
        private TextBox textTStep1T;
        private Label label8;
        private Label label4;
        private TextBox textTStep1C;
        private Label label5;
        private Label label3;
        private TextBox textTStep1O;
        private Label label2;
        private Label label1;
        private Panel panel4;
        private Label label57;
        private TextBox textVStep5C;
        private Label label58;
        private Label label61;
        private TextBox textVStep5O;
        private Label label62;
        private Label label63;
        private Label label29;
        private TextBox textVStep9C;
        private Label label30;
        private Label label33;
        private TextBox textVStep9O;
        private Label label34;
        private Label label35;
        private Label label36;
        private TextBox textVStep8C;
        private Label label37;
        private Label label40;
        private TextBox textVStep8O;
        private Label label41;
        private Label label42;
        private Label label43;
        private TextBox textVStep7C;
        private Label label44;
        private Label label47;
        private TextBox textVStep7O;
        private Label label48;
        private Label label49;
        private Label label50;
        private TextBox textVStep6C;
        private Label label51;
        private Label label54;
        private TextBox textVStep6O;
        private Label label55;
        private Label label56;
        private Label label22;
        private TextBox textTStep4T;
        private Label label23;
        private Label label24;
        private TextBox textTStep4C;
        private Label label25;
        private Label label26;
        private TextBox textTStep4O;
        private Label label27;
        private Label label28;
        private Label label15;
        private TextBox textTStep3T;
        private Label label16;
        private Label label17;
        private TextBox textTStep3C;
        private Label label18;
        private Label label19;
        private TextBox textTStep3O;
        private Label label20;
        private Label label21;
        private Label label6;
        private TextBox textTStep2T;
        private Label label9;
        private Label label10;
        private TextBox textTStep2C;
        private Label label11;
        private Label label12;
        private TextBox textTStep2O;
        private Label label13;
        private Label label14;
        private Panel panel5;
        private Label label64;
        private TextBox textVStep4C;
        private Label label65;
        private Label label68;
        private TextBox textVStep4O;
        private Label label69;
        private Label label70;
        private Label label71;
        private TextBox textVStep3C;
        private Label label72;
        private Label label75;
        private TextBox textVStep3O;
        private Label label76;
        private Label label77;
        private Label label78;
        private TextBox textVStep2C;
        private Label label79;
        private Label label82;
        private TextBox textVStep2O;
        private Label label83;
        private Label label84;
        private Label label85;
        private TextBox textVStep1C;
        private Label label86;
        private Label label89;
        private TextBox textVStep1O;
        private Label label90;
        private Label label91;
        private Panel panel6;
        private Label label66;
        private TextBox textTStep5T;
        private Label label67;
        private Label label73;
        private TextBox textTStep5C;
        private Label label74;
        private Label label80;
        private TextBox textTStep5O;
        private Label label81;
        private Label label87;
        private Label label88;
        private TextBox textTStep9T;
        private Label label92;
        private Label label93;
        private TextBox textTStep9C;
        private Label label94;
        private Label label95;
        private TextBox textTStep9O;
        private Label label96;
        private Label label97;
        private Label label98;
        private TextBox textTStep8T;
        private Label label99;
        private Label label100;
        private TextBox textTStep8C;
        private Label label101;
        private Label label102;
        private TextBox textTStep8O;
        private Label label103;
        private Label label104;
        private Label label105;
        private TextBox textTStep7T;
        private Label label106;
        private Label label107;
        private TextBox textTStep7C;
        private Label label108;
        private Label label109;
        private TextBox textTStep7O;
        private Label label110;
        private Label label111;
        private Label label112;
        private TextBox textTStep6T;
        private Label label113;
        private Label label114;
        private TextBox textTStep6C;
        private Label label115;
        private Label label116;
        private TextBox textTStep6O;
        private Label label117;
        private Label label118;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private TabPage tabPage4;
        private Label labTCO;
        private Label label31;
        private Button butSave;
        private TextBox textFlow;
        private Label label38;
    }
}
