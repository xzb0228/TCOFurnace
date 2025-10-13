using ModBusRTU;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EquipDriver
{
    /// <summary>
    /// 串口配置类（对应 SerialPort 节点）
    /// </summary>
    public class PortConfig
    {
        /// <summary>
        /// 串口类型 串口，TCP,UDP
        /// </summary>
        public PortType PortType { get; set; } = PortType.None;

        /// <summary>
        /// 串口id
        /// </summary>
        public string PortName { get; set; } = string.Empty;

        /// <summary>
        /// 串口号（如 COM1、COM2）
        /// </summary>
        public string Com { get; set; } = string.Empty;

        /// <summary>
        /// 波特率（如 9600、19200）
        /// </summary>
        public int BaudRate { get; set; } = 9600;

        /// <summary>
        /// 校验位（None/Even/Odd）
        /// </summary>
        public string Parity { get; set; } = "None";

        /// <summary>
        /// 数据位（通常为 8）
        /// </summary>
        public int DataBits { get; set; } = 8;

        /// <summary>
        /// 停止位（通常为 1）
        /// </summary>
        public int StopBits { get; set; } = 1;

        /// <summary>
        /// 端口描述
        /// </summary>
        public string Describe { get; set; } = "";

        #region TCP UDP
        public string IP { get; set; } = "";
        public int PortNum { get; set; } = 0;
        public int LocalPort { get; set; } = 0;

        #endregion

    }
}
