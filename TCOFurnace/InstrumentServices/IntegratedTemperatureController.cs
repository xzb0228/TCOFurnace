using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCOFurnace.InstrumentServices
{
    /// <summary>
    /// 分段功率加温控制器
    /// 特点：根据温差分阶段调整功率，平衡升温速度与超调
    /// </summary>
    public class IntegratedTemperatureController
    {
        #region 配置参数
        /// <summary>目标温度</summary>
        public double TargetTemperature { get; set; }=0;

        /// <summary>最小输出功率(0-100%)</summary>
        public double MinPower { get; set; } = 45;

        /// <summary>最大输出功率(0-100%)</summary>
        public double MaxPower { get; set; } = 90;

        /// <summary>加温转恒温的切换阈值(℃)</summary>
        public double SwitchThreshold { get; set; } = 5.0;

        // 分段加温参数
        public double HighTemperatureDiff { get; set; } = 30;  // 高功率温差阈值
        public double MediumTemperatureDiff { get; set; } = 15; // 中功率温差阈值

        // PI恒温参数
        public double Kp { get; set; } = 5.0;  // 比例系数
        public double Ki { get; set; } = 0.1; // 积分系数
        #endregion

        #region 状态变量
        /// <summary>当前温度</summary>
        public double CurrentTemperature { get; private set; }

        /// <summary>当前控制模式</summary>
        public ControlMode CurrentMode { get; private set; } = ControlMode.Heating;

        /// <summary>温度滤波缓冲区</summary>
        private Queue<double> _tempFilterBuffer = new Queue<double>();
        private const int _filterBufferSize = 5;  // 滤波窗口大小

        // PI算法内部变量
        private double _integralSum = 0;
        private DateTime _lastCalculateTime = DateTime.Now;
        #endregion

        //向下位机发送温度控制命令
        private Action<int> SendPower;

        #region 通信接口

        public IntegratedTemperatureController()
        {
           
        }

        public void SetSendPower(Action<int> sendPower)
        { 
         SendPower = sendPower;
        }
        public void SetPara(double targetTemperature, double minPower, double maxPower)
        {
            TargetTemperature = targetTemperature; 
            MinPower = minPower; 
            MaxPower = maxPower;

            // 切换时重置PI参数
            ResetPiParameters();
        }

        #endregion

        #region 核心控制逻辑
        /// <summary>处理接收到的温度数据</summary>
        public void OnTemperatureReceived(double rawTemperature)
        {
            // 温度滤波处理
            CurrentTemperature = FilterTemperature(rawTemperature);

            // 安全检查
            if (IsTemperatureAbnormal())
            {
                SendPower?.Invoke(0);  // 异常时关闭加热
                return;
            }

            // 模式自动切换判断
            CheckModeSwitch();

            // 计算并输出功率
            double power = CalculatePower();
            SendPower?.Invoke((int)power);  // 异常时关闭加热
        }

        /// <summary>检查并切换控制模式</summary>
        private void CheckModeSwitch()
        {
            double temperatureDiff = TargetTemperature - CurrentTemperature;

            // 加温模式 -> 恒温模式
            if (CurrentMode == ControlMode.Heating &&
                temperatureDiff <= SwitchThreshold &&
                temperatureDiff > 0)
            {
                CurrentMode = ControlMode.Constant;
                ResetPiParameters();  // 切换时重置PI参数
            }
            // 恒温模式 -> 加温模式（当温度低于目标较多时）
            else if (CurrentMode == ControlMode.Constant &&
                     temperatureDiff > SwitchThreshold * 2)
            {
                CurrentMode = ControlMode.Heating;
            }
        }

        /// <summary>根据当前模式计算功率</summary>
        private double CalculatePower()
        {
            return CurrentMode == ControlMode.Heating ? CalculateHeatingPower() : CurrentMode == ControlMode.Constant ? CalculateConstantPower() : 0; ;
        }
        #endregion

        #region 加温算法（分段功率控制）
        /// <summary>计算加温阶段的输出功率</summary>
        private double CalculateHeatingPower()
        {
            double temperatureDiff = TargetTemperature - CurrentTemperature;

            // 已达到或超过目标温度，停止加热
            if (temperatureDiff <= 0)
                return 0;

            // 分段功率计算
            double power = temperatureDiff > HighTemperatureDiff ? MaxPower : temperatureDiff > MediumTemperatureDiff ? MaxPower * 0.7 : MaxPower * 0.8;

            return power < MinPower ? MinPower : (power > MaxPower ? MaxPower : power);
        }
        #endregion

        #region 恒温算法（PI控制）
        /// <summary>计算恒温阶段的输出功率</summary>
        private double CalculateConstantPower()
        {
            double temperatureDiff = TargetTemperature - CurrentTemperature;
            DateTime now = DateTime.Now;
            double timeInterval = (now - _lastCalculateTime).TotalSeconds;
            _lastCalculateTime = now;

            // 积分项计算（带抗积分饱和）
            if (Math.Abs(temperatureDiff) < 1.5)  // 温差较小时才累积积分
            {
                _integralSum += temperatureDiff * timeInterval;
                // 限制积分范围，防止积分饱和
                double maxIntegral = MaxPower / Ki;
                double minIntegral = MinPower / Ki;
                _integralSum = _integralSum = _integralSum < minIntegral ? minIntegral : (_integralSum > maxIntegral ? maxIntegral : _integralSum);
            }

            // PI控制公式
            double power = Kp * temperatureDiff + Ki * _integralSum;
            return power < MinPower ? MinPower : (power > MaxPower ? MaxPower : power);
        }

        /// <summary>重置PI控制器参数</summary>
        private void ResetPiParameters()
        {
            _integralSum = 0;
            _lastCalculateTime = DateTime.Now;
        }
        #endregion

        #region 辅助功能
        /// <summary>温度滤波（滑动平均）</summary>
        private double FilterTemperature(double newTemperature)
        {
            _tempFilterBuffer.Enqueue(newTemperature);
            if (_tempFilterBuffer.Count > _filterBufferSize)
                _tempFilterBuffer.Dequeue();

            return _tempFilterBuffer.Average();
        }

        /// <summary>检查温度是否异常</summary>
        private bool IsTemperatureAbnormal()
        {
            // 超温保护（超过目标温度10℃）
            if (CurrentTemperature > TargetTemperature + 10 || CurrentTemperature > 800)
                return true;

            return false;
        }
        #endregion
    }

    /// <summary>控制模式枚举</summary>
    public enum ControlMode
    {
        /// <summary>加温模式</summary>
        Heating,
        /// <summary>恒温模式</summary>
        Constant
    }
}
