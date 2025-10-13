using EquipDriver;
using ModBusRTU;
using System.Collections.Generic;
using System.IO.Ports;
using System.Web.UI.WebControls;
using TCOFurnace.Models;

namespace TCOFurnace
{
    internal static class GlobalPara
    {
        //通过 Mutex(互斥体) GUID保证程序只可以被打开一次
        public static readonly string MutexName = "1F4B8A7C-7A5E-4F3A-9B8D-2E7C9D8A7B6F";
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
        ///  是否仿真模式，没有连接实际仪器时使用
        /// </summary>
        public static bool IsEmulatorMode = false;

        /// <summary>
        /// 初始化全局变量 如退出时
        /// </summary>
        public static void Init()
        {
            CurrentUser = null;
            instrumentConfig = null;
            IsEmulatorMode = false;
        }

        public static InstrumentConfig instrumentConfig = new InstrumentConfig();

        //所有命令
        public static ModbusCommands Regs { get; set; } = new ModbusCommands();
        // public static Equipment deviceProtocol = new Equipment();
    }
}
