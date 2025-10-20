using Common;
using EquipDriver;
using System;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TCOFurnace.Common;
using TCOFurnace.DataService;
using TCOFurnace.Forms;

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
            // 程序已经在运行，不允许重复打开
            bool isNewInstance;
            new Mutex(true, GlobalPara.MutexName, out isNewInstance);
            if (!isNewInstance)
            {
                MessageBox.Show("程序已经在运行，不允许重复打开");
                return;
            }

            // 注册 UI 线程异常处理事件
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            // 捕获Task未观察异常
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //中英文转换 语言包初始化
            LanguageManager.Initialize();

            //页面控件权限配置
            PermissionManager.Initialize();

            //数据库升级更新
            if (!DatabaseUpgrader.Upgrader())
            {
                MessageBox.Show(LanguageManager.GetMsg("10007"));
                return;
            }

            //上位机配置 所有串口
            if (!ConfigSerializerManager.InitConfig())
            {
                MessageBox.Show(LanguageManager.GetMsg("10008"));
                return;
            }

            //是否启动仿真模式
            GlobalPara.IsEmulatorMode = ConfigurationManager.AppSettings["IsEmulatorMode"].ToUpper() == "TRUE" ? true : false;

            //启动仿真模式给出弹框提示
            if (GlobalPara.IsEmulatorMode)
            {
                MessageBox.Show(LanguageManager.GetMsg("10024"));
            }

            //初始化所有串口
            EquipmentManager.Init(GlobalPara.instrumentConfig.ParametsBases, GlobalPara.IsEmulatorMode);

            // 先启动登录窗口
            using (var loginForm = new LoginForm())
            {
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    //启动所有串口开始发送命令
                    EquipmentManager.InitEvent();

                    //定时发送命令集添加到 deviceProtocol中

                    //GlobalPara.deviceProtocol.equipinfo.timesModbusReg.Add(GlobalPara.Regs["1流量计流量读"]);
                    //GlobalPara.deviceProtocol.equipinfo.timesModbusReg.Add(GlobalPara.Regs["2流量计流量读"]);
                    //GlobalPara.deviceProtocol.equipinfo.timesModbusReg.Add(GlobalPara.Regs["1路温度回传"]);

                    // 启动主窗口（传递用户上下文）
                    Application.Run(new FormMain());

                    //注销所有资源
                    GlobalPara.Init();

                }
            }
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            Loger.Error("ThreadException未处理异常:" + e.Exception.Message, exc: e.Exception);

            //将所有反应管置于初始态避免主页面关闭反应管还在加热
            FormEquipRunMain.stateInstruments.ForEach(instr =>
            {
                instr.InitPort();
            });

            //等待关闭加热的命令执行完
            Thread.Sleep(1500);
        }
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            if (ex == null) return;
            Loger.Error("UnhandledException未处理异常:" + ex.Message, exc: ex);

            //将所有反应管置于初始态避免主页面关闭反应管还在加热
            FormEquipRunMain.stateInstruments.ForEach(instr =>
            {
                instr.InitPort();
            });

            //等待关闭加热的命令执行完
            Thread.Sleep(1500);
        }
        private static void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            Exception ex = e.Exception;
            if (ex == null) return;
            Loger.Error("UnobservedTaskException未处理异常:" + ex.Message, exc: ex);

            //将所有反应管置于初始态避免主页面关闭反应管还在加热
            FormEquipRunMain.stateInstruments.ForEach(instr =>
            {
                instr.InitPort();
            });

            //等待关闭加热的命令执行完
            Thread.Sleep(1500);
        }
    }
}
