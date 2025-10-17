using Common;
using EquipDriver.Model;
using Microsoft.Win32;
using ModBusRTU;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;
using TCOFurnace.BusinessModels;

namespace TCOFurnace.Common
{
    /// <summary>
    /// XML 配置文件序列化 / 反序列化工具类
    /// </summary>
    public static class ConfigSerializerManager
    {
        public static bool InitConfig()
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"XML\\{"UpperComputerConfig"}.xml");
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentNullException(nameof(filePath));

            var xmlDoc = new XmlDocument();

            try
            {
                xmlDoc.Load(filePath);
            }
            catch (Exception ex)
            {
                Loger.Error($"加载文件加载失败: {ex.Message}", exc:ex);
                return false;
            }

            // 获取根节点 <ControlSystem>
            XmlNode rootNode = xmlDoc.SelectSingleNode("UpperComputerConfig");
            if (rootNode == null)
            {
                Loger.Error($"XML格式错误，未找到根节点UpperComputerConfig");
                return false;
            }

            try
            {
                GlobalPara.instrumentConfig = new InstrumentConfig();
                // 解析所有串口节点 <SerialPorts>
                GlobalPara.instrumentConfig.Ports = ParsePorts(rootNode.SelectSingleNode("Ports"));

                //解析SerialPorts 信息为设备通讯类入参
                GlobalPara.instrumentConfig.ParametsBases = ParametsBases(GlobalPara.instrumentConfig.Ports);

                //加载仪器配置信息
                GlobalPara.instrumentConfig.instruments = ParseInstrument(rootNode.SelectSingleNode("Instruments"));

                //解析ModeBus命令
                GlobalPara.Regs = ParseRegs(rootNode.SelectSingleNode("ModbusCommands"));
                return true;
            }
            catch (Exception ex)
            {
                Loger.Error($"解析上位机配置文件报错: {ex.Message}", exc:ex);
                return false;
            }
        }

        /// <summary>
        /// 获取串口相关信息
        /// </summary>
        /// <param name="xmlContent"></param>
        /// <returns></returns>
        public static List<PortConfig> ParsePorts(XmlNode serialPortsNode)
        {
            List<PortConfig> Ports = new List<PortConfig>();
            if (serialPortsNode != null)
            {
                foreach (XmlNode serialPortNode in serialPortsNode.SelectNodes("Port"))
                {
                    var port = ParseSerialPort(serialPortNode);
                    Ports.Add(port);
                }
            }
            return Ports;
        }
        /// <summary>
        /// 获取串口相关信息
        /// </summary>
        /// <param name="xmlContent"></param>
        /// <returns></returns>
        public static ModbusCommands ParseRegs(XmlNode serialPortsNode)
        {
            ModbusCommands modbusCommands = new ModbusCommands();
            if (serialPortsNode != null)
            {
                //解析普通命令
                foreach (XmlNode element in serialPortsNode.SelectNodes("ModbusReg"))
                {
                    ModbusReg modbusReg = new ModbusReg
                    {
                        portName = element.Attributes["portName"]?.Value,
                        name = element.Attributes["name"]?.Value
                    };

                    // 解析命令字符串
                    string commend = element.Attributes["commend"]?.Value;
                    if (!string.IsNullOrEmpty(commend))
                    {
                        byte[] commandBytes = ParseCommandBytes(commend);
                        ParseCommandBytesToReg(modbusReg, commandBytes);
                    }
                    modbusCommands.AddModbusRegs(modbusReg);
                }

                //解析循环执行命令
                foreach (XmlNode element in serialPortsNode.SelectNodes("TimesModbusReg"))
                {
                    ModbusReg modbusReg = new ModbusReg
                    {
                        portName = element.Attributes["portName"]?.Value,
                        name = element.Attributes["name"]?.Value
                    };

                    // 解析命令字符串
                    string commend = element.Attributes["commend"]?.Value;
                    if (!string.IsNullOrEmpty(commend))
                    {
                        byte[] commandBytes = ParseCommandBytes(commend);
                        ParseCommandBytesToReg(modbusReg, commandBytes);
                    }

                    TimesModbusReg timesModbusReg = new TimesModbusReg(modbusReg);
                    timesModbusReg.IntervalMs = int.Parse(element.Attributes["IntervalMs"]?.Value);
                    modbusCommands.AddTimesModbusReg(timesModbusReg);
                }

                //解析定时命令
                foreach (XmlNode element in serialPortsNode.SelectNodes("OneTimeModbusReg"))
                {
                    ModbusReg modbusReg = new ModbusReg
                    {
                        portName = element.Attributes["portName"]?.Value,
                        name = element.Attributes["name"]?.Value
                    };

                    // 解析命令字符串
                    string commend = element.Attributes["commend"]?.Value;
                    if (!string.IsNullOrEmpty(commend))
                    {
                        byte[] commandBytes = ParseCommandBytes(commend);
                        ParseCommandBytesToReg(modbusReg, commandBytes);
                    }

                    OneTimeModbusReg oneTimeModbusReg = new OneTimeModbusReg(modbusReg);
                    oneTimeModbusReg.SendTime = element.Attributes["name"]?.Value == null ? DateTime.MinValue : Convert.ToDateTime(element.Attributes["name"]?.Value);
                    modbusCommands.AddOneTimeModbusReg(oneTimeModbusReg);
                }
            }
            return modbusCommands;
        }

        /// <summary>
        /// 将命令字符串（如"0x0A 0x05 0x00 0x00 0xFF 0x00 0x8C 0x3A"）转换为字节数组
        /// </summary>
        private static byte[] ParseCommandBytes(string commend)
        {
            // 分割字符串并移除空项
            string[] hexParts = commend.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            byte[] bytes = new byte[hexParts.Length];

            for (int i = 0; i < hexParts.Length; i++)
            {
                // 移除0x前缀并转换为字节
                string hex = hexParts[i].Replace("0x", "").Replace("0X", "").Replace("oX", "").Replace("ox", "");
                if (!byte.TryParse(hex, System.Globalization.NumberStyles.HexNumber, null, out bytes[i]))
                {
                    throw new FormatException($"无效的十六进制值: {hexParts[i]}");
                }
            }

            return bytes;
        }

        /// 从命令字节数组中提取地址、功能码等信息到ModbusReg对象
        /// 重点适配新的ModbusCode枚举值与功能码的对应关系
        /// </summary>
        private static void ParseCommandBytesToReg(ModbusReg reg, byte[] commandBytes)
        {
            // 基本验证：最小长度需要包含地址(1) + 功能码(1) + 数据(至少2字节) + CRC(2字节)
            if (commandBytes.Length < 6)
                throw new ArgumentException("命令字节数组长度不足，无法解析");

            // 提取地址（第1个字节）
            reg.addr = commandBytes[0];

            // 提取功能码（第2个字节）并转换为对应的ModbusCode枚举
            byte codeByte = commandBytes[1];
            switch (codeByte)
            {
                case 0x01:
                    reg.code = ModbusCode.ReadCoil;
                    break;
                case 0x02:
                    reg.code = ModbusCode.ReadDI;
                    break;
                case 0x03:
                    reg.code = ModbusCode.ReadHolding;
                    break;
                case 0x04:
                    reg.code = ModbusCode.ReadInput;
                    break;
                case 0x05:
                    reg.code = ModbusCode.WriteCoil;
                    break;
                case 0x06:
                    reg.code = ModbusCode.WriteReg;
                    break;
                case 0x0F:
                    reg.code = ModbusCode.WriteCoils;
                    break;
                case 0x10:
                    reg.code = ModbusCode.WriteRegs;
                    break;
                default:
                    reg.code = ModbusCode.None;
                    break;
            }

            // 提取起始地址（第3-4字节，高位在前）
            reg.regstart = (commandBytes[2] << 8) | commandBytes[3];

            // 根据功能码解析寄存器数量和数据部分
            switch (reg.code)
            {
                case ModbusCode.ReadCoil:
                case ModbusCode.ReadDI:
                case ModbusCode.ReadHolding:
                case ModbusCode.ReadInput:
                case ModbusCode.WriteCoils:
                case ModbusCode.WriteRegs:
                    // 这些功能码的寄存器数量在第5-6字节
                    reg.regnum = (commandBytes[4] << 8) | commandBytes[5];

                    // 提取数据部分（排除地址、功能码、起始地址、数量、CRC）
                    int dataStartIndex = 6;
                    // 写多个命令有数据长度字节，需要跳过
                    if (reg.code == ModbusCode.WriteCoils || reg.code == ModbusCode.WriteRegs)
                    {
                        dataStartIndex = 7; // 跳过数据长度字节
                    }
                    int dataLength = commandBytes.Length - dataStartIndex - 2; // 减去CRC的2字节
                    if (dataLength > 0)
                    {
                        reg.vbyte = new byte[dataLength];
                        Array.Copy(commandBytes, dataStartIndex, reg.vbyte, 0, dataLength);
                    }
                    break;

                case ModbusCode.WriteCoil:
                case ModbusCode.WriteReg:
                    // 单个写命令的寄存器数量默认为1
                    reg.regnum = 1;

                    // 数据部分在第5-6字节
                    reg.vbyte = new byte[2];
                    reg.vbyte[0] = commandBytes[4];
                    reg.vbyte[1] = commandBytes[5];
                    break;

                case ModbusCode.None:
                    throw new NotSupportedException($"非法功能码: 0x{codeByte:X2}");

                default:
                    throw new NotSupportedException($"不支持的功能码: {reg.code}");
            }
        }

        /// <summary>
        ///  //解析SerialPorts 信息为设备通讯类入参
        /// </summary>
        /// <param name="xmlContent"></param>
        /// <returns></returns>
        public static List<ParametsBase> ParametsBases(List<PortConfig> portConfigs)
        {
            List<ParametsBase> Ports = new List<ParametsBase>();
            if (portConfigs != null)
            {
                foreach (var item in portConfigs)
                {
                    if (item.PortType == PortType.Serial)
                    {
                        SerialParamets serialParamets = new SerialParamets();
                        serialParamets.PortType = PortType.Serial;
                        serialParamets.PortName = item.PortName;
                        serialParamets.Com = item.Com;
                        serialParamets.BaudRate = item.BaudRate;
                        serialParamets.DataBits = item.DataBits;
                        serialParamets.StopBits = Convert.ToInt16(item.StopBits) == 1 ? StopBits.One : StopBits.Two;
                        serialParamets.Parity = Convert.ToInt16(item.Parity) == 1 ? Parity.Even : (Convert.ToInt16(item.Parity) == 2 ? Parity.Odd : Parity.None);
                        Ports.Add(serialParamets);
                    }
                    else if (item.PortType == PortType.TCP)
                    {
                        TCPParamets serialParamets = new TCPParamets();
                        serialParamets.PortType = PortType.Serial;
                        serialParamets.PortName = item.PortName;
                        serialParamets.IP = item.IP;
                        serialParamets.PortNum = item.PortNum;

                        Ports.Add(serialParamets);
                    }
                    else if (item.PortType == PortType.UDP)
                    {
                        UdpParamets serialParamets = new UdpParamets();
                        serialParamets.IP = serialParamets.IP;
                        serialParamets.PortNum = serialParamets.PortNum;
                        serialParamets.LocalPort = serialParamets.LocalPort;
                        Ports.Add(serialParamets);
                    }
                }
            }
            return Ports;
        }



        /// <summary>
        /// 获取仪器的相关信息
        /// </summary>
        /// <param name="xmlContent"></param>
        /// <returns></returns>
        public static List<Instrument> ParseInstrument(XmlNode serialPortsNode)
        {
            List<Instrument> instruments = new List<Instrument>();

            // 解析根节点下的所有Instrument节点
            foreach (XmlNode instrumentElement in serialPortsNode.SelectNodes("Instrument"))
            {
                var instrument = new Instrument
                {
                    // 解析属性
                    InstrumentId = instrumentElement.Attributes["InstrumentId"].Value,
                    Name = instrumentElement.Attributes["Name"].Value
                };

                instrument.Settings = new List<InstrumentSetting>();

                // 解析Settings子节点
                XmlNode settingsElement = instrumentElement.SelectSingleNode("Settings");
                if (settingsElement != null)
                {
                    foreach (XmlNode settingElement in settingsElement.SelectNodes("Setting"))
                    {
                        instrument.Settings.Add(new InstrumentSetting
                        {
                            Name = settingElement.Attributes["Name"].Value,
                            Value = settingElement.Attributes["Value"].Value
                        });
                    }
                }
                instruments.Add(instrument);
            }

            return instruments;
        }
        /// <summary>
        /// 解析单个串口节点
        /// </summary>
        private static PortConfig ParseSerialPort(XmlNode serialPortNode)
        {
            var serialPort = new PortConfig();

            // 读取串口名称
            if (serialPortNode.Attributes["PortType"] != null)
            {
                Enum.TryParse<PortType>(serialPortNode.Attributes["PortType"].Value, ignoreCase: false, out PortType portType);
                serialPort.PortType = portType;
            }
            else
            {
                Loger.Fatal("配置文件 UpperComputerConfig中 Port 节点 PortType 没有配置");
                throw new Exception("配置文件 UpperComputerConfig中 Port 节点 PortType 没有配置");
            }

            // 读取串口名称
            if (serialPortNode.Attributes["PortName"] != null)
            {
                serialPort.PortName = serialPortNode.Attributes["PortName"].Value;
            }
            else
            {
                Loger.Fatal("配置文件 UpperComputerConfig中 Port 节点 PortName 没有配置");
                throw new Exception("配置文件 UpperComputerConfig中 Port 节点 PortName 没有配置");
            }
            // 读取该端口描述
            if (serialPortNode.Attributes["Describe"] != null)
            {
                serialPort.Describe = serialPortNode.Attributes["Describe"].Value;
            }



            // 为串口
            if (serialPort.PortType == PortType.Serial)
            {
                serialPort.Com = GetStringValue(serialPortNode, "Com", "");
                if (string.IsNullOrEmpty(serialPort.Com))
                {
                    Loger.Fatal("配置文件 UpperComputerConfig中 Port 节点 Com 没有配置");
                    throw new Exception("配置文件 UpperComputerConfig中 Port 节点 Com 没有配置");
                }
                serialPort.BaudRate = GetIntValue(serialPortNode, "BaudRate", 9600);
                serialPort.Parity = GetStringValue(serialPortNode, "Parity", "None");
                serialPort.DataBits = GetIntValue(serialPortNode, "DataBits", 8);
                serialPort.StopBits = GetIntValue(serialPortNode, "StopBits", 1);
            }
            else if (serialPort.PortType == PortType.TCP)
            {
                serialPort.IP = GetStringValue(serialPortNode, "IP", "");
                serialPort.PortNum = GetIntValue(serialPortNode, "PortNum", 0);
                if (string.IsNullOrEmpty(serialPort.IP))
                {
                    Loger.Fatal("配置文件 UpperComputerConfig中 Port 节点 IP 没有配置");
                    throw new Exception("配置文件 UpperComputerConfig中 Port 节点 IP 没有配置");
                }

                if (serialPort.PortNum == 0)
                {
                    Loger.Fatal("配置文件 UpperComputerConfig中 Port 节点 PortNum 配置错误");
                    throw new Exception("配置文件 UpperComputerConfig中 Port 节点 PortNum 配置错误");
                }
            }
            else if (serialPort.PortType == PortType.UDP)
            {
                serialPort.IP = GetStringValue(serialPortNode, "IP", "");
                serialPort.PortNum = GetIntValue(serialPortNode, "PortNum", 0);
                serialPort.LocalPort = GetIntValue(serialPortNode, "LocalPort", 0);

                if (string.IsNullOrEmpty(serialPort.IP))
                {
                    Loger.Fatal("配置文件 UpperComputerConfig中 Port 节点 IP 没有配置");
                    throw new Exception("配置文件 UpperComputerConfig中 Port 节点 IP 没有配置");
                }

                if (serialPort.PortNum == 0)
                {
                    Loger.Fatal("配置文件 UpperComputerConfig中 Port 节点 PortNum 配置错误");
                    throw new Exception("配置文件 UpperComputerConfig中 Port 节点 PortNum 配置错误");
                }

                if (serialPort.LocalPort == 0)
                {
                    Loger.Fatal("配置文件 UpperComputerConfig中 Port 节点 LocalPort 配置错误");
                    throw new Exception("配置文件 UpperComputerConfig中 Port 节点 LocalPort 配置错误");
                }
            }
            else
            {
                Loger.Fatal("配置文件 UpperComputerConfig中 Port 节点 PortType 通讯方式配置错误");
                throw new Exception("配置文件 UpperComputerConfig中 Port 节点 PortType 通讯方式配置错误");
            }
            return serialPort;
        }

        // 辅助方法：获取节点的字符串值
        private static string GetStringValue(XmlNode parentNode, string nodeName, string defaultValue)
        {
            XmlNode node = parentNode.SelectSingleNode(nodeName);
            return node?.InnerText ?? defaultValue;
        }

        // 辅助方法：获取节点的整数值
        private static int GetIntValue(XmlNode parentNode, string nodeName, int defaultValue)
        {
            XmlNode node = parentNode.SelectSingleNode(nodeName);
            if (node != null && int.TryParse(node.InnerText, out int value))
            {
                return value;
            }
            return defaultValue;
        }
    }
}
