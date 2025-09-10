using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModBusRTU
{
    /// <summary>
    /// 定义数据操作类型枚举
    /// </summary>
    public enum OperationType
    {
        Read=1,// 读数据操作
        Write = 2,  // 写数据操作
        WriteS = 3,// 读多个数据操作
    }
}
