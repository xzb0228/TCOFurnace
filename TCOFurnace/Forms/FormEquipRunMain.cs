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
using TCOFurnace.InstrumentsServices;
using TCOFurnace.Models;
using TCOFurnace.UserControls;


namespace TCOFurnace.Forms
{
    public partial class FormEquipRunMain : BaseForm
    {
        #region 在左上角连续点击7次弹框输入密码
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


        //两路仪器
        public static readonly List<StateInstrument> stateInstruments = new List<StateInstrument>();
        //当前页面的数据信息
        public static readonly MonitoringData monitoringData = new MonitoringData();
        public static readonly FormEquipRunMain _singEquipRunMain = new FormEquipRunMain();

        //当前仪器编号
        private FormEquipRunMain()
        {
            InitializeComponent();
            //给页面赋初始值 并且注册页面数据变化更新事件 
            AddPropertyChanged();

            //加载两路反应管仪器-命令
            ParseequipmentMBReg();

            //仪器属性有变化通知页面
            stateInstruments.ForEach(instr => AddPropertyChanged(instr.monitoringData));

            // 初始化目标区域（左上角200x200的区域）
            targetArea = new System.Drawing.Rectangle(10, 10, 200, 200);
            // 为窗体添加点击事件
            this.MouseDown += SevenClickForm_MouseDown;
        }

        /// <summary>
        /// 在左上角连续点击7次弹框输入密码
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
                    var passwordForm = new PasswordForm();
                    DialogResult result = passwordForm.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        butParaSet.Visible = true;
                    }
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

        /// <summary>
        /// 模式选择页面进入 加载业务数据
        /// </summary>
        public void SetCOFRunningMode(TCOFRunningMode tCOF) {
            foreach (var item in stateInstruments) {
                //初始化状态时可以将模式覆盖上一次进入时的模式
                if (item.CurrentState is InitializingState)
                {
                    item.tCOFRunningMode = tCOF;
                    item.monitoringData.LabModel = item.tCOFRunningMode.ModeName;
                    //monitoringData.Lab3_6 = item.monitoringData.Lab3_6;
                    //修改监控页面 整体状态
                    CopyMonitoringData(monitoringData, item.monitoringData);
                    break;
                }
            }
        }

        private void but2_6_Click(object sender, EventArgs e)
        {
            if (monitoringData.Lab3_6 == "1")
            {
                CopyMonitoringData(monitoringData, stateInstruments.FirstOrDefault(c => c.monitoringData.Lab3_6 == "2")?.monitoringData);
            }
            else if (monitoringData.Lab3_6 == "2")
            {
                CopyMonitoringData(monitoringData, stateInstruments.FirstOrDefault(c => c.monitoringData.Lab3_6 == "1")?.monitoringData);
            }
        }

        private void butParaSet_Click(object sender, EventArgs e)
        {
            FormParaSet formParaSet = new FormParaSet();
            formParaSet.ShowDialog();
        }

        private void CopyMonitoringData(MonitoringData tar, MonitoringData Res)
        {
            if (Res == null)
                return;
            //状态值
            tar.Lab1_2 = Res.Lab1_2;//步骤信息
            tar.Lab3_4 = Res.Lab3_4;//流量(L/min) 设定值
            tar.Lab3_5 = Res.Lab3_5;//流量(L/min) 检测值
            tar.Lab3_6 = Res.Lab3_6;//系统名称
            tar.Lab4_4 = Res.Lab4_4;//氧化区温度(℃) 设定值
            tar.Lab4_5 = Res.Lab4_5;//氧化区温度(℃) 检测值
            tar.Lab5_4 = Res.Lab5_4;//催化区温度(℃) 设定值
            tar.Lab5_5 = Res.Lab5_5;//催化区温度(℃) 检测值
            tar.Lab6_3 = Res.Lab6_3;//设置时间(min)
            tar.Lab6_5 = Res.Lab6_5;//运行时间(min)
            tar.LabModel = Res.LabModel;//当前模式

            //颜色
            tar.Lab2_2 = Res.Lab2_2;//流量计
            tar.Lab3_2 = Res.Lab3_2;//氧/氩气
            tar.Lab4_2 = Res.Lab4_2;//氧化区 状态展示
            tar.Lab5_2 = Res.Lab5_2;//催化区 状态展示
            tar.ButSysRun = Res.ButSysRun;//系统运行状态
        }

        private void ParseequipmentMBReg()
        {
            foreach (var itme in GlobalPara.upperComputerConfig.instruments)
            {
                StateInstrument stateInstrument = new StateInstrument();
                if (stateInstrument.equipmentMBReg == null)
                {
                    stateInstrument.equipmentMBReg = new InstrumentMBReg();
                }
                if (itme.InstrumentId == "1")
                {
                    stateInstrument.monitoringData.Lab3_6 = "1";
                    stateInstrument.equipmentMBReg.K1 = Smess.WriteCoil1_1.Clone();
                    stateInstrument.equipmentMBReg.K2 = Smess.WriteCoil1_2.Clone();
                    stateInstrument.equipmentMBReg.OVol = Smess.WriteReg1_1.Clone();
                    stateInstrument.equipmentMBReg.CVol = Smess.WriteReg1_2.Clone();
                    stateInstrument.equipmentMBReg.Flow = Smess.WriteReg1_5.Clone();
                    stateInstrument.equipmentMBReg.FlowRed = Smess.ReadHolding1_1.Clone();
                    stateInstrument.equipmentMBReg.TempRed = Smess.CReadHolding1_1.Clone();
                }
                else if (itme.InstrumentId == "2")
                {
                    stateInstrument.monitoringData.Lab3_6 = "2";
                    stateInstrument.equipmentMBReg.K1 = Smess.WriteCoil2_3.Clone();
                    stateInstrument.equipmentMBReg.K2 = Smess.WriteCoil2_4.Clone();
                    stateInstrument.equipmentMBReg.OVol = Smess.WriteReg2_3.Clone();
                    stateInstrument.equipmentMBReg.CVol = Smess.WriteReg2_4.Clone();
                    stateInstrument.equipmentMBReg.Flow = Smess.WriteReg2_6.Clone();
                    stateInstrument.equipmentMBReg.FlowRed = Smess.ReadHolding2_2.Clone();
                    stateInstrument.equipmentMBReg.TempRed = Smess.CReadHolding2_1.Clone();
                }

                stateInstruments.Add(stateInstrument);

            }
        }

        private void butInitializing_Click(object sender, EventArgs e)
        {
            //找到当前仪器 初始化
            stateInstruments.FirstOrDefault(c => c.monitoringData.Lab3_6 == monitoringData.Lab3_6)?.Initialize();
        }

        private void butStopped_Click(object sender, EventArgs e)
        {
            //找到当前仪器 停止
            stateInstruments.FirstOrDefault(c => c.monitoringData.Lab3_6 == monitoringData.Lab3_6)?.Stop();
        }

        private void butRunning_Click(object sender, EventArgs e)
        {
            //找到当前仪器 启动
            stateInstruments.FirstOrDefault(c => c.monitoringData.Lab3_6 == monitoringData.Lab3_6)?.Start();
        }
    }
}
