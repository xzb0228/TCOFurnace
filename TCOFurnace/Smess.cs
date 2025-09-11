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
        public static CModbusReg CM2= new CModbusReg("2路常开",10, CModbusCode.WriteCoil,1,2,new byte[2]{ 0xff, 0x00});
    }
}
