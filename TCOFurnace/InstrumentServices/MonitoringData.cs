using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TCOFurnace.Common;

namespace TCOFurnace.InstrumentsServices
{
    /// <summary>
    /// 监控数据类，管理界面标签的文本和颜色属性
    /// </summary>
    public class MonitoringData : INotifyPropertyChanged
    {
        public MonitoringData()
        {
            //初始化
            InitializeDefaults();
        }
        private string _lab1_2;//步骤信息
        private string _lab3_4;//流量(L/min) 设定值
        private string _lab3_5;//流量(L/min) 检测值
        private string _lab3_6;//系统名称
        private string _lab4_4;//氧化区温度(℃) 设定值
        private string _lab4_5;//氧化区温度(℃) 检测值
        private string _lab5_4;//催化区温度(℃) 设定值
        private string _lab5_5;//催化区温度(℃) 检测值
        private string _lab6_3;//设置时间(min)
        private string _lab6_5;//运行时间(min)
        private string _labModel;//当前模式

        // 颜色字段
        private Color _lab2_2;//流量计
        private Color _lab3_2;//氧/氩气
        private Color _lab4_2;//氧化区 状态展示
        private Color _lab5_2;//催化区 状态展示
        private Color _butSysRun;//系统运行状态

        /// <summary>
        /// 标签1_2的文本
        /// </summary>
        public string Lab1_2
        {
            get => _lab1_2;
            set
            {
                if (_lab1_2 != value)
                {
                    _lab1_2 = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 标签3_4的文本
        /// </summary>
        public string Lab3_4
        {
            get => _lab3_4;
            set
            {
                if (_lab3_4 != value)
                {
                    _lab3_4 = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 标签3_5的文本
        /// </summary>
        public string Lab3_5
        {
            get => _lab3_5;
            set
            {
                if (_lab3_5 != value)
                {
                    _lab3_5 = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 标签3_6的文本
        /// </summary>
        public string Lab3_6
        {
            get => _lab3_6;
            set
            {
                if (value == "1" || value == "2")
                {
                    _lab3_6 = value;
                    OnPropertyChanged();
                }

            }
        }

        /// <summary>
        /// 标签4_4的文本
        /// </summary>
        public string Lab4_4
        {
            get => _lab4_4;
            set
            {
                if (_lab4_4 != value)
                {
                    _lab4_4 = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 标签4_5的文本
        /// </summary>
        public string Lab4_5
        {
            get => _lab4_5;
            set
            {
                if (_lab4_5 != value)
                {
                    _lab4_5 = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 标签5_4的文本
        /// </summary>
        public string Lab5_4
        {
            get => _lab5_4;
            set
            {
                if (_lab5_4 != value)
                {
                    _lab5_4 = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 标签5_5的文本
        /// </summary>
        public string Lab5_5
        {
            get => _lab5_5;
            set
            {
                if (_lab5_5 != value)
                {
                    _lab5_5 = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 标签6_3的文本
        /// </summary>
        public string Lab6_3
        {
            get => _lab6_3;
            set
            {
                if (_lab6_3 != value)
                {
                    _lab6_3 = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 标签6_5的文本
        /// </summary>
        public string Lab6_5
        {
            get => _lab6_5;
            set
            {
                if (_lab6_5 != value)
                {
                    _lab6_5 = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 型号标签的文本
        /// </summary>
        public string LabModel
        {
            get => _labModel;
            set
            {
                if (_labModel != value)
                {
                    _labModel = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 标签2_2的颜色
        /// </summary>
        public Color Lab2_2
        {
            get => _lab2_2;
            set
            {
                if (_lab2_2 != value)
                {
                    _lab2_2 = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 标签3_2的颜色
        /// </summary>
        public Color Lab3_2
        {
            get => _lab3_2;
            set
            {
                if (_lab3_2 != value)
                {
                    _lab3_2 = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 标签4_2的颜色
        /// </summary>
        public Color Lab4_2
        {
            get => _lab4_2;
            set
            {
                if (_lab4_2 != value)
                {
                    _lab4_2 = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 标签5_2的颜色
        /// </summary>
        public Color Lab5_2
        {
            get => _lab5_2;
            set
            {
                if (_lab5_2 != value)
                {
                    _lab5_2 = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 系统运行按钮的颜色
        /// </summary>
        public Color ButSysRun
        {
            get => _butSysRun;
            set
            {
                if (_butSysRun != value)
                {
                    _butSysRun = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 属性变更事件
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 触发属性变更事件
        /// </summary>
        /// <param name="propertyName">变更的属性名（自动获取）</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// 初始化所有属性的默认值
        /// </summary>
        public void InitializeDefaults()
        {
            // 初始化文本为默认空值
            Lab1_2 = "0";
            Lab3_4 = "0";
            Lab3_5 = "0";
            Lab3_6 = "1";
            Lab4_4 = "0";
            Lab4_5 = "0";
            Lab5_4 = "0";
            Lab5_5 = "0";
            Lab6_3 = "0";
            Lab6_5 = "0";
            LabModel = "标准模式";

            // 初始化颜色为默认值
            Lab2_2 = ComColor.EquipNotRunColor;
            Lab3_2 = ComColor.EquipNotRunColor;
            Lab4_2 = ComColor.EquipNotRunColor;
            Lab5_2 = ComColor.EquipNotRunColor;
            ButSysRun = ComColor.EquipNotRunColor; // 运行按钮默认绿色
        }
    }
}
