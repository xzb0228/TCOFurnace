using Common;
using EquipDriver;
using ModBusRTU.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.SessionState;
using System.Windows.Forms;
using TCOFurnace.Common;
using TCOFurnace.DataService;

namespace TCOFurnace
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            // 注册 UI 线程异常处理事件
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //中英文转换 语言包初始化
            LanguageManager.Initialize();

            //页面控件权限配置
            PermissionManager.Initialize();

            //数据库升级更新
            if (!DatabaseUpgrader.Upgrader()) {
                MessageBox.Show(LanguageManager.GetMsg("10007"));
                return;
            }

            //上位机配置 有几个串口，每个串口有几块板子等
            if (!ConfigSerializerManager.InitConfig())
            {
                MessageBox.Show(LanguageManager.GetMsg("10008"));
                return;
            }

            // 先启动登录窗口
            using (var loginForm = new LoginForm())
            {
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    //启动命令轮询发布程序
                    CEquipServer.InitEvent();

                    // 启动主窗口（传递用户上下文）
                    Application.Run(new FormMain());
                    GlobalPara.CurrentUser = null;
                }
            }
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            Log.Error("ThreadException未处理异常:" + e.Exception.Message);
        }
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            if (ex == null) return;
            Log.Error("UnhandledException未处理异常:" + ex.Message);
        }
    }
}
