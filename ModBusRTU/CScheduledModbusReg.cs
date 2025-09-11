using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ModBusRTU
{
    //定时发送命令集
    public class CScheduledModbusReg: CModbusReg
    {
        public int IntervalMs { get; set; } = 1000; // 执行周期（毫秒）
        public DateTime LastSendTime { get; set; } = DateTime.MinValue;

        public CScheduledModbusReg(string _name, int _addr, CModbusCode _code, int _regstart, int _regnum, byte[] src = null) : base(_name, _addr, _code, _regstart, _regnum, src)
        {
         
        }
    }
}
