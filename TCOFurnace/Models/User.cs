using System;

namespace TCOFurnace.Models
{
    public class User
    {
        /// <summary>
        /// 主键，全局唯一
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 登录账号，唯一
        /// </summary>
        public string UserCode { get; set; }

        /// <summary>
        /// 加盐哈希后的密码(32个字节)，数据库不存明文 (​​Base64​​：44字符)
        /// </summary>
        public string UserName { get; set; } 
        /// <summary>
        /// 128 位(16个字节)随机盐，Base64 字符串(​​Base64​​：24字符)
        /// </summary>
        public string PassWord { get; set; } 

        /// <summary>
        /// 注册时间（UTC）
        /// </summary>
        public DateTime Created_at { get; set; }

        /// <summary>
        /// 最后修改时间（UTC）
        /// </summary>
        public DateTime Updated_at { get; set; }
    }
}
