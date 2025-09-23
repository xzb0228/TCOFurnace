using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ModBusRTU.Model
{
    public class UpperComputerConfig
    {
        public List<SerialPortConfig> SerialPorts { get; set; } = new List<SerialPortConfig>();

        public List<Instrument> instruments { get; set; } = new List<Instrument>();


        /// <summary>
        /// 索引器：通过串口号（如"COM1"）获取对应的串口配置
        /// </summary>
        /// <param name="portName">串口号（区分大小写）</param>
        /// <returns>对应的串口配置，不存在则返回null</returns>
        public Instrument this[string com]
        {
            get
            {
                // 查找匹配的串口号（精确匹配，区分大小写）
                return instruments.FirstOrDefault(sp => sp.InstrumentId == com);
            }
        }

        public SerialPortConfig this[int com]
        {
            get
            {
                // 查找匹配的串口号（精确匹配，区分大小写）
                return SerialPorts.FirstOrDefault();
            }
        }
    }
}
