using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ModBusRTU
{
    //循环执行命令
    public class TimesModbusReg : ModbusReg
    {
        //不能通过无参构造函数来实例化对象
        private TimesModbusReg() 
        {

        }
        public int IntervalMs { get; set; } = 1000; // 执行周期（毫秒）
        public DateTime LastSendTime { get; set; } = DateTime.MinValue;

        public TimesModbusReg(ModbusReg modbusReg) :
          base(modbusReg.portName, modbusReg.name, modbusReg.addr, modbusReg.code, modbusReg.regstart, modbusReg.regnum,  modbusReg.vbyte, modbusReg.ResponseData)
        {
        }
        public TimesModbusReg(string portName, string _name, int _addr, ModbusCode _code, int _regstart, int _regnum, byte[] src = null, int intervalMs = 1000) : base(portName, _name, _addr, _code, _regstart, _regnum, src)
        {
            if (intervalMs < 30)
            {
                Loger.Error($"循环执行命令 输入执行周期（毫秒）不能小于30ms");
                // 抛出异常，包含具体的错误信息
                throw new ArgumentException($"循环执行命令 输入执行周期（毫秒）不能小于30ms");
            }
            IntervalMs = intervalMs;
        }

        /// <summary>
        /// 每次获取命令的深拷贝
        /// </summary>
        /// <returns></returns>
        public new TimesModbusReg Clone()
        {
            // 1. 先克隆基类部分
            TimesModbusReg baseClone = new TimesModbusReg(base.Clone());
            baseClone.IntervalMs = this.IntervalMs;
            baseClone.LastSendTime = this.LastSendTime;
            return baseClone;
        }
    }
}
