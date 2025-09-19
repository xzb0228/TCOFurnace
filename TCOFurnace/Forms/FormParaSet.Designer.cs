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
            this.label22 = new System.Windows.Forms.Label();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.textBox11 = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.textBox12 = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label57 = new System.Windows.Forms.Label();
            this.textBox25 = new System.Windows.Forms.TextBox();
            this.label58 = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.textBox26 = new System.Windows.Forms.TextBox();
            this.label60 = new System.Windows.Forms.Label();
            this.label61 = new System.Windows.Forms.Label();
            this.textBox27 = new System.Windows.Forms.TextBox();
            this.label62 = new System.Windows.Forms.Label();
            this.label63 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.textBox13 = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.textBox14 = new System.Windows.Forms.TextBox();
            this.label32 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.textBox15 = new System.Windows.Forms.TextBox();
            this.label34 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.textBox16 = new System.Windows.Forms.TextBox();
            this.label37 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.textBox17 = new System.Windows.Forms.TextBox();
            this.label39 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.textBox18 = new System.Windows.Forms.TextBox();
            this.label41 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.textBox19 = new System.Windows.Forms.TextBox();
            this.label44 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.textBox20 = new System.Windows.Forms.TextBox();
            this.label46 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.textBox21 = new System.Windows.Forms.TextBox();
            this.label48 = new System.Windows.Forms.Label();
            this.label49 = new System.Windows.Forms.Label();
            this.label50 = new System.Windows.Forms.Label();
            this.textBox22 = new System.Windows.Forms.TextBox();
            this.label51 = new System.Windows.Forms.Label();
            this.label52 = new System.Windows.Forms.Label();
            this.textBox23 = new System.Windows.Forms.TextBox();
            this.label53 = new System.Windows.Forms.Label();
            this.label54 = new System.Windows.Forms.Label();
            this.textBox24 = new System.Windows.Forms.TextBox();
            this.label55 = new System.Windows.Forms.Label();
            this.label56 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label64 = new System.Windows.Forms.Label();
            this.textBox28 = new System.Windows.Forms.TextBox();
            this.label65 = new System.Windows.Forms.Label();
            this.label68 = new System.Windows.Forms.Label();
            this.textBox30 = new System.Windows.Forms.TextBox();
            this.label69 = new System.Windows.Forms.Label();
            this.label70 = new System.Windows.Forms.Label();
            this.label71 = new System.Windows.Forms.Label();
            this.textBox31 = new System.Windows.Forms.TextBox();
            this.label72 = new System.Windows.Forms.Label();
            this.label75 = new System.Windows.Forms.Label();
            this.textBox33 = new System.Windows.Forms.TextBox();
            this.label76 = new System.Windows.Forms.Label();
            this.label77 = new System.Windows.Forms.Label();
            this.label78 = new System.Windows.Forms.Label();
            this.textBox34 = new System.Windows.Forms.TextBox();
            this.label79 = new System.Windows.Forms.Label();
            this.label82 = new System.Windows.Forms.Label();
            this.textBox36 = new System.Windows.Forms.TextBox();
            this.label83 = new System.Windows.Forms.Label();
            this.label84 = new System.Windows.Forms.Label();
            this.label85 = new System.Windows.Forms.Label();
            this.textBox37 = new System.Windows.Forms.TextBox();
            this.label86 = new System.Windows.Forms.Label();
            this.label89 = new System.Windows.Forms.Label();
            this.textBox39 = new System.Windows.Forms.TextBox();
            this.label90 = new System.Windows.Forms.Label();
            this.label91 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label66 = new System.Windows.Forms.Label();
            this.textBox29 = new System.Windows.Forms.TextBox();
            this.label67 = new System.Windows.Forms.Label();
            this.label73 = new System.Windows.Forms.Label();
            this.textBox32 = new System.Windows.Forms.TextBox();
            this.label74 = new System.Windows.Forms.Label();
            this.label80 = new System.Windows.Forms.Label();
            this.textBox35 = new System.Windows.Forms.TextBox();
            this.label81 = new System.Windows.Forms.Label();
            this.label87 = new System.Windows.Forms.Label();
            this.label88 = new System.Windows.Forms.Label();
            this.textBox38 = new System.Windows.Forms.TextBox();
            this.label92 = new System.Windows.Forms.Label();
            this.label93 = new System.Windows.Forms.Label();
            this.textBox40 = new System.Windows.Forms.TextBox();
            this.label94 = new System.Windows.Forms.Label();
            this.label95 = new System.Windows.Forms.Label();
            this.textBox41 = new System.Windows.Forms.TextBox();
            this.label96 = new System.Windows.Forms.Label();
            this.label97 = new System.Windows.Forms.Label();
            this.label98 = new System.Windows.Forms.Label();
            this.textBox42 = new System.Windows.Forms.TextBox();
            this.label99 = new System.Windows.Forms.Label();
            this.label100 = new System.Windows.Forms.Label();
            this.textBox43 = new System.Windows.Forms.TextBox();
            this.label101 = new System.Windows.Forms.Label();
            this.label102 = new System.Windows.Forms.Label();
            this.textBox44 = new System.Windows.Forms.TextBox();
            this.label103 = new System.Windows.Forms.Label();
            this.label104 = new System.Windows.Forms.Label();
            this.label105 = new System.Windows.Forms.Label();
            this.textBox45 = new System.Windows.Forms.TextBox();
            this.label106 = new System.Windows.Forms.Label();
            this.label107 = new System.Windows.Forms.Label();
            this.textBox46 = new System.Windows.Forms.TextBox();
            this.label108 = new System.Windows.Forms.Label();
            this.label109 = new System.Windows.Forms.Label();
            this.textBox47 = new System.Windows.Forms.TextBox();
            this.label110 = new System.Windows.Forms.Label();
            this.label111 = new System.Windows.Forms.Label();
            this.label112 = new System.Windows.Forms.Label();
            this.textBox48 = new System.Windows.Forms.TextBox();
            this.label113 = new System.Windows.Forms.Label();
            this.label114 = new System.Windows.Forms.Label();
            this.textBox49 = new System.Windows.Forms.TextBox();
            this.label115 = new System.Windows.Forms.Label();
            this.label116 = new System.Windows.Forms.Label();
            this.textBox50 = new System.Windows.Forms.TextBox();
            this.label117 = new System.Windows.Forms.Label();
            this.label118 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.panel1.SuspendLayout();
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
            this.panel1.Controls.Add(this.label22);
            this.panel1.Controls.Add(this.textBox10);
            this.panel1.Controls.Add(this.label23);
            this.panel1.Controls.Add(this.label24);
            this.panel1.Controls.Add(this.textBox11);
            this.panel1.Controls.Add(this.label25);
            this.panel1.Controls.Add(this.label26);
            this.panel1.Controls.Add(this.textBox12);
            this.panel1.Controls.Add(this.label27);
            this.panel1.Controls.Add(this.label28);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.textBox7);
            this.panel1.Controls.Add(this.label16);
            this.panel1.Controls.Add(this.label17);
            this.panel1.Controls.Add(this.textBox8);
            this.panel1.Controls.Add(this.label18);
            this.panel1.Controls.Add(this.label19);
            this.panel1.Controls.Add(this.textBox9);
            this.panel1.Controls.Add(this.label20);
            this.panel1.Controls.Add(this.label21);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.textBox4);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.textBox5);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.textBox6);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.textBox3);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.textBox2);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(3, 14);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(660, 200);
            this.panel1.TabIndex = 0;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(607, 175);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(17, 12);
            this.label22.TabIndex = 41;
            this.label22.Text = "℃";
            // 
            // textBox10
            // 
            this.textBox10.Location = new System.Drawing.Point(556, 171);
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new System.Drawing.Size(45, 21);
            this.textBox10.TabIndex = 40;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(508, 174);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(29, 12);
            this.label23.TabIndex = 39;
            this.label23.Text = "时间";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(382, 174);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(17, 12);
            this.label24.TabIndex = 38;
            this.label24.Text = "℃";
            // 
            // textBox11
            // 
            this.textBox11.Location = new System.Drawing.Point(331, 171);
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new System.Drawing.Size(45, 21);
            this.textBox11.TabIndex = 37;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(284, 174);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(41, 12);
            this.label25.TabIndex = 36;
            this.label25.Text = "催化区";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(171, 174);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(17, 12);
            this.label26.TabIndex = 35;
            this.label26.Text = "℃";
            // 
            // textBox12
            // 
            this.textBox12.Location = new System.Drawing.Point(120, 170);
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new System.Drawing.Size(45, 21);
            this.textBox12.TabIndex = 34;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(62, 174);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(41, 12);
            this.label27.TabIndex = 33;
            this.label27.Text = "氧化区";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(20, 174);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(35, 12);
            this.label28.TabIndex = 32;
            this.label28.Text = "Step4";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(607, 129);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(17, 12);
            this.label15.TabIndex = 31;
            this.label15.Text = "℃";
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(556, 125);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(45, 21);
            this.textBox7.TabIndex = 30;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(508, 129);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(29, 12);
            this.label16.TabIndex = 29;
            this.label16.Text = "时间";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(382, 128);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(17, 12);
            this.label17.TabIndex = 28;
            this.label17.Text = "℃";
            // 
            // textBox8
            // 
            this.textBox8.Location = new System.Drawing.Point(331, 125);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(45, 21);
            this.textBox8.TabIndex = 27;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(284, 128);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(41, 12);
            this.label18.TabIndex = 26;
            this.label18.Text = "催化区";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(171, 128);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(17, 12);
            this.label19.TabIndex = 25;
            this.label19.Text = "℃";
            // 
            // textBox9
            // 
            this.textBox9.Location = new System.Drawing.Point(120, 124);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(45, 21);
            this.textBox9.TabIndex = 24;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(62, 128);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(41, 12);
            this.label20.TabIndex = 23;
            this.label20.Text = "氧化区";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(20, 128);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(35, 12);
            this.label21.TabIndex = 22;
            this.label21.Text = "Step3";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(607, 88);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 12);
            this.label6.TabIndex = 21;
            this.label6.Text = "℃";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(556, 84);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(45, 21);
            this.textBox4.TabIndex = 20;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(508, 88);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 12);
            this.label9.TabIndex = 19;
            this.label9.Text = "时间";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(382, 87);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(17, 12);
            this.label10.TabIndex = 18;
            this.label10.Text = "℃";
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(331, 84);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(45, 21);
            this.textBox5.TabIndex = 17;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(284, 87);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(41, 12);
            this.label11.TabIndex = 16;
            this.label11.Text = "催化区";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(171, 87);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(17, 12);
            this.label12.TabIndex = 15;
            this.label12.Text = "℃";
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(120, 83);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(45, 21);
            this.textBox6.TabIndex = 14;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(62, 87);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(41, 12);
            this.label13.TabIndex = 13;
            this.label13.Text = "氧化区";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(20, 87);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(35, 12);
            this.label14.TabIndex = 12;
            this.label14.Text = "Step2";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(607, 45);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 12);
            this.label7.TabIndex = 11;
            this.label7.Text = "℃";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(556, 41);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(45, 21);
            this.textBox3.TabIndex = 10;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(508, 43);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 9;
            this.label8.Text = "时间";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(382, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "℃";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(331, 41);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(45, 21);
            this.textBox2.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(284, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 5;
            this.label5.Text = "催化区";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(171, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "℃";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(120, 40);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(45, 21);
            this.textBox1.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(62, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "氧化区";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Step1";
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(12, 40);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(677, 54);
            this.panel2.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Location = new System.Drawing.Point(12, 373);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(677, 45);
            this.panel3.TabIndex = 2;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label57);
            this.panel4.Controls.Add(this.textBox25);
            this.panel4.Controls.Add(this.label58);
            this.panel4.Controls.Add(this.label59);
            this.panel4.Controls.Add(this.textBox26);
            this.panel4.Controls.Add(this.label60);
            this.panel4.Controls.Add(this.label61);
            this.panel4.Controls.Add(this.textBox27);
            this.panel4.Controls.Add(this.label62);
            this.panel4.Controls.Add(this.label63);
            this.panel4.Controls.Add(this.label29);
            this.panel4.Controls.Add(this.textBox13);
            this.panel4.Controls.Add(this.label30);
            this.panel4.Controls.Add(this.label31);
            this.panel4.Controls.Add(this.textBox14);
            this.panel4.Controls.Add(this.label32);
            this.panel4.Controls.Add(this.label33);
            this.panel4.Controls.Add(this.textBox15);
            this.panel4.Controls.Add(this.label34);
            this.panel4.Controls.Add(this.label35);
            this.panel4.Controls.Add(this.label36);
            this.panel4.Controls.Add(this.textBox16);
            this.panel4.Controls.Add(this.label37);
            this.panel4.Controls.Add(this.label38);
            this.panel4.Controls.Add(this.textBox17);
            this.panel4.Controls.Add(this.label39);
            this.panel4.Controls.Add(this.label40);
            this.panel4.Controls.Add(this.textBox18);
            this.panel4.Controls.Add(this.label41);
            this.panel4.Controls.Add(this.label42);
            this.panel4.Controls.Add(this.label43);
            this.panel4.Controls.Add(this.textBox19);
            this.panel4.Controls.Add(this.label44);
            this.panel4.Controls.Add(this.label45);
            this.panel4.Controls.Add(this.textBox20);
            this.panel4.Controls.Add(this.label46);
            this.panel4.Controls.Add(this.label47);
            this.panel4.Controls.Add(this.textBox21);
            this.panel4.Controls.Add(this.label48);
            this.panel4.Controls.Add(this.label49);
            this.panel4.Controls.Add(this.label50);
            this.panel4.Controls.Add(this.textBox22);
            this.panel4.Controls.Add(this.label51);
            this.panel4.Controls.Add(this.label52);
            this.panel4.Controls.Add(this.textBox23);
            this.panel4.Controls.Add(this.label53);
            this.panel4.Controls.Add(this.label54);
            this.panel4.Controls.Add(this.textBox24);
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
            this.label57.Location = new System.Drawing.Point(607, 8);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(17, 12);
            this.label57.TabIndex = 51;
            this.label57.Text = "℃";
            // 
            // textBox25
            // 
            this.textBox25.Location = new System.Drawing.Point(556, 4);
            this.textBox25.Name = "textBox25";
            this.textBox25.Size = new System.Drawing.Size(45, 21);
            this.textBox25.TabIndex = 50;
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.Location = new System.Drawing.Point(508, 6);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(29, 12);
            this.label58.TabIndex = 49;
            this.label58.Text = "时间";
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.Location = new System.Drawing.Point(382, 7);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(17, 12);
            this.label59.TabIndex = 48;
            this.label59.Text = "℃";
            // 
            // textBox26
            // 
            this.textBox26.Location = new System.Drawing.Point(331, 4);
            this.textBox26.Name = "textBox26";
            this.textBox26.Size = new System.Drawing.Size(45, 21);
            this.textBox26.TabIndex = 47;
            // 
            // label60
            // 
            this.label60.AutoSize = true;
            this.label60.Location = new System.Drawing.Point(284, 7);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(41, 12);
            this.label60.TabIndex = 46;
            this.label60.Text = "催化区";
            // 
            // label61
            // 
            this.label61.AutoSize = true;
            this.label61.Location = new System.Drawing.Point(171, 7);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(17, 12);
            this.label61.TabIndex = 45;
            this.label61.Text = "℃";
            // 
            // textBox27
            // 
            this.textBox27.Location = new System.Drawing.Point(120, 3);
            this.textBox27.Name = "textBox27";
            this.textBox27.Size = new System.Drawing.Size(45, 21);
            this.textBox27.TabIndex = 44;
            // 
            // label62
            // 
            this.label62.AutoSize = true;
            this.label62.Location = new System.Drawing.Point(62, 7);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(41, 12);
            this.label62.TabIndex = 43;
            this.label62.Text = "氧化区";
            // 
            // label63
            // 
            this.label63.AutoSize = true;
            this.label63.Location = new System.Drawing.Point(20, 7);
            this.label63.Name = "label63";
            this.label63.Size = new System.Drawing.Size(35, 12);
            this.label63.TabIndex = 42;
            this.label63.Text = "Step5";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(607, 175);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(17, 12);
            this.label29.TabIndex = 41;
            this.label29.Text = "℃";
            // 
            // textBox13
            // 
            this.textBox13.Location = new System.Drawing.Point(556, 171);
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new System.Drawing.Size(45, 21);
            this.textBox13.TabIndex = 40;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(508, 174);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(29, 12);
            this.label30.TabIndex = 39;
            this.label30.Text = "时间";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(382, 174);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(17, 12);
            this.label31.TabIndex = 38;
            this.label31.Text = "℃";
            // 
            // textBox14
            // 
            this.textBox14.Location = new System.Drawing.Point(331, 171);
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new System.Drawing.Size(45, 21);
            this.textBox14.TabIndex = 37;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(284, 174);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(41, 12);
            this.label32.TabIndex = 36;
            this.label32.Text = "催化区";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(171, 174);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(17, 12);
            this.label33.TabIndex = 35;
            this.label33.Text = "℃";
            // 
            // textBox15
            // 
            this.textBox15.Location = new System.Drawing.Point(120, 170);
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new System.Drawing.Size(45, 21);
            this.textBox15.TabIndex = 34;
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(62, 174);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(41, 12);
            this.label34.TabIndex = 33;
            this.label34.Text = "氧化区";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(20, 174);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(35, 12);
            this.label35.TabIndex = 32;
            this.label35.Text = "Step9";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(607, 129);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(17, 12);
            this.label36.TabIndex = 31;
            this.label36.Text = "℃";
            // 
            // textBox16
            // 
            this.textBox16.Location = new System.Drawing.Point(556, 125);
            this.textBox16.Name = "textBox16";
            this.textBox16.Size = new System.Drawing.Size(45, 21);
            this.textBox16.TabIndex = 30;
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(508, 129);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(29, 12);
            this.label37.TabIndex = 29;
            this.label37.Text = "时间";
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(382, 128);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(17, 12);
            this.label38.TabIndex = 28;
            this.label38.Text = "℃";
            // 
            // textBox17
            // 
            this.textBox17.Location = new System.Drawing.Point(331, 125);
            this.textBox17.Name = "textBox17";
            this.textBox17.Size = new System.Drawing.Size(45, 21);
            this.textBox17.TabIndex = 27;
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(284, 128);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(41, 12);
            this.label39.TabIndex = 26;
            this.label39.Text = "催化区";
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(171, 128);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(17, 12);
            this.label40.TabIndex = 25;
            this.label40.Text = "℃";
            // 
            // textBox18
            // 
            this.textBox18.Location = new System.Drawing.Point(120, 124);
            this.textBox18.Name = "textBox18";
            this.textBox18.Size = new System.Drawing.Size(45, 21);
            this.textBox18.TabIndex = 24;
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(62, 128);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(41, 12);
            this.label41.TabIndex = 23;
            this.label41.Text = "氧化区";
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(20, 128);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(35, 12);
            this.label42.TabIndex = 22;
            this.label42.Text = "Step8";
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Location = new System.Drawing.Point(607, 88);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(17, 12);
            this.label43.TabIndex = 21;
            this.label43.Text = "℃";
            // 
            // textBox19
            // 
            this.textBox19.Location = new System.Drawing.Point(556, 84);
            this.textBox19.Name = "textBox19";
            this.textBox19.Size = new System.Drawing.Size(45, 21);
            this.textBox19.TabIndex = 20;
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Location = new System.Drawing.Point(508, 88);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(29, 12);
            this.label44.TabIndex = 19;
            this.label44.Text = "时间";
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(382, 87);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(17, 12);
            this.label45.TabIndex = 18;
            this.label45.Text = "℃";
            // 
            // textBox20
            // 
            this.textBox20.Location = new System.Drawing.Point(331, 84);
            this.textBox20.Name = "textBox20";
            this.textBox20.Size = new System.Drawing.Size(45, 21);
            this.textBox20.TabIndex = 17;
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(284, 87);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(41, 12);
            this.label46.TabIndex = 16;
            this.label46.Text = "催化区";
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(171, 87);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(17, 12);
            this.label47.TabIndex = 15;
            this.label47.Text = "℃";
            // 
            // textBox21
            // 
            this.textBox21.Location = new System.Drawing.Point(120, 83);
            this.textBox21.Name = "textBox21";
            this.textBox21.Size = new System.Drawing.Size(45, 21);
            this.textBox21.TabIndex = 14;
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Location = new System.Drawing.Point(62, 87);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(41, 12);
            this.label48.TabIndex = 13;
            this.label48.Text = "氧化区";
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Location = new System.Drawing.Point(20, 87);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(35, 12);
            this.label49.TabIndex = 12;
            this.label49.Text = "Step7";
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Location = new System.Drawing.Point(607, 45);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(17, 12);
            this.label50.TabIndex = 11;
            this.label50.Text = "℃";
            // 
            // textBox22
            // 
            this.textBox22.Location = new System.Drawing.Point(556, 41);
            this.textBox22.Name = "textBox22";
            this.textBox22.Size = new System.Drawing.Size(45, 21);
            this.textBox22.TabIndex = 10;
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.Location = new System.Drawing.Point(508, 43);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(29, 12);
            this.label51.TabIndex = 9;
            this.label51.Text = "时间";
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Location = new System.Drawing.Point(382, 44);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(17, 12);
            this.label52.TabIndex = 7;
            this.label52.Text = "℃";
            // 
            // textBox23
            // 
            this.textBox23.Location = new System.Drawing.Point(331, 41);
            this.textBox23.Name = "textBox23";
            this.textBox23.Size = new System.Drawing.Size(45, 21);
            this.textBox23.TabIndex = 6;
            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.Location = new System.Drawing.Point(284, 44);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(41, 12);
            this.label53.TabIndex = 5;
            this.label53.Text = "催化区";
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Location = new System.Drawing.Point(171, 44);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(17, 12);
            this.label54.TabIndex = 3;
            this.label54.Text = "℃";
            // 
            // textBox24
            // 
            this.textBox24.Location = new System.Drawing.Point(120, 40);
            this.textBox24.Name = "textBox24";
            this.textBox24.Size = new System.Drawing.Size(45, 21);
            this.textBox24.TabIndex = 2;
            // 
            // label55
            // 
            this.label55.AutoSize = true;
            this.label55.Location = new System.Drawing.Point(62, 44);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(41, 12);
            this.label55.TabIndex = 1;
            this.label55.Text = "氧化区";
            // 
            // label56
            // 
            this.label56.AutoSize = true;
            this.label56.Location = new System.Drawing.Point(20, 44);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(35, 12);
            this.label56.TabIndex = 0;
            this.label56.Text = "Step6";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.label64);
            this.panel5.Controls.Add(this.textBox28);
            this.panel5.Controls.Add(this.label65);
            this.panel5.Controls.Add(this.label68);
            this.panel5.Controls.Add(this.textBox30);
            this.panel5.Controls.Add(this.label69);
            this.panel5.Controls.Add(this.label70);
            this.panel5.Controls.Add(this.label71);
            this.panel5.Controls.Add(this.textBox31);
            this.panel5.Controls.Add(this.label72);
            this.panel5.Controls.Add(this.label75);
            this.panel5.Controls.Add(this.textBox33);
            this.panel5.Controls.Add(this.label76);
            this.panel5.Controls.Add(this.label77);
            this.panel5.Controls.Add(this.label78);
            this.panel5.Controls.Add(this.textBox34);
            this.panel5.Controls.Add(this.label79);
            this.panel5.Controls.Add(this.label82);
            this.panel5.Controls.Add(this.textBox36);
            this.panel5.Controls.Add(this.label83);
            this.panel5.Controls.Add(this.label84);
            this.panel5.Controls.Add(this.label85);
            this.panel5.Controls.Add(this.textBox37);
            this.panel5.Controls.Add(this.label86);
            this.panel5.Controls.Add(this.label89);
            this.panel5.Controls.Add(this.textBox39);
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
            this.label64.Location = new System.Drawing.Point(538, 175);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(11, 12);
            this.label64.TabIndex = 41;
            this.label64.Text = "V";
            // 
            // textBox28
            // 
            this.textBox28.Location = new System.Drawing.Point(487, 171);
            this.textBox28.Name = "textBox28";
            this.textBox28.Size = new System.Drawing.Size(45, 21);
            this.textBox28.TabIndex = 40;
            // 
            // label65
            // 
            this.label65.AutoSize = true;
            this.label65.Location = new System.Drawing.Point(439, 174);
            this.label65.Name = "label65";
            this.label65.Size = new System.Drawing.Size(29, 12);
            this.label65.TabIndex = 39;
            this.label65.Text = "时间";
            // 
            // label68
            // 
            this.label68.AutoSize = true;
            this.label68.Location = new System.Drawing.Point(251, 172);
            this.label68.Name = "label68";
            this.label68.Size = new System.Drawing.Size(11, 12);
            this.label68.TabIndex = 35;
            this.label68.Text = "v";
            // 
            // textBox30
            // 
            this.textBox30.Location = new System.Drawing.Point(200, 168);
            this.textBox30.Name = "textBox30";
            this.textBox30.Size = new System.Drawing.Size(45, 21);
            this.textBox30.TabIndex = 34;
            // 
            // label69
            // 
            this.label69.AutoSize = true;
            this.label69.Location = new System.Drawing.Point(142, 172);
            this.label69.Name = "label69";
            this.label69.Size = new System.Drawing.Size(41, 12);
            this.label69.TabIndex = 33;
            this.label69.Text = "氧化区";
            // 
            // label70
            // 
            this.label70.AutoSize = true;
            this.label70.Location = new System.Drawing.Point(100, 172);
            this.label70.Name = "label70";
            this.label70.Size = new System.Drawing.Size(35, 12);
            this.label70.TabIndex = 32;
            this.label70.Text = "Step4";
            // 
            // label71
            // 
            this.label71.AutoSize = true;
            this.label71.Location = new System.Drawing.Point(538, 129);
            this.label71.Name = "label71";
            this.label71.Size = new System.Drawing.Size(11, 12);
            this.label71.TabIndex = 31;
            this.label71.Text = "V";
            // 
            // textBox31
            // 
            this.textBox31.Location = new System.Drawing.Point(487, 125);
            this.textBox31.Name = "textBox31";
            this.textBox31.Size = new System.Drawing.Size(45, 21);
            this.textBox31.TabIndex = 30;
            // 
            // label72
            // 
            this.label72.AutoSize = true;
            this.label72.Location = new System.Drawing.Point(439, 129);
            this.label72.Name = "label72";
            this.label72.Size = new System.Drawing.Size(29, 12);
            this.label72.TabIndex = 29;
            this.label72.Text = "时间";
            // 
            // label75
            // 
            this.label75.AutoSize = true;
            this.label75.Location = new System.Drawing.Point(251, 126);
            this.label75.Name = "label75";
            this.label75.Size = new System.Drawing.Size(11, 12);
            this.label75.TabIndex = 25;
            this.label75.Text = "V";
            // 
            // textBox33
            // 
            this.textBox33.Location = new System.Drawing.Point(200, 122);
            this.textBox33.Name = "textBox33";
            this.textBox33.Size = new System.Drawing.Size(45, 21);
            this.textBox33.TabIndex = 24;
            // 
            // label76
            // 
            this.label76.AutoSize = true;
            this.label76.Location = new System.Drawing.Point(142, 126);
            this.label76.Name = "label76";
            this.label76.Size = new System.Drawing.Size(41, 12);
            this.label76.TabIndex = 23;
            this.label76.Text = "氧化区";
            // 
            // label77
            // 
            this.label77.AutoSize = true;
            this.label77.Location = new System.Drawing.Point(100, 126);
            this.label77.Name = "label77";
            this.label77.Size = new System.Drawing.Size(35, 12);
            this.label77.TabIndex = 22;
            this.label77.Text = "Step3";
            // 
            // label78
            // 
            this.label78.AutoSize = true;
            this.label78.Location = new System.Drawing.Point(538, 88);
            this.label78.Name = "label78";
            this.label78.Size = new System.Drawing.Size(11, 12);
            this.label78.TabIndex = 21;
            this.label78.Text = "V";
            // 
            // textBox34
            // 
            this.textBox34.Location = new System.Drawing.Point(487, 84);
            this.textBox34.Name = "textBox34";
            this.textBox34.Size = new System.Drawing.Size(45, 21);
            this.textBox34.TabIndex = 20;
            // 
            // label79
            // 
            this.label79.AutoSize = true;
            this.label79.Location = new System.Drawing.Point(439, 88);
            this.label79.Name = "label79";
            this.label79.Size = new System.Drawing.Size(29, 12);
            this.label79.TabIndex = 19;
            this.label79.Text = "时间";
            // 
            // label82
            // 
            this.label82.AutoSize = true;
            this.label82.Location = new System.Drawing.Point(251, 85);
            this.label82.Name = "label82";
            this.label82.Size = new System.Drawing.Size(11, 12);
            this.label82.TabIndex = 15;
            this.label82.Text = "V";
            // 
            // textBox36
            // 
            this.textBox36.Location = new System.Drawing.Point(200, 81);
            this.textBox36.Name = "textBox36";
            this.textBox36.Size = new System.Drawing.Size(45, 21);
            this.textBox36.TabIndex = 14;
            // 
            // label83
            // 
            this.label83.AutoSize = true;
            this.label83.Location = new System.Drawing.Point(142, 85);
            this.label83.Name = "label83";
            this.label83.Size = new System.Drawing.Size(41, 12);
            this.label83.TabIndex = 13;
            this.label83.Text = "氧化区";
            // 
            // label84
            // 
            this.label84.AutoSize = true;
            this.label84.Location = new System.Drawing.Point(100, 85);
            this.label84.Name = "label84";
            this.label84.Size = new System.Drawing.Size(35, 12);
            this.label84.TabIndex = 12;
            this.label84.Text = "Step2";
            // 
            // label85
            // 
            this.label85.AutoSize = true;
            this.label85.Location = new System.Drawing.Point(538, 45);
            this.label85.Name = "label85";
            this.label85.Size = new System.Drawing.Size(11, 12);
            this.label85.TabIndex = 11;
            this.label85.Text = "V";
            // 
            // textBox37
            // 
            this.textBox37.Location = new System.Drawing.Point(487, 41);
            this.textBox37.Name = "textBox37";
            this.textBox37.Size = new System.Drawing.Size(45, 21);
            this.textBox37.TabIndex = 10;
            // 
            // label86
            // 
            this.label86.AutoSize = true;
            this.label86.Location = new System.Drawing.Point(439, 43);
            this.label86.Name = "label86";
            this.label86.Size = new System.Drawing.Size(29, 12);
            this.label86.TabIndex = 9;
            this.label86.Text = "时间";
            // 
            // label89
            // 
            this.label89.AutoSize = true;
            this.label89.Location = new System.Drawing.Point(251, 42);
            this.label89.Name = "label89";
            this.label89.Size = new System.Drawing.Size(11, 12);
            this.label89.TabIndex = 3;
            this.label89.Text = "V";
            // 
            // textBox39
            // 
            this.textBox39.Location = new System.Drawing.Point(200, 38);
            this.textBox39.Name = "textBox39";
            this.textBox39.Size = new System.Drawing.Size(45, 21);
            this.textBox39.TabIndex = 2;
            // 
            // label90
            // 
            this.label90.AutoSize = true;
            this.label90.Location = new System.Drawing.Point(142, 42);
            this.label90.Name = "label90";
            this.label90.Size = new System.Drawing.Size(41, 12);
            this.label90.TabIndex = 1;
            this.label90.Text = "氧化区";
            // 
            // label91
            // 
            this.label91.AutoSize = true;
            this.label91.Location = new System.Drawing.Point(100, 42);
            this.label91.Name = "label91";
            this.label91.Size = new System.Drawing.Size(35, 12);
            this.label91.TabIndex = 0;
            this.label91.Text = "Step1";
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.label66);
            this.panel6.Controls.Add(this.textBox29);
            this.panel6.Controls.Add(this.label67);
            this.panel6.Controls.Add(this.label73);
            this.panel6.Controls.Add(this.textBox32);
            this.panel6.Controls.Add(this.label74);
            this.panel6.Controls.Add(this.label80);
            this.panel6.Controls.Add(this.textBox35);
            this.panel6.Controls.Add(this.label81);
            this.panel6.Controls.Add(this.label87);
            this.panel6.Controls.Add(this.label88);
            this.panel6.Controls.Add(this.textBox38);
            this.panel6.Controls.Add(this.label92);
            this.panel6.Controls.Add(this.label93);
            this.panel6.Controls.Add(this.textBox40);
            this.panel6.Controls.Add(this.label94);
            this.panel6.Controls.Add(this.label95);
            this.panel6.Controls.Add(this.textBox41);
            this.panel6.Controls.Add(this.label96);
            this.panel6.Controls.Add(this.label97);
            this.panel6.Controls.Add(this.label98);
            this.panel6.Controls.Add(this.textBox42);
            this.panel6.Controls.Add(this.label99);
            this.panel6.Controls.Add(this.label100);
            this.panel6.Controls.Add(this.textBox43);
            this.panel6.Controls.Add(this.label101);
            this.panel6.Controls.Add(this.label102);
            this.panel6.Controls.Add(this.textBox44);
            this.panel6.Controls.Add(this.label103);
            this.panel6.Controls.Add(this.label104);
            this.panel6.Controls.Add(this.label105);
            this.panel6.Controls.Add(this.textBox45);
            this.panel6.Controls.Add(this.label106);
            this.panel6.Controls.Add(this.label107);
            this.panel6.Controls.Add(this.textBox46);
            this.panel6.Controls.Add(this.label108);
            this.panel6.Controls.Add(this.label109);
            this.panel6.Controls.Add(this.textBox47);
            this.panel6.Controls.Add(this.label110);
            this.panel6.Controls.Add(this.label111);
            this.panel6.Controls.Add(this.label112);
            this.panel6.Controls.Add(this.textBox48);
            this.panel6.Controls.Add(this.label113);
            this.panel6.Controls.Add(this.label114);
            this.panel6.Controls.Add(this.textBox49);
            this.panel6.Controls.Add(this.label115);
            this.panel6.Controls.Add(this.label116);
            this.panel6.Controls.Add(this.textBox50);
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
            this.label66.Size = new System.Drawing.Size(17, 12);
            this.label66.TabIndex = 51;
            this.label66.Text = "℃";
            // 
            // textBox29
            // 
            this.textBox29.Location = new System.Drawing.Point(556, 4);
            this.textBox29.Name = "textBox29";
            this.textBox29.Size = new System.Drawing.Size(45, 21);
            this.textBox29.TabIndex = 50;
            // 
            // label67
            // 
            this.label67.AutoSize = true;
            this.label67.Location = new System.Drawing.Point(508, 6);
            this.label67.Name = "label67";
            this.label67.Size = new System.Drawing.Size(29, 12);
            this.label67.TabIndex = 49;
            this.label67.Text = "时间";
            // 
            // label73
            // 
            this.label73.AutoSize = true;
            this.label73.Location = new System.Drawing.Point(382, 7);
            this.label73.Name = "label73";
            this.label73.Size = new System.Drawing.Size(17, 12);
            this.label73.TabIndex = 48;
            this.label73.Text = "℃";
            // 
            // textBox32
            // 
            this.textBox32.Location = new System.Drawing.Point(331, 4);
            this.textBox32.Name = "textBox32";
            this.textBox32.Size = new System.Drawing.Size(45, 21);
            this.textBox32.TabIndex = 47;
            // 
            // label74
            // 
            this.label74.AutoSize = true;
            this.label74.Location = new System.Drawing.Point(284, 7);
            this.label74.Name = "label74";
            this.label74.Size = new System.Drawing.Size(41, 12);
            this.label74.TabIndex = 46;
            this.label74.Text = "催化区";
            // 
            // label80
            // 
            this.label80.AutoSize = true;
            this.label80.Location = new System.Drawing.Point(171, 7);
            this.label80.Name = "label80";
            this.label80.Size = new System.Drawing.Size(17, 12);
            this.label80.TabIndex = 45;
            this.label80.Text = "℃";
            // 
            // textBox35
            // 
            this.textBox35.Location = new System.Drawing.Point(120, 3);
            this.textBox35.Name = "textBox35";
            this.textBox35.Size = new System.Drawing.Size(45, 21);
            this.textBox35.TabIndex = 44;
            // 
            // label81
            // 
            this.label81.AutoSize = true;
            this.label81.Location = new System.Drawing.Point(62, 7);
            this.label81.Name = "label81";
            this.label81.Size = new System.Drawing.Size(41, 12);
            this.label81.TabIndex = 43;
            this.label81.Text = "氧化区";
            // 
            // label87
            // 
            this.label87.AutoSize = true;
            this.label87.Location = new System.Drawing.Point(20, 8);
            this.label87.Name = "label87";
            this.label87.Size = new System.Drawing.Size(35, 12);
            this.label87.TabIndex = 42;
            this.label87.Text = "Step5";
            // 
            // label88
            // 
            this.label88.AutoSize = true;
            this.label88.Location = new System.Drawing.Point(607, 175);
            this.label88.Name = "label88";
            this.label88.Size = new System.Drawing.Size(17, 12);
            this.label88.TabIndex = 41;
            this.label88.Text = "℃";
            // 
            // textBox38
            // 
            this.textBox38.Location = new System.Drawing.Point(556, 171);
            this.textBox38.Name = "textBox38";
            this.textBox38.Size = new System.Drawing.Size(45, 21);
            this.textBox38.TabIndex = 40;
            // 
            // label92
            // 
            this.label92.AutoSize = true;
            this.label92.Location = new System.Drawing.Point(508, 174);
            this.label92.Name = "label92";
            this.label92.Size = new System.Drawing.Size(29, 12);
            this.label92.TabIndex = 39;
            this.label92.Text = "时间";
            // 
            // label93
            // 
            this.label93.AutoSize = true;
            this.label93.Location = new System.Drawing.Point(382, 174);
            this.label93.Name = "label93";
            this.label93.Size = new System.Drawing.Size(17, 12);
            this.label93.TabIndex = 38;
            this.label93.Text = "℃";
            // 
            // textBox40
            // 
            this.textBox40.Location = new System.Drawing.Point(331, 171);
            this.textBox40.Name = "textBox40";
            this.textBox40.Size = new System.Drawing.Size(45, 21);
            this.textBox40.TabIndex = 37;
            // 
            // label94
            // 
            this.label94.AutoSize = true;
            this.label94.Location = new System.Drawing.Point(284, 174);
            this.label94.Name = "label94";
            this.label94.Size = new System.Drawing.Size(41, 12);
            this.label94.TabIndex = 36;
            this.label94.Text = "催化区";
            // 
            // label95
            // 
            this.label95.AutoSize = true;
            this.label95.Location = new System.Drawing.Point(171, 174);
            this.label95.Name = "label95";
            this.label95.Size = new System.Drawing.Size(17, 12);
            this.label95.TabIndex = 35;
            this.label95.Text = "℃";
            // 
            // textBox41
            // 
            this.textBox41.Location = new System.Drawing.Point(120, 170);
            this.textBox41.Name = "textBox41";
            this.textBox41.Size = new System.Drawing.Size(45, 21);
            this.textBox41.TabIndex = 34;
            // 
            // label96
            // 
            this.label96.AutoSize = true;
            this.label96.Location = new System.Drawing.Point(62, 174);
            this.label96.Name = "label96";
            this.label96.Size = new System.Drawing.Size(41, 12);
            this.label96.TabIndex = 33;
            this.label96.Text = "氧化区";
            // 
            // label97
            // 
            this.label97.AutoSize = true;
            this.label97.Location = new System.Drawing.Point(20, 174);
            this.label97.Name = "label97";
            this.label97.Size = new System.Drawing.Size(35, 12);
            this.label97.TabIndex = 32;
            this.label97.Text = "Step9";
            // 
            // label98
            // 
            this.label98.AutoSize = true;
            this.label98.Location = new System.Drawing.Point(607, 129);
            this.label98.Name = "label98";
            this.label98.Size = new System.Drawing.Size(17, 12);
            this.label98.TabIndex = 31;
            this.label98.Text = "℃";
            // 
            // textBox42
            // 
            this.textBox42.Location = new System.Drawing.Point(556, 125);
            this.textBox42.Name = "textBox42";
            this.textBox42.Size = new System.Drawing.Size(45, 21);
            this.textBox42.TabIndex = 30;
            // 
            // label99
            // 
            this.label99.AutoSize = true;
            this.label99.Location = new System.Drawing.Point(508, 129);
            this.label99.Name = "label99";
            this.label99.Size = new System.Drawing.Size(29, 12);
            this.label99.TabIndex = 29;
            this.label99.Text = "时间";
            // 
            // label100
            // 
            this.label100.AutoSize = true;
            this.label100.Location = new System.Drawing.Point(382, 128);
            this.label100.Name = "label100";
            this.label100.Size = new System.Drawing.Size(17, 12);
            this.label100.TabIndex = 28;
            this.label100.Text = "℃";
            // 
            // textBox43
            // 
            this.textBox43.Location = new System.Drawing.Point(331, 125);
            this.textBox43.Name = "textBox43";
            this.textBox43.Size = new System.Drawing.Size(45, 21);
            this.textBox43.TabIndex = 27;
            // 
            // label101
            // 
            this.label101.AutoSize = true;
            this.label101.Location = new System.Drawing.Point(284, 128);
            this.label101.Name = "label101";
            this.label101.Size = new System.Drawing.Size(41, 12);
            this.label101.TabIndex = 26;
            this.label101.Text = "催化区";
            // 
            // label102
            // 
            this.label102.AutoSize = true;
            this.label102.Location = new System.Drawing.Point(171, 128);
            this.label102.Name = "label102";
            this.label102.Size = new System.Drawing.Size(17, 12);
            this.label102.TabIndex = 25;
            this.label102.Text = "℃";
            // 
            // textBox44
            // 
            this.textBox44.Location = new System.Drawing.Point(120, 124);
            this.textBox44.Name = "textBox44";
            this.textBox44.Size = new System.Drawing.Size(45, 21);
            this.textBox44.TabIndex = 24;
            // 
            // label103
            // 
            this.label103.AutoSize = true;
            this.label103.Location = new System.Drawing.Point(62, 128);
            this.label103.Name = "label103";
            this.label103.Size = new System.Drawing.Size(41, 12);
            this.label103.TabIndex = 23;
            this.label103.Text = "氧化区";
            // 
            // label104
            // 
            this.label104.AutoSize = true;
            this.label104.Location = new System.Drawing.Point(20, 128);
            this.label104.Name = "label104";
            this.label104.Size = new System.Drawing.Size(35, 12);
            this.label104.TabIndex = 22;
            this.label104.Text = "Step8";
            // 
            // label105
            // 
            this.label105.AutoSize = true;
            this.label105.Location = new System.Drawing.Point(607, 88);
            this.label105.Name = "label105";
            this.label105.Size = new System.Drawing.Size(17, 12);
            this.label105.TabIndex = 21;
            this.label105.Text = "℃";
            // 
            // textBox45
            // 
            this.textBox45.Location = new System.Drawing.Point(556, 84);
            this.textBox45.Name = "textBox45";
            this.textBox45.Size = new System.Drawing.Size(45, 21);
            this.textBox45.TabIndex = 20;
            // 
            // label106
            // 
            this.label106.AutoSize = true;
            this.label106.Location = new System.Drawing.Point(508, 88);
            this.label106.Name = "label106";
            this.label106.Size = new System.Drawing.Size(29, 12);
            this.label106.TabIndex = 19;
            this.label106.Text = "时间";
            // 
            // label107
            // 
            this.label107.AutoSize = true;
            this.label107.Location = new System.Drawing.Point(382, 87);
            this.label107.Name = "label107";
            this.label107.Size = new System.Drawing.Size(17, 12);
            this.label107.TabIndex = 18;
            this.label107.Text = "℃";
            // 
            // textBox46
            // 
            this.textBox46.Location = new System.Drawing.Point(331, 84);
            this.textBox46.Name = "textBox46";
            this.textBox46.Size = new System.Drawing.Size(45, 21);
            this.textBox46.TabIndex = 17;
            // 
            // label108
            // 
            this.label108.AutoSize = true;
            this.label108.Location = new System.Drawing.Point(284, 87);
            this.label108.Name = "label108";
            this.label108.Size = new System.Drawing.Size(41, 12);
            this.label108.TabIndex = 16;
            this.label108.Text = "催化区";
            // 
            // label109
            // 
            this.label109.AutoSize = true;
            this.label109.Location = new System.Drawing.Point(171, 87);
            this.label109.Name = "label109";
            this.label109.Size = new System.Drawing.Size(17, 12);
            this.label109.TabIndex = 15;
            this.label109.Text = "℃";
            // 
            // textBox47
            // 
            this.textBox47.Location = new System.Drawing.Point(120, 83);
            this.textBox47.Name = "textBox47";
            this.textBox47.Size = new System.Drawing.Size(45, 21);
            this.textBox47.TabIndex = 14;
            // 
            // label110
            // 
            this.label110.AutoSize = true;
            this.label110.Location = new System.Drawing.Point(62, 87);
            this.label110.Name = "label110";
            this.label110.Size = new System.Drawing.Size(41, 12);
            this.label110.TabIndex = 13;
            this.label110.Text = "氧化区";
            // 
            // label111
            // 
            this.label111.AutoSize = true;
            this.label111.Location = new System.Drawing.Point(20, 87);
            this.label111.Name = "label111";
            this.label111.Size = new System.Drawing.Size(35, 12);
            this.label111.TabIndex = 12;
            this.label111.Text = "Step7";
            // 
            // label112
            // 
            this.label112.AutoSize = true;
            this.label112.Location = new System.Drawing.Point(607, 45);
            this.label112.Name = "label112";
            this.label112.Size = new System.Drawing.Size(17, 12);
            this.label112.TabIndex = 11;
            this.label112.Text = "℃";
            // 
            // textBox48
            // 
            this.textBox48.Location = new System.Drawing.Point(556, 41);
            this.textBox48.Name = "textBox48";
            this.textBox48.Size = new System.Drawing.Size(45, 21);
            this.textBox48.TabIndex = 10;
            // 
            // label113
            // 
            this.label113.AutoSize = true;
            this.label113.Location = new System.Drawing.Point(508, 43);
            this.label113.Name = "label113";
            this.label113.Size = new System.Drawing.Size(29, 12);
            this.label113.TabIndex = 9;
            this.label113.Text = "时间";
            // 
            // label114
            // 
            this.label114.AutoSize = true;
            this.label114.Location = new System.Drawing.Point(382, 44);
            this.label114.Name = "label114";
            this.label114.Size = new System.Drawing.Size(17, 12);
            this.label114.TabIndex = 7;
            this.label114.Text = "℃";
            // 
            // textBox49
            // 
            this.textBox49.Location = new System.Drawing.Point(331, 41);
            this.textBox49.Name = "textBox49";
            this.textBox49.Size = new System.Drawing.Size(45, 21);
            this.textBox49.TabIndex = 6;
            // 
            // label115
            // 
            this.label115.AutoSize = true;
            this.label115.Location = new System.Drawing.Point(284, 44);
            this.label115.Name = "label115";
            this.label115.Size = new System.Drawing.Size(41, 12);
            this.label115.TabIndex = 5;
            this.label115.Text = "催化区";
            // 
            // label116
            // 
            this.label116.AutoSize = true;
            this.label116.Location = new System.Drawing.Point(171, 44);
            this.label116.Name = "label116";
            this.label116.Size = new System.Drawing.Size(17, 12);
            this.label116.TabIndex = 3;
            this.label116.Text = "℃";
            // 
            // textBox50
            // 
            this.textBox50.Location = new System.Drawing.Point(120, 40);
            this.textBox50.Name = "textBox50";
            this.textBox50.Size = new System.Drawing.Size(45, 21);
            this.textBox50.TabIndex = 2;
            // 
            // label117
            // 
            this.label117.AutoSize = true;
            this.label117.Location = new System.Drawing.Point(62, 44);
            this.label117.Name = "label117";
            this.label117.Size = new System.Drawing.Size(41, 12);
            this.label117.TabIndex = 1;
            this.label117.Text = "氧化区";
            // 
            // label118
            // 
            this.label118.AutoSize = true;
            this.label118.Location = new System.Drawing.Point(20, 44);
            this.label118.Name = "label118";
            this.label118.Size = new System.Drawing.Size(35, 12);
            this.label118.TabIndex = 0;
            this.label118.Text = "Step6";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(12, 100);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(677, 267);
            this.tabControl1.TabIndex = 53;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(669, 345);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "温度设置Step1-4";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panel6);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(669, 241);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "温度设置Step5-9";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.panel5);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(669, 345);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "电压设置Step1-4";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.panel4);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(669, 345);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "电压设置5-9";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // FormParaSet
            // 
            this.ClientSize = new System.Drawing.Size(780, 463);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Name = "FormParaSet";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
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
        private TextBox textBox3;
        private Label label8;
        private Label label4;
        private TextBox textBox2;
        private Label label5;
        private Label label3;
        private TextBox textBox1;
        private Label label2;
        private Label label1;
        private Panel panel4;
        private Label label57;
        private TextBox textBox25;
        private Label label58;
        private Label label59;
        private TextBox textBox26;
        private Label label60;
        private Label label61;
        private TextBox textBox27;
        private Label label62;
        private Label label63;
        private Label label29;
        private TextBox textBox13;
        private Label label30;
        private Label label31;
        private TextBox textBox14;
        private Label label32;
        private Label label33;
        private TextBox textBox15;
        private Label label34;
        private Label label35;
        private Label label36;
        private TextBox textBox16;
        private Label label37;
        private Label label38;
        private TextBox textBox17;
        private Label label39;
        private Label label40;
        private TextBox textBox18;
        private Label label41;
        private Label label42;
        private Label label43;
        private TextBox textBox19;
        private Label label44;
        private Label label45;
        private TextBox textBox20;
        private Label label46;
        private Label label47;
        private TextBox textBox21;
        private Label label48;
        private Label label49;
        private Label label50;
        private TextBox textBox22;
        private Label label51;
        private Label label52;
        private TextBox textBox23;
        private Label label53;
        private Label label54;
        private TextBox textBox24;
        private Label label55;
        private Label label56;
        private Label label22;
        private TextBox textBox10;
        private Label label23;
        private Label label24;
        private TextBox textBox11;
        private Label label25;
        private Label label26;
        private TextBox textBox12;
        private Label label27;
        private Label label28;
        private Label label15;
        private TextBox textBox7;
        private Label label16;
        private Label label17;
        private TextBox textBox8;
        private Label label18;
        private Label label19;
        private TextBox textBox9;
        private Label label20;
        private Label label21;
        private Label label6;
        private TextBox textBox4;
        private Label label9;
        private Label label10;
        private TextBox textBox5;
        private Label label11;
        private Label label12;
        private TextBox textBox6;
        private Label label13;
        private Label label14;
        private Panel panel5;
        private Label label64;
        private TextBox textBox28;
        private Label label65;
        private Label label68;
        private TextBox textBox30;
        private Label label69;
        private Label label70;
        private Label label71;
        private TextBox textBox31;
        private Label label72;
        private Label label75;
        private TextBox textBox33;
        private Label label76;
        private Label label77;
        private Label label78;
        private TextBox textBox34;
        private Label label79;
        private Label label82;
        private TextBox textBox36;
        private Label label83;
        private Label label84;
        private Label label85;
        private TextBox textBox37;
        private Label label86;
        private Label label89;
        private TextBox textBox39;
        private Label label90;
        private Label label91;
        private Panel panel6;
        private Label label66;
        private TextBox textBox29;
        private Label label67;
        private Label label73;
        private TextBox textBox32;
        private Label label74;
        private Label label80;
        private TextBox textBox35;
        private Label label81;
        private Label label87;
        private Label label88;
        private TextBox textBox38;
        private Label label92;
        private Label label93;
        private TextBox textBox40;
        private Label label94;
        private Label label95;
        private TextBox textBox41;
        private Label label96;
        private Label label97;
        private Label label98;
        private TextBox textBox42;
        private Label label99;
        private Label label100;
        private TextBox textBox43;
        private Label label101;
        private Label label102;
        private TextBox textBox44;
        private Label label103;
        private Label label104;
        private Label label105;
        private TextBox textBox45;
        private Label label106;
        private Label label107;
        private TextBox textBox46;
        private Label label108;
        private Label label109;
        private TextBox textBox47;
        private Label label110;
        private Label label111;
        private Label label112;
        private TextBox textBox48;
        private Label label113;
        private Label label114;
        private TextBox textBox49;
        private Label label115;
        private Label label116;
        private TextBox textBox50;
        private Label label117;
        private Label label118;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private TabPage tabPage4;
    }
}
