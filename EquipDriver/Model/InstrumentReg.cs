using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EquipDriver
{
    //仪器类中所有命令集合
    public class InstrumentReg
    {
        /// <summary>
        /// 对应业务类中的字段名称
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 命令名称
        /// </summary>
        public string RegName { get; set; }

        /// <summary>
        /// 通信端口
        /// </summary>
        public string portName { get; set; }
    }
}
