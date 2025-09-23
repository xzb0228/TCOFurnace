using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCOFurnace.InstrumentsServices
{
    public class StoppedState : IState
    {
        public void Initialize(StateEquipment machine)
        {
            Console.WriteLine("从停止状态重新初始化...");
            machine.SetState(new InitializingState());
        }

        public void Start(StateEquipment machine)
        {
            Console.WriteLine("设备已停止，请先初始化再启动");
        }

        public void Stop(StateEquipment machine)
        {
            Console.WriteLine("设备已处于停止状态");
        }
    }
}
