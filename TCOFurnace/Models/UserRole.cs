using System;
using System.Collections.Generic;

namespace TCOFurnace.Models
{    /// <summary>
     /// 用户角色表
     /// </summary>
    public class UserRole
    {
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserCode { get; set; }

        /// <summary>
        /// 用户username
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// role_code
        /// </summary>
        public int RoleCode { get; set; }

        //角色
        public List<int> RoleCodes { get; set; }

    }
}
