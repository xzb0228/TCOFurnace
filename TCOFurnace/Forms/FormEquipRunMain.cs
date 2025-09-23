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
using TCOFurnace.UserControls;


namespace TCOFurnace.Forms
{
    public partial class FormEquipRunMain : BaseForm
    {
        //当前页面的数据信息
        public static readonly MonitoringData monitoringData = new MonitoringData();

        public static readonly FormEquipRunMain _singEquipRunMain = new FormEquipRunMain();

        private FormEquipRunMain()
        {
            InitializeComponent();
            //给页面赋初始值 并且注册页面数据变化更新事件 

            AddPropertyChanged();
            monitoringData.InitializeDefaults();

        }

        private void but2_6_Click(object sender, EventArgs e)
        {
            if (monitoringData.Lab3_6 == "1")
            {
                monitoringData.Lab3_6 = "2";
            }
            else if (monitoringData.Lab3_6 == "2")
            {
                monitoringData.Lab3_6 = "1";
            }
        }

        private void butParaSet_Click(object sender, EventArgs e)
        {
            FormParaSet formParaSet   = new FormParaSet();
            formParaSet.ShowDialog();
        }
    }
}
