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
        public static CModbusReg WriteCoil1_1 = new CModbusReg("1路常开",10, CModbusCode.WriteCoil,0,2,new byte[2]{ 0xff, 0x00});
        public static CModbusReg WriteCoil1_2 = new CModbusReg("1路常闭",10, CModbusCode.WriteCoil,1,2,new byte[2] { 0xff, 0x00 });
        public static CModbusReg WriteCoil2_3 = new CModbusReg("2路常开",10, CModbusCode.WriteCoil, 2, 2, new byte[2] { 0xff, 0x00 });
        public static CModbusReg WriteCoil2_4 = new CModbusReg("2路常闭",10, CModbusCode.WriteCoil, 3, 2, new byte[2] { 0xff, 0x00 });

        //调压设定
        public static CModbusReg WriteReg1_1 = new CModbusReg("1氧调压", 20, CModbusCode.WriteReg, 1, 2, new byte[2] { 0x00, 0x00 });
        public static CModbusReg WriteReg1_2 = new CModbusReg("1催调压", 20, CModbusCode.WriteReg, 2, 2, new byte[2] { 0x00, 0x00 });
        public static CModbusReg WriteReg2_3 = new CModbusReg("2氧调压", 20, CModbusCode.WriteReg, 3, 2, new byte[2] { 0x00, 0x00 });
        public static CModbusReg WriteReg2_4 = new CModbusReg("2催调压", 20, CModbusCode.WriteReg, 4, 2, new byte[2] { 0x00, 0x00 });

        //流量计设定
        public static CModbusReg WriteReg1_5 = new CModbusReg("1流量计4到20mA流量设定", 20, CModbusCode.WriteReg, 5, 2, new byte[2] { 0x00, 0x00 });
        public static CModbusReg WriteReg2_6 = new CModbusReg("2流量计4到20mA流量设定", 20, CModbusCode.WriteReg , 6, 2, new byte[2] { 0x00, 0x00 });

        //流量计读取 定时发送命令
        public static CScheduledModbusReg ReadHolding1_1 = new CScheduledModbusReg("1流量计流量读", 20, CModbusCode.ReadInput, 0, 1, new byte[2] { 0x00, 0x00 });
        public static CScheduledModbusReg ReadHolding2_2 = new CScheduledModbusReg("2流量计流量读", 20, CModbusCode.ReadInput, 1, 1, new byte[2] { 0x00, 0x00 });
    }
}
