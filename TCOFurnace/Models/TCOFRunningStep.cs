using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TCOFurnace.Models
{
    /// <summary>
    /// 业务功能权限表
    /// </summary>
    public class TCOFRunningStep
    {
        /// <summary>
        /// 模式编号（联合主键）
        /// </summary>
        public int ModeCode { get; set; }

        /// <summary>
        /// 步骤编号（联合主键）
        /// </summary>
        public int StepNum { get; set; }

        /// <summary>
        /// 分钟数（步骤持续时间）
        /// </summary>
        public int Times { get; set; } = 0;  // 默认值0

        /// <summary>
        /// 电磁阀1状态（0=关闭，1=打开等）
        /// </summary>
        public int K1 { get; set; } = 0;     // 默认值0

        /// <summary>
        /// 电磁阀2状态（0=关闭，1=打开等）
        /// </summary>
        public int K2 { get; set; } = 0;     // 默认值0

        /// <summary>
        /// 氧化区温度（℃）
        /// </summary>
        public int Otemp { get; set; } = 0;  // 默认值0

        /// <summary>
        /// 催化区温度（℃）
        /// </summary>
        public int CTemp { get; set; } = 0;  // 默认值0

        /// <summary>
        /// 氧化区电压（V）
        /// </summary>
        public int OVol { get; set; } = 0;   // 默认值0

        /// <summary>
        /// 催化区电压（V）
        /// </summary>
        public int CVol { get; set; } = 0;   // 默认值0

        /// <summary>
        /// 流量（单位根据业务定义，如L/min）
        /// </summary>
        public float Flow { get; set; } = 0;   // 默认值0
    }
}
