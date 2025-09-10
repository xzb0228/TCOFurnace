using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModBusRTU
{
    public static class CMethord
    {
        public static byte[] ASIItab = { 0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0x41, 0x42, 0x43, 0x44, 0x45, 0x46 };


        public static byte[] u16tou8(ushort data)
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
        public static string bytetostring(byte[] src, int startindex, int length)
        {
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < length; i += 2)
            {
                str.Append((char)src[startindex + i + 0]);
                str.Append((char)src[startindex + i + 1]);
            }

            return str.ToString();
        }


        public static Int16 bytetos16(byte[] src, int startindex)
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

        public static UInt16 bytetou16(byte[] src, int startindex)
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
        public static Int32 bytetolong(byte[] src, int startindex, bool IsInverse = false)
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
        public static float bytetofloat(byte[] src, int startindex, bool IsInverse = false)
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
        public static double bytetodouble(byte[] src, int startindex, bool IsInverse = false)
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
    }
}
