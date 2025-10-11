using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModBusRTU.Model
{
    /// <summary>
    /// 通讯方式
    /// </summary>
    public enum PortType
    {
        None = 0,
        Serial = 1, //串口232 485
        TCP = 2,   //TCP
        UDP = 3, //UDP
    }
}
