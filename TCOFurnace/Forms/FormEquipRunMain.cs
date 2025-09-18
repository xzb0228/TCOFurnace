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

        // 外层表格（控制行）
        private TableLayoutPanel outerTableLayoutPanel;

        // 每行的内层表格
        private TableLayoutPanel row1Table;
        private TableLayoutPanel row2Table;
        private TableLayoutPanel row6Table;
        private TextBox textBox2_2;
        private TextBox textBox2_3;
        private Button button2_4;
        private TextBox textBox2_5;
        private TextBox textBox2_6;

        // 第六行控件
        private TextBox textBox6_1;  // 合并(6,1)-(6,2)
        private Button button6_3;
        private TextBox textBox6_4;
        private TextBox textBox6_5;
        private Label label1;
        private Label label2;
        private Panel panel1;
        private Label label4;
        private Label label3;
        private Label label5;
        private Label label6;
        private Button button6_6;

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
            this.outerTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.row1Table = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.row2Table = new System.Windows.Forms.TableLayoutPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox2_2 = new System.Windows.Forms.TextBox();
            this.textBox2_3 = new System.Windows.Forms.TextBox();
            this.button2_4 = new System.Windows.Forms.Button();
            this.textBox2_5 = new System.Windows.Forms.TextBox();
            this.textBox2_6 = new System.Windows.Forms.TextBox();
            this.row6Table = new System.Windows.Forms.TableLayoutPanel();
            this.button6_3 = new System.Windows.Forms.Button();
            this.textBox6_4 = new System.Windows.Forms.TextBox();
            this.textBox6_5 = new System.Windows.Forms.TextBox();
            this.button6_6 = new System.Windows.Forms.Button();
            this.textBox6_1 = new System.Windows.Forms.TextBox();
            this.outerTableLayoutPanel.SuspendLayout();
            this.row1Table.SuspendLayout();
            this.panel1.SuspendLayout();
            this.row2Table.SuspendLayout();
            this.row6Table.SuspendLayout();
            this.SuspendLayout();
            // 
            // outerTableLayoutPanel
            // 
            this.outerTableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.outerTableLayoutPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.outerTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.outerTableLayoutPanel.Controls.Add(this.row1Table, 0, 0);
            this.outerTableLayoutPanel.Controls.Add(this.row2Table, 0, 1);
            this.outerTableLayoutPanel.Controls.Add(this.row6Table, 0, 5);
            this.outerTableLayoutPanel.Location = new System.Drawing.Point(0, 84);
            this.outerTableLayoutPanel.Name = "outerTableLayoutPanel";
            this.outerTableLayoutPanel.RowCount = 6;
            this.outerTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66666F));
            this.outerTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66666F));
            this.outerTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66666F));
            this.outerTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66666F));
            this.outerTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66666F));
            this.outerTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.6667F));
            this.outerTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.outerTableLayoutPanel.Size = new System.Drawing.Size(904, 471);
            this.outerTableLayoutPanel.TabIndex = 0;
            // 
            // row1Table
            // 
            this.row1Table.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.row1Table.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.row1Table.ColumnCount = 3;
            this.row1Table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.07991F));
            this.row1Table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 52.16426F));
            this.row1Table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.57965F));
            this.row1Table.Controls.Add(this.label4, 2, 0);
            this.row1Table.Controls.Add(this.label1, 0, 0);
            this.row1Table.Controls.Add(this.panel1, 1, 0);
            this.row1Table.Location = new System.Drawing.Point(1, 1);
            this.row1Table.Margin = new System.Windows.Forms.Padding(0);
            this.row1Table.Name = "row1Table";
            this.row1Table.RowCount = 1;
            this.row1Table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.row1Table.Size = new System.Drawing.Size(902, 77);
            this.row1Table.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(770, 28);
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
            this.label1.Location = new System.Drawing.Point(80, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 21);
            this.label1.TabIndex = 3;
            this.label1.Text = "运行状态";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(257, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(463, 69);
            this.panel1.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label3.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(294, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 21);
            this.label3.TabIndex = 5;
            this.label3.Text = "0";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AllowDrop = true;
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(40, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(226, 21);
            this.label2.TabIndex = 4;
            this.label2.Text = "运 行 参 数     Step";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // row2Table
            // 
            this.row2Table.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.row2Table.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.row2Table.ColumnCount = 6;
            this.row2Table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.31742F));
            this.row2Table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.98446F));
            this.row2Table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.31299F));
            this.row2Table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.31188F));
            this.row2Table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.87236F));
            this.row2Table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.64484F));
            this.row2Table.Controls.Add(this.label6, 0, 1);
            this.row2Table.Controls.Add(this.label5, 0, 0);
            this.row2Table.Controls.Add(this.textBox2_2, 1, 0);
            this.row2Table.Controls.Add(this.textBox2_3, 2, 0);
            this.row2Table.Controls.Add(this.button2_4, 3, 0);
            this.row2Table.Controls.Add(this.textBox2_5, 4, 0);
            this.row2Table.Controls.Add(this.textBox2_6, 5, 0);
            this.row2Table.Location = new System.Drawing.Point(1, 79);
            this.row2Table.Margin = new System.Windows.Forms.Padding(0);
            this.row2Table.Name = "row2Table";
            this.row2Table.RowCount = 6;
            this.row2Table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.row2Table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.row2Table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.row2Table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.row2Table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 19F));
            this.row2Table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 9F));
            this.row2Table.Size = new System.Drawing.Size(902, 77);
            this.row2Table.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(22, -30);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 20);
            this.label6.TabIndex = 8;
            this.label6.Text = "氧/氩气";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(28, 1);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 1);
            this.label5.TabIndex = 6;
            this.label5.Text = "流量计";
            // 
            // textBox2_2
            // 
            this.textBox2_2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBox2_2.Location = new System.Drawing.Point(141, 4);
            this.textBox2_2.Name = "textBox2_2";
            this.textBox2_2.Size = new System.Drawing.Size(100, 21);
            this.textBox2_2.TabIndex = 1;
            this.textBox2_2.Text = "Text (2,2)";
            this.textBox2_2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox2_3
            // 
            this.textBox2_3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBox2_3.Location = new System.Drawing.Point(285, 4);
            this.textBox2_3.Name = "textBox2_3";
            this.textBox2_3.Size = new System.Drawing.Size(100, 21);
            this.textBox2_3.TabIndex = 2;
            this.textBox2_3.Text = "Text (2,3)";
            this.textBox2_3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // button2_4
            // 
            this.button2_4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button2_4.Location = new System.Drawing.Point(466, 4);
            this.button2_4.Name = "button2_4";
            this.button2_4.Size = new System.Drawing.Size(75, 1);
            this.button2_4.TabIndex = 3;
            this.button2_4.Text = "Btn (2,4)";
            // 
            // textBox2_5
            // 
            this.textBox2_5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBox2_5.Location = new System.Drawing.Point(607, 4);
            this.textBox2_5.Name = "textBox2_5";
            this.textBox2_5.Size = new System.Drawing.Size(100, 21);
            this.textBox2_5.TabIndex = 4;
            this.textBox2_5.Text = "Text (2,5)";
            this.textBox2_5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox2_6
            // 
            this.textBox2_6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBox2_6.Location = new System.Drawing.Point(762, 4);
            this.textBox2_6.Name = "textBox2_6";
            this.textBox2_6.Size = new System.Drawing.Size(100, 21);
            this.textBox2_6.TabIndex = 5;
            this.textBox2_6.Text = "Text (2,6)";
            this.textBox2_6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // row6Table
            // 
            this.row6Table.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.row6Table.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.row6Table.ColumnCount = 5;
            this.row6Table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.07991F));
            this.row6Table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.09101F));
            this.row6Table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.42286F));
            this.row6Table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.98335F));
            this.row6Table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.86682F));
            this.row6Table.Controls.Add(this.button6_3, 1, 0);
            this.row6Table.Controls.Add(this.textBox6_4, 2, 0);
            this.row6Table.Controls.Add(this.textBox6_5, 3, 0);
            this.row6Table.Controls.Add(this.button6_6, 4, 0);
            this.row6Table.Controls.Add(this.textBox6_1, 0, 0);
            this.row6Table.Location = new System.Drawing.Point(1, 391);
            this.row6Table.Margin = new System.Windows.Forms.Padding(0);
            this.row6Table.Name = "row6Table";
            this.row6Table.RowCount = 1;
            this.row6Table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.row6Table.Size = new System.Drawing.Size(902, 79);
            this.row6Table.TabIndex = 5;
            // 
            // button6_3
            // 
            this.button6_3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button6_3.Location = new System.Drawing.Point(295, 29);
            this.button6_3.Name = "button6_3";
            this.button6_3.Size = new System.Drawing.Size(75, 21);
            this.button6_3.TabIndex = 1;
            this.button6_3.Text = "Btn (6,3)";
            // 
            // textBox6_4
            // 
            this.textBox6_4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBox6_4.Location = new System.Drawing.Point(450, 29);
            this.textBox6_4.Name = "textBox6_4";
            this.textBox6_4.Size = new System.Drawing.Size(100, 21);
            this.textBox6_4.TabIndex = 2;
            this.textBox6_4.Text = "Text (6,4)";
            this.textBox6_4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox6_5
            // 
            this.textBox6_5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBox6_5.Location = new System.Drawing.Point(604, 29);
            this.textBox6_5.Name = "textBox6_5";
            this.textBox6_5.Size = new System.Drawing.Size(100, 21);
            this.textBox6_5.TabIndex = 3;
            this.textBox6_5.Text = "Text (6,5)";
            this.textBox6_5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // button6_6
            // 
            this.button6_6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button6_6.Location = new System.Drawing.Point(774, 29);
            this.button6_6.Name = "button6_6";
            this.button6_6.Size = new System.Drawing.Size(75, 21);
            this.button6_6.TabIndex = 4;
            this.button6_6.Text = "Btn (6,6)";
            // 
            // textBox6_1
            // 
            this.textBox6_1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBox6_1.Location = new System.Drawing.Point(76, 29);
            this.textBox6_1.Name = "textBox6_1";
            this.textBox6_1.Size = new System.Drawing.Size(100, 21);
            this.textBox6_1.TabIndex = 0;
            this.textBox6_1.Text = "合并 (6,1)-(6,2)";
            this.textBox6_1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // FormEquipRunMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(916, 653);
            this.Controls.Add(this.outerTableLayoutPanel);
            this.Name = "FormEquipRunMain";
            this.Text = "合并单元格选择性显示竖线";
            this.outerTableLayoutPanel.ResumeLayout(false);
            this.row1Table.ResumeLayout(false);
            this.row1Table.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.row2Table.ResumeLayout(false);
            this.row2Table.PerformLayout();
            this.row6Table.ResumeLayout(false);
            this.row6Table.PerformLayout();
            this.ResumeLayout(false);

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
