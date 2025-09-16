using Common;
using ModBusRTU;
using ModBusRTU.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace EquipDriver
{
    /// <summary>
    /// 表示一套完整的协议
    /// </summary>
    public class EquipmentManage
    {
        //不允许使用无参构造函数
        public EquipmentManage() { 
        }
        public EquipmentManage(IEquipDriver iEquipDriver)
        {
           
        }
        public EquipInfo equipinfo = new EquipInfo();

    }
}
