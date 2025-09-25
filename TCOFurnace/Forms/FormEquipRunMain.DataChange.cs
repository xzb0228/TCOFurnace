using System;
using System.ComponentModel;
using System.Data.Entity.Infrastructure;
using System.Drawing;
using System.Windows.Forms;
using TCOFurnace.Common;
using TCOFurnace.InstrumentsServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TCOFurnace.Forms
{
    public partial class FormEquipRunMain
    {
        private bool isAdd = false;
        //注册数据变化时 更新界面的事件
        public void AddPropertyChanged()
        {
            //避免多次注册
            if (isAdd) { return; }
            isAdd = true;
            PropertyChangedEventHandler _dataChang = delegate (object sender, PropertyChangedEventArgs e)
            {
                switch (e.PropertyName)
                {
                    case nameof(MonitoringData.Lab1_2):
                        // 处理lab1_2文本变更（例如更新对应标签）
                        lab1_2.Text = monitoringData.Lab1_2;
                        break;
                    case nameof(MonitoringData.Lab3_4):
                        // 处理lab3_4文本变更
                        lab3_4.Text = monitoringData.Lab3_4;
                        break;
                    case nameof(MonitoringData.Lab3_5):
                        // 处理lab3_5文本变更
                        lab3_5.Text = monitoringData.Lab3_5;
                        break;
                    case nameof(MonitoringData.Lab3_6):
                        // 处理lab3_6文本变更
                        if (monitoringData.Lab3_6 == "1")
                        {
                            lab3_6.Text = "1#" + LanguageManager.GetMsg("10022");
                        }
                        else if (monitoringData.Lab3_6 == "2")
                        {
                            lab3_6.Text = "2#" + LanguageManager.GetMsg("10022");
                        }
                        break;

                    case nameof(MonitoringData.Lab4_4):
                        // 处理lab4_4文本变更
                        lab4_4.Text = monitoringData.Lab4_4;
                        break;

                    case nameof(MonitoringData.Lab4_5):
                        // 处理lab4_5文本变更
                        lab4_5.Text = monitoringData.Lab4_5;
                        break;

                    case nameof(MonitoringData.Lab5_4):
                        // 处理lab5_4文本变更
                        lab5_4.Text = monitoringData.Lab5_4;
                        break;

                    case nameof(MonitoringData.Lab5_5):
                        // 处理lab5_5文本变更
                        lab5_5.Text = monitoringData.Lab5_5;
                        break;

                    case nameof(MonitoringData.Lab6_3):
                        // 处理lab6_3文本变更
                        lab6_3.Text = monitoringData.Lab6_3;
                        break;

                    case nameof(MonitoringData.Lab6_5):
                        // 处理lab6_5文本变更
                        lab6_5.Text = monitoringData.Lab6_5;
                        break;

                    case nameof(MonitoringData.LabModel):
                        // 处理型号标签文本变更
                        labModel.Text = monitoringData.LabModel;
                        if (labModel.Text == "自定义")
                        {
                            butParaSet.Visible = true;
                        }
                        else { butParaSet.Visible = false; }
                        break;

                    case nameof(MonitoringData.Lab2_2):
                        // 处理lab2_2颜色变更（例如更新标签前景色或背景色）
                        lab2_2.BackColor = monitoringData.Lab2_2; // 或使用BackColor
                        break;

                    case nameof(MonitoringData.Lab3_2):
                        // 处理lab3_2颜色变更
                        lab3_2.BackColor = monitoringData.Lab3_2;
                        break;

                    case nameof(MonitoringData.Lab4_2):
                        // 处理lab4_2颜色变更
                        lab4_2.BackColor = monitoringData.Lab4_2;
                        break;

                    case nameof(MonitoringData.Lab5_2):
                        // 处理lab5_2颜色变更
                        lab5_2.BackColor = monitoringData.Lab5_2;
                        break;

                    case nameof(MonitoringData.ButSysRun):
                        // 处理系统运行按钮颜色变更
                        butSysRun.BackColor = monitoringData.ButSysRun;

                        break;
                }
            };

            monitoringData.PropertyChanged += (s, e) =>
            {
                // 确保在UI线程执行控件操作
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() => _dataChang(s, e)));
                }
                else
                {
                    _dataChang(s, e);
                }
            };
        }

        //注册仪器属性改变事件 ，属性有变化就传给监控页面，监控页面更新UI
        public void AddPropertyChanged(MonitoringData moniData)
        {
            PropertyChangedEventHandler _dataChang = delegate (object sender, PropertyChangedEventArgs e)
            {
                //不是当前页面就不做修改
                if (monitoringData.Lab3_6 != moniData.Lab3_6)
                    return;
                switch (e.PropertyName)
                {
                    case nameof(MonitoringData.Lab1_2):
                        // 处理lab1_2文本变更（例如更新对应标签）
                        monitoringData.Lab1_2 = moniData.Lab1_2;
                        break;
                    case nameof(MonitoringData.Lab3_4):
                        // 处理lab3_4文本变更
                        monitoringData.Lab3_4 = moniData.Lab3_4;
                        break;
                    case nameof(MonitoringData.Lab3_5):
                        // 处理lab3_5文本变更
                        monitoringData.Lab3_5 = moniData.Lab3_5;
                        break;
                    case nameof(MonitoringData.Lab3_6):
                        monitoringData.Lab3_6 = moniData.Lab3_6;
                        break;
                    case nameof(MonitoringData.Lab4_4):
                        // 处理lab4_4文本变更
                        monitoringData.Lab4_4 = moniData.Lab4_4;
                        break;
                    case nameof(MonitoringData.Lab4_5):
                        // 处理lab4_5文本变更
                        monitoringData.Lab4_5 = moniData.Lab4_5;
                        break;

                    case nameof(MonitoringData.Lab5_4):
                        // 处理lab5_4文本变更
                        monitoringData.Lab5_4 = moniData.Lab5_4;
                        break;
                    case nameof(MonitoringData.Lab5_5):
                        // 处理lab5_5文本变更
                        monitoringData.Lab5_5 = moniData.Lab5_5;
                        break;
                    case nameof(MonitoringData.Lab6_3):
                        // 处理lab6_3文本变更
                        monitoringData.Lab6_3 = moniData.Lab6_3;
                        break;
                    case nameof(MonitoringData.Lab6_5):
                        // 处理lab6_5文本变更
                        monitoringData.Lab6_5 = moniData.Lab6_5;
                        break;
                    case nameof(MonitoringData.LabModel):
                        // 处理型号标签文本变更
                        monitoringData.LabModel = moniData.LabModel;
                        break;

                    case nameof(MonitoringData.Lab2_2):
                        // 处理lab2_2颜色变更（例如更新标签前景色或背景色）
                        monitoringData.Lab2_2 = moniData.Lab2_2; // 或使用BackColor
                        break;
                    case nameof(MonitoringData.Lab3_2):
                        // 处理lab3_2颜色变更
                        monitoringData.Lab3_2 = moniData.Lab3_2;
                        break;
                    case nameof(MonitoringData.Lab4_2):
                        // 处理lab4_2颜色变更
                        monitoringData.Lab4_2 = moniData.Lab4_2;
                        break;

                    case nameof(MonitoringData.Lab5_2):
                        // 处理lab5_2颜色变更
                        monitoringData.Lab5_2 = moniData.Lab5_2;
                        break;
                    case nameof(MonitoringData.ButSysRun):
                        // 处理系统运行按钮颜色变更
                        monitoringData.ButSysRun = moniData.ButSysRun;
                        break;
                }
            };

            moniData.PropertyChanged += (s, e) =>
            {
                // 确保在UI线程执行控件操作
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() => _dataChang(s, e)));
                }
                else
                {
                    _dataChang(s, e);
                }
            };
        }
    }
}

