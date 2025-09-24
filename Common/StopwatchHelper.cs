using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// 高精度计时帮助类，支持多任务计时和多种时间单位显示
    /// </summary>
    public class StopwatchHelper
    {
        private static readonly Dictionary<string, Stopwatch> _stopwatches = new Dictionary<string, Stopwatch>();
        private static readonly Dictionary<string, long> _lapTimes = new Dictionary<string, long>();
        private static readonly object _lockObj = new object();

        /// <summary>
        /// 开始一个新的计时任务
        /// </summary>
        /// <param name="taskName">任务名称，用于标识不同的计时任务</param>
        /// <param name="restartIfExists">如果任务已存在，是否重启计时</param>
        public static void Start(string taskName, bool restartIfExists = false)
        {
            lock (_lockObj)
            {
                if (_stopwatches.TryGetValue(taskName, out var stopwatch))
                {
                    if (restartIfExists)
                    {
                        stopwatch.Restart();
                        _lapTimes[taskName] = 0;
                    }
                    else if (!stopwatch.IsRunning)
                    {
                        stopwatch.Start();
                    }
                }
                else
                {
                    stopwatch = new Stopwatch();
                    stopwatch.Start();
                    _stopwatches[taskName] = stopwatch;
                    _lapTimes[taskName] = 0;
                }
                Loger.Info($"{taskName}-开始 ：" + stopwatch.ElapsedMilliseconds + " | " , "watcher");
            }
        }

        /// <summary>
        /// 停止指定的计时任务
        /// </summary>
        /// <param name="taskName">任务名称</param>
        public static void Stop(string taskName)
        {
            lock (_lockObj)
            {
                if (_stopwatches.TryGetValue(taskName, out var stopwatch) && stopwatch.IsRunning)
                {
                    stopwatch.Stop();
                    Loger.Info($"{taskName}-结束： ({stopwatch.ElapsedMilliseconds})       ","watcher");
                }
            }
        }

        /// <summary>
        /// 记录一个时间点，返回从上次记录点到现在的间隔时间（毫秒）
        /// </summary>
        /// <param name="taskName">任务名称</param>
        /// <returns>与上次记录点的时间间隔（毫秒）</returns>
        public static void Lap(string taskName, string Msg)
        {
            lock (_lockObj)
            {
                if (!_stopwatches.TryGetValue(taskName, out var stopwatch) || !stopwatch.IsRunning)
                {
                    return;
                }

                long currentElapsed = stopwatch.ElapsedMilliseconds;
                long lap = currentElapsed - _lapTimes[taskName];
                _lapTimes[taskName] = currentElapsed;
                Loger.Info($"{taskName}-记录：({stopwatch.ElapsedMilliseconds})   {Msg}。  耗时：" + lap, "watcher");
            }
        }


        /// <summary>
        /// 重置指定任务的计时
        /// </summary>
        /// <param name="taskName">任务名称</param>
        public static void Reset(string taskName)
        {
            lock (_lockObj)
            {
                if (_stopwatches.TryGetValue(taskName, out var stopwatch))
                {
                    stopwatch.Reset();
                    _lapTimes[taskName] = 0;
                }
            }
        }

    }
}
