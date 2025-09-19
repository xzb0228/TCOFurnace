using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TCOFurnace.Common;
using TCOFurnace.Forms;

namespace TCOFurnace
{
    public class BaseForm : Form
    {
        // 时钟控件
        protected Label lblClock;


        protected Panel footerPanel;       // 底部公司信息面板
        private Label companyInfoLabel1;   // 第一行公司信息
        private Label companyInfoLabel2;   // 第二行公司信息
        // 定时器用于更新时间
        public BaseForm()
        {
            InitializeComponent();
        }
        private void InitializeComponent()
        {


        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // 默认为中文 不为中文就更换
            if (LanguageManager.CurrentLanguage != "zh-CN")
                LanguageManager.UpdateFormLanguage(this);

            // 是否启动页面控件权限 默认不启用
            if (GlobalPara.isOpenPermission)
                PermissionManager.UpdateFormConPermissions(this);

            if (this.Name != "UserForm" && this.Name != "LoginForm" && this.Name != "FormRolePermisManager" && this.Name != "FormTestMode" && !DesignMode)
            {
                this.lblClock = new System.Windows.Forms.Label();
                this.footerPanel = new System.Windows.Forms.Panel();
                this.companyInfoLabel1 = new System.Windows.Forms.Label();
                this.companyInfoLabel2 = new System.Windows.Forms.Label();
                this.footerPanel.SuspendLayout();
                this.SuspendLayout();
                // 
                // lblClock
                // 
                this.lblClock.AutoSize = true;
                this.lblClock.Font = new System.Drawing.Font("Segoe UI", 15F);
                this.lblClock.ForeColor = System.Drawing.SystemColors.ControlText;
                this.lblClock.Location = new System.Drawing.Point(0, 0);
                this.lblClock.Name = "lblClock";
                this.lblClock.Size = new System.Drawing.Size(100, 23);
                this.lblClock.TabIndex = 0;
                this.lblClock.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
                // 
                // footerPanel
                // 
                this.footerPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
                if (this.Name == "FormMain")
                {
                    this.footerPanel.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
                }

                this.footerPanel.Controls.Add(this.companyInfoLabel1);
                this.footerPanel.Controls.Add(this.companyInfoLabel2);
                this.footerPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
                this.footerPanel.Location = new System.Drawing.Point(0, 211);
                this.footerPanel.Margin = new System.Windows.Forms.Padding(0);
                this.footerPanel.Name = "footerPanel";
                this.footerPanel.Padding = new System.Windows.Forms.Padding(20, 10, 20, 10);
                this.footerPanel.Size = new System.Drawing.Size(373, 60);
                this.footerPanel.TabIndex = 1;
                // 
                // companyInfoLabel1
                // 
                this.companyInfoLabel1.Dock = System.Windows.Forms.DockStyle.Top;
                this.companyInfoLabel1.Font = new System.Drawing.Font("微软雅黑", 9F);
                this.companyInfoLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
                this.companyInfoLabel1.Location = new System.Drawing.Point(20, 20);
                this.companyInfoLabel1.Name = "companyInfoLabel1";
                this.companyInfoLabel1.Size = new System.Drawing.Size(333, 20);
                this.companyInfoLabel1.TabIndex = 1;
                this.companyInfoLabel1.Text = "Hubei Fanyuan Scientific Instruments Co.. Ltd.";
                this.companyInfoLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                // 
                // companyInfoLabel2
                // 
                this.companyInfoLabel2.Dock = System.Windows.Forms.DockStyle.Top;
                this.companyInfoLabel2.Font = new System.Drawing.Font("微软雅黑", 9F);
                this.companyInfoLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
                this.companyInfoLabel2.Location = new System.Drawing.Point(20, 0);
                this.companyInfoLabel2.Name = "companyInfoLabel2";
                this.companyInfoLabel2.Size = new System.Drawing.Size(333, 20);
                this.companyInfoLabel2.TabIndex = 0;
                this.companyInfoLabel2.Text = "湖北方圆科学仪器股份有限公司";
                this.companyInfoLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;


                this.Controls.Add(this.footerPanel);
                this.Name = "BaseForm";
                this.footerPanel.ResumeLayout(false);
                this.ResumeLayout(false);

                this.Name = "BaseForm";
                this.footerPanel.ResumeLayout(false);
                this.ResumeLayout(false);

                // 将时钟添加到窗体
                this.Controls.Add(lblClock);
                // 确保窗体大小改变时重新定位时钟
                this.Resize += BaseForm_Resize;
                #region 给每个界面加一个时钟
                // 初始化定时器
                Timer clockTimer = new Timer();
                clockTimer.Interval = 1000; // 每秒更新一次
                clockTimer.Tick += ClockTimer_Tick;
                clockTimer.Start();
                #endregion
            }
        }


        // 定时器事件：更新时间显示
        private void ClockTimer_Tick(object sender, EventArgs e)
        {
            // 显示当前日期和时间，格式可自定义
            lblClock.Text = DateTime.Now.ToString("HH:mm:ss");

            // 重新定位时钟到右上角
            PositionClock();
        }

        // 定位时钟到窗体右上角
        private void PositionClock()
        {
            if (lblClock != null && this.ClientSize.Width > 0 && this.ClientSize.Height > 0)
            {
                // 右上角，留出10px边距
                lblClock.Location = new Point(
                    this.ClientSize.Width - lblClock.Width - 10,
                    10
                );
            }
        }

        // 窗体大小改变时重新定位时钟
        private void BaseForm_Resize(object sender, EventArgs e)
        {
            PositionClock();
        }

  
    }
}
