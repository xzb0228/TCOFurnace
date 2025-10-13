using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EquipDriver
{
    public class InstrumentConfig
    {
        /// <summary>
        /// 串口信息
        /// </summary>
        public List<PortConfig> Ports { get; set; } = new List<PortConfig>();
        /// <summary>
        /// 仪器信息
        /// </summary>
        public List<Instrument> instruments { get; set; } = new List<Instrument>();


        /// <summary>
        /// 索引器：通过串口号（如"COM1"）获取对应的串口配置
        /// </summary>
        /// <param name="portName">串口号（区分大小写）</param>
        /// <returns>对应的串口配置，不存在则返回null</returns>

        public PortConfig this[string PortName]
        {
            get
            {
                // 查找匹配的串口号（精确匹配，区分大小写）
                return Ports.FirstOrDefault(c=>c.PortName == PortName);
            }
        }
    }
}
