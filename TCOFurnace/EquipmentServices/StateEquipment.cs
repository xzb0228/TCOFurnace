using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCOFurnace.Models;

namespace TCOFurnace.InstrumentsServices
{
    // 状态机核心类
    public class StateEquipment
    {
        //所需要的命令参数
        public EquipmentMBReg equipmentMBReg;
        //所需要的业务参数
        public TCOFRunningMode tCOFRunningMode;
        //当前仪器所有的状体信息
        public MonitoringData monitoringData;

        //当前状态
        public IState CurrentState { get; private set; }

        // 初始化时进入初始化状态
        public StateEquipment()
        {
            CurrentState = new InitializingState();
            Console.WriteLine($"初始状态: {CurrentState.GetType().Name}");
        }

        // 切换状态
        public void SetState(IState newState)
        {
            CurrentState = newState;
            Console.WriteLine($"状态已切换至: {CurrentState.GetType().Name}");
        }

        // 暴露外部操作接口
        public void Initialize() => CurrentState.Initialize(this);
        public void Start() => CurrentState.Start(this);
        public void Stop() => CurrentState.Stop(this);
    }
}
