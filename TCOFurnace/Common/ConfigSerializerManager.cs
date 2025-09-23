using Common;
using Microsoft.Win32;
using ModBusRTU;
using ModBusRTU.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using TCOFurnace.Models;
using TCOFurnace.Properties;

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

            var config = new UpperComputerConfig();
            var xmlDoc = new XmlDocument();

            try
            {
                xmlDoc.Load(filePath);
            }
            catch (Exception ex)
            {
                Loger.Error($"加载文件加载失败: {ex.Message}");
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
                // 解析所有串口节点 <SerialPorts>
                XmlNode serialPortsNode = rootNode.SelectSingleNode("SerialPorts");
                if (serialPortsNode != null)
                {
                    foreach (XmlNode serialPortNode in serialPortsNode.SelectNodes("SerialPort"))
                    {
                        var serialPort = ParseSerialPort(serialPortNode);
                        config.SerialPorts.Add(serialPort);
                    }
                }
                GlobalPara.upperComputerConfig = config;

                //预定义的命令集合
                GlobalPara.upperComputerConfig.instruments = ParseInstrument(rootNode.SelectSingleNode("Instruments"));

                return true;
            }
            catch (Exception ex)
            {
                Loger.Error($"解析上位机配置文件报错: {ex.Message}");
                return false;
            }
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
            foreach (XmlNode instrumentElement  in serialPortsNode.SelectNodes("Instrument"))
            {
                var instrument = new Instrument
                {
                    // 解析属性
                    InstrumentId = instrumentElement.Attributes["InstrumentId"].Value ,
                    Name = instrumentElement.Attributes["Name"].Value
                };

                instrument.MBRegs = new List<InstrumentMBReg>();
                instrument.Settings = new List<InstrumentSetting>();

                // 解析MBRegs子节点
                XmlNode mbRegsElement = instrumentElement.SelectSingleNode("MBRegs");
                if (mbRegsElement != null)
                {
                    foreach (XmlNode regElement in mbRegsElement.SelectNodes("Reg"))
                    {
                        instrument.MBRegs.Add(new InstrumentMBReg
                        {
                            Id = regElement.Attributes["Id"].Value,
                            RegName = regElement.Attributes["RegName"].Value,
                            Com = regElement.Attributes["Com"].Value
                        });
                    }
                }

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
        private static SerialPortConfig ParseSerialPort(XmlNode serialPortNode)
        {
            var serialPort = new SerialPortConfig();
            // 读取串口号属性
            if (serialPortNode.Attributes["Com"] != null)
            {
                serialPort.Com = serialPortNode.Attributes["Com"].Value;
            }
            else
            {
                Loger.Fatal("配置文件 UpperComputerConfig中 SerialPort 节点 Com 没有配置");
                throw new Exception("Com 没有配置");
            }


            // 读取串口通信参数
            serialPort.BaudRate = GetIntValue(serialPortNode, "BaudRate", 9600);
            serialPort.Parity = GetStringValue(serialPortNode, "Parity", "None");
            serialPort.DataBits = GetIntValue(serialPortNode, "DataBits", 8);
            serialPort.StopBits = GetIntValue(serialPortNode, "StopBits", 1);

            // 解析控制板节点
            XmlNode controlBoardsNode = serialPortNode.SelectSingleNode("ControlBoards");
            if (controlBoardsNode != null)
            {
                foreach (XmlNode controlBoardNode in controlBoardsNode.SelectNodes("ControlBoard"))
                {
                    var controlBoard = ParseControlBoard(controlBoardNode);
                    serialPort.ControlBoards.Add(controlBoard);
                }
            }

            serialPort.modbusRegs = new List<ModbusReg>();
            #region 命令信息解析

           
            XmlNode modbusCommands = serialPortNode.SelectSingleNode("ModbusCommands");
            if (modbusCommands != null)
            {
                //注入普通命令
                foreach (XmlNode controlBoardNode in modbusCommands.SelectNodes("CModbusReg"))
                {
                    string name = "";
                    string commend = "";

                    if (controlBoardNode.Attributes["name"] != null)
                    {
                        name = controlBoardNode.Attributes["name"].Value.Trim();
                    }
                    if (controlBoardNode.Attributes["commend"] != null)
                    {
                        commend = controlBoardNode.Attributes["commend"].Value.Trim();
                    }
                    if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(commend))
                    {
                        serialPort.modbusRegs.Add(new ModbusReg(name, commend));

                    }
                }

                serialPort.timesModbusRegs = new List<TimesModbusReg>();
                //循环命令
                foreach (XmlNode controlBoardNode in modbusCommands.SelectNodes("TimesModbusReg"))
                {
                    string name = "";
                    string commend = "";
                    int IntervalMs = 0;
                    if (controlBoardNode.Attributes["name"] != null)
                    {
                        name = controlBoardNode.Attributes["name"].Value.Trim();
                    }
                    if (controlBoardNode.Attributes["commend"] != null)
                    {
                        commend = controlBoardNode.Attributes["commend"].Value.Trim();
                    }
                    if (controlBoardNode.Attributes["IntervalMs"] != null)
                    {
                        int.TryParse( controlBoardNode.Attributes["IntervalMs"].Value.Trim(),out IntervalMs);
                    }
                    if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(commend))
                    {
                        TimesModbusReg timesModbusReg = new TimesModbusReg(new ModbusReg(name, commend));
                        if (IntervalMs > 0)
                        {
                            timesModbusReg.IntervalMs= IntervalMs;
                        }
                        serialPort.timesModbusRegs.Add(timesModbusReg);
                    }
                }

                serialPort.oneTimeModbusRegs = new List<OneTimeModbusReg>();
                //单次命令
                foreach (XmlNode controlBoardNode in modbusCommands.SelectNodes("OneTimeModbusReg"))
                {
                    string name = "";
                    string commend = "";
                    DateTime SendTime =DateTime.Now;
                    if (controlBoardNode.Attributes["name"] != null)
                    {
                        name = controlBoardNode.Attributes["name"].Value.Trim();
                    }
                    if (controlBoardNode.Attributes["commend"] != null)
                    {
                        commend = controlBoardNode.Attributes["commend"].Value.Trim();
                    }
                    if (controlBoardNode.Attributes["SendTime"] != null)
                    {
                        DateTime.TryParse(controlBoardNode.Attributes["SendTime"].Value.Trim(), out SendTime);
                    }
                    if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(commend))
                    {
                        OneTimeModbusReg oneTimeModbusReg = new OneTimeModbusReg(new ModbusReg(name, commend));
                        if (SendTime != null)
                        {
                            oneTimeModbusReg.SendTime = SendTime;
                        }
                        serialPort.oneTimeModbusRegs.Add(oneTimeModbusReg);
                    }
                }
            }

            #endregion
            return serialPort;
        }

        /// <summary>
        /// 解析单个控制板节点
        /// </summary>
        private static ControlBoardConfig ParseControlBoard(XmlNode controlBoardNode)
        {
            var controlBoard = new ControlBoardConfig();

            // 读取控制板属性
            if (controlBoardNode.Attributes["deviceAddress"] != null)
            {
                int.TryParse(controlBoardNode.Attributes["deviceAddress"].Value, out int address);
                controlBoard.DeviceAddress = address;
            }

            if (controlBoardNode.Attributes["name"] != null)
            {
                controlBoard.Name = controlBoardNode.Attributes["name"].Value;
            }
            if (controlBoardNode.Attributes["offsetAddress"] != null)
            {
                controlBoard.OffsetAddress = controlBoardNode.Attributes["offsetAddress"].Value;
            }
            // 解析线圈配置
            XmlNode coilsNode = controlBoardNode.SelectSingleNode("Coils");
            if (coilsNode != null && coilsNode.Attributes["totalCount"] != null)
            {
                int.TryParse(coilsNode.Attributes["totalCount"].Value, out int count);
                controlBoard.Coils = new CoilsConfig { TotalCount = count };

                // 解析通道配置
                foreach (XmlNode channelNode in coilsNode.SelectNodes("Channel"))
                {
                    var channel = ParseChannel(channelNode);
                    controlBoard.Coils.Channels.Add(channel);
                }
            }

            // 解析离散输入配置
            XmlNode discreteInputsNode = controlBoardNode.SelectSingleNode("DiscreteInputs");
            if (discreteInputsNode != null && discreteInputsNode.Attributes["totalCount"] != null)
            {
                int.TryParse(discreteInputsNode.Attributes["totalCount"].Value, out int count);
                controlBoard.DiscreteInputs = new DiscreteInputsConfig { TotalCount = count };
                // 解析通道配置
                foreach (XmlNode channelNode in discreteInputsNode.SelectNodes("Channel"))
                {
                    var channel = ParseChannel(channelNode);
                    controlBoard.DiscreteInputs.Channels.Add(channel);
                }
            }
            // 解析保持寄存器配置
            controlBoard.HoldingRegisters = ParseRegisters(controlBoardNode, "HoldingRegisters");

            // 解析输入寄存器配置
            controlBoard.InputRegisters = ParseInputRegisters(controlBoardNode, "InputRegisters");

            return controlBoard;
        }

        /// <summary>
        /// 解析保持寄存器节点
        /// </summary>
        private static HoldingRegistersConfig ParseRegisters(XmlNode parentNode, string nodeName)
        {
            var registers = new HoldingRegistersConfig();
            XmlNode registersNode = parentNode.SelectSingleNode(nodeName);

            if (registersNode != null)
            {
                // 读取总数量
                if (registersNode.Attributes["totalCount"] != null)
                {
                    int.TryParse(registersNode.Attributes["totalCount"].Value, out int count);
                    registers.TotalCount = count;
                }

                // 解析通道配置
                foreach (XmlNode channelNode in registersNode.SelectNodes("Channel"))
                {
                    var channel = ParseChannel(channelNode);
                    registers.Channels.Add(channel);
                }
            }
            return registers;
        }

        /// <summary>
        /// 解析输入寄存器节点
        /// </summary>
        private static InputRegistersConfig ParseInputRegisters(XmlNode parentNode, string nodeName)
        {
            var registers = new InputRegistersConfig();
            XmlNode registersNode = parentNode.SelectSingleNode(nodeName);

            if (registersNode != null)
            {
                // 读取总数量
                if (registersNode.Attributes["totalCount"] != null)
                {
                    int.TryParse(registersNode.Attributes["totalCount"].Value, out int count);
                    registers.TotalCount = count;
                }

                // 解析通道配置
                foreach (XmlNode channelNode in registersNode.SelectNodes("Channel"))
                {
                    var channel = ParseChannel(channelNode);
                    registers.Channels.Add(channel);
                }
            }
            return registers;
        }

        /// <summary>
        /// 解析通道节点
        /// </summary>
        private static ChannelConfig ParseChannel(XmlNode channelNode)
        {
            var channel = new ChannelConfig();

            if (channelNode.Attributes["id"] != null)
            {
                int.TryParse(channelNode.Attributes["id"].Value, out int id);
                channel.Id = id;
            }

            if (channelNode.Attributes["name"] != null)
            {
                channel.Name = channelNode.Attributes["name"].Value;
            }

            if (channelNode.Attributes["unit"] != null)
            {
                channel.Unit = channelNode.Attributes["unit"].Value;
            }

            if (channelNode.Attributes["dataFormat"] != null)
            {
                channel.DataFormat = channelNode.Attributes["dataFormat"].Value;
            }

            return channel;
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

        public static void ParseModbusCommandsConfig(XmlNode modbusCommands)
        {
            GlobalPara.ModbusCommands = new List<ModbusReg>();
            // 循环遍历所有PortReference节点
            foreach (XElement portElement in modbusCommands.SelectNodes("ModbusCommand"))
            {
                string _name = portElement.Attribute("name")?.Value;
                int addr = int.Parse(portElement.Attribute("addr")?.Value);
                int regstart = int.Parse(portElement.Attribute("regstart")?.Value);
                Enum.TryParse<ModbusCode>(portElement.Attribute("code")?.Value, out ModbusCode code);
                int regnum = int.Parse(portElement.Attribute("regnum")?.Value);
                byte[] vbyte = MBRTU.HexToByte(portElement.Attribute("vbyte")?.Value);

                // 解析端口引用属性
                var portRef = new ModbusReg(_name, addr, code, regstart, regnum, vbyte);

                if (GlobalPara.ModbusCommands.FirstOrDefault() == null)
                    GlobalPara.ModbusCommands.Add(portRef);
            }
        }
    }
}
