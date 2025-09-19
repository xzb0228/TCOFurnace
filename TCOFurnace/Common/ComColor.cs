using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCOFurnace.Common
{
    public static class ComColor
    {
        #region 用于运行监控界面中 第2-4行 第二列 的颜色变化
        //仪器没有运行的状体颜色  
        public static Color EquipNotRunColor = System.Drawing.SystemColors.ControlDarkDark;
        //仪器运行状体颜色
        public static Color EquipRunColor = System.Drawing.Color.GreenYellow;
        #endregion

    }
}
