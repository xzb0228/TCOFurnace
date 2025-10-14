using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipDriver.Model
{
    public class TCPParamets: ParametsBase
    {
        /// <summary>
        /// 目标主机地址（客户端用）
        /// </summary>
        public string IP { get; set; } = "127.0.0.1";

        /// <summary>
        /// 目标端口（客户端用）
        /// </summary>
        public int PortNum { get; set; } = 8080;
    }
}
