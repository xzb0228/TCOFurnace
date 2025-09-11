using ModBusRTU;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace EquipDriver
{
    public class ModbusRtuPacketCollector : IDisposable
    {
        private readonly SerialPort _serialPort;
        private readonly List<byte> _receiveBuffer = new List<byte>();
        private readonly object _bufferLock = new object();
        private readonly Timer _frameTimeoutTimer;
        private readonly double _frameTimeoutMs; // 帧超时时间（3.5个字符时间）
        private bool _isCollecting; // 是否正在收集一帧数据

        /// <summary>
        /// 完整数据包收集完成事件
        /// </summary>
        public event Action<byte[]> PacketCollected;

        /// <summary>
        /// 错误事件
        /// </summary>
        public event Action<string> ErrorOccurred;

        /// <summary>
        /// 初始化数据包收集器
        /// </summary>
        /// <param name="serialPort">串口对象</param>
        /// <param name="baudRate">波特率</param>
        public ModbusRtuPacketCollector(SerialPort serialPort, int baudRate)
        {
            _serialPort = serialPort ?? throw new ArgumentNullException(nameof(serialPort));
            _serialPort.DataReceived += SerialPort_DataReceived;

            // 计算3.5个字符时间（11位/字符：1起始+8数据+1停止+0校验）
            _frameTimeoutMs = 3.5 * 11 * 1000.0 / baudRate;

            // 初始化超时定时器
            _frameTimeoutTimer = new Timer(_frameTimeoutMs);
            _frameTimeoutTimer.Elapsed += FrameTimeout_Elapsed;
            _frameTimeoutTimer.AutoReset = false;
        }

        /// <summary>
        /// 串口数据接收事件
        /// </summary>
        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                if (!_serialPort.IsOpen) return;

                // 读取当前缓冲区所有数据
                byte[] buffer = new byte[_serialPort.BytesToRead];
                int bytesRead = _serialPort.Read(buffer, 0, buffer.Length);

                if (bytesRead > 0)
                {
                    lock (_bufferLock)
                    {
                        // 开始收集新帧或追加到现有帧
                        if (!_isCollecting)
                        {
                            _receiveBuffer.Clear();
                            _isCollecting = true;
                        }
                        _receiveBuffer.AddRange(buffer);

                        // 重置超时定时器
                        _frameTimeoutTimer.Stop();
                        _frameTimeoutTimer.Start();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorOccurred?.Invoke($"接收错误: {ex.Message}");
            }
        }

        /// <summary>
        /// 帧超时事件（表示一帧数据收集完成）
        /// </summary>
        private void FrameTimeout_Elapsed(object sender, ElapsedEventArgs e)
        {
            lock (_bufferLock)
            {
                if (_isCollecting && _receiveBuffer.Count > 0)
                {
                    // 复制缓冲区数据
                    byte[] rawPacket = _receiveBuffer.ToArray();
                    _isCollecting = false;

                    // 验证数据包完整性
                    if (CMethord.Validate(rawPacket, out string validationError))
                    {
                        // 验证通过，发布完整数据包
                        PacketCollected?.Invoke(rawPacket);
                    }
                    else
                    {
                        ErrorOccurred?.Invoke($"数据包无效: {validationError}");
                    }
                }
            }
        }

        public void Dispose()
        {
            _serialPort.DataReceived -= SerialPort_DataReceived;
            _frameTimeoutTimer?.Dispose();
        }
    }
}
