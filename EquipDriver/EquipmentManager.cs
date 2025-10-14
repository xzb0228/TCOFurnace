using Common;
using EquipDriver.Model;
using ModBusRTU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipDriver
{
    public static class EquipmentManager
    {
        //是否走仿真模式
        public static bool IsEmulatorMode { get; set; } = false;
        private static List<Equipment> equipments = new List<Equipment>();
        public static void Init(List<ParametsBase> portConfigs, bool isEmulatorMode)
        {
            foreach (var itme in portConfigs)
            {
                Equipment equipment;
                if (itme.PortType == PortType.Serial)
                {
                    equipment = new Equipment(new SerialDriver(), itme as SerialParamets);
                }
                else if (itme.PortType == PortType.TCP)
                {
                    equipment = new Equipment(new TCPDriver(), itme as TCPParamets);
                }
                else if (itme.PortType == PortType.UDP)
                {
                    equipment = new Equipment(new UDPDriver(), itme as UdpParamets);
                }
                else
                {
                    continue;
                }
                equipments.Add(equipment);
            }
            IsEmulatorMode = isEmulatorMode;
        }

        //启动队列发送命令
        public static void InitEvent()
        {
            foreach (var itme in equipments)
            {
                itme.InitEvent();
            }
        }

        //启动队列发送命令
        public static void CloseAll()
        {
            foreach (var itme in equipments)
            {
                itme.CloseAll();
            }
        }

        #region 对equipinfo中队列的命令管理,同时提供统一的命令队列入口
        public static void AddTimesModbusReg(TimesModbusReg mreg)
        {
            if (CheckReg(mreg.portName))
                equipments.FirstOrDefault(c => c.portPar.PortName == mreg.portName).equipinfo.AddTimesModbusReg(mreg);
        }
        public static void RemoveTimesModbusReg(string regName, string portName)
        {
            if (CheckReg(portName))
                equipments.FirstOrDefault(c => c.portPar.PortName == portName).equipinfo.RemoveTimesModbusReg(regName);
        }

        public static void AddoneTimeModbusReg(OneTimeModbusReg mreg)
        {
            if (CheckReg(mreg.portName))
                equipments.FirstOrDefault(c => c.portPar.PortName == mreg.portName).equipinfo.AddoneTimeModbusReg(mreg);
        }
        public static void RemoveoneTimeModbusReg(string regName, string portName)
        {
            if (CheckReg(portName))
                equipments.FirstOrDefault(c => c.portPar.PortName == portName).equipinfo.RemoveoneTimeModbusReg(regName);
        }

        public static string AddMainQueue(ModbusReg mreg)
        {
            if (CheckReg(mreg.portName))
                equipments.FirstOrDefault(c => c.portPar.PortName == mreg.portName).equipinfo.AddMainQueue(mreg);
            return "";
        }
        public static string AddSecondaryQueue(ModbusReg mreg)
        {
            if (CheckReg(mreg.portName))
                equipments.FirstOrDefault(c => c.portPar.PortName == mreg.portName).equipinfo.AddSecondaryQueue(mreg);
            return "";
        }
        private static bool CheckReg(string portName)
        {
            var equipment = equipments.FirstOrDefault(c => c.portPar.PortName == portName);
            if (equipment == null)
            {
                Loger.Fatal("没有找到名称为" + portName + "的串口");
                throw new Exception("没有找到名称为" + portName + "的串口");
            }
            return true;
        }
        #endregion
    }
}
