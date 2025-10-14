using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TCOFurnace.BusinessModels
{
    public class InstrumentSetting
    {
        /// <summary>
        /// 设置名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 设置值
        /// </summary>
        public string Value { get; set; }
    }
}
