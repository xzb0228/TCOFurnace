using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCOFurnace.UserControls
{
    public class ToggleSwitch : Control
    {
        // 开关状态
        private bool _isOn;
        public bool IsOn
        {
            get => _isOn;
            set
            {
                if (_isOn != value)
                {
                    _isOn = value;
                    Invalidate(); // 重绘控件
                    OnToggleChanged(EventArgs.Empty);
                }
            }
        }

        // 状态改变事件
        public event EventHandler ToggleChanged;
        protected virtual void OnToggleChanged(EventArgs e)
        {
            ToggleChanged?.Invoke(this, e);
        }

        // 颜色设置
        public Color OnBackColor { get; set; } = Color.LimeGreen;
        public Color OffBackColor { get; set; } = Color.LightGray;
        public Color SwitchColor { get; set; } = Color.White;
        public Color OnTextColor { get; set; } = Color.White;
        public Color OffTextColor { get; set; } = Color.DimGray;

        // 文本设置
        public string OnText { get; set; } = "ON";
        public string OffText { get; set; } = "OFF";

        public ToggleSwitch()
        {
            // 设置控件样式
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.OptimizedDoubleBuffer, true);
            DoubleBuffered = true;
            Size = new Size(25, 20); // 默认大小
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // 绘制背景
            var backColor = IsOn ? OnBackColor : OffBackColor;
            using (var brush = new SolidBrush(backColor))
            {
                g.FillRoundedRectangle(brush, 0, 0, Width, Height, Height / 2);
            }

            // 绘制文本
            var text = IsOn ? OnText : OffText;
            var textColor = IsOn ? OnTextColor : OffTextColor;
            using (var font = new Font("Segoe UI", 8.25f, FontStyle.Bold))
            using (var brush = new SolidBrush(textColor))
            {
                var textSize = g.MeasureString(text, font);
                var textX = IsOn ? Width - textSize.Width - 18 : 18;
                var textY = (Height - textSize.Height) / 2;
                g.DrawString(text, font, brush, textX, textY);
            }

            // 绘制开关滑块
            var switchX = IsOn ? Width - Height : 0;
            using (var brush = new SolidBrush(SwitchColor))
            using (var pen = new Pen(Color.LightGray, 1))
            {
                g.FillEllipse(brush, switchX + 1, 1, Height - 2, Height - 2);
                g.DrawEllipse(pen, switchX + 1, 1, Height - 2, Height - 2);
            }
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            IsOn = !IsOn; // 切换状态
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            // 确保高度合理，防止变形
            if (Height < 20) Height = 20;
            Width = (int)(Height * 2.667); // 保持宽高比例
        }
    }

    // 扩展方法：绘制圆角矩形
    public static class GraphicsExtensions
    {
        public static void FillRoundedRectangle(this Graphics g, Brush brush, float x, float y, float width, float height, float radius)
        {
            using (var path = new System.Drawing.Drawing2D.GraphicsPath())
            {
                path.AddArc(x, y, radius, radius, 180, 90);
                path.AddArc(x + width - radius, y, radius, radius, 270, 90);
                path.AddArc(x + width - radius, y + height - radius, radius, radius, 0, 90);
                path.AddArc(x, y + height - radius, radius, radius, 90, 90);
                path.CloseAllFigures();
                g.FillPath(brush, path);
            }
        }
    }
}
