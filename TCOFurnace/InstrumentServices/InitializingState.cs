using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCOFurnace.InstrumentsServices
{
    internal class InitializingState : IState
    {
        public void Initialize(StateInstrument machine)
        {
            //Console.WriteLine("已在初始化状态，正在加载配置...");
            //// 模拟初始化完成
            //Console.WriteLine("初始化完成！");
            machine.SetState(new RunningState());
        }

        public void Start(StateInstrument machine)
        {
            MessageBox.Show("请先完成初始化，再执行启动操作");
        }

        public void Stop(StateInstrument machine)
        {
            MessageBox.Show("初始化过程中无法停止，正在强制终止...");
            machine.SetState(new StoppedState());
        }
    }
}
