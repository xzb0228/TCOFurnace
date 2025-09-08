using System;

namespace TCOFurnace.Models
{
    /// <summary>
    /// 权限表
    /// </summary>
    public class UserPermission
    {
        /// <summary>
        /// 主键，自动递增
        /// </summary>
        public string UserCode { get; set; }

        /// <summary>
        /// 权限ID
        /// </summary>
        public string permissions_code { get; set; }

    }
}
