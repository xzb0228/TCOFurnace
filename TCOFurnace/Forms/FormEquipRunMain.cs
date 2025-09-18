using Common;
using EquipDriver;
using ModBusRTU;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TCOFurnace.UserControls;


namespace TCOFurnace.Forms
{
    public class FormEquipRunMain : BaseForm
    {
        public static readonly FormEquipRunMain singFormEquipRunMain = new FormEquipRunMain();
        private FormEquipRunMain()
        {
            InitializeComponent();
        }


        private System.ComponentModel.IContainer components = null;

        // 每行的内层表格
        private TableLayoutPanel row1Table;
        private Label label1;
        private Label label2;
        private Panel panel1;
        private Label label4;
        private TableLayoutPanel row3Table;
        private TableLayoutPanel outTableLayoutPanel;
        private TableLayoutPanel row2Table;
        private Label label5;
        private Label label9;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label10;
        private Label label11;
        private Label label12;
        private Button but2_6;
        private Button but6_6;
        private Label lab3_6;
        private Button but4_6;
        private Button but5_6;
        private Label label13;
        private Label lab6_3;
        private Label labTCO;
        private Label lab6_5;
        private Label label14;
        private Label labModel;
        private Button button1;
        private Label lab1_2;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.row1Table = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lab1_2 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.row3Table = new System.Windows.Forms.TableLayoutPanel();
            this.outTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.row2Table = new System.Windows.Forms.TableLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.but2_6 = new System.Windows.Forms.Button();
            this.lab3_6 = new System.Windows.Forms.Label();
            this.but4_6 = new System.Windows.Forms.Button();
            this.but5_6 = new System.Windows.Forms.Button();
            this.but6_6 = new System.Windows.Forms.Button();
            this.lab6_3 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.labTCO = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lab6_5 = new System.Windows.Forms.Label();
            this.labModel = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.row1Table.SuspendLayout();
            this.panel1.SuspendLayout();
            this.row3Table.SuspendLayout();
            this.outTableLayoutPanel.SuspendLayout();
            this.row2Table.SuspendLayout();
            this.SuspendLayout();
            // 
            // row1Table
            // 
            this.row1Table.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.row1Table.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.row1Table.ColumnCount = 3;
            this.row1Table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26F));
            this.row1Table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 51F));
            this.row1Table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23F));
            this.row1Table.Controls.Add(this.label1, 0, 0);
            this.row1Table.Controls.Add(this.panel1, 1, 0);
            this.row1Table.Controls.Add(this.label4, 2, 0);
            this.row1Table.Location = new System.Drawing.Point(0, 0);
            this.row1Table.Margin = new System.Windows.Forms.Padding(0);
            this.row1Table.Name = "row1Table";
            this.row1Table.RowCount = 1;
            this.row1Table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.row1Table.Size = new System.Drawing.Size(843, 39);
            this.row1Table.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(702, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 21);
            this.label4.TabIndex = 5;
            this.label4.Text = "功   能";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(63, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 21);
            this.label1.TabIndex = 3;
            this.label1.Text = "运行状态";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lab1_2);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(223, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(421, 31);
            this.panel1.TabIndex = 4;
            // 
            // lab1_2
            // 
            this.lab1_2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lab1_2.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab1_2.Location = new System.Drawing.Point(277, 5);
            this.lab1_2.Name = "lab1_2";
            this.lab1_2.Size = new System.Drawing.Size(37, 21);
            this.lab1_2.TabIndex = 5;
            this.lab1_2.Text = "0";
            this.lab1_2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AllowDrop = true;
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(32, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(226, 21);
            this.label2.TabIndex = 4;
            this.label2.Text = "运 行 参 数     Step";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // row3Table
            // 
            this.row3Table.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.row3Table.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.row3Table.ColumnCount = 5;
            this.row3Table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26F));
            this.row3Table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17F));
            this.row3Table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17F));
            this.row3Table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17F));
            this.row3Table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23F));
            this.row3Table.Controls.Add(this.lab6_5, 3, 0);
            this.row3Table.Controls.Add(this.label13, 2, 0);
            this.row3Table.Controls.Add(this.but6_6, 4, 0);
            this.row3Table.Controls.Add(this.label9, 0, 0);
            this.row3Table.Controls.Add(this.lab6_3, 1, 0);
            this.row3Table.Location = new System.Drawing.Point(0, 207);
            this.row3Table.Margin = new System.Windows.Forms.Padding(0);
            this.row3Table.Name = "row3Table";
            this.row3Table.RowCount = 1;
            this.row3Table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.row3Table.Size = new System.Drawing.Size(843, 41);
            this.row3Table.TabIndex = 5;
            // 
            // outTableLayoutPanel
            // 
            this.outTableLayoutPanel.ColumnCount = 1;
            this.outTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.outTableLayoutPanel.Controls.Add(this.row1Table, 0, 0);
            this.outTableLayoutPanel.Controls.Add(this.row3Table, 0, 2);
            this.outTableLayoutPanel.Controls.Add(this.row2Table, 0, 1);
            this.outTableLayoutPanel.Location = new System.Drawing.Point(16, 110);
            this.outTableLayoutPanel.Name = "outTableLayoutPanel";
            this.outTableLayoutPanel.RowCount = 3;
            this.outTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16F));
            this.outTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 68F));
            this.outTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16F));
            this.outTableLayoutPanel.Size = new System.Drawing.Size(843, 248);
            this.outTableLayoutPanel.TabIndex = 6;
            // 
            // row2Table
            // 
            this.row2Table.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.row2Table.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.row2Table.ColumnCount = 6;
            this.row2Table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.99974F));
            this.row2Table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.99974F));
            this.row2Table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.99966F));
            this.row2Table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.99966F));
            this.row2Table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.00166F));
            this.row2Table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.99954F));
            this.row2Table.Controls.Add(this.label6, 0, 1);
            this.row2Table.Controls.Add(this.label7, 0, 2);
            this.row2Table.Controls.Add(this.label8, 0, 3);
            this.row2Table.Controls.Add(this.label5, 0, 0);
            this.row2Table.Controls.Add(this.label10, 2, 0);
            this.row2Table.Controls.Add(this.label11, 3, 0);
            this.row2Table.Controls.Add(this.label12, 4, 0);
            this.row2Table.Controls.Add(this.but2_6, 5, 0);
            this.row2Table.Controls.Add(this.lab3_6, 5, 1);
            this.row2Table.Controls.Add(this.but4_6, 5, 2);
            this.row2Table.Controls.Add(this.but5_6, 5, 3);
            this.row2Table.Location = new System.Drawing.Point(0, 39);
            this.row2Table.Margin = new System.Windows.Forms.Padding(0);
            this.row2Table.Name = "row2Table";
            this.row2Table.RowCount = 4;
            this.row2Table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.row2Table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.row2Table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.row2Table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.row2Table.Size = new System.Drawing.Size(843, 168);
            this.row2Table.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(18, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 21);
            this.label5.TabIndex = 4;
            this.label5.Text = "流量计";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(13, 51);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 21);
            this.label6.TabIndex = 5;
            this.label6.Text = "氧/氩气";
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(18, 92);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(73, 21);
            this.label7.TabIndex = 6;
            this.label7.Text = "氧化区";
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(18, 135);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(73, 21);
            this.label8.TabIndex = 7;
            this.label8.Text = "催化区";
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(42, 11);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(135, 19);
            this.label9.TabIndex = 8;
            this.label9.Text = "设置时间(min)";
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(242, 10);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(96, 21);
            this.label10.TabIndex = 8;
            this.label10.Text = "项    目";
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(396, 10);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(73, 21);
            this.label11.TabIndex = 9;
            this.label11.Text = "设定值";
            // 
            // label12
            // 
            this.label12.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.Location = new System.Drawing.Point(539, 10);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(73, 21);
            this.label12.TabIndex = 10;
            this.label12.Text = "检测值";
            // 
            // but2_6
            // 
            this.but2_6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.but2_6.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.but2_6.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.but2_6.Location = new System.Drawing.Point(670, 7);
            this.but2_6.Name = "but2_6";
            this.but2_6.Size = new System.Drawing.Size(150, 28);
            this.but2_6.TabIndex = 11;
            this.but2_6.Text = "1# /2# 系统切换";
            this.but2_6.UseVisualStyleBackColor = false;
            // 
            // lab3_6
            // 
            this.lab3_6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lab3_6.AutoSize = true;
            this.lab3_6.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.lab3_6.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab3_6.Location = new System.Drawing.Point(708, 51);
            this.lab3_6.Name = "lab3_6";
            this.lab3_6.Size = new System.Drawing.Size(74, 21);
            this.lab3_6.TabIndex = 12;
            this.lab3_6.Text = "1#系统";
            // 
            // but4_6
            // 
            this.but4_6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.but4_6.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.but4_6.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.but4_6.Location = new System.Drawing.Point(670, 89);
            this.but4_6.Name = "but4_6";
            this.but4_6.Size = new System.Drawing.Size(150, 28);
            this.but4_6.TabIndex = 13;
            this.but4_6.Text = "初 始 化";
            this.but4_6.UseVisualStyleBackColor = false;
            // 
            // but5_6
            // 
            this.but5_6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.but5_6.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.but5_6.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.but5_6.Location = new System.Drawing.Point(670, 131);
            this.but5_6.Name = "but5_6";
            this.but5_6.Size = new System.Drawing.Size(150, 28);
            this.but5_6.TabIndex = 14;
            this.but5_6.Text = "停    止";
            this.but5_6.UseVisualStyleBackColor = false;
            // 
            // but6_6
            // 
            this.but6_6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.but6_6.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.but6_6.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.but6_6.Location = new System.Drawing.Point(670, 6);
            this.but6_6.Name = "but6_6";
            this.but6_6.Size = new System.Drawing.Size(150, 28);
            this.but6_6.TabIndex = 15;
            this.but6_6.Text = "启   动";
            this.but6_6.UseVisualStyleBackColor = false;
            // 
            // lab6_3
            // 
            this.lab6_3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lab6_3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lab6_3.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab6_3.Location = new System.Drawing.Point(271, 10);
            this.lab6_3.Name = "lab6_3";
            this.lab6_3.Size = new System.Drawing.Size(37, 21);
            this.lab6_3.TabIndex = 16;
            this.lab6_3.Text = "0";
            this.lab6_3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label13
            // 
            this.label13.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.Location = new System.Drawing.Point(365, 11);
            this.label13.Margin = new System.Windows.Forms.Padding(0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(135, 19);
            this.label13.TabIndex = 17;
            this.label13.Text = "运行时间(min)";
            // 
            // labTCO
            // 
            this.labTCO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labTCO.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labTCO.Location = new System.Drawing.Point(12, 35);
            this.labTCO.Name = "labTCO";
            this.labTCO.Size = new System.Drawing.Size(843, 29);
            this.labTCO.TabIndex = 7;
            this.labTCO.Text = "有机氚碳氧化系统";
            this.labTCO.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label14.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label14.Location = new System.Drawing.Point(12, 72);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(843, 29);
            this.label14.TabIndex = 8;
            this.label14.Text = "仪器运行监控界面";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lab6_5
            // 
            this.lab6_5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lab6_5.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lab6_5.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab6_5.Location = new System.Drawing.Point(557, 10);
            this.lab6_5.Name = "lab6_5";
            this.lab6_5.Size = new System.Drawing.Size(37, 21);
            this.lab6_5.TabIndex = 18;
            this.lab6_5.Text = "0";
            this.lab6_5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labModel
            // 
            this.labModel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labModel.AutoSize = true;
            this.labModel.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labModel.Location = new System.Drawing.Point(79, 369);
            this.labModel.Name = "labModel";
            this.labModel.Size = new System.Drawing.Size(98, 21);
            this.labModel.TabIndex = 9;
            this.labModel.Text = "标准模式";
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.button1.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(383, 384);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(102, 28);
            this.button1.TabIndex = 15;
            this.button1.Text = "系统运行";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // FormEquipRunMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(873, 474);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.labModel);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.labTCO);
            this.Controls.Add(this.outTableLayoutPanel);
            this.Name = "FormEquipRunMain";
            this.Text = "合并单元格选择性显示竖线";
            this.row1Table.ResumeLayout(false);
            this.row1Table.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.row3Table.ResumeLayout(false);
            this.row3Table.PerformLayout();
            this.outTableLayoutPanel.ResumeLayout(false);
            this.row2Table.ResumeLayout(false);
            this.row2Table.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                MessageBox.Show($"按钮 {btn.Name} 被点击！");
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
