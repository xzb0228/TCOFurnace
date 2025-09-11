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

        /// <summary>
        /// 获取单个操作对应的功能码（如“读线圈”“写单个保持寄存器”）
        /// </summary>
        /// <param name="regType">寄存器类型</param>
        /// <param name="operation">读写操作</param>
        /// <returns>匹配的功能码，非法组合返回 CModbusCode.None</returns>
        public CModbusCode GetModbusCode(RegistersType regType, OperationType operation)
        {
            // 双层switch：先按寄存器类型分类，再按操作类型匹配
            switch (regType)
            {
                // 线圈寄存器：支持读、写单个
                case RegistersType.Coils:
                    switch (operation)
                    {
                        case OperationType.Read:
                            return CModbusCode.ReadCoil; // 读线圈（0x01）
                        case OperationType.Write:
                            return CModbusCode.WriteCoil; // 写单个线圈（0x05）
                        case OperationType.WriteS:
                            return CModbusCode.WriteCoils; // 写单个线圈（0x05）
                        default:
                            return CModbusCode.None;
                    }

                // 离散输入寄存器：仅支持读
                case RegistersType.DiscreteInputs:
                    switch (operation)
                    {
                        case OperationType.Read:
                            return CModbusCode.ReadDI; // 读离散输入（0x02）
                        case OperationType.Write:
                            return CModbusCode.None; // 离散输入只读，写操作非法
                        default:
                            return CModbusCode.None;
                    }

                // 保持寄存器：支持读、写单个
                case RegistersType.HoldingRegisters:
                    switch (operation)
                    {
                        case OperationType.Read:
                            return CModbusCode.ReadHolding; // 读多个保持寄存器（0x03）
                        case OperationType.Write:
                            return CModbusCode.WriteReg; // 写单个保持寄存器（0x06）
                        case OperationType.WriteS:
                            return CModbusCode.WriteCoils; //写多个保持寄存器（0x06）
                        default:
                            return CModbusCode.None;
                    }

                // 输入寄存器：仅支持读
                case RegistersType.InputRegisters:
                    switch (operation)
                    {
                        case OperationType.Read:
                            return CModbusCode.ReadInput; // 读输入寄存器（0x04）
                        case OperationType.Write:
                            return CModbusCode.None; // 输入寄存器只读，写操作非法
                        default:
                            return CModbusCode.None;
                    }

                // 未定义的寄存器类型
                default:
                    return CModbusCode.None;
            }
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
