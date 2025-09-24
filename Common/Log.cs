using log4net;

namespace Common
{
    public class Loger
    {
        public static ILog GetLogger(string loggerName)
        {
            return LogManager.GetLogger(loggerName);
        }

        public static void Debug(string msg,string loger= "logger") {
            ILog logger = GetLogger(loger);
            logger.Debug(msg);
        }
        public static void Info(string msg, string loger = "logger")
        {
            ILog logger = GetLogger(loger);
            logger.Info(msg);
        }
        public static void Warn(string msg, string loger = "logger")
        {
            ILog logger = GetLogger(loger);
            logger.Warn(msg);
        }
        public static void Error(string msg, string loger = "logger")
        {
            ILog logger = GetLogger(loger);
            logger.Error(msg);
        }
        public static void Fatal(string msg, string loger = "logger")
        {
            ILog logger = GetLogger(loger);
            logger.Fatal(msg);
        }
    }
}
