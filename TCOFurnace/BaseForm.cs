using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TCOFurnace.Common;

namespace TCOFurnace
{
    public class BaseForm : Form
    {
        // 时钟控件
        protected Label lblClock;
        // 定时器用于更新时间
        private Timer clockTimer;
        public BaseForm()
        {
            if (this.Name != "UserForm" && this.Name != "LoginForm" && !DesignMode)
            {
                lblClock = new Label();
                lblClock.AutoSize = true;
                lblClock.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
                lblClock.TextAlign = ContentAlignment.MiddleRight;
                lblClock.ForeColor = SystemColors.ControlText;

                // 将时钟添加到窗体
                this.Controls.Add(lblClock);

                // 确保窗体大小改变时重新定位时钟
                this.Resize += BaseForm_Resize;

                #region 给每个界面加一个时钟
                // 初始化定时器
                clockTimer = new Timer();
                clockTimer.Interval = 1000; // 每秒更新一次
                clockTimer.Tick += ClockTimer_Tick;
                clockTimer.Start();
                #endregion
            }
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // 默认为中文 不为中文就更换
            if (LanguageManager.CurrentLanguage != "zh-CN")
                LanguageManager.UpdateFormLanguage(this);

            // 是否启动页面控件权限 默认不启用
            if (GlobalPara.IsOpenPermission)
                PermissionManager.UpdateFormConPermissions(this);
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

        // 窗体关闭时释放定时器资源
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (clockTimer != null)
            {
                clockTimer.Stop();
                clockTimer.Dispose();
            }
        }

    }
}
