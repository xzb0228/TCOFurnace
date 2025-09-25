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
            machine.InitData();

            MessageBox.Show(LanguageManager.GetMsg("10021"));
        }
    }
}
