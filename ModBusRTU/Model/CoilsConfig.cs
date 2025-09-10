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
    /// 线圈配置类（对应 Coils 节点）
    /// </summary>
    public class CoilsConfig
    {
        /// <summary>
        /// 线圈总数量
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 保持寄存器对应的通道列表（原 Register 节点改为 Channel 节点）
        /// </summary>
        public List<ChannelConfig> Channels { get; set; } = new List<ChannelConfig>();

        /// <summary>
        /// 索引器：通过串口号（如"COM1"）获取对应的串口配置
        /// </summary>
        /// <param name="portName">串口号（区分大小写）</param>
        /// <returns>对应的串口配置，不存在则返回null</returns>
        public ChannelConfig this[int portName]
        {
            get
            {
                // 查找匹配的串口号（精确匹配，区分大小写）
                return Channels.FirstOrDefault(sp => sp.Id == portName);
            }
        }
    }
}
