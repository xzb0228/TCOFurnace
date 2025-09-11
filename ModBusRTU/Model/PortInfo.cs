using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModBusRTU.Model
{
    public class PortInfo
    {
        public string Id { get; }           //上位机程序中使用的id为 寄存器类型+序号
        public string PortId { get; }      // 所在的串口id (如"COM1", "ETH0")
        public string BoardId { get; }      // 所属控制板ID
        public RegistersType RegisterType { get; } // 寄存器类型 
        public int ChannelId { get; }     // 端口号
        public string Description { get; }   // 描述

        public PortInfo(string id, string portId, string boardId, string registerType, string channelId, string description)
        {
            Id = id;
            PortId = portId;
            BoardId = boardId;
            RegisterType = (RegistersType)Enum.Parse(typeof(RegistersType), registerType, true);
            ChannelId = int.Parse(channelId);
            Description = description;
        }
    }
}
