using System;
using System.Diagnostics;
using System.Windows.Forms;
using TCOFurnace.DataService;
using TCOFurnace.Models;
using TCOFurnace.Common;
using static System.Collections.Specialized.BitVector32;
using Common;
using System.Data.SqlTypes;

namespace TCOFurnace.Forms
{
    public partial class PasswordForm : Form
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblPassword;
        private TextBox txtPassword;
        private Button ButAdd;
        private Button BtnCancel;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        public PasswordForm()
        {
            InitializeComponent();

      
        }
       
        private void InitializeComponent()
        {
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.ButAdd = new System.Windows.Forms.Button();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(12, 42);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(65, 12);
            this.lblPassword.TabIndex = 2;
            this.lblPassword.Text = "密    码：";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(83, 39);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(140, 21);
            this.txtPassword.TabIndex = 3;
            // 
            // ButAdd
            // 
            this.ButAdd.Location = new System.Drawing.Point(73, 85);
            this.ButAdd.Name = "ButAdd";
            this.ButAdd.Size = new System.Drawing.Size(75, 21);
            this.ButAdd.TabIndex = 4;
            this.ButAdd.Text = "确定";
            this.ButAdd.UseVisualStyleBackColor = true;
            this.ButAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnCancel.Location = new System.Drawing.Point(154, 85);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(69, 21);
            this.BtnCancel.TabIndex = 5;
            this.BtnCancel.Text = "取消";
            this.BtnCancel.UseVisualStyleBackColor = true;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // PasswordForm
            // 
            this.AcceptButton = this.ButAdd;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.BtnCancel;
            this.ClientSize = new System.Drawing.Size(235, 134);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.ButAdd);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblPassword);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PasswordForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "密码验证";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            //密码为年月日数字之和乘以5

            // 获取当前日期
            DateTime today = DateTime.Today;

            // 提取年、月、日
            int year = today.Year;
            int month = today.Month;
            int day = today.Day;

            // 计算各位数字之和
            int sum = year + month + day;
            if (int.TryParse(txtPassword.Text, out int result) && result==(year + month + day)*5)
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }



    }
}