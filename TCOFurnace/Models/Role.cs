using System;
using System.Collections.Generic;

namespace TCOFurnace.Models
{
    /// <summary>
    /// 角色表实体
    /// </summary>
    public class Role
    {
        /// <summary>
        /// 主键，自动递增
        /// </summary>
        public int Id { get; set; }

        //角色编码
        public int Code { get; set; }

        /// <summary>
        /// 角色名称（唯一）
        /// </summary>
        public string Name { get; set; } = string.Empty;

        public int PermissionCode { get; set; }

        // 必须添加的权限集合
        public List<Permission> Permissions { get; set; } = new List<Permission>();
    }
}
