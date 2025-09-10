using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EquipDriver
{
    public static class CEquipDelegateEvent
    {
        //声明一个delegate（委托）类型：testDelegate，该类型可以搭载返回值为空，参数只有一个(long型)的方法。  
        public delegate void DIDOInfoDelegate(string opr);

        //声明一个testDelegate类型的对象。该对象代表了返回值为空，参数只有一个(long型)的方法。它可以搭载N个方法。  
        public static DIDOInfoDelegate DIDOInfoThread;

        //声明一个delegate（委托）类型：testDelegate，该类型可以搭载返回值为空，参数只有一个(long型)的方法。  
        public delegate void AIInfoDelegate(string opr);

        //声明一个testDelegate类型的对象。该对象代表了返回值为空，参数只有一个(long型)的方法。它可以搭载N个方法。  
        public static AIInfoDelegate AIInfoThread;

        //声明一个delegate（委托）类型：testDelegate，该类型可以搭载返回值为空，参数只有一个(long型)的方法。  
        public delegate void AOInfoDelegate(string opr);

        //声明一个testDelegate类型的对象。该对象代表了返回值为空，参数只有一个(long型)的方法。它可以搭载N个方法。  
        public static AOInfoDelegate AOInfoThread;

        //声明一个delegate（委托）类型：testDelegate，该类型可以搭载返回值为空，参数只有一个(long型)的方法。  
        public delegate void ParamInfoDelegate(string opr, byte[] info);

        //声明一个testDelegate类型的对象。该对象代表了返回值为空，参数只有一个(long型)的方法。它可以搭载N个方法。  
        public static ParamInfoDelegate ParamInfoThread;

        //声明一个delegate（委托）类型：testDelegate，该类型可以搭载返回值为空，参数只有一个(long型)的方法。  
        public delegate void EquipChgDelegate();

        //声明一个testDelegate类型的对象。该对象代表了返回值为空，参数只有一个(long型)的方法。它可以搭载N个方法。  
        public static EquipChgDelegate EquipChgThread;

    }
}
