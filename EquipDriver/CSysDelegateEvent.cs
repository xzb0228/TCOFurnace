using ModBusRTU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipDriver
{
    public static class CSysDelegateEvent
    {
        //声明一个delegate（委托）类型：testDelegate，该类型可以搭载返回值为空，参数只有一个(long型)的方法。  
        public delegate void SerialSendDelegate(byte[] info, int startindex = 0, int infolen = 0, bool isrcv = false);

        //声明一个testDelegate类型的对象。该对象代表了返回值为空，参数只有一个(long型)的方法。它可以搭载N个方法。  
        public static SerialSendDelegate SerialSendThread;
        public delegate void SerialRcvDelegate(byte[] info, int startindex = 0, int infolen = 0, bool isrcv = true);

        //声明一个testDelegate类型的对象。该对象代表了返回值为空，参数只有一个(long型)的方法。它可以搭载N个方法。  
        public static SerialRcvDelegate SerialRcvThread;

        public delegate void LogInfoDelegate(string info);

        //声明一个testDelegate类型的对象。该对象代表了返回值为空，参数只有一个(long型)的方法。它可以搭载N个方法。  
        public static LogInfoDelegate LogInfoThread;
        //声明一个delegate（委托）类型：testDelegate，该类型可以搭载返回值为空，参数只有一个(long型)的方法。  
        public delegate void DebugInfoDelegate(string info);

        //声明一个testDelegate类型的对象。该对象代表了返回值为空，参数只有一个(long型)的方法。它可以搭载N个方法。  
        public static DebugInfoDelegate DebugInfoThread;

        //声明一个delegate（委托）类型：testDelegate，该类型可以搭载返回值为空，参数只有一个(long型)的方法。
        public delegate void StatusInfoDelegate(string info);

        //声明一个testDelegate类型的对象。该对象代表了返回值为空，参数只有一个(long型)的方法。它可以搭载N个方法。  
        public static StatusInfoDelegate StatusInfoThread;

        //声明一个delegate（委托）类型：testDelegate，该类型可以搭载返回值为空，参数只有一个(long型)的方法。  
        public delegate void ProgressInfoDelegate(int value);

        //声明一个testDelegate类型的对象。该对象代表了返回值为空，参数只有一个(long型)的方法。它可以搭载N个方法。  
        public static ProgressInfoDelegate ProgressInfoThread;

        //声明一个delegate（委托）类型：testDelegate，该类型在ModBus命令有返回值的时候触发。  
        public delegate void ReciveCModbusRegDelegate(CModbusReg reg);

        //声明一个testDelegate类型的对象。该类型在ModBus命令有返回值的时候触发。  
        public static ReciveCModbusRegDelegate ReciveCModbusRegThread;

        public static void ShowDebugInfo(string info)
        {
            DebugInfoThread?.Invoke(info);
        }

        public static void ShowStatusInfo(string info)
        {
            StatusInfoThread?.Invoke(info);
        }

        public static void ShowProgressInfo(int value)
        {
            ProgressInfoThread?.Invoke(value);
        }
    }
}
