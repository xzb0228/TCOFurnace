using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ModBusRTU
{
    public class ModbusReg
    {
        public string portName;//串口名称
        public string name;// 命令功能描述 在同一个串口下唯一
        public int addr;//地址
        public ModbusCode code;//功能码
        public int regstart;//开始地址
        public int regnum;//读写数据个数
        public byte[] vbyte;//通讯数据信息 收发的数据部分

        // 响应信息
        public int[] ResponseData { get; set; } // 接收的响应帧
        public bool IsCompleted { get; set; } = false;
        public bool IsSuccess { get; set; } = false;

        //业务（冗余字段）
        public string businessData;

        //是否启动仿真模式
        public bool IsEmulatorMode { get; set; } = false;
        

        //只能在基类中使用，不能通过无参构造函数实例化兑现
        public ModbusReg()
        {
            vbyte = null;
        }

        /// <summary>
        /// 构建命令
        /// </summary>
        /// <param name="_name">命令功能与名称</param>
        /// <param name="_addr">设备地址</param>
        /// <param name="_code">功能码</param>
        /// <param name="_regstart">起始地址</param>
        /// <param name="_regnum">寄存器数量</param>
        /// <param name="src">命令中存入寄存器的数据</param>
        public ModbusReg(string _portName, string _name, int _addr, ModbusCode _code, int _regstart, int _regnum, byte[] _vbyte = null)
        {
            name = _name;
            addr = _addr;
            code = _code;
            regstart = _regstart;
            regnum = _regnum;
            vbyte = _vbyte;
            portName = _portName;
        }

        /// <summary>
        /// 构建命令
        /// </summary>
        /// <param name="_name">命令功能与名称</param>
        /// <param name="Command">ModBus命令 字符串</param>
        public ModbusReg(string _name, string Command)
        {

        }

        /// <summary>
        /// 每次获取命令的深拷贝
        /// </summary>
        /// <returns></returns>
        public ModbusReg Clone()
        {
            var cloned = (ModbusReg)this.MemberwiseClone();
            cloned.vbyte = (byte[])this.vbyte?.Clone(); // byte[] 实现了 ICloneable，Clone() 为深拷贝
            cloned.ResponseData = (int[])this.ResponseData?.Clone(); // byte[] 实现了 ICloneable，Clone() 为深拷贝
            return cloned;
        }
    }
}
