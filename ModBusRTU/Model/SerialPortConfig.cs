using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ModBusRTU.Model
{
    /// <summary>
    /// 串口配置类（对应 SerialPort 节点）
    /// </summary>
    public class SerialPortConfig
    {
        /// <summary>
        /// 串口号（如 COM1、COM2）
        /// </summary>
        public string Id { get; set; } = string.Empty;

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
        /// 当前串口下连接的所有控制板
        /// </summary>
        public List<ControlBoardConfig> ControlBoards { get; set; } = new List<ControlBoardConfig>();

        /// <summary>
        /// 索引器：板子名称获取某一块板子
        /// </summary>
        /// <param name="Name">串口号（区分大小写）</param>
        public ControlBoardConfig this[string Name]
        {
            get
            {
                // 查找匹配的串口号（精确匹配，区分大小写）
                return ControlBoards.FirstOrDefault(sp => sp.Name == Name);
            }
        }
    }
}
