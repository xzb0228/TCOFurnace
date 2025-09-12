using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ModBusRTU
{
    public class CModbusReg
    {
        /// <summary>
        /// 命令功能描述
        /// </summary>
        public string name;
        public int addr;//地址
        public CModbusCode code;//功能码
        public int regstart;//开始地址
        public int regnum;//读写数据个数
        public byte[] vbyte;//通讯数据信息  收发的数据部分

        public string strinfo;//通讯原始信息 解析前原始数据包信息

        // 响应信息
        public int[] ResponseData { get; set; } // 接收的响应帧
        public bool IsCompleted { get; set; } = false;
        public bool IsSuccess { get; set; } = false;


        public CModbusReg()
        {
            vbyte = null;
            strinfo = string.Empty;
        }

        public CModbusReg(string _name,int _addr, CModbusCode _code, int _regstart, int _regnum, short[] src)
        {
            byte[] tm;
            if (src == null) tm = null;
            else
            {
                tm = new byte[src.Length * 2];

                for (int i = 0; i < src.Length; i++)
                {
                    tm[2 * i + 0] = (byte)(src[i] >> 8);
                    tm[2 * i + 1] = (byte)(src[i] >> 0);
                }
            }
            name = _name;
            addr = _addr;
            code = _code;
            regstart = _regstart;
            regnum = _regnum;
            vbyte = tm;
        }

        public CModbusReg(string _name, int _addr, CModbusCode _code, int _regstart, int _regnum, byte[] src = null)
        {
            name = _name;
            addr = _addr;
            code = _code;
            regstart = _regstart;
            regnum = _regnum;
            vbyte = src;
        }
        public CModbusReg(string _name, int _addr, CModbusCode _code, int _regstart, int _regnum, string src)
        {
            name = _name;
            addr = _addr;
            code = _code;
            regstart = _regstart;
            regnum = _regnum;
            vbyte = CMethord.HexToByte(src);
        }
    }
}
