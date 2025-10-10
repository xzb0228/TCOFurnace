using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TCOFurnace.Common;

namespace TCOFurnace.InstrumentsServices
{
    public class StoppedState : IState
    {
        public void Initialize(StateInstrument machine)
        {
            MessageBox.Show(LanguageManager.GetMsg("10019"));
            machine.SetState(new InitializingState());
        }

        public void Start(StateInstrument machine)
        {
            MessageBox.Show(LanguageManager.GetMsg("10020"));
        }

        public void Stop(StateInstrument machine)
        {
            //将某台仪器所有命令 置于最初始状态
            machine.InitPort();

            //将当前仪器状态 置于最初始状态
            machine.InitData();

            //停止状态运行完了置于初始态 
            machine.SetState(new InitializingState());

            MessageBox.Show(LanguageManager.GetMsg("10021"));
        }
    }
}
