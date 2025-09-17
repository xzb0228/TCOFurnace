using Common;
using EquipDriver;
using ModBusRTU;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TCOFurnace.UserControls;


namespace TCOFurnace.Forms
{
    public class FormTestMode : BaseForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnRefreshCom = new System.Windows.Forms.Button();
            this.cboComPort = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.numAddress = new System.Windows.Forms.NumericUpDown();
            this.cboBaudRate = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnAutoScan = new System.Windows.Forms.Button();
            this.btnTestSingle = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textFunCode = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAddress)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textFunCode);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.textBox3);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.btnRefreshCom);
            this.groupBox1.Controls.Add(this.cboComPort);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.numAddress);
            this.groupBox1.Controls.Add(this.cboBaudRate);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnStop);
            this.groupBox1.Controls.Add(this.btnAutoScan);
            this.groupBox1.Controls.Add(this.btnTestSingle);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(586, 145);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "测试参数";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(340, 96);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(97, 27);
            this.button1.TabIndex = 16;
            this.button1.Text = "发送";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(81, 100);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(241, 21);
            this.textBox3.TabIndex = 15;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 103);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 14;
            this.label6.Text = "命令：";
            // 
            // btnRefreshCom
            // 
            this.btnRefreshCom.Location = new System.Drawing.Point(200, 26);
            this.btnRefreshCom.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnRefreshCom.Name = "btnRefreshCom";
            this.btnRefreshCom.Size = new System.Drawing.Size(32, 18);
            this.btnRefreshCom.TabIndex = 9;
            this.btnRefreshCom.Text = "↺";
            this.btnRefreshCom.UseVisualStyleBackColor = true;
            this.btnRefreshCom.Click += new System.EventHandler(this.btnRefreshCom_Click);
            // 
            // cboComPort
            // 
            this.cboComPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboComPort.FormattingEnabled = true;
            this.cboComPort.Location = new System.Drawing.Point(81, 26);
            this.cboComPort.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboComPort.Name = "cboComPort";
            this.cboComPort.Size = new System.Drawing.Size(114, 20);
            this.cboComPort.TabIndex = 8;
            this.cboComPort.SelectedIndexChanged += new System.EventHandler(this.cboComPort_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "串口号：";
            // 
            // numAddress
            // 
            this.numAddress.Location = new System.Drawing.Point(81, 49);
            this.numAddress.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numAddress.Name = "numAddress";
            this.numAddress.Size = new System.Drawing.Size(113, 21);
            this.numAddress.TabIndex = 6;
            // 
            // cboBaudRate
            // 
            this.cboBaudRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBaudRate.FormattingEnabled = true;
            this.cboBaudRate.Location = new System.Drawing.Point(81, 72);
            this.cboBaudRate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboBaudRate.Name = "cboBaudRate";
            this.cboBaudRate.Size = new System.Drawing.Size(114, 20);
            this.cboBaudRate.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "波特率：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "地址：";
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(456, 49);
            this.btnStop.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(97, 27);
            this.btnStop.TabIndex = 2;
            this.btnStop.Text = "停止扫描";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnAutoScan
            // 
            this.btnAutoScan.Location = new System.Drawing.Point(340, 49);
            this.btnAutoScan.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnAutoScan.Name = "btnAutoScan";
            this.btnAutoScan.Size = new System.Drawing.Size(97, 27);
            this.btnAutoScan.TabIndex = 1;
            this.btnAutoScan.Text = "自动扫描";
            this.btnAutoScan.UseVisualStyleBackColor = true;
            this.btnAutoScan.Click += new System.EventHandler(this.btnAutoScan_Click);
            // 
            // btnTestSingle
            // 
            this.btnTestSingle.Location = new System.Drawing.Point(225, 49);
            this.btnTestSingle.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnTestSingle.Name = "btnTestSingle";
            this.btnTestSingle.Size = new System.Drawing.Size(97, 27);
            this.btnTestSingle.TabIndex = 0;
            this.btnTestSingle.Text = "单次测试";
            this.btnTestSingle.UseVisualStyleBackColor = true;
            this.btnTestSingle.Click += new System.EventHandler(this.btnTestSingle_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtLog);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 145);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(586, 215);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "测试日志";
            // 
            // txtLog
            // 
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLog.Location = new System.Drawing.Point(3, 16);
            this.txtLog.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(580, 197);
            this.txtLog.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(254, 28);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 17;
            this.label7.Text = "功能码：";
            // 
            // textFunCode
            // 
            this.textFunCode.Location = new System.Drawing.Point(340, 23);
            this.textFunCode.Name = "textFunCode";
            this.textFunCode.Size = new System.Drawing.Size(97, 21);
            this.textFunCode.TabIndex = 18;
            this.textFunCode.Text = "3";
            // 
            // FormTestMode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 360);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FormTestMode";
            this.Text = "控制板地址与波特率测试工具";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAddress)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnTestSingle;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numAddress;
        private System.Windows.Forms.ComboBox cboBaudRate;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnAutoScan;
        private System.Windows.Forms.Button btnRefreshCom;
        private System.Windows.Forms.ComboBox cboComPort;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox2;
        private TextBox textBox3;
        private Label label6;
        private Button button1;
        private Label label7;
        private TextBox textFunCode;
        private System.Windows.Forms.TextBox txtLog;
        public FormTestMode()
        {
            InitializeComponent();
            InitializeControls();
            InitializeSerialPort();
        }

        private SerialPort _serialPort;
        private CancellationTokenSource _cts; // 用于取消自动扫描
        private bool _isTesting = false;

        // 常见的波特率列表
        private readonly int[] _commonBaudRates = { 9600, 4800, 19200, 38400, 57600, 115200, 230400 };

        // 地址测试范围 (根据实际设备调整)
        private readonly int _minAddress = 1;
        private readonly int _maxAddress = 247;

        private void InitializeControls()
        {
            // 初始化波特率下拉框
            foreach (var baud in _commonBaudRates)
            {
                cboBaudRate.Items.Add(baud);
            }
            cboBaudRate.SelectedItem = 9600;

            // 初始化地址输入框
            numAddress.Minimum = _minAddress;
            numAddress.Maximum = _maxAddress;
            numAddress.Value = 1;

            // 初始化串口下拉框
            RefreshComPorts();

            // 设置按钮状态
            //btnTestSingle.Enabled = false;
            //btnAutoScan.Enabled = false;
            //btnStop.Enabled = false;
        }

        private void InitializeSerialPort()
        {
            _serialPort = new SerialPort
            {
                Parity = Parity.None,
                DataBits = 8,
                StopBits = StopBits.One,
                ReadTimeout = 1000,
                WriteTimeout = 1000
            };

            _serialPort.DataReceived += SerialPort_DataReceived;
        }

        private void RefreshComPorts()
        {
            cboComPort.Items.Clear();
            foreach (var port in SerialPort.GetPortNames())
            {
                cboComPort.Items.Add(port);
            }
            if (cboComPort.Items.Count > 0)
            {
                cboComPort.SelectedIndex = 0;
            }
        }

        private void Log(string message)
        {
            if (this.InvokeRequired)
            {
                Invoke(new Action<string>(Log), message);
                return;
            }

            txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {message}{Environment.NewLine}");
            txtLog.ScrollToCaret();
        }

        private async void btnTestSingle_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cboComPort.Text))
            {
                MessageBox.Show("请选择串口");
                return;
            }

            int address = (int)numAddress.Value;
            int baudRate = (int)cboBaudRate.SelectedItem;

            Log($"开始测试 - 地址: {address}, 波特率: {baudRate}");
            btnTestSingle.Enabled = false;

            if (!_serialPort.IsOpen)
                _serialPort.Open();


            bool success = await TestConnection(baudRate, address);

            if (success)
            {
                Log($"测试成功! 地址: {address}, 波特率: {baudRate}");
                MessageBox.Show("测试成功!", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                Log($"测试失败 - 地址: {address}, 波特率: {baudRate}");
            }

            btnTestSingle.Enabled = true;
        }

        private async void btnAutoScan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cboComPort.Text))
            {
                MessageBox.Show("请选择串口");
                return;
            }

            _isTesting = true;
            _cts = new CancellationTokenSource();
            btnAutoScan.Enabled = false;
            btnTestSingle.Enabled = false;
            btnStop.Enabled = true;
            Log("开始自动扫描地址和波特率...");
            await Task.Run(() => AutoScan(_cts.Token), _cts.Token);

            if (!_cts.Token.IsCancellationRequested)
            {
                Log("自动扫描完成");
            }
            else
            {
                Log("自动扫描已取消");
            }

            _isTesting = false;
            btnAutoScan.Enabled = true;
            btnTestSingle.Enabled = true;
            btnStop.Enabled = false;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            _cts?.Cancel();
        }

        private void btnRefreshCom_Click(object sender, EventArgs e)
        {
            RefreshComPorts();
            isSecusess = false;
        }

        private void AutoScan(CancellationToken token)
        {
            foreach (var baudRate in _commonBaudRates)
            {
                if (token.IsCancellationRequested) break;

                for (int address = _minAddress; address <= _maxAddress; address++)
                {
                    if (token.IsCancellationRequested) break;

                    Log($"扫描中 - 地址: {address}, 波特率: {baudRate}");

                    if (TestConnection(baudRate, address).Result)
                    {
                        //Log($"找到有效配置! 地址: {address}, 波特率: {baudRate}");
                        //SetDaudAdr(address.ToString(), baudRate.ToString());
                        //_cts?.Cancel();
                        break;
                    }

                    // 扫描间隔，避免设备过载
                    Thread.Sleep(100);
                }
            }
        }

        private async Task<bool> TestConnection(int baudRate, int address)
        {
            try
            {
                if (_serialPort.IsOpen)
                    _serialPort.Close();

                // 配置串口参数
                _serialPort.BaudRate = baudRate;
                // 打开串口
                _serialPort.Open();

                Log("串口已打开");

                // 发送测试命令 (根据设备协议调整)
                // 示例: Modbus协议的读取命令 - 读取设备地址为address的保持寄存器
                byte[] command = BuildTestCommand(address);
                _serialPort.Write(command, 0, command.Length);
                Log($"已发送测试命令: {BitConverter.ToString(command)}");

                // 等待响应 (最多等待1秒)
                await Task.Delay(50);

                // 检查是否收到有效响应
                return _responseReceived;
            }
            catch (Exception ex)
            {
                Log($"测试出错: {ex.Message}");
                return false;
            }
            finally
            {
                _responseReceived = false; // 重置响应标记
            }
        }

        private bool _responseReceived = false;

        bool isSecusess = false;
        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                int bytesToRead = _serialPort.BytesToRead;
                if (bytesToRead <= 0) return;

                byte[] buffer = new byte[bytesToRead];
                _serialPort.Read(buffer, 0, bytesToRead);

                Log($"收到响应: {BitConverter.ToString(buffer)}");


                if (!isSecusess)
                {

                    UpdateTextBox(_serialPort.BaudRate, buffer[0]);
                    isSecusess = true;
                }

                _responseReceived = true;
                _cts?.Cancel();

            }
            catch (Exception ex)
            {
                Log($"接收数据出错: {ex.Message}");
            }
        }

        private void UpdateTextBox(int selectedItem, byte value)
        {
            // 检查是否需要跨线程调用
            if (txtLog.InvokeRequired)
            {
                // 需要跨线程：将操作委托给 UI 线程
                txtLog.Invoke(new Action<int, byte>(UpdateTextBox), selectedItem, value);
            }
            else
            {
                cboBaudRate.SelectedItem = selectedItem;
                numAddress.Value = value;
            }
        }


        // 根据设备协议构建测试命令
        // 示例: Modbus RTU 读取保持寄存器命令
        private byte[] BuildTestCommand(int address)
        {
            // 地址(1字节) + 功能码(1字节) + 起始地址(2字节) + 寄存器数量(2字节) + CRC(2字节)
            List<byte> command = new List<byte>();
            command.Add((byte)address);                // 设备地址
            command.Add((byte)(int.Parse(textFunCode.Text)));                         // 功能码: 读取保持寄存器
            command.Add(0x00); command.Add(0x00);      // 起始地址: 0
            command.Add(0x00); command.Add(0x01);      // 读取数量: 1

            byte[] dst = new byte[command.Count + 2];
            // 计算CRC校验
            dst = MBRTU.CommandCRC(command.ToArray());
            return dst;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_serialPort.IsOpen)
                _serialPort.Close();
            _cts?.Cancel();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_serialPort.IsOpen)
                    // 打开串口
                    _serialPort.Open();


                _serialPort.BaudRate = (int)cboBaudRate.SelectedItem;


                Log("串口已打开");

                // 发送测试命令 (根据设备协议调整)
                // 示例: Modbus协议的读取命令 - 读取设备地址为address的保持寄存器
                // byte[] buffer1 = Methord.HexStringToCommand("0A 05 00 00 FF 00");
                byte[] buffer1 = Methord.HexStringToCommand(textBox3.Text);

                byte[] buffer2 = MBRTU.CommandCRC(buffer1);
                _serialPort.Write(buffer2, 0, buffer2.Length);
                Log($"已发送测试命令: {BitConverter.ToString(buffer2)}");
            }
            catch (Exception ex)
            {
                Log($"测试出错: {ex.Message}");
            }
            finally
            {

            }
        }

        private void cboComPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_serialPort != null)
            {
                if (_serialPort.IsOpen)
                    _serialPort.Close();

                _serialPort.PortName = cboComPort.Text;
                _serialPort.BaudRate = (int)cboBaudRate.SelectedItem;
                _serialPort.Open();
            }
        }
    }

}
