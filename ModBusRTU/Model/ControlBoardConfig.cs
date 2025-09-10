using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ModBusRTU.Model
{
    /// <summary>
    /// 控制板配置类（对应 ControlBoard 节点）
    /// </summary>
    public class ControlBoardConfig
    {
        /// <summary>
        /// 设备地址（Modbus 从站地址）
        /// </summary>
        public int DeviceAddress { get; set; }

        /// <summary>
        /// 控制板名称（如 温度控制板、电机控制板）
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 控制板名称（如 温度控制板、电机控制板）
        /// </summary>
        public string OffsetAddress { get; set; } = string.Empty;

        
        /// <summary>
        /// 线圈配置（输出位）
        /// </summary>
        public CoilsConfig Coils { get; set; } = new CoilsConfig();

        /// <summary>
        /// 离散输入配置（输入位，只读）
        /// </summary>
        public DiscreteInputsConfig DiscreteInputs { get; set; } = new DiscreteInputsConfig();

        /// <summary>
        /// 保持寄存器配置（可读可写）
        /// </summary>
        public HoldingRegistersConfig HoldingRegisters { get; set; } = new HoldingRegistersConfig();

        /// <summary>
        /// 输入寄存器配置（只读）
        /// </summary>
        public InputRegistersConfig InputRegisters { get; set; } = new InputRegistersConfig();
    }
}
