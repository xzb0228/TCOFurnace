using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCOFurnace.InstrumentsServices
{
    public class StoppedState : IState
    {
        public void Initialize(StateInstrument machine)
        {
            MessageBox.Show("从停止状态重新初始化...");
            machine.SetState(new InitializingState());
        }

        public void Start(StateInstrument machine)
        {
            MessageBox.Show("设备已停止，请先初始化再启动");
        }

        public void Stop(StateInstrument machine)
        {
            MessageBox.Show("设备已处于停止状态");
        }
    }
}
