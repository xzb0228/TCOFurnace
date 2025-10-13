using ModBusRTU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipDriver
{
    public class ModbusCommands
    {
        List<ModbusReg> modbusRegs = new List<ModbusReg>();
        List<TimesModbusReg> modbusRegsTimes = new List<TimesModbusReg>();
        List<OneTimeModbusReg> oneTimeModbusReg = new List<OneTimeModbusReg>();
    }
}
