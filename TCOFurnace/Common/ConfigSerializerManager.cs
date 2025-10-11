using Common;
using EquipDriver;
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
                XmlNode serialPortsNode = rootNode.SelectSingleNode("Ports");
                if (serialPortsNode != null)
                {
                    foreach (XmlNode serialPortNode in serialPortsNode.SelectNodes("Port"))
                    {
                        var port = ParseSerialPort(serialPortNode);
                        config.Ports.Add(port);
                    }
                }
                GlobalPara.upperComputerConfig = config;

                //加载仪器配置信息
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
