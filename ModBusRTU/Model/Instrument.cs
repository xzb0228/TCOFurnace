using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ModBusRTU.Model
{
    public class Instrument
    {
        /// <summary>
        /// 仪器ID
        /// </summary>
        public string InstrumentId { get; set; }

        /// <summary>
        /// 仪器名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 仪器设置列表
        /// </summary>
        public List<InstrumentSetting> Settings { get; set; } = new List<InstrumentSetting>();
    }
}
