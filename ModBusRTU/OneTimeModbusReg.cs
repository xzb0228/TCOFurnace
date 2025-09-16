using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ModBusRTU
{
    //定点时间执行命令 只执行一次
    public class OneTimeModbusReg : ModbusReg
    {
        private OneTimeModbusReg() { }

        /// <summary>
        /// 设定的执行时间
        /// </summary>
        public DateTime SendTime { get; set; } = DateTime.MinValue;

        /// <summary>
        /// 真实的入执行队列时间
        /// </summary>
        public DateTime? LastSendTime { get; set; } = null;

        public OneTimeModbusReg(string _name, int _addr, ModbusCode _code, int _regstart, int _regnum, byte[] src , DateTime sendTime) : base(_name, _addr, _code, _regstart, _regnum, src)
        {
  
            // 检查输入时间是否小于当前时间
            if (sendTime < DateTime.Now)
            {
                Loger.Error($"输入时间不能小于当前时间！输入时间: {sendTime:yyyy-MM-dd HH:mm:ss}, 当前时间: {DateTime.Now:yyyy-MM-dd HH:mm:ss}" );
                // 抛出异常，包含具体的错误信息
                throw new ArgumentException(
                    $"输入时间不能小于当前时间！输入时间: {sendTime:yyyy-MM-dd HH:mm:ss}, 当前时间: {DateTime.Now:yyyy-MM-dd HH:mm:ss}",
                    nameof(sendTime)
                );
            }
            SendTime = sendTime;
        }
    }
}
