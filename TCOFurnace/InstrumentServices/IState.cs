using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCOFurnace.InstrumentsServices
{
    public interface IState
    {
        // 初始化操作
        void Initialize(StateInstrument machine);
        // 启动操作
        void Start(StateInstrument machine);
        // 停止操作
        void Stop(StateInstrument machine);
    }
}
