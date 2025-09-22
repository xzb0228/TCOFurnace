using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TCOFurnace.Models
{
    /// <summary>
    /// 业务功能权限表
    /// </summary>
    public class TCOFRunningMode
    {
        /// <summary>
        /// 模式编号（主键）
        /// </summary>
        public int ModeCode { get; set; }

        /// <summary>
        /// 模式名称
        /// </summary>
        public string ModeName { get; set; }

        /// <summary>
        /// 总步骤数
        /// </summary>
        public int StepCount { get; set; } = 0;  // 默认值0

        /// <summary>
        /// 流量（单位根据业务定义，如L/min）
        /// </summary>
        public float Flow { get; set; } = 0;   // 默认值0
        /// <summary>
        /// 导航属性：该模式包含的所有步骤（可选，用于EF core关联查询）
        /// </summary>
        public virtual List<TCOFRunningStep> RunningSteps { get; set; } = new List<TCOFRunningStep>();
    }
}
