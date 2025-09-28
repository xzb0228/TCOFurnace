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

            return (float)Math.Round(((float)((electric / 1000 - 4) / 20 * range)), 2);
        }

        /// <summary>
        /// 气体流量大小 转电流大小 用于设定流量计的数值,返回值取 微安um 。  0-5L/min 对应4-20mA
        /// </summary>
        /// <param name="flow">流量大小 L/min</param> 
        /// <param name="range"></param>
        /// <returns></returns>
        public static int FlowToElectric(float flow, int rangee = 5)
        {
            return (int)(((flow) / rangee * (20 - 4) + 4) * 1000);
        }
    }
}
