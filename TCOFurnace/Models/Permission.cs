using System;
using System.Collections.Generic;

namespace TCOFurnace.Models
{
    /// <summary>
    /// 业务功能权限表
    /// </summary>
    public class Permission
    {
        /// <summary>
        /// 主键，自动递增
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 功能点编码
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// 父id
        /// </summary>
        public int? ParentCode { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        public List<Permission> Children { get; set; } = new List<Permission>();
    }
}
