using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModBusRTU.Model
{
    /// <summary>
    /// 寄存器类型
    /// </summary>
    public enum RegistersType
    {
        Coils = 1,             //0x01:线圈
        DiscreteInputs=2,   //0x02:离散输入
        HoldingRegisters=3, //0x03:保持寄存器
        InputRegisters= 4   //0x03:输入寄存器
    }
}
