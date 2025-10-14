using Common;
using ModBusRTU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TCOFurnace.BusinessModels
{
    public class ModbusCommands
    {
        private List<ModbusReg> modbusRegs = new List<ModbusReg>();
        List<TimesModbusReg> modbusRegsTimes = new List<TimesModbusReg>();
        List<OneTimeModbusReg> oneTimeModbusReg = new List<OneTimeModbusReg>();

        /// <summary>
        /// 添加普通命令
        /// </summary>
        /// <param name="modbusReg"></param>
        public void AddModbusRegs(ModbusReg modbusReg)
        {
            Loger.Info("添加了普通命令 " + BuildCommandString(modbusReg));
            modbusRegs.Add(modbusReg);
        }

        /// <summary>
        /// 添加循环执行命令
        /// </summary>
        /// <param name="modbusReg"></param>
        public void AddTimesModbusReg(TimesModbusReg modbusReg)
        {
            Loger.Info("添加了普通命令 " + BuildCommandString(modbusReg) + ";  循环间隔为" + modbusReg.IntervalMs);
            modbusRegsTimes.Add(modbusReg);
        }
        /// <summary>
        /// 添加定时命令
        /// </summary>
        /// <param name="modbusReg"></param>
        public void AddOneTimeModbusReg(OneTimeModbusReg modbusReg)
        {
            Loger.Info("添加了普通命令 " + BuildCommandString(modbusReg) + "; 设置的执行时间 " + modbusReg.SendTime.ToString("yyyyMMddHHmmss"));
            oneTimeModbusReg.Add(modbusReg);
        }

        public string BuildCommandString(ModbusReg modbusReg)
        {
            // 先构建字节数组形式的命令
            byte[] commandBytes = BuildCommand(modbusReg);

            // 将字节数组转换为十六进制字符串
            StringBuilder sb = new StringBuilder();
            foreach (byte b in commandBytes)
            {
                // 每个字节转换为两位十六进制，大写形式
                sb.AppendFormat("{0:X2} ", b);
            }

            // 移除最后一个空格并返回
            return sb.ToString().TrimEnd();
        }
        private byte[] BuildCommand(ModbusReg modbusReg)
        {
            if (modbusReg.regnum < 1 && (modbusReg.code != ModbusCode.WriteCoil && modbusReg.code != ModbusCode.WriteReg))
                throw new ArgumentOutOfRangeException(nameof(modbusReg.regnum), "寄存器数量必须大于0");

            // 计算基本命令长度
            int baseLength = 6; // 地址(1) + 功能码(1) + 起始地址(2) + 数量(2)
            int dataLength = (modbusReg.vbyte?.Length ?? 0);

            // 对于写多个寄存器/线圈的命令，需要包含数据长度字段
            bool isWriteMultiple = modbusReg.code == ModbusCode.WriteCoil || modbusReg.code == ModbusCode.WriteRegs;
            if (isWriteMultiple)
                baseLength++; // 增加数据长度字节

            // 创建命令数组
            byte[] command = new byte[baseLength + dataLength + 2]; // +2是CRC校验位
            int index = 0;

            // 填充地址
            command[index++] = (byte)modbusReg.addr;

            // 填充功能码
            command[index++] = (byte)modbusReg.code;

            // 填充起始地址（高字节在前）
            command[index++] = (byte)(modbusReg.regstart >> 8);
            command[index++] = (byte)(modbusReg.regstart & 0xFF);

            // 填充数量（高字节在前）
            command[index++] = (byte)(modbusReg.regnum >> 8);
            command[index++] = (byte)(modbusReg.regnum & 0xFF);

            // 填充数据长度（仅针对写多个的命令）
            if (isWriteMultiple && modbusReg.vbyte != null)
            {
                command[index++] = (byte)modbusReg.vbyte.Length;
            }

            // 填充数据
            if (modbusReg.vbyte != null && modbusReg.vbyte.Length > 0)
            {
                Array.Copy(modbusReg.vbyte, 0, command, index, modbusReg.vbyte.Length);
                index += modbusReg.vbyte.Length;
            }

            // 计算并添加CRC校验
            return MBRTU.CommandCRC(command);
        }

        //本系统只有一个串口
        public ModbusReg this[string regName, string portName = "Port1"]
        {
            get
            {
                ModbusReg reg = modbusRegs.FirstOrDefault(x => x.name == regName && x.portName == portName);

                if (reg == null)
                {
                    reg = modbusRegsTimes.FirstOrDefault(x => x.name == regName && x.portName == portName);
                }
                if (reg == null)
                {
                    reg = oneTimeModbusReg.FirstOrDefault(x => x.name == regName && x.portName == portName);
                }
                if (GlobalPara.IsEmulatorMode)
                {
                    reg = reg.Clone();

                    //仿真模式 处理对数据的处理
                    // reg.IsEmulatorMode = true;
                    ChangeEmulatorModeReg(reg);
                    //数据处理;
                }
                return reg.Clone();
            }
        }

        /// <summary>
        /// 仿真模式下 处理 Modbus命令
        /// </summary>
        /// <param name="reg"></param>
        private void ChangeEmulatorModeReg(ModbusReg reg)
        {


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
                            reg.ResponseData[i] = new Random().Next(4000, 20000);
                        if (reg.name == "1路温度回传" || reg.name == "2路温度回传")
                            reg.ResponseData[i] = new Random().Next(50, 700);
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
    }
}
