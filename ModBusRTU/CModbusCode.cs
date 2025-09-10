using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModBusRTU
{
    /// <summary>
    /// 对应功能码
    /// </summary>
    public enum CModbusCode
    {
        None = 0,         //非法指令
        ReadCoil,         //0x01:读线圈
        ReadDI,            //0x02:读离散输入
        ReadHolding,       //0x03:读保持寄存器
        ReadInput,         //0x04:读只读寄存器状态
        WriteCoil,         //0x05:写单个线圈
        WriteReg,          //0x06:写单个保持寄存器
        WriteCoils = 15,   //0x0F:写多个线圈寄存器
        WriteRegs = 16,    //0x10:写多个保持寄存器
        Error = 100,
        Wait
    }
}
