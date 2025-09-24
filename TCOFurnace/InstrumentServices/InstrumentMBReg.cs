using ModBusRTU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCOFurnace.InstrumentsServices
{
    /// <summary>
    /// 一台仪器所控制的所有部件与监听部件的 ModBus命令集合 还是将其写到配置文件中
    /// </summary>
    public class InstrumentMBReg
    {
        /// <summary>
        /// 第一路电磁阀
        /// </summary>
        public ModbusReg K1 { get; set; }

        /// <summary>
        /// 第二路电磁阀
        /// </summary>
        public ModbusReg K2 { get; set; }

        /// <summary>
        /// 氧化区电压--通过控制输出电流
        /// </summary>
        public ModbusReg OVol { get; set; }

        /// <summary>
        /// 催化区电压--通过控制输出电流
        /// </summary>
        public ModbusReg CVol { get; set; }

        /// <summary>
        /// 流量计设置调节
        /// </summary>
        public ModbusReg Flow { get; set; }

        /// <summary>
        /// 流量计读取--循环执行命令
        /// </summary>
        public TimesModbusReg FlowRed { get; set; }

        /// <summary>
        /// 氧化区温度读取--循环执行命令
        /// </summary>
        public TimesModbusReg TempRed { get; set; }
    }
}
