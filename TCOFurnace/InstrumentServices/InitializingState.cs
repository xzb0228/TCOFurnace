using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TCOFurnace.Common;
using TCOFurnace.Models;

namespace TCOFurnace.InstrumentsServices
{
    internal class InitializingState : IState
    {
        public void Initialize(StateInstrument machine)
        {
            //Console.WriteLine("已在初始化状态，正在加载配置...");
            //// 模拟初始化完成
            //Console.WriteLine("初始化完成！");
            if (machine.tCOFRunningMode == null)
            {
                MessageBox.Show(LanguageManager.GetMsg("10014"));
                return;
            }
            machine.InitPort();
            machine.SetState(new RunningState());
        }

        public void Start(StateInstrument machine)
        {
            MessageBox.Show(LanguageManager.GetMsg("10015")); 
        }

        public void Stop(StateInstrument machine)
        {
            MessageBox.Show(LanguageManager.GetMsg("10016"));
            machine.SetState(new StoppedState());
        }
    }
}
