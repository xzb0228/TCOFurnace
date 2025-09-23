using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCOFurnace.InstrumentsServices
{
    public class RunningState : IState
    {
        public void Initialize(StateInstrument machine)
        {
            Console.WriteLine("设备已在运行中，无需重复初始化");
        }

        public void Start(StateInstrument machine)
        {
            Console.WriteLine("设备已处于运行状态");
        }

        public void Stop(StateInstrument machine)
        {
            Console.WriteLine("正在停止设备...");
            // 模拟停止过程
            Console.WriteLine("设备已停止");
            machine.SetState(new StoppedState());
        }
    }
}
