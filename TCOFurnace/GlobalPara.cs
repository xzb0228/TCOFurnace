using EquipDriver;
using ModBusRTU;
using ModBusRTU.Model;
using System.Collections.Generic;
using System.IO.Ports;
using System.Web.UI.WebControls;
using TCOFurnace.Models;

namespace TCOFurnace
{
    internal static class GlobalPara
    {
        /// <summary>
        /// 登录用户信息
        /// </summary>
        private static User _currentUser;

        /// <summary>
        /// 登录用户信息
        /// </summary>
        public static User CurrentUser
        {
            get { return _currentUser; }
            set { _currentUser = value; }
        }

        /// <summary>
        /// 是否启动页面控件权限
        /// </summary>
        public static bool isOpenPermission=false;

        /// <summary>
        /// 初始化全局变量 如退出时
        /// </summary>
        public static void Init()
        {
            CurrentUser = null;
            upperComputerConfig = null;
            instrumentConfig = null;
            ModbusCommands = null;
        }

        public static UpperComputerConfig upperComputerConfig = new UpperComputerConfig();

        public static List<InstrumentConfig> instrumentConfig = new List<InstrumentConfig>();

        public static List<ModbusReg> ModbusCommands=new List<ModbusReg>();

        public static Equipment deviceProtocol = new Equipment();
    }
}
