using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCOFurnace.InstrumentServices
{
    /// <summary>
    /// 单位转换类
    /// </summary>
    public class UnitConverter
    {
        /// <summary>
        /// 电流大小转气体流量大小 用于读取流量计的数值,返回值取 L/min 。  0-5L/min 对应4-20mA
        /// </summary>
        /// <param name="electric">电流大小 微安um</param>
        /// <param name="range">流量计的量程</param>
        /// <returns></returns>
        public static float ElectricToFlow(int electric, int range = 5)
        {
            if (electric <= 4000)
            {
                return 0;
            }

            return (float)Math.Round(((float)((electric / 1000.00 - 4) / 20 * range)), 2);
        }

        /// <summary>
        /// 气体流量大小 转电流大小 用于设定流量计的数值,返回值取 微安um 。  0-5L/min 对应4-20mA
        /// </summary>
        /// <param name="flow">流量大小 L/min</param> 
        /// <param name="range"></param>
        /// <returns></returns>
        public static int FlowToElectric(float flow, int rangee = 5)
        {
            return (int)(((flow) / rangee * (20.00 - 4) + 4) * 1000);
        }

        /// <summary>
        /// 电流大小转电压大小,4-20mA 对应 0-220V 线性对应关系
        /// </summary>
        /// <param name="voltage">电流大小</param>
        /// <param name="range">电压的量程</param>
        /// <returns></returns>
        public static int ElectricToVoltage(int electric, int range = 220)
        {
            if (electric <= 4)
            {
                return 0;
            }

            return (int)Math.Round(((float)((electric - 4) / 20 * range)), 2);
        }

        /// <summary>
        /// 电压大小转电流 用于设定电压的数值, 0-220V 对应4-20mA  线性对应关系
        /// </summary>
        /// <param name="flow">流量大小 L/min</param> 
        /// <param name="range"></param>
        /// <returns></returns>
        public static int VoltageToElectric(int Voltage, float rangee = 220.00f)
        {
            return (int)(((Voltage) / rangee * (20.00 - 4) + 4) * 1000);
        }
    }
}
