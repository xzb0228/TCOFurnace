using EquipDriver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TCOFurnace.Models;

namespace TCOFurnace.InstrumentsServices
{
    // 状态机核心类
    public class StateInstrument
    {
        public Equipment equipment;
        //所需要的命令参数
        public InstrumentMBReg equipmentMBReg =new InstrumentMBReg();
        //所需要的业务参数
        public TCOFRunningMode tCOFRunningMode=new TCOFRunningMode();
        //当前仪器所有端口的状体信息
        public MonitoringData monitoringData=new MonitoringData();

        //用户处理命令的发送
        public System.Threading.Timer timer;
        //当前状态
        public IState CurrentState { get; private set; }

        // 初始化时进入初始化状态 并且注入协议类
        public StateInstrument(Equipment equip)
        {
            equipment = equip;
            CurrentState = new InitializingState();
           // MessageBox.Show($"初始状态: {CurrentState.GetType().Name}");
        }

        // 切换状态
        public void SetState(IState newState)
        {
            CurrentState = newState;
            MessageBox.Show($"状态已切换至: {CurrentState.GetType().Name}");
        }

        // 暴露外部操作接口
        public void Initialize() => CurrentState.Initialize(this);
        public void Start() => CurrentState.Start(this);
        public void Stop() => CurrentState.Stop(this);
    }
}
