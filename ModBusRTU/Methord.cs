using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModBusRTU
{
    public static class Methord
    {
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

        /// <summary>
        /// 通过波特率统计一个命令发送所需要的时间
        /// </summary>
        /// <param name="code"></param>
        /// <param name="baud"></param>
        /// <param name="mbregnum">需要发送的数据数</param>
        /// <returns></returns>
        public static int CalcRcvTimeout(ModbusCode code, int baud, int mbregnum)//返回毫秒
        {
            //发送数据的字节数
            int sendbyte = 0;
            //收数据的字节数
            int rcvbyte = 0;

            int waittime = 20;
            switch (code)
            {
                case ModbusCode.ReadCoil:
                case ModbusCode.ReadDI:
                    sendbyte = 1 + 5 + 2;
                    rcvbyte = 1 + 2 + (mbregnum + 7) / 8 + 2;
                    break;
                case ModbusCode.ReadHolding:
                case ModbusCode.ReadInput:
                    sendbyte = 1 + 5 + 2;
                    rcvbyte = 1 + 2 + mbregnum * 2 + 2;
                    break;
                case ModbusCode.WriteCoil:
                    sendbyte = 1 + 5 + 2;
                    rcvbyte = 1 + 5 + 2;
                    break;
                case ModbusCode.WriteReg:
                    sendbyte = 1 + 5 + 2;
                    rcvbyte = 1 + 5 + 2;
                    waittime = 600;//写参数等待时间较长 至少1秒反应时间
                    break;
                case ModbusCode.WriteCoils:
                    sendbyte = 1 + 6 + (mbregnum + 7) / 8 + 2;
                    rcvbyte = 1 + 5 + 2;
                    break;
                case ModbusCode.WriteRegs:
                    sendbyte = 1 + 6 + mbregnum * 2 + 2;
                    rcvbyte = 1 + 5 + 2;
                    waittime = 800;//写参数等待时间较长 至少1秒反应时间
                    break;
            }

            int ms = 100 + 1000 * 12 * (rcvbyte + sendbyte) / baud;
            if (ms < 10) ms = 10;

            return ms + waittime;
        }

        public static byte[] DealMasterSnd(ModbusReg mreg)
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
                case ModbusCode.ReadCoil://1：读线圈寄存器
                case ModbusCode.ReadDI: //2：读光耦状态
                case ModbusCode.ReadHolding://读多个保持寄存器
                case ModbusCode.ReadInput: //4：读只读寄存器状态
                    //寄存器起始地址
                    dsttemp[dstindex++] = (byte)(regstart >> 8);
                    dsttemp[dstindex++] = (byte)(regstart >> 0);
                    //寄存器起始地址
                    dsttemp[dstindex++] = (byte)(regnum >> 8);
                    dsttemp[dstindex++] = (byte)(regnum >> 0);
                    break;
                case ModbusCode.WriteCoil: //写单个线圈
                case ModbusCode.WriteReg://写单个保持寄存器
                    //寄存器起始地址
                    dsttemp[dstindex++] = (byte)(regstart >> 8);
                    dsttemp[dstindex++] = (byte)(regstart >> 0);
                    //写取线圈状态
                    foreach (byte bt in bytesrc) dsttemp[dstindex++] = bt;
                    break;
                case ModbusCode.WriteCoils: //写多个线圈寄存器
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
                case ModbusCode.WriteRegs://写多个保持寄存器
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
            return MBRTU.CommandCRC(dst);
        }

        public static ModbusCode AnalysisMBCode(byte mbcode)
        {
            switch (mbcode)
            {
                case 1:
                    return ModbusCode.ReadCoil;
                case 2:
                    return ModbusCode.ReadDI;
                case 3:
                    return ModbusCode.ReadHolding;
                case 4:
                    return ModbusCode.ReadInput;
                case 5:
                    return ModbusCode.WriteCoil;
                case 6:
                    return ModbusCode.WriteReg;
                case 15:
                    return ModbusCode.WriteCoils;
                case 16:
                    return ModbusCode.WriteRegs;
            }
            return ModbusCode.None;
        }

        public static ModbusReg DeserModbusReg(string name,string Command)
        {
            // 1. 解析字节数组
            byte[] bytes = ParseBytes(Command);

            // 2. 验证Modbus帧长度（最小帧长：地址+功能码+数据+CRC=4字节，此处简化处理)
            if (bytes.Length < 4)
                throw new FormatException("Modbus帧长度不足，至少需要4字节");

            // 3. 解析基础字段
            int addr = bytes[0];                  // 设备地址（1字节）
            byte codeByte = bytes[1];             // 功能码（1字节）
            ModbusCode code = ParseModbusCode(codeByte);

            // 4. 根据功能码解析寄存器地址和数量（核心逻辑）
            (int regstart, int regnum, byte[] data) = ParseDataByCode(code, bytes);


            return new ModbusReg(_name: name, _addr: addr, _code: code, _regstart: regstart, _regnum: regnum, _vbyte: data);
        }

        /// <summary>
        /// 将十六进制字符串解析为字节数组
        /// </summary>
        private static byte[] ParseBytes(string commandString)
        {
            if (string.IsNullOrWhiteSpace(commandString))
                throw new ArgumentException("命令字符串不能为空", nameof(commandString));

            return commandString
                .Split(' ')
                .Select(s => s.Trim())
                .Where(s => !string.IsNullOrEmpty(s))
                .Select(s =>
                {
                    // 支持0x前缀和纯十六进制格式（如"0xaa"或"aa"）
                    if (s.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                        return Convert.ToByte(s.Substring(2), 16);
                    return Convert.ToByte(s, 16);
                })
                .ToArray();
        }

        /// <summary>
        /// 将功能码字节转换为ModbusCode枚举
        /// </summary>
        private static ModbusCode ParseModbusCode(byte codeByte)
        {
            // 检查是否为错误响应（最高位为1）
            if ((codeByte & 0x80) != 0)
                return ModbusCode.Error;

            switch (codeByte)
            {
                case 0x01:
                    return ModbusCode.ReadCoil;
                case 0x02:
                    return ModbusCode.ReadDI;
                case 0x03:
                    return ModbusCode.ReadHolding;
                case 0x04:
                    return ModbusCode.ReadInput;
                case 0x05:
                    return ModbusCode.WriteCoil;
                case 0x06:
                    return ModbusCode.WriteReg;
                case 0x0F:
                    return ModbusCode.WriteCoils;
                case 0x10:
                    return ModbusCode.WriteRegs;
                default:
                    return ModbusCode.None;
            }
        }

        /// <summary>
        /// 根据功能码解析寄存器地址、数量和数据
        /// </summary>
        private static (int regstart, int regnum, byte[] data) ParseDataByCode(ModbusCode code, byte[] bytes)
        {
            // 排除地址和功能码，取数据部分（不含CRC）
            byte[] dataSection = bytes.Skip(2).Take(bytes.Length - 4).ToArray(); // 去掉最后2字节CRC

            switch (code)
            {
                case ModbusCode.ReadCoil:
                case ModbusCode.ReadDI:
                case ModbusCode.ReadHolding:
                case ModbusCode.ReadInput:
                    // 读操作：寄存器地址（2字节）+ 数量（2字节）
                    if (dataSection.Length < 4)
                        throw new FormatException("读操作帧数据不完整");
                    int startAddr = BitConverter.ToUInt16(dataSection.Take(2).Reverse().ToArray(), 0); // 大端转小端
                    int count = BitConverter.ToUInt16(dataSection.Skip(2).Take(2).Reverse().ToArray(), 0);
                    return (startAddr, count, dataSection);

                case ModbusCode.WriteCoil:
                    // 写单个线圈：寄存器地址（2字节）+ 值（2字节，0xFF00=ON，0x0000=OFF）
                    if (dataSection.Length < 4)
                        throw new FormatException("写线圈帧数据不完整");
                    startAddr = BitConverter.ToUInt16(dataSection.Take(2).Reverse().ToArray(), 0);
                    return (startAddr, 1, dataSection);

                case ModbusCode.WriteReg:
                    // 写单个寄存器：寄存器地址（2字节）+ 值（2字节）
                    if (dataSection.Length < 4)
                        throw new FormatException("写寄存器帧数据不完整");
                    startAddr = BitConverter.ToUInt16(dataSection.Take(2).Reverse().ToArray(), 0);
                    return (startAddr, 1, dataSection);

                case ModbusCode.WriteCoils:
                case ModbusCode.WriteRegs:
                    // 写多个：寄存器地址（2字节）+ 数量（2字节）+ 字节数（1字节）+ 数据
                    if (dataSection.Length < 5)
                        throw new FormatException("写多个帧数据不完整");
                    startAddr = BitConverter.ToUInt16(dataSection.Take(2).Reverse().ToArray(), 0);
                    count = BitConverter.ToUInt16(dataSection.Skip(2).Take(2).Reverse().ToArray(), 0);
                    return (startAddr, count, dataSection);

                default:
                    throw new NotSupportedException($"不支持的功能码: {code}");
            }
        }
        public static byte[] HexStringToCommand(string hexString)
        {
            if (string.IsNullOrWhiteSpace(hexString))
            {
                throw new ArgumentException("输入字符串不能为空");
            }

            try
            {
                // 去除字符串中的空格，按每两个字符分割为十六进制片段
                string[] hexParts = hexString.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                // 转换每个十六进制片段为字节
                byte[] command = hexParts.Select(hex => Convert.ToByte(hex, 16)).ToArray();

                return command;
            }
            catch (FormatException ex)
            {
                throw new FormatException("无效的十六进制格式，请检查输入字符串", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("转换失败", ex);
            }
        }
    }
}
