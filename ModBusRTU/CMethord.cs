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
            // 校验：字节数组必须非空且长度为2的倍数（确保每个ushort对应2字节）
            if (byteArray == null)
                throw new ArgumentNullException(nameof(byteArray));

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

        /// <summary>
        /// 十进制4660 => 0x1234
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 根据Modbus功能码，将有效数据去掉起始地址和字节数后转换为List<int>
        /// </summary>
        /// <param name="functionCode">Modbus功能码</param>
        /// <param name="fullEffectiveData">完整的有效数据（功能码之后的所有数据）</param>
        /// <returns>仅包含核心数据的整数列表</returns>
        /// <exception cref="ArgumentException">数据格式错误或不支持的功能码</exception>
        public static List<int> ConvertToCoreDataList(byte functionCode, byte[] fullEffectiveData)
        {
            if (fullEffectiveData == null)
                return null;

            List<int> result = new List<int>();
            byte[] coreData; // 去掉起始地址和字节数后的核心数据

            switch (functionCode)
            {
                // 0x01: 读线圈状态 - 去掉[字节数]字段
                case 0x01:
                    // 完整有效数据格式: [字节数] + [线圈状态字节...]
                    if (fullEffectiveData.Length < 1)
                        return null;

                    // 核心数据是字节数之后的所有字节
                    coreData = new byte[fullEffectiveData.Length - 1];
                    Array.Copy(fullEffectiveData, 1, coreData, 0, coreData.Length);

                    // 解析每个位
                    foreach (byte b in coreData)
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            result.Add(((b >> i) & 0x01) == 0x01 ? 1 : 0);
                        }
                    }
                    break;

                // 0x02: 读离散输入 - 与0x01格式相同
                case 0x02:
                    if (fullEffectiveData.Length < 1)
                        return null;

                    coreData = new byte[fullEffectiveData.Length - 1];
                    Array.Copy(fullEffectiveData, 1, coreData, 0, coreData.Length);

                    foreach (byte b in coreData)
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            result.Add(((b >> i) & 0x01) == 0x01 ? 1 : 0);
                        }
                    }
                    break;

                // 0x03: 读保持寄存器 - 去掉[字节数]字段
                case 0x03:
                    // 完整有效数据格式: [字节数] + [寄存器值(2字节)...]
                    if (fullEffectiveData.Length < 1)
                        return null;

                    coreData = new byte[fullEffectiveData.Length - 1];
                    Array.Copy(fullEffectiveData, 1, coreData, 0, coreData.Length);

                    if (coreData.Length % 2 != 0)
                        return null;

                    // 解析寄存器值
                    for (int i = 0; i < coreData.Length; i += 2)
                    {
                        result.Add((coreData[i] << 8) | coreData[i + 1]);
                    }
                    break;

                // 0x04: 读输入寄存器 - 与0x03格式相同
                case 0x04:
                    if (fullEffectiveData.Length < 1)
                        return null;

                    coreData = new byte[fullEffectiveData.Length - 1];
                    Array.Copy(fullEffectiveData, 1, coreData, 0, coreData.Length);

                    if (coreData.Length % 2 != 0)
                        return null;

                    for (int i = 0; i < coreData.Length; i += 2)
                    {
                        result.Add((coreData[i] << 8) | coreData[i + 1]);
                    }
                    break;

                // 0x05: 写单个线圈 - 去掉[线圈地址]，保留状态
                case 0x05:
                    // 完整有效数据格式: [地址高8位] + [地址低8位] + [状态高8位] + [状态低8位]
                    if (fullEffectiveData.Length != 4)
                        return null;

                    // 核心数据是状态部分（只取有效位）
                    bool isOn = fullEffectiveData[2] == 0xFF && fullEffectiveData[3] == 0x00;
                    result.Add(isOn ? 1 : 0);
                    break;

                // 0x06: 写单个寄存器 - 去掉[寄存器地址]，保留值
                case 0x06:
                    // 完整有效数据格式: [地址高8位] + [地址低8位] + [值高8位] + [值低8位]
                    if (fullEffectiveData.Length != 4)
                        return null;

                    // 核心数据是寄存器值
                    int regValue = (fullEffectiveData[2] << 8) | fullEffectiveData[3];
                    result.Add(regValue);
                    break;

                // 0x0F: 写多个线圈 - 去掉[起始地址]，保留线圈数量
                case 0x0F:
                    // 完整有效数据格式: [起始地址高8位] + [起始地址低8位] + [数量高8位] + [数量低8位]
                    if (fullEffectiveData.Length != 4)
                        return null;

                    // 核心数据是线圈数量
                    int coilCount = (fullEffectiveData[2] << 8) | fullEffectiveData[3];
                    result.Add(coilCount);
                    break;

                // 0x10: 写多个寄存器 - 去掉[起始地址]，保留寄存器数量
                case 0x10:
                    // 完整有效数据格式: [起始地址高8位] + [起始地址低8位] + [数量高8位] + [数量低8位]
                    if (fullEffectiveData.Length != 4)
                        return null;

                    // 核心数据是寄存器数量
                    int regCount = (fullEffectiveData[2] << 8) | fullEffectiveData[3];
                    result.Add(regCount);
                    break;

                // 0x17: 读写多个寄存器 - 去掉地址信息，保留读取的寄存器值
                case 0x17:
                    // 完整有效数据格式: [字节数] + [寄存器值(2字节)...]
                    if (fullEffectiveData.Length < 1)
                        return null;

                    coreData = new byte[fullEffectiveData.Length - 1];
                    Array.Copy(fullEffectiveData, 1, coreData, 0, coreData.Length);

                    if (coreData.Length % 2 != 0)
                        return null;

                    for (int i = 0; i < coreData.Length; i += 2)
                    {
                        result.Add((coreData[i] << 8) | coreData[i + 1]);
                    }
                    break;

                // 异常响应 (功能码最高位为1)
                case byte n when (n & 0x80) != 0:
                    // 完整有效数据格式: [异常码]
                    if (fullEffectiveData.Length != 1)
                        return null;

                    // 核心数据是异常码
                    result.Add(fullEffectiveData[0]);
                    break;

                default:
                    return null;
            }

            return result;
        }

        public static bool Validate(byte[] packet, out string errorMessage)
        {
            errorMessage = string.Empty;

            // 1. 基本长度检查
            if (packet == null || packet.Length < 4)
            {
                errorMessage = $"长度不足（至少4字节），实际: {packet?.Length ?? 0}字节";
                return false;
            }

            // 2. 功能码检查
            byte functionCode = packet[1];
            if (functionCode == 0x00 || functionCode > 0x17)
            {
                errorMessage = $"无效功能码: 0x{functionCode:X2}";
                return false;
            }

            // 3. 异常响应检查
            if ((functionCode & 0x80) != 0)
            {
                errorMessage = $"从设备异常响应，异常码: 0x{packet[2]:X2}";
                return false;
            }

            // 4. CRC校验
            if (!ValidateCrc(packet))
            {
                errorMessage = "CRC校验失败";
                return false;
            }

            // 5. 功能码对应的长度检查
            if (!ValidateFunctionCodeLength(functionCode, packet.Length, out errorMessage))
            {
                return false;
            }

            return true;
        }

        private static bool ValidateFunctionCodeLength(byte functionCode, int packetLength, out string errorMessage)
        {
            errorMessage = string.Empty;
            int minLength = 4; // 基础长度：地址+功能码+CRC

            switch (functionCode)
            {
                case 0x01: // 读线圈响应
                case 0x02: // 读离散输入响应
                    if (packetLength < 5) // 基础长度+1字节计数
                    {
                        errorMessage = $"{functionCode:X2}响应长度不足";
                        return false;
                    }
                    break;
                case 0x03: // 读保持寄存器响应
                case 0x04: // 读输入寄存器响应
                    if (packetLength < 5) // 基础长度+1字节计数
                    {
                        errorMessage = $"{functionCode:X2}响应长度不足";
                        return false;
                    }
                    break;
                case 0x05: // 写单个线圈响应
                case 0x06: // 写单个寄存器响应
                    if (packetLength != 6) // 基础长度+2字节地址+2字节值 → 实际6字节
                    {
                        errorMessage = $"{functionCode:X2}响应长度应为6字节，实际: {packetLength}";
                        return false;
                    }
                    break;
                    // 可根据需要扩展其他功能码
            }

            return true;
        }

        private static bool ValidateCrc(byte[] packet)
        {
            ushort receivedCrc = (ushort)(packet[packet.Length - 2] << 8 | packet[packet.Length - 1]);
            ushort calculatedCrc = CalculateCrc(packet, 0, packet.Length - 2);
            return receivedCrc == calculatedCrc;
        }

        private static ushort CalculateCrc(byte[] data, int start, int length)
        {
            ushort crc = 0xFFFF;
            for (int i = start; i < start + length; i++)
            {
                crc ^= (ushort)data[i];
                for (int j = 0; j < 8; j++)
                {
                    crc = (crc & 0x0001) != 0 ? (ushort)((crc >> 1) ^ 0xA001) : (ushort)(crc >> 1);
                }
            }
            return crc;
        }

        /// <summary>
        /// 通过波特率统计一个命令发送所需要的时间
        /// </summary>
        /// <param name="code"></param>
        /// <param name="baud"></param>
        /// <param name="mbregnum">需要发送的数据数</param>
        /// <returns></returns>
        public static int CalcRcvTimeout(CModbusCode code, int baud, int mbregnum)//返回毫秒
        {
            //发送数据的字节数
            int sendbyte = 0;
            //收数据的字节数
            int rcvbyte = 0;

            int waittime = 20;
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
