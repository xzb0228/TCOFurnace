using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ModBusRTU.Model
{
    public class InstrMBRegMap
    {
        /// <summary>
        /// 寄存器ID
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 寄存器名称
        /// </summary>
        public string CommType { get; set; }
        /// <summary>
        /// 寄存器名称
        /// </summary>
        public string RegName { get; set; }

        /// <summary>
        /// 通信端口
        /// </summary>
        public string Com { get; set; }
    }
}
