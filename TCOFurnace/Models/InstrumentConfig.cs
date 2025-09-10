using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCOFurnace.Models
{
    public class InstrumentConfig
    {
        public string InstrumentId { get; set; }         // 仪器唯一标识
        public string Name { get; set; }                 // 仪器名称
        public List<PortInfo> AssociatedPorts { get; set; }  // 关联的端口列表
        public Dictionary<string, string> Settings { get; }  // 仪器特定设置
    }
}
