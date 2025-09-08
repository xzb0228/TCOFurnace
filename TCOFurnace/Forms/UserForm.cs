using System;
using System.Diagnostics;
using System.Windows.Forms;
using TCOFurnace.DataService;
using TCOFurnace.Models;
using TCOFurnace.Common;
using static System.Collections.Specialized.BitVector32;
using Common;

namespace TCOFurnace.Forms
{
    public partial class UserForm : BaseForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblUsername;
        private TextBox txtUsername;
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
        public UserForm()
        {
            InitializeComponent();
        }
        private void InitializeComponent()
        {
            this.lblUsername = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.ButAdd = new System.Windows.Forms.Button();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(12, 14);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(65, 12);
            this.lblUsername.TabIndex = 0;
            this.lblUsername.Text = "用 户 名：";
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(73, 12);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(150, 21);
            this.txtUsername.TabIndex = 1;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(12, 59);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(65, 12);
            this.lblPassword.TabIndex = 2;
            this.lblPassword.Text = "密    码：";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(73, 57);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(150, 21);
            this.txtPassword.TabIndex = 3;
            // 
            // ButAdd
            // 
            this.ButAdd.Location = new System.Drawing.Point(73, 101);
            this.ButAdd.Name = "ButAdd";
            this.ButAdd.Size = new System.Drawing.Size(75, 21);
            this.ButAdd.TabIndex = 4;
            this.ButAdd.Text = "新增";
            this.ButAdd.UseVisualStyleBackColor = true;
            this.ButAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnCancel.Location = new System.Drawing.Point(154, 101);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(69, 21);
            this.BtnCancel.TabIndex = 5;
            this.BtnCancel.Text = "取消";
            this.BtnCancel.UseVisualStyleBackColor = true;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // UserForm
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
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.lblUsername);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UserForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "用户管理";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show(LanguageManager.GetMsg("10001"), LanguageManager.GetMsg("10000"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show(LanguageManager.GetMsg("10002"), LanguageManager.GetMsg("10000"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            User us = SqliteHelper.QueryFirstOrDefault<User>($"SELECT * FROM users WHERE username = '{username}' ");

            if (us != null)
            {
                MessageBox.Show(LanguageManager.GetMsg("10003"), LanguageManager.GetMsg("10000"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                //获取员工编号
                if (int.TryParse(SqliteHelper.ExecuteScalar($" select max(usercode) +1 from Users ").ToString(), out int usercode))
                {
                    int Result = SqliteHelper.ExecuteNonQuery($"INSERT INTO Users (usercode, username,password,created_at,updated_at) VALUES ('{usercode}','{username}','{AesEncryptor.EncryptStr(password)}',CURRENT_TIMESTAMP,CURRENT_TIMESTAMP);");
                    if (Result > 0)
                    {
                        MessageBox.Show(LanguageManager.GetMsg("10004"), LanguageManager.GetMsg("10000"), MessageBoxButtons.OK, MessageBoxIcon.None);
                        return;
                    }
                    else {
                        MessageBox.Show(LanguageManager.GetMsg("10005"), LanguageManager.GetMsg("10000"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        
    }
}