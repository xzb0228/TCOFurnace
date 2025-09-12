using log4net;

namespace Common
{
    public class Loger
    {
        private static readonly ILog logger = LogManager.GetLogger("logger");
        public static readonly ILog watcher = LogManager.GetLogger("watcher");

        public static void Debug(string msg) {
            logger.Debug(msg);
        }
        public static void Info(string msg)
        {
            logger.Info(msg);
        }
        public static void Warn(string msg)
        {
            logger.Warn(msg);
        }
        public static void Error(string msg)
        {
            logger.Error(msg);
        }
        public static void Fatal(string msg)
        {
            logger.Fatal(msg);
        }
    }
}
