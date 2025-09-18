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
    /// 管理所有串口，TCP,UDP
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
