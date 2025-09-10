using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModBusRTU
{
    public class CModbusReg
    {
        public string sn;
        public int addr;//地址
        public CModbusCode code;//功能码
        public int regstart;//开始地址
        public int regnum;//读写数据个数
        public byte[] vbyte;//通讯数据信息  收发的数据部分

        public string strinfo;//通讯原始信息 解析前原始数据包信息

        public CModbusReg()
        {
            vbyte = null;
            strinfo = string.Empty;
        }

        public CModbusReg(int _addr, CModbusCode _code, int _regstart, int _regnum, short[] src)
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

            addr = _addr;
            code = _code;
            regstart = _regstart;
            regnum = _regnum;
            vbyte = tm;
        }

        public CModbusReg(int _addr, CModbusCode _code, int _regstart, int _regnum, byte[] src = null)
        {
            addr = _addr;
            code = _code;
            regstart = _regstart;
            regnum = _regnum;
            vbyte = src;
        }
        public CModbusReg(int _addr, CModbusCode _code, int _regstart, int _regnum, string src)
        {
            addr = _addr;
            code = _code;
            regstart = _regstart;
            regnum = _regnum;
            vbyte = CMethord.HexToByte(src);
        }

        public static CModbusCode AnalysisMBCode(byte mbcode)
        {
            switch (mbcode)
            {
                case 1:
                    return CModbusCode.ReadCoil;
                case 2:
                    return CModbusCode.ReadDI;
                case 3:
                    return CModbusCode.ReadHolding;
                case 4:
                    return CModbusCode.ReadInput;
                case 5:
                    return CModbusCode.WriteCoil;
                case 6:
                    return CModbusCode.WriteReg;
                case 15:
                    return CModbusCode.WriteCoils;
                case 16:
                    return CModbusCode.WriteRegs;
            }
            return CModbusCode.None;
        }

        /// <summary>
        /// 通过波特率统计一个命令发送所需要的时间
        /// </summary>
        /// <param name="baud"></param>
        /// <returns></returns>
        public int CalcRcvTimeout(int baud)//返回毫秒
        {
            //发送数据的字节数
            int sendbyte = 0;
            //收数据的字节数
            int rcvbyte = 0;

            int waittime = 20;
            int mbregnum = regnum;
            switch (code)
            {
                case CModbusCode.ReadCoil:
                case CModbusCode.ReadDI:
                    sendbyte = 1 + 5 + 2;
                    rcvbyte = 1 + 2 + (mbregnum + 7) / 8 + 2;
                    break;
                case CModbusCode.ReadHolding:
                case CModbusCode.ReadInput:
                    sendbyte = 1 + 5 + 2;
                    rcvbyte = 1 + 2 + mbregnum * 2 + 2;
                    break;
                case CModbusCode.WriteCoil:
                    sendbyte = 1 + 5 + 2;
                    rcvbyte = 1 + 5 + 2;
                    break;
                case CModbusCode.WriteReg:
                    sendbyte = 1 + 5 + 2;
                    rcvbyte = 1 + 5 + 2;
                    waittime = 600;//写参数等待时间较长 至少1秒反应时间
                    break;
                case CModbusCode.WriteCoils:
                    sendbyte = 1 + 6 + (mbregnum + 7) / 8 + 2;
                    rcvbyte = 1 + 5 + 2;
                    break;
                case CModbusCode.WriteRegs:
                    sendbyte = 1 + 6 + mbregnum * 2 + 2;
                    rcvbyte = 1 + 5 + 2;
                    waittime = 800;//写参数等待时间较长 至少1秒反应时间
                    break;
            }

            int ms = 100 + 1000 * 12 * (rcvbyte + sendbyte) / baud;
            if (ms < 10) ms = 10;

            return ms + waittime;
        }
    }
}
