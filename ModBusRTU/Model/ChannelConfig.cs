using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ModBusRTU.Model
{
    /// <summary>
    /// 通道配置类（对应 Channel 节点，原 Register 节点改造）
    /// </summary>
    public class ChannelConfig
    {
        /// <summary>
        /// 通道编号（对应寄存器地址，从 0 开始）
        /// </summary>
        [XmlAttribute("id")]
        public int Id { get; set; }

        /// <summary>
        /// 实际映射的 通道编号（如 目标温度设定、当前转速反馈）
        /// </summary>
        [XmlAttribute("mapping")]
        public string Mapping { get; set; } = string.Empty;

        /// <summary>
        /// 通道名称（如 目标温度设定、当前转速反馈）
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 回参单位（如 ℃、rpm、A，无单位则为空）
        /// </summary>
        [XmlAttribute("unit")]
        public string Unit { get; set; } = string.Empty;

        /// <summary>
        /// 回参进制（Decimal 十进制 / Hexadecimal 十六进制）
        /// </summary>
        [XmlAttribute("dataFormat")]
        public string DataFormat { get; set; } = "1";
    }
}
