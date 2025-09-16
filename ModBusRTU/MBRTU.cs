using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModBusRTU
{
    public static class MBRTU
    {
        private readonly static ushort[] crcTable = {
            0X0000, 0XC0C1, 0XC181, 0X0140, 0XC301, 0X03C0, 0X0280, 0XC241,
            0XC601, 0X06C0, 0X0780, 0XC741, 0X0500, 0XC5C1, 0XC481, 0X0440,
            0XCC01, 0X0CC0, 0X0D80, 0XCD41, 0X0F00, 0XCFC1, 0XCE81, 0X0E40,
            0X0A00, 0XCAC1, 0XCB81, 0X0B40, 0XC901, 0X09C0, 0X0880, 0XC841,
            0XD801, 0X18C0, 0X1980, 0XD941, 0X1B00, 0XDBC1, 0XDA81, 0X1A40,
            0X1E00, 0XDEC1, 0XDF81, 0X1F40, 0XDD01, 0X1DC0, 0X1C80, 0XDC41,
            0X1400, 0XD4C1, 0XD581, 0X1540, 0XD701, 0X17C0, 0X1680, 0XD641,
            0XD201, 0X12C0, 0X1380, 0XD341, 0X1100, 0XD1C1, 0XD081, 0X1040,
            0XF001, 0X30C0, 0X3180, 0XF141, 0X3300, 0XF3C1, 0XF281, 0X3240,
            0X3600, 0XF6C1, 0XF781, 0X3740, 0XF501, 0X35C0, 0X3480, 0XF441,
            0X3C00, 0XFCC1, 0XFD81, 0X3D40, 0XFF01, 0X3FC0, 0X3E80, 0XFE41,
            0XFA01, 0X3AC0, 0X3B80, 0XFB41, 0X3900, 0XF9C1, 0XF881, 0X3840,
            0X2800, 0XE8C1, 0XE981, 0X2940, 0XEB01, 0X2BC0, 0X2A80, 0XEA41,
            0XEE01, 0X2EC0, 0X2F80, 0XEF41, 0X2D00, 0XEDC1, 0XEC81, 0X2C40,
            0XE401, 0X24C0, 0X2580, 0XE541, 0X2700, 0XE7C1, 0XE681, 0X2640,
            0X2200, 0XE2C1, 0XE381, 0X2340, 0XE101, 0X21C0, 0X2080, 0XE041,
            0XA001, 0X60C0, 0X6180, 0XA141, 0X6300, 0XA3C1, 0XA281, 0X6240,
            0X6600, 0XA6C1, 0XA781, 0X6740, 0XA501, 0X65C0, 0X6480, 0XA441,
            0X6C00, 0XACC1, 0XAD81, 0X6D40, 0XAF01, 0X6FC0, 0X6E80, 0XAE41,
            0XAA01, 0X6AC0, 0X6B80, 0XAB41, 0X6900, 0XA9C1, 0XA881, 0X6840,
            0X7800, 0XB8C1, 0XB981, 0X7940, 0XBB01, 0X7BC0, 0X7A80, 0XBA41,
            0XBE01, 0X7EC0, 0X7F80, 0XBF41, 0X7D00, 0XBDC1, 0XBC81, 0X7C40,
            0XB401, 0X74C0, 0X7580, 0XB541, 0X7700, 0XB7C1, 0XB681, 0X7640,
            0X7200, 0XB2C1, 0XB381, 0X7340, 0XB101, 0X71C0, 0X7080, 0XB041,
            0X5000, 0X90C1, 0X9181, 0X5140, 0X9301, 0X53C0, 0X5280, 0X9241,
            0X9601, 0X56C0, 0X5780, 0X9741, 0X5500, 0X95C1, 0X9481, 0X5440,
            0X9C01, 0X5CC0, 0X5D80, 0X9D41, 0X5F00, 0X9FC1, 0X9E81, 0X5E40,
            0X5A00, 0X9AC1, 0X9B81, 0X5B40, 0X9901, 0X59C0, 0X5880, 0X9841,
            0X8801, 0X48C0, 0X4980, 0X8941, 0X4B00, 0X8BC1, 0X8A81, 0X4A40,
            0X4E00, 0X8EC1, 0X8F81, 0X4F40, 0X8D01, 0X4DC0, 0X4C80, 0X8C41,
            0X4400, 0X84C1, 0X8581, 0X4540, 0X8701, 0X47C0, 0X4680, 0X8641,
            0X8201, 0X42C0, 0X4380, 0X8341, 0X4100, 0X81C1, 0X8081, 0X4040
        };

        public static ushort CalculateCrc(byte[] data)
        {
            if (data == null)
                return 0;

            ushort crc = ushort.MaxValue;
            byte tableIndex;

            foreach (byte b in data)
            {
                tableIndex = (byte)(crc ^ b);
                crc >>= 8;
                crc ^= crcTable[tableIndex];
            }
            return crc;
        }

        public static ushort CalculateCrc(byte[] data, int len)
        {
            if (data == null)
                return 0;
            if (data.Length < len) return 0;


            ushort crc = ushort.MaxValue;
            byte tableIndex;

            for (int i = 0; i < len; i++)
            {
                tableIndex = (byte)(crc ^ data[i]);
                crc >>= 8;
                crc ^= crcTable[tableIndex];
            }
            return crc;
        }

        /// <summary>
        /// 给发送命令添加CRC校验
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static byte[] ModbusRTU(byte[] src)
        {
            //000000-Tx:FE 10 00 00 00 07 0E 00 10 00 01 00 00 07 D0 00 64 00 01 00 01 55 E0
            ushort crc = CalculateCrc(src);

            byte[] dst = new byte[src.Length + 2];
            ushort dstindex = 0;
            foreach (byte bt in src)
            {
                dst[dstindex++] = bt;
            }

            dst[dstindex++] = (byte)(crc >> 0);
            dst[dstindex++] = (byte)(crc >> 8);
            return dst;
        }


        /// <summary>
        /// 十进制4660 => 0x1234
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] U16tou8(ushort data)
        {
            byte[] dst = new byte[2];
            dst[0] = (byte)((data >> 8) & 0xff);
            dst[1] = (byte)((data >> 0) & 0xff);
            return dst;
        }

        public static string ToHexString(byte[] bytes, int startindex, int len, string newval = "") // 0xae 0x00 0xcf => "AE00CF "
        {
            return BitConverter.ToString(bytes, startindex, len).Replace("-", newval);
        }

        public static string ToHexString(byte[] bytes) // 0xae 0x00 0xcf => "AE00CF "
        {
            return ToHexString(bytes, 0, bytes.Length);
        }

        public static byte[] HexToByte(string hexString)  //"AE00CF "   0xae 0x00 0xcf
        {
            hexString = hexString.Replace("-", "");
            hexString = hexString.Replace(" ", "");
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }

        public static byte[] CopyByte(byte[] src, int startindex, int Count)
        {
            if (src == null) return null;
            byte[] rst = new byte[Count];
            Array.Copy(src, startindex, rst, 0, Count);
            return rst;
        }
        public static string Bytetostring(byte[] src, int startindex, int length)
        {
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < length; i += 2)
            {
                str.Append((char)src[startindex + i + 0]);
                str.Append((char)src[startindex + i + 1]);
            }

            return str.ToString();
        }

        public static Int16 Bytetos16(byte[] src, int startindex)
        {
            byte[] temp = new byte[2];
            temp[0] = src[startindex + 1];
            temp[1] = src[startindex + 0];
            return BitConverter.ToInt16(temp, 0);
        }
        public static byte[] Converts16Tobyte(ref byte[] src, int startindex, Int16 val)
        {
            byte[] dst = BitConverter.GetBytes(val);
            src[startindex++] = dst[1];
            src[startindex++] = dst[0];
            return src;
        }

        public static UInt16 Bytetou16(byte[] src, int startindex)
        {
            byte[] temp = new byte[2];
            temp[0] = src[startindex + 1];
            temp[1] = src[startindex + 0];
            return BitConverter.ToUInt16(temp, 0); ;
        }
        public static byte[] Convertu16Tobyte(ref byte[] src, int startindex, UInt16 val)
        {
            byte[] dst = BitConverter.GetBytes(val);
            src[startindex++] = dst[1];
            src[startindex++] = dst[0];
            return src;
        }
        public static Int32 Bytetolong(byte[] src, int startindex, bool IsInverse = false)
        {
            byte[] temp = new byte[4];
            if (IsInverse == false)
            {
                temp[0] = src[startindex + 1];
                temp[1] = src[startindex + 0];
                temp[2] = src[startindex + 3];
                temp[3] = src[startindex + 2];
            }
            else
            {
                temp[0] = src[startindex + 3];
                temp[1] = src[startindex + 2];
                temp[2] = src[startindex + 1];
                temp[3] = src[startindex + 0];
            }

            return BitConverter.ToInt32(temp, 0); ;
        }
        public static byte[] ConvertlongTobyte(ref byte[] src, int startindex, Int32 val, bool IsInverse = false)
        {
            byte[] dst = BitConverter.GetBytes(val);
            if (IsInverse == false)
            {
                src[startindex++] = dst[1];
                src[startindex++] = dst[0];
                src[startindex++] = dst[3];
                src[startindex++] = dst[2];
            }
            else
            {
                src[startindex++] = dst[3];
                src[startindex++] = dst[2];
                src[startindex++] = dst[1];
                src[startindex++] = dst[0];
            }
            return src;
        }
        public static float Bytetofloat(byte[] src, int startindex, bool IsInverse = false)
        {
            byte[] temp = new byte[4];
            if (IsInverse == false)
            {
                temp[0] = src[startindex + 1];
                temp[1] = src[startindex + 0];
                temp[2] = src[startindex + 3];
                temp[3] = src[startindex + 2];
            }
            else
            {
                temp[0] = src[startindex + 3];
                temp[1] = src[startindex + 2];
                temp[2] = src[startindex + 1];
                temp[3] = src[startindex + 0];
            }

            return BitConverter.ToSingle(temp, 0); ;
        }
        public static byte[] ConvertfloatTobyte(ref byte[] src, int startindex, float val, bool IsInverse = false)
        {
            byte[] dst = BitConverter.GetBytes(val);
            if (IsInverse == false)
            {
                src[startindex++] = dst[1];
                src[startindex++] = dst[0];
                src[startindex++] = dst[3];
                src[startindex++] = dst[2];
            }
            else
            {
                src[startindex++] = dst[3];
                src[startindex++] = dst[2];
                src[startindex++] = dst[1];
                src[startindex++] = dst[0];
            }

            return src;
        }
        public static double Bytetodouble(byte[] src, int startindex, bool IsInverse = false)
        {
            byte[] temp = new byte[8];
            if (IsInverse == false)
            {
                temp[0] = src[startindex + 1];
                temp[1] = src[startindex + 0];
                temp[2] = src[startindex + 3];
                temp[3] = src[startindex + 2];
                temp[4] = src[startindex + 5];
                temp[5] = src[startindex + 4];
                temp[6] = src[startindex + 7];
                temp[7] = src[startindex + 6];
            }
            else
            {
                temp[0] = src[startindex + 7];
                temp[1] = src[startindex + 6];
                temp[2] = src[startindex + 5];
                temp[3] = src[startindex + 4];
                temp[4] = src[startindex + 3];
                temp[5] = src[startindex + 2];
                temp[6] = src[startindex + 1];
                temp[7] = src[startindex + 0];
            }
            return BitConverter.ToDouble(temp, 0); ;
        }
        public static byte[] ConvertdoubleTobyte(ref byte[] src, int startindex, double val, bool IsInverse = false)
        {
            byte[] dst = BitConverter.GetBytes(val);
            if (IsInverse == false)
            {
                src[startindex++] = dst[1];
                src[startindex++] = dst[0];
                src[startindex++] = dst[3];
                src[startindex++] = dst[2];
                src[startindex++] = dst[5];
                src[startindex++] = dst[4];
                src[startindex++] = dst[7];
                src[startindex++] = dst[6];
            }
            else
            {
                src[startindex++] = dst[7];
                src[startindex++] = dst[6];
                src[startindex++] = dst[5];
                src[startindex++] = dst[4];
                src[startindex++] = dst[3];
                src[startindex++] = dst[2];
                src[startindex++] = dst[1];
                src[startindex++] = dst[0];
            }

            return src;
        }

        /// <summary>
        /// 将byte[]转换为WriteMultipleCoils所需的bool[]
        /// </summary>
        /// <param name="byteArray">输入字节数组（每个字节的8位对应8个线圈）</param>
        /// <param name="totalCoils">需要转换的总线圈数量（可能小于字节数组能表示的最大数量）</param>
        /// <returns>线圈状态数组（true=导通，false=断开）</returns>
        public static bool[] BytesToBools(byte[] byteArray, int totalCoils)
        {
            // 计算所需的最小字节数（向上取整）
            int requiredBytes = (totalCoils + 7) / 8;
            bool[] coils = new bool[totalCoils];

            for (int i = 0; i < totalCoils; i++)
            {
                // 计算当前线圈位于哪个字节
                int byteIndex = i / 8;
                // 计算当前线圈在字节中的位索引（0-7）
                int bitIndex = i % 8;

                // 提取对应位的值（1表示导通）
                coils[i] = (byteArray[byteIndex] & (1 << bitIndex)) != 0;
            }

            return coils;
        }

        /// <summary>
        /// 将字节数组转换为ushort[]（严格按照2字节→1个ushort，遵循Modbus大端序）
        /// </summary>
        /// <param name="byteArray">输入字节数组（长度必须是2的倍数）</param>
        /// <returns>Modbus寄存器数组</returns>
        public static ushort[] BytesToRegisters(byte[] byteArray)
        {
            if (byteArray.Length % 2 != 0)
                throw new ArgumentException(
                    $"字节数组长度必须是2的倍数（每个寄存器需要2字节），当前长度：{byteArray.Length}",
                    nameof(byteArray)
                );

            // 计算寄存器数量（总字节数 ÷ 2）
            int registerCount = byteArray.Length / 2;
            ushort[] registers = new ushort[registerCount];

            // 逐个处理：每2个字节转换为1个ushort（大端序）
            for (int i = 0; i < registerCount; i++)
            {
                // 第1个字节是高位（High Byte），第2个字节是低位（Low Byte）
                byte highByte = byteArray[i * 2];       // 索引：0, 2, 4...
                byte lowByte = byteArray[i * 2 + 1];    // 索引：1, 3, 5...

                // 拼接为ushort：高位字节左移8位 + 低位字节
                registers[i] = (ushort)((highByte << 8) | lowByte);
            }

            return registers;
        }
    }
}
