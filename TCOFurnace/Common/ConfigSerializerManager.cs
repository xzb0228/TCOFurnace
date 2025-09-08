using Common;
using Microsoft.Win32;
using ModBusRTU.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

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
                Log.Error($"加载文件加载失败: {ex.Message}");
                return false;
            }



            // 获取根节点 <ControlSystem>
            XmlNode rootNode = xmlDoc.SelectSingleNode("UpperComputerConfig");
            if (rootNode == null)
            {
                Log.Error($"XML格式错误，未找到根节点UpperComputerConfig");
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
                return true;
            }
            catch (Exception ex)
            {
                Log.Error($"解析上位机配置文件报错: {ex.Message}");
                return false;
            }

        }

        /// <summary>
        /// 解析单个串口节点
        /// </summary>
        private static SerialPortConfig ParseSerialPort(XmlNode serialPortNode)
        {
            var serialPort = new SerialPortConfig();

            // 读取串口号属性
            if (serialPortNode.Attributes["id"] != null)
            {
                serialPort.Id = serialPortNode.Attributes["id"].Value;
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
    }
}
