using Common;
using EquipDriver;
using ModBusRTU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TCOFurnace.Common;
using TCOFurnace.InstrumentServices;
using TCOFurnace.Models;
using static EquipDriver.SysDelegateEvent;
using static System.Windows.Forms.AxHost;

namespace TCOFurnace.InstrumentsServices
{
    public class RunningState : IState
    {
        public void Initialize(StateInstrument machine)
        {
            MessageBox.Show(LanguageManager.GetMsg("10017"));
        }
        public void Start(StateInstrument machine)
        {
            MessageBox.Show(LanguageManager.GetMsg("10018")); 
        }

        public void Stop(StateInstrument machine)
        {
            //所有步骤都执行完了
            machine.timer.Change(Timeout.Infinite, Timeout.Infinite);

            if (machine.reciveModbusRegThread != null)
            {
                //注册事件到 串口管理类
                SysDelegateEvent.ReciveModbusRegThread -= machine.reciveModbusRegThread;
            }
            machine.SetState(new StoppedState());

            //将某台仪器所有命令 置于最初始状态
            machine.InitPort();
            machine.InitStateData();
        }
    }
}
