using Microsoft.VisualBasic.Devices;
using ModBusRTU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EquipDriver
{
    public class ModbusCommands
    {
        List<ModbusReg> modbusRegs = new List<ModbusReg>();
        List<TimesModbusReg> modbusRegsTimes = new List<TimesModbusReg>();
        List<OneTimeModbusReg> oneTimeModbusReg = new List<OneTimeModbusReg>();

        //本系统只有一个串口
        public ModbusReg this[string regName,string portName= "Port1"]
        {
            get
            {
                ModbusReg reg = modbusRegs.FirstOrDefault(x => x.name == regName && x.portName== portName);

                if (reg == null)
                {
                    reg = modbusRegsTimes.FirstOrDefault(x => x.name == regName && x.portName == portName);
                }
                if (reg == null)
                {
                    reg = oneTimeModbusReg.FirstOrDefault(x => x.name == regName && x.portName == portName);
                }
                if (EquipmentManager.IsEmulatorMode)
                {
                    reg= reg.Clone();

                    //仿真模式 处理对数据的处理
                    reg.IsEmulatorMode = true;

                    //数据处理;
                }

                return reg.Clone();
            }
        }
    }
}
