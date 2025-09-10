using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModBusRTU
{
    public static class CModbus
    {
        public static CModbusReg DealMasterRcv(byte[] src, int addr)
        {
            if (src == null) return null;
            if (src.Length < 6) return null;
            if (src[0] == 0) return null;
            if (src[0] == 0xff) return null;

            CModbusReg mreg = new CModbusReg();
            mreg.addr = src[0];
            /*
            if(mreg.addr!=0xfe)
            {
                if((addr!=0x00)&& (mreg.addr != addr)) return null;
            }*/


            mreg.code = CModbusReg.AnalysisMBCode(src[1]);
            int infolen = src[2];
            switch (mreg.code)
            {
                case CModbusCode.ReadCoil://1：读线圈寄存器
                case CModbusCode.ReadDI: //2：读光耦状态
                    if (infolen > 4) return null;
                    if (src.Length < (5 + infolen)) { mreg.code = CModbusCode.Wait; return mreg; }
                    if (CMBRTU.CalculateCrc(src, 5 + infolen) != 0) return null;
                    mreg.regnum = infolen;
                    mreg.vbyte = CMethord.CopyByte(src, 3, infolen);
                    mreg.strinfo = CMethord.ToHexString(src, 0, 5 + infolen);
                    return mreg;
                case CModbusCode.ReadHolding://读多个保持寄存器
                case CModbusCode.ReadInput: //4：读只读寄存器状态
                    if (infolen > 250) return null;
                    if (src.Length < (5 + infolen)) { mreg.code = CModbusCode.Wait; return mreg; }
                    if (CMBRTU.CalculateCrc(src, 5 + infolen) != 0) return null;
                    mreg.regnum = infolen / 2;
                    mreg.vbyte = CMethord.CopyByte(src, 3, infolen);
                    mreg.strinfo = CMethord.ToHexString(src, 0, 5 + infolen);
                    return mreg;
                case CModbusCode.WriteCoil: //写单个线圈
                case CModbusCode.WriteReg://写单个保持寄存器
                    if (src.Length < 8) { mreg.code = CModbusCode.Wait; return mreg; }
                    if (CMBRTU.CalculateCrc(src, 4 + 4) != 0) return null;
                    mreg.regstart = CMethord.bytetou16(src, 2);
                    mreg.regnum = 1;
                    mreg.vbyte = CMethord.CopyByte(src, 4, 2);
                    mreg.strinfo = CMethord.ToHexString(src, 0, 4 + 4);
                    return mreg;
                case CModbusCode.WriteCoils: //写多个线圈寄存器
                case CModbusCode.WriteRegs://写多个保持寄存器
                    if (src.Length < 8) { mreg.code = CModbusCode.Wait; return mreg; }
                    if (CMBRTU.CalculateCrc(src, 4 + 4) != 0) return null;
                    mreg.regstart = CMethord.bytetou16(src, 2);
                    mreg.regnum = CMethord.bytetou16(src, 4);
                    mreg.vbyte = null;
                    mreg.strinfo = CMethord.ToHexString(src, 0, 4 + 4);
                    return mreg;
            }
            return null;
        }

        public static byte[] DealMasterSnd(CModbusReg mreg)
        {
            byte[] dsttemp = new byte[256];
            ushort dstindex = 0;

            dsttemp[dstindex++] = (byte)(mreg.addr == 0 ? 0xfe : mreg.addr);
            dsttemp[dstindex++] = (byte)mreg.code;

            ushort regstart = (ushort)mreg.regstart;
            ushort regnum = (ushort)mreg.regnum;
            byte[] bytesrc = mreg.vbyte;
            switch (mreg.code)
            {
                case CModbusCode.ReadCoil://1：读线圈寄存器
                case CModbusCode.ReadDI: //2：读光耦状态
                case CModbusCode.ReadHolding://读多个保持寄存器
                case CModbusCode.ReadInput: //4：读只读寄存器状态
                    //寄存器起始地址
                    dsttemp[dstindex++] = (byte)(regstart >> 8);
                    dsttemp[dstindex++] = (byte)(regstart >> 0);
                    //寄存器起始地址
                    dsttemp[dstindex++] = (byte)(regnum >> 8);
                    dsttemp[dstindex++] = (byte)(regnum >> 0);
                    break;
                case CModbusCode.WriteCoil: //写单个线圈
                case CModbusCode.WriteReg://写单个保持寄存器
                    //寄存器起始地址
                    dsttemp[dstindex++] = (byte)(regstart >> 8);
                    dsttemp[dstindex++] = (byte)(regstart >> 0);
                    //写取线圈状态
                    foreach (byte bt in bytesrc) dsttemp[dstindex++] = bt;
                    break;
                case CModbusCode.WriteCoils: //写多个线圈寄存器
                    //寄存器起始地址
                    dsttemp[dstindex++] = (byte)(regstart >> 8);
                    dsttemp[dstindex++] = (byte)(regstart >> 0);
                    //寄存器数量
                    dsttemp[dstindex++] = (byte)(regnum >> 8);
                    dsttemp[dstindex++] = (byte)(regnum >> 0);
                    if (((regnum + 7) / 8) != bytesrc.Length) return null;
                    dsttemp[dstindex++] = (byte)bytesrc.Length;
                    foreach (byte bt in bytesrc) dsttemp[dstindex++] = bt;
                    break;
                case CModbusCode.WriteRegs://写多个保持寄存器
                    //寄存器起始地址
                    dsttemp[dstindex++] = (byte)(regstart >> 8);
                    dsttemp[dstindex++] = (byte)(regstart >> 0);
                    //寄存器数量
                    dsttemp[dstindex++] = (byte)(regnum >> 8);
                    dsttemp[dstindex++] = (byte)(regnum >> 0);

                    if ((regnum * 2) != bytesrc.Length) return null;
                    dsttemp[dstindex++] = (byte)(bytesrc.Length);
                    foreach (byte bt in bytesrc) dsttemp[dstindex++] = bt;
                    break;
                default:
                    break;
            }

            byte[] dst = new byte[dstindex];
            for (int i = 0; i < dstindex; i++)
                dst[i] = dsttemp[i];
            return CMBRTU.ModbusRTU(dst);
        }
    }
}
