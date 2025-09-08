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
        public static bool IsOpenPermission=false;

        /// <summary>
        /// 初始化全局变量 如退出时
        /// </summary>
        public static void Init()
        {
            CurrentUser = null;
        }
    }
}
