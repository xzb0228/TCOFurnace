using Common;
using ModBusRTU;
using ModBusRTU.Model;
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
        private static bool IsEmulatorMode = false;
        private static List<Equipment> equipments = new List<Equipment>();
        public static void Init(List<PortConfig> portConfigs, bool isEmulatorMode)
        {
            foreach (var itme in portConfigs)
            {
                Equipment equipment;
                if (itme.PortType == PortType.Serial)
                {
                    equipment = new Equipment(new SerialDriver(), itme);
                    equipment.ConnSerial();
                }
                else if (itme.PortType == PortType.TCP)
                {
                    equipment = new Equipment(new TCPDriver(), itme);
                    equipment.ConnTCP();
                }
                else if (itme.PortType == PortType.UDP)
                {
                    equipment = new Equipment(new UDPDriver(), itme);
                    equipment.ConnUDP();
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

        #region 对equipinfo中队列的命令管理,同时提供统一的命令队列入口
        public static void AddTimesModbusReg(TimesModbusReg mreg)
        {

            var equipment = equipments.FirstOrDefault(c => c.PortConfig.PortName == mreg.portName);
            if (equipment == null)
            {
                Loger.Fatal("没有找到名称为" + mreg.portName + "的串口");
                throw new Exception("没有找到名称为" + mreg.portName + "的串口");
            }
            equipment.equipinfo.AddTimesModbusReg(mreg);
        }
        public static void RemoveTimesModbusReg(string regName, string portName)
        {
            var equipment = equipments.FirstOrDefault(c => c.PortConfig.PortName == portName);
            if (equipment == null)
            {
                Loger.Fatal("没有找到名称为" + portName + "的串口");
                throw new Exception("没有找到名称为" + portName + "的串口");
            }
            equipment.equipinfo.RemoveTimesModbusReg(regName);
        }

        public static void AddoneTimeModbusReg(OneTimeModbusReg mreg)
        {
            var equipment = equipments.FirstOrDefault(c => c.PortConfig.PortName == mreg.portName);
            if (equipment == null)
            {
                Loger.Fatal("没有找到名称为" + mreg.portName + "的串口");
                throw new Exception("没有找到名称为" + mreg.portName + "的串口");
            }
            equipment.equipinfo.AddoneTimeModbusReg(mreg);
        }
        public static void RemoveoneTimeModbusReg(string regName, string portName)
        {
            var equipment = equipments.FirstOrDefault(c => c.PortConfig.PortName == portName);
            if (equipment == null)
            {
                Loger.Fatal("没有找到名称为" + portName + "的串口");
                throw new Exception("没有找到名称为" + portName + "的串口");
            }
            equipment.equipinfo.RemoveoneTimeModbusReg(regName);
        }

        public static string AddMainQueue(ModbusReg mreg)
        {
            var equipment = equipments.FirstOrDefault(c => c.PortConfig.PortName == mreg.portName);
            if (equipment == null)
            {
                Loger.Fatal("没有找到名称为" + mreg.portName + "的串口");
                throw new Exception("没有找到名称为" + mreg.portName + "的串口");
            }
            equipment.equipinfo.AddMainQueue(mreg);
            return "";
        }
        public static string AddSecondaryQueue(ModbusReg mreg)
        {
            var equipment = equipments.FirstOrDefault(c => c.PortConfig.PortName == mreg.portName);
            if (equipment == null)
            {
                Loger.Fatal("没有找到名称为" + mreg.portName + "的串口");
                throw new Exception("没有找到名称为" + mreg.portName + "的串口");
            }
            equipment.equipinfo.AddSecondaryQueue(mreg);
            return "";
        }
        #endregion
    }
}
