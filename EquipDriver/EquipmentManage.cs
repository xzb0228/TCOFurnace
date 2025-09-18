using Common;
using ModBusRTU;
using ModBusRTU.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace EquipDriver
{
    /// <summary>
    /// 管理所有串口，TCP,UDP
    /// </summary>
    public static class EquipmentManage
    {
      static  List<Equipment>  equipmentList = new List<Equipment>();

        public static void CloseAll() {
            foreach (var item in equipmentList)
            {
                item.Close();
            }
        }

       // public EquipInfo equipinfo = new EquipInfo();

    }
}
