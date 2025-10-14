using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipDriver.Model
{
    public class ParametsBase
    {
        /// <summary>
        /// 串口类型 串口，TCP,UDP
        /// </summary>
        public PortType PortType { get; set; } = PortType.None;

        /// <summary>
        /// 串口id
        /// </summary>
        public string PortName { get; set; } = string.Empty;
    }
}
