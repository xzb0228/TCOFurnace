using System.Windows.Forms;

namespace TCOFurnace.Forms
{
    public partial class FormEquipRunMain
    {
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
        private Button butRunning;
        private Label lab3_6;
        private Button butInitializing;
        private Button butStopped;
        private Label label13;
        private Label lab6_3;
        private Label labTCO;
        private Label lab6_5;
        private Label label14;
        private Label labModel;
        private Button butSysRun;
        private Label lab2_2;
        private Label lab3_2;
        private Label lab4_2;
        private Label lab5_2;
        private Label label3;
        private Label label15;
        private Label label16;
        private Label lab5_5;
        private Label lab5_4;
        private Label lab4_5;
        private Label lab4_4;
        private Label lab3_5;
        private Label lab3_4;
        private Button butParaSet;
        private Label lab1_2;

 

        private void InitializeComponent()
        {
            this.butParaSet = new System.Windows.Forms.Button();
            this.butSysRun = new System.Windows.Forms.Button();
            this.labModel = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.labTCO = new System.Windows.Forms.Label();
            this.outTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.row1Table = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lab1_2 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.row3Table = new System.Windows.Forms.TableLayoutPanel();
            this.lab6_5 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.butRunning = new System.Windows.Forms.Button();
            this.lab6_3 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.row2Table = new System.Windows.Forms.TableLayoutPanel();
            this.lab5_5 = new System.Windows.Forms.Label();
            this.lab5_4 = new System.Windows.Forms.Label();
            this.lab4_5 = new System.Windows.Forms.Label();
            this.lab4_4 = new System.Windows.Forms.Label();
            this.lab3_5 = new System.Windows.Forms.Label();
            this.lab3_4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lab3_2 = new System.Windows.Forms.Label();
            this.lab2_2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.but2_6 = new System.Windows.Forms.Button();
            this.lab3_6 = new System.Windows.Forms.Label();
            this.butInitializing = new System.Windows.Forms.Button();
            this.butStopped = new System.Windows.Forms.Button();
            this.lab4_2 = new System.Windows.Forms.Label();
            this.lab5_2 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.outTableLayoutPanel.SuspendLayout();
            this.row1Table.SuspendLayout();
            this.panel1.SuspendLayout();
            this.row3Table.SuspendLayout();
            this.row2Table.SuspendLayout();
            this.SuspendLayout();
            // 
            // butParaSet
            // 
            this.butParaSet.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.butParaSet.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.butParaSet.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.butParaSet.Location = new System.Drawing.Point(686, 371);
            this.butParaSet.Margin = new System.Windows.Forms.Padding(0);
            this.butParaSet.Name = "butParaSet";
            this.butParaSet.Size = new System.Drawing.Size(150, 30);
            this.butParaSet.TabIndex = 16;
            this.butParaSet.Text = "参数设置";
            this.butParaSet.UseVisualStyleBackColor = false;
            this.butParaSet.Visible = false;
            this.butParaSet.Click += new System.EventHandler(this.butParaSet_Click);
            // 
            // butSysRun
            // 
            this.butSysRun.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.butSysRun.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.butSysRun.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.butSysRun.Location = new System.Drawing.Point(385, 371);
            this.butSysRun.Margin = new System.Windows.Forms.Padding(0);
            this.butSysRun.Name = "butSysRun";
            this.butSysRun.Size = new System.Drawing.Size(102, 30);
            this.butSysRun.TabIndex = 15;
            this.butSysRun.Text = "系统运行";
            this.butSysRun.UseVisualStyleBackColor = false;
            // 
            // labModel
            // 
            this.labModel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labModel.AutoSize = true;
            this.labModel.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labModel.Location = new System.Drawing.Point(75, 375);
            this.labModel.Name = "labModel";
            this.labModel.Size = new System.Drawing.Size(98, 21);
            this.labModel.TabIndex = 9;
            this.labModel.Text = "";
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
            this.lab1_2.Location = new System.Drawing.Point(323, 5);
            this.lab1_2.Name = "lab1_2";
            this.lab1_2.Size = new System.Drawing.Size(60, 21);
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
            this.row3Table.Controls.Add(this.butRunning, 4, 0);
            this.row3Table.Controls.Add(this.lab6_3, 1, 0);
            this.row3Table.Controls.Add(this.label9, 0, 0);
            this.row3Table.Location = new System.Drawing.Point(0, 207);
            this.row3Table.Margin = new System.Windows.Forms.Padding(0);
            this.row3Table.Name = "row3Table";
            this.row3Table.RowCount = 1;
            this.row3Table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.row3Table.Size = new System.Drawing.Size(843, 41);
            this.row3Table.TabIndex = 5;
            // 
            // lab6_5
            // 
            this.lab6_5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lab6_5.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lab6_5.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab6_5.Location = new System.Drawing.Point(546, 10);
            this.lab6_5.Name = "lab6_5";
            this.lab6_5.Size = new System.Drawing.Size(60, 21);
            this.lab6_5.TabIndex = 18;
            this.lab6_5.Text = "0";
            this.lab6_5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            // butRunning
            // 
            this.butRunning.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.butRunning.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.butRunning.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.butRunning.Location = new System.Drawing.Point(670, 5);
            this.butRunning.Name = "butRunning";
            this.butRunning.Size = new System.Drawing.Size(150, 30);
            this.butRunning.TabIndex = 15;
            this.butRunning.Text = "启   动";
            this.butRunning.UseVisualStyleBackColor = false;
            this.butRunning.Click += new System.EventHandler(this.butRunning_Click);
            // 
            // lab6_3
            // 
            this.lab6_3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lab6_3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lab6_3.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab6_3.Location = new System.Drawing.Point(260, 10);
            this.lab6_3.Name = "lab6_3";
            this.lab6_3.Size = new System.Drawing.Size(60, 21);
            this.lab6_3.TabIndex = 16;
            this.lab6_3.Text = "0";
            this.lab6_3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.row2Table.Controls.Add(this.lab5_5, 4, 3);
            this.row2Table.Controls.Add(this.lab5_4, 3, 3);
            this.row2Table.Controls.Add(this.lab4_5, 4, 2);
            this.row2Table.Controls.Add(this.lab4_4, 3, 2);
            this.row2Table.Controls.Add(this.lab3_5, 4, 1);
            this.row2Table.Controls.Add(this.lab3_4, 3, 1);
            this.row2Table.Controls.Add(this.label3, 2, 1);
            this.row2Table.Controls.Add(this.lab3_2, 1, 1);
            this.row2Table.Controls.Add(this.lab2_2, 1, 0);
            this.row2Table.Controls.Add(this.label6, 0, 1);
            this.row2Table.Controls.Add(this.label7, 0, 2);
            this.row2Table.Controls.Add(this.label8, 0, 3);
            this.row2Table.Controls.Add(this.label5, 0, 0);
            this.row2Table.Controls.Add(this.label10, 2, 0);
            this.row2Table.Controls.Add(this.label11, 3, 0);
            this.row2Table.Controls.Add(this.label12, 4, 0);
            this.row2Table.Controls.Add(this.but2_6, 5, 0);
            this.row2Table.Controls.Add(this.lab3_6, 5, 1);
            this.row2Table.Controls.Add(this.butInitializing, 5, 2);
            this.row2Table.Controls.Add(this.butStopped, 5, 3);
            this.row2Table.Controls.Add(this.lab4_2, 1, 2);
            this.row2Table.Controls.Add(this.lab5_2, 1, 3);
            this.row2Table.Controls.Add(this.label15, 2, 2);
            this.row2Table.Controls.Add(this.label16, 2, 3);
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
            // lab5_5
            // 
            this.lab5_5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lab5_5.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lab5_5.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab5_5.Location = new System.Drawing.Point(546, 135);
            this.lab5_5.Name = "lab5_5";
            this.lab5_5.Size = new System.Drawing.Size(60, 21);
            this.lab5_5.TabIndex = 27;
            this.lab5_5.Text = "0";
            this.lab5_5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lab5_4
            // 
            this.lab5_4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lab5_4.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lab5_4.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab5_4.Location = new System.Drawing.Point(403, 135);
            this.lab5_4.Name = "lab5_4";
            this.lab5_4.Size = new System.Drawing.Size(60, 21);
            this.lab5_4.TabIndex = 26;
            this.lab5_4.Text = "0";
            this.lab5_4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lab4_5
            // 
            this.lab4_5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lab4_5.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lab4_5.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab4_5.Location = new System.Drawing.Point(546, 92);
            this.lab4_5.Name = "lab4_5";
            this.lab4_5.Size = new System.Drawing.Size(60, 21);
            this.lab4_5.TabIndex = 25;
            this.lab4_5.Text = "0";
            this.lab4_5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lab4_4
            // 
            this.lab4_4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lab4_4.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lab4_4.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab4_4.Location = new System.Drawing.Point(403, 92);
            this.lab4_4.Name = "lab4_4";
            this.lab4_4.Size = new System.Drawing.Size(60, 21);
            this.lab4_4.TabIndex = 24;
            this.lab4_4.Text = "0";
            this.lab4_4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lab3_5
            // 
            this.lab3_5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lab3_5.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lab3_5.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab3_5.Location = new System.Drawing.Point(546, 51);
            this.lab3_5.Name = "lab3_5";
            this.lab3_5.Size = new System.Drawing.Size(60, 21);
            this.lab3_5.TabIndex = 23;
            this.lab3_5.Text = "0";
            this.lab3_5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lab3_4
            // 
            this.lab3_4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lab3_4.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lab3_4.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab3_4.Location = new System.Drawing.Point(403, 51);
            this.lab3_4.Name = "lab3_4";
            this.lab3_4.Size = new System.Drawing.Size(60, 21);
            this.lab3_4.TabIndex = 22;
            this.lab3_4.Text = "0";
            this.lab3_4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(242, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 16);
            this.label3.TabIndex = 19;
            this.label3.Text = "流量(L/min)";
            // 
            // lab3_2
            // 
            this.lab3_2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lab3_2.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lab3_2.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab3_2.Location = new System.Drawing.Point(127, 51);
            this.lab3_2.Name = "lab3_2";
            this.lab3_2.Size = new System.Drawing.Size(73, 21);
            this.lab3_2.TabIndex = 16;
            // 
            // lab2_2
            // 
            this.lab2_2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lab2_2.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lab2_2.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab2_2.Location = new System.Drawing.Point(127, 10);
            this.lab2_2.Name = "lab2_2";
            this.lab2_2.Size = new System.Drawing.Size(73, 21);
            this.lab2_2.TabIndex = 15;
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
            this.but2_6.Location = new System.Drawing.Point(670, 6);
            this.but2_6.Name = "but2_6";
            this.but2_6.Size = new System.Drawing.Size(150, 30);
            this.but2_6.TabIndex = 11;
            this.but2_6.Text = "1# /2# 系统切换";
            this.but2_6.UseVisualStyleBackColor = false;
            this.but2_6.Click += new System.EventHandler(this.but2_6_Click);
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
            // butInitializing
            // 
            this.butInitializing.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.butInitializing.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.butInitializing.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.butInitializing.Location = new System.Drawing.Point(670, 88);
            this.butInitializing.Name = "butInitializing";
            this.butInitializing.Size = new System.Drawing.Size(150, 30);
            this.butInitializing.TabIndex = 13;
            this.butInitializing.Text = "初 始 化";
            this.butInitializing.UseVisualStyleBackColor = false;
            this.butInitializing.Click += new System.EventHandler(this.butInitializing_Click);
            // 
            // butStopped
            // 
            this.butStopped.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.butStopped.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.butStopped.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.butStopped.Location = new System.Drawing.Point(670, 130);
            this.butStopped.Name = "butStopped";
            this.butStopped.Size = new System.Drawing.Size(150, 30);
            this.butStopped.TabIndex = 14;
            this.butStopped.Text = "停    止";
            this.butStopped.UseVisualStyleBackColor = false;
            this.butStopped.Click += new System.EventHandler(this.butStopped_Click);
            // 
            // lab4_2
            // 
            this.lab4_2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lab4_2.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lab4_2.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab4_2.Location = new System.Drawing.Point(127, 92);
            this.lab4_2.Name = "lab4_2";
            this.lab4_2.Size = new System.Drawing.Size(73, 21);
            this.lab4_2.TabIndex = 17;
            // 
            // lab5_2
            // 
            this.lab5_2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lab5_2.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lab5_2.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab5_2.Location = new System.Drawing.Point(127, 135);
            this.lab5_2.Name = "lab5_2";
            this.lab5_2.Size = new System.Drawing.Size(73, 21);
            this.lab5_2.TabIndex = 18;
            // 
            // label15
            // 
            this.label15.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label15.Location = new System.Drawing.Point(230, 95);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(119, 16);
            this.label15.TabIndex = 20;
            this.label15.Text = "氧化区温度(℃)";
            // 
            // label16
            // 
            this.label16.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label16.Location = new System.Drawing.Point(230, 137);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(119, 16);
            this.label16.TabIndex = 21;
            this.label16.Text = "催化区温度(℃)";
            // 
            // FormEquipRunMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(873, 474);
            this.Controls.Add(this.butParaSet);
            this.Controls.Add(this.butSysRun);
            this.Controls.Add(this.labModel);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.labTCO);
            this.Controls.Add(this.outTableLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FormEquipRunMain";
            this.Text = "仪器运行监控界面";
            this.outTableLayoutPanel.ResumeLayout(false);
            this.row1Table.ResumeLayout(false);
            this.row1Table.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.row3Table.ResumeLayout(false);
            this.row3Table.PerformLayout();
            this.row2Table.ResumeLayout(false);
            this.row2Table.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


    }
}

