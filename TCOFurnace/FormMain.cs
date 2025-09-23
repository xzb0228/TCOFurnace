
using System;
using System.Windows.Forms;
using TCOFurnace.Forms;

namespace TCOFurnace
{
    public partial class FormMain : BaseForm
    {
        #region 在左上角连续点击7次弹框新增用户
        // 点击计数器
        private int clickCount = 0;
        private int clickCount1 = 0;
        // 记录上次点击时间
        private DateTime lastClickTime = DateTime.MinValue;
        // 定义有效点击时间窗口（毫秒）
        private int ClickTimeWindow = 1500; // 1.5秒内
        // 定义需要点击的区域（这里设置为一个200x200的区域，位于窗体左上角）
        private readonly System.Drawing.Rectangle targetArea;
        #endregion

        #region 跟踪 ctrl+H+D 快捷键
        // 用于跟踪按键状态
        private bool isCtrlPressed = false;
        private bool isHPressed = false;
        #endregion
        public FormMain()
        {
            InitializeComponent();

            // 初始化目标区域（左上角200x200的区域）
            targetArea = new System.Drawing.Rectangle(10, 10, 200, 200);
            // 为窗体添加点击事件
            this.MouseDown += SevenClickForm_MouseDown;

            // 确保窗体可以接收按键事件
            this.KeyPreview = true;
        }

        #region 跟踪 ctrl+H+D 快捷键
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            // 跟踪Ctrl键状态
            if (e.KeyCode == Keys.ControlKey)
            {
                isCtrlPressed = true;
            }
            // 跟踪H键状态（仅在Ctrl已按下时）
            else if (isCtrlPressed && e.KeyCode == Keys.T)
            {
                isHPressed = true;
                e.SuppressKeyPress = true; // 阻止系统处理此按键
            }
            // 检查D键（当Ctrl和H都已按下时）
            else if (isCtrlPressed && isHPressed && e.KeyCode == Keys.M)
            {

                FormTestMode frmHM = new FormTestMode();
                frmHM.ShowDialog();
                // 重置所有状态
                ResetKeyStates();
                e.SuppressKeyPress = true; // 阻止系统处理此按键
            }
        }
        // 重置按键状态
        private void ResetKeyStates()
        {
            isCtrlPressed = false;
            isHPressed = false;
        }
        #endregion

        /// <summary>
        /// 在左上角连续点击7次弹框新增用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SevenClickForm_MouseDown(object sender, MouseEventArgs e)
        {
            clickCount1++;
            // 检查点击是否在目标区域内
            if (targetArea.Contains(e.Location))
            {
                DateTime currentTime = DateTime.Now;

                // 检查是否在有效时间窗口内
                if (currentTime.Subtract(lastClickTime).TotalMilliseconds < ClickTimeWindow)
                {
                    // 在时间窗口内，增加计数
                    clickCount++;
                }
                else
                {
                    // 超出时间窗口，重置计数
                    clickCount = 1;
                }

                // 更新上次点击时间
                lastClickTime = currentTime;

                // 检查是否达到七次点击
                if (clickCount >= 7)
                {
                    var loginForm = new UserForm();
                    loginForm.ShowDialog();
                    // 重置计数器
                    clickCount = 0;
                }
            }
            else
            {
                // 点击不在目标区域，重置计数
                clickCount = 0;
            }
        }

        private void butManualMode_Click(object sender, EventArgs e)
        {
            FormHandMode frmHM = new FormHandMode();
            frmHM.ShowDialog();
        }

        private void butCustomize_Click(object sender, EventArgs e)
        {
            FormEquipRunMain.monitoringData.LabModel = "自定义";
            FormEquipRunMain._singEquipRunMain.ShowDialog();
        }

        private void butModel_Click(object sender, EventArgs e)
        {
            FormEquipRunMain.monitoringData.LabModel = ((Button)sender).Text;
            FormEquipRunMain._singEquipRunMain.ShowDialog();
        }

        private void butFish_Click(object sender, EventArgs e)
        {
            FormEquipRunMain.monitoringData.LabModel = ((Button)sender).Text;
            FormEquipRunMain._singEquipRunMain.ShowDialog();
        }

        private void butMeat_Click(object sender, EventArgs e)
        {

            FormEquipRunMain.monitoringData.LabModel = ((Button)sender).Text;
            FormEquipRunMain._singEquipRunMain.ShowDialog();
        }

        private void butSoil_Click(object sender, EventArgs e)
        {
            FormEquipRunMain.monitoringData.LabModel = ((Button)sender).Text;
            FormEquipRunMain._singEquipRunMain.ShowDialog();
        }

        private void butPlant_Click(object sender, EventArgs e)
        {
            FormEquipRunMain.monitoringData.LabModel = ((Button)sender).Text;
            FormEquipRunMain._singEquipRunMain.ShowDialog();
        }
    }
}
