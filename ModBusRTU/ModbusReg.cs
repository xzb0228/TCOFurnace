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
        /// <summary>
        /// 命令功能描述
        /// </summary>
        public string name;
        public int addr;//地址
        public ModbusCode code;//功能码
        public int regstart;//开始地址
        public int regnum;//读写数据个数
        public byte[] vbyte;//通讯数据信息  收发的数据部分
        public string Command;

        public string strinfo;//通讯原始信息 解析前原始数据包信息

        // 响应信息
        public int[] ResponseData { get; set; } // 接收的响应帧
        public bool IsCompleted { get; set; } = false;
        public bool IsSuccess { get; set; } = false;


        public ModbusReg()
        {
            vbyte = null;
            strinfo = string.Empty;
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
        public ModbusReg(string _name, int _addr, ModbusCode _code, int _regstart, int _regnum, byte[] src = null)
        {
            name = _name;
            addr = _addr;
            code = _code;
            regstart = _regstart;
            regnum = _regnum;
            vbyte = src;
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
        public ModbusReg(string _name, int _addr, ModbusCode _code, int _regstart, int _regnum, string src)
        {
            name = _name;
            addr = _addr;
            code = _code;
            regstart = _regstart;
            regnum = _regnum;
            vbyte = MBRTU.HexToByte(src);
        }

        /// <summary>
        /// 构建命令
        /// </summary>
        /// <param name="_name">命令功能与名称</param>
        /// <param name="Command">ModBus命令 字符串</param>
        public ModbusReg(string _name , string Command)
        {
            
        }
    }
}
