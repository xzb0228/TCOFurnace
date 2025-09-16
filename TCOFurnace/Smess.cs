using ModBusRTU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCOFurnace
{
    public class Smess
    {


        //电磁阀
        public static ModbusReg WriteCoil1_1 = new ModbusReg("1路常开",10, ModbusCode.WriteCoil,0,2,new byte[2]{ 0xff, 0x00});
        public static ModbusReg WriteCoil1_2 = new ModbusReg("1路常闭",10, ModbusCode.WriteCoil,1,2,new byte[2] { 0xff, 0x00 });
        public static ModbusReg WriteCoil2_3 = new ModbusReg("2路常开",10, ModbusCode.WriteCoil, 2, 2, new byte[2] { 0xff, 0x00 });
        public static ModbusReg WriteCoil2_4 = new ModbusReg("2路常闭",10, ModbusCode.WriteCoil, 3, 2, new byte[2] { 0xff, 0x00 });

        //调压设定
        public static ModbusReg WriteReg1_1 = new ModbusReg("1氧调压", 20, ModbusCode.WriteReg, 0, 2, new byte[2] { 0x00, 0x00 });
        public static ModbusReg WriteReg1_2 = new ModbusReg("1催调压", 20, ModbusCode.WriteReg, 1, 2, new byte[2] { 0x00, 0x00 });
        public static ModbusReg WriteReg2_3 = new ModbusReg("2氧调压", 20, ModbusCode.WriteReg, 2, 2, new byte[2] { 0x00, 0x00 });
        public static ModbusReg WriteReg2_4 = new ModbusReg("2催调压", 20, ModbusCode.WriteReg, 3, 2, new byte[2] { 0x00, 0x00 });

        //流量计设定
        public static ModbusReg WriteReg1_5 = new ModbusReg("1流量计4到20mA流量设定", 20, ModbusCode.WriteReg, 4, 2, new byte[2] { 0x00, 0x00 });
        public static ModbusReg WriteReg2_6 = new ModbusReg("2流量计4到20mA流量设定", 20, ModbusCode.WriteReg , 5, 2, new byte[2] { 0x00, 0x00 });

        //流量计读取 定时发送命令
        public static TimesModbusReg ReadHolding1_1 = new TimesModbusReg("1流量计流量读", 20, ModbusCode.ReadInput, 0, 1, new byte[2] { 0x00, 0x00 });
        public static TimesModbusReg ReadHolding2_2 = new TimesModbusReg("2流量计流量读", 20, ModbusCode.ReadInput, 1, 1, new byte[2] { 0x00, 0x00 });
    }
}
