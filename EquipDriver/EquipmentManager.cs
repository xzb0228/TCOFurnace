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
        private static Random random = new Random();
        public static void Init(List<ParametsBase> portConfigs, bool isEmulatorMode)
        {
            //这句代码必须先执行
            IsEmulatorMode = isEmulatorMode;

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
            ChangeEmulatorModeReg(mreg);
            if (CheckReg(mreg.portName))
                equipments.FirstOrDefault(c => c.portPar.PortName == mreg.portName).equipinfo.AddTimesModbusReg(mreg);
        }
        public static void RemoveTimesModbusReg(string regName, string portName)
        {
            if (CheckReg(portName))
                equipments.FirstOrDefault(c => c.portPar.PortName == portName).equipinfo.RemoveTimesModbusReg(regName);
        }

        public static void AddOneTimeModbusReg(OneTimeModbusReg mreg)
        {
            ChangeEmulatorModeReg(mreg);
            if (CheckReg(mreg.portName))
                equipments.FirstOrDefault(c => c.portPar.PortName == mreg.portName).equipinfo.AddOneTimeModbusReg(mreg);
        }
        public static void RemoveOneTimeModbusReg(string regName, string portName)
        {
            if (CheckReg(portName))
                equipments.FirstOrDefault(c => c.portPar.PortName == portName).equipinfo.RemoveOneTimeModbusReg(regName);
        }

        public static string AddMainQueue(ModbusReg mreg)
        {
            ChangeEmulatorModeReg(mreg);
            if (CheckReg(mreg.portName))
                equipments.FirstOrDefault(c => c.portPar.PortName == mreg.portName).equipinfo.AddMainQueue(mreg);
            return "";
        }
        public static string AddSecondaryQueue(ModbusReg mreg)
        {
            ChangeEmulatorModeReg(mreg);
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

        private static void ChangeEmulatorModeReg(ModbusReg reg)
        {

            if (!IsEmulatorMode)
                return;
            // 根据不同功能码生成模拟响应
            switch (reg.code)
            {
                case ModbusCode.ReadCoil:          // 0x01 读线圈
                case ModbusCode.ReadDI:           // 0x02 读离散输入
                    reg.ResponseData = new int[1] { reg.vbyte[0] == 0xff ? 1 : 0 };
                    break;
                case ModbusCode.ReadHolding:      // 0x03 读保持寄存器
                case ModbusCode.ReadInput:        // 0x04 读输入寄存器
                                                  // 寄存器是16位值(0-65535)
                    reg.ResponseData = new int[reg.regnum];
                    for (int i = 0; i < reg.regnum; i++)
                    {
                        // 模拟有意义的寄存器值，如温度、压力等
                        // 这里使用起始地址+偏移量作为基础值
                        if (reg.name == "1流量计流量读" || reg.name == "2流量计流量读")
                            reg.ResponseData[i] = random.Next(4000, 20000);
                        if (reg.name == "1路温度回传" || reg.name == "2路温度回传")
                            reg.ResponseData[i] = random.Next(50, 700);
                    }
                    break;

                case ModbusCode.WriteCoil:        // 0x05 写单个线圈
                    reg.ResponseData = new int[1] { reg.vbyte[0] == 0xff ? 1 : 0 };
                    break;

                case ModbusCode.WriteReg:         // 0x06 写单个寄存器
                    ushort[] uRegs1 = MBRTU.BytesToRegisters(reg.vbyte);
                    reg.ResponseData = new int[1] { (int)uRegs1[0] };
                    break;

                case ModbusCode.WriteCoils:       // 0x0F 写多个线圈
                    reg.ResponseData = new int[reg.regnum];
                    bool[] bCoils = MBRTU.BytesToBools(reg.vbyte, reg.regnum);
                    for (int i = 0; i < bCoils.Length; i++)
                    {
                        reg.ResponseData[i] = bCoils[i] ? 1 : 0;
                    }
                    break;

                case ModbusCode.WriteRegs:        // 0x10 写多个寄存器
                    reg.ResponseData = new int[reg.regnum];
                    ushort[] uRegs = MBRTU.BytesToRegisters(reg.vbyte);
                    for (int i = 0; i < uRegs.Length; i++)
                    {
                        reg.ResponseData[i] = (int)uRegs[i];
                    }
                    break;

                default:
                    reg.ResponseData = new int[0];
                    break;
            }
        }
        #endregion
    }
}
