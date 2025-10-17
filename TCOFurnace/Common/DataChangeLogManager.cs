using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using TCOFurnace.Models;
using Common;
namespace TCOFurnace.Common
{
    public static class DataChangeLogManager
    {

        #region 审计追踪 审计数据入库
        /// <summary>
        /// 审计追踪页面控件配置 第一个参数为 控件名称 第二个参数为 控件描述 第三个控件为原始值 第四个参数为新值
        /// </summary>
        public static Dictionary<string, Dictionary<string, string>> DicAuditTrai = new Dictionary<string, Dictionary<string, string>>();
        public static Dictionary<string, Dictionary<string, string>> DicAuditTraiOldValue = new Dictionary<string, Dictionary<string, string>>();
        public static Dictionary<string, Dictionary<string, string>> DicAuditTraiNewValue = new Dictionary<string, Dictionary<string, string>>();
        public static Dictionary<string, string> DicAuditTraiFormName = new Dictionary<string, string>();


        public static void InitAuditTrai()
        {
            try
            {
                string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"XML\\{"ControlAuthority"}.xml");
                ReadAuditTraiXML(configPath);
            }
            catch (Exception ex)
            {
                Loger.Error("审计追踪InitAuditTrai 方法报错" + ex.Message, exc:ex);
            }
        }

        private static void ReadAuditTraiXML(string xmlPath)
        {
            if (!File.Exists(xmlPath))
            {
                Loger.Error($"语言配置文件不存在: {xmlPath}");
                return;
            }
            DicAuditTraiOldValue.Clear();
            DicAuditTrai.Clear();
            DicAuditTraiNewValue.Clear();
            // 加载并解析XML
            XDocument doc = XDocument.Load(xmlPath);
            XElement root = doc.Root;

            // 加载所有窗体的控件文本
            foreach (XElement formElement in root.Element("AuditTrail").Elements("Form"))
            {
                string formName = formElement.Attribute("Name").Value;
                string formText = formElement.Attribute("Text").Value;
                DicAuditTraiFormName.Add(formName, formText);
                Dictionary<string, string> controls = new Dictionary<string, string>();
                Dictionary<string, string> controlval = new Dictionary<string, string>();
                Dictionary<string, string> controlNewval = new Dictionary<string, string>();
                // 添加所有控件翻译
                foreach (XElement control in formElement.Elements("Control"))
                {
                    string controlName = control.Attribute("Name").Value;
                    string Text = control.Attribute("Text").Value;
                    controls.Add(controlName, Text);
                    controlval.Add(controlName, "");
                    controlNewval.Add(controlName, "");
                }
                DicAuditTrai.Add(formName, controls);
                DicAuditTraiOldValue.Add(formName, controlval);
                DicAuditTraiNewValue.Add(formName, controlNewval);
            }
        }

        //将页面控件的值写入缓存
        public static void WriteAuditTrai(Form from)
        {
            try
            {
                if (DicAuditTrai.ContainsKey(from.Name) && DicAuditTrai[from.Name].Count > 0)
                {
                    // 递归遍历所有控件
                    GetControlValues(from, DicAuditTrai[from.Name], DicAuditTraiOldValue[from.Name]);

                    // 获取ToolStrip控件的值
                    GetToolStripValues(from, DicAuditTrai[from.Name], DicAuditTraiOldValue[from.Name]);
                }
            }
            catch (Exception ex)
            {
                Loger.Error($"获取页面【{from.Name}】控件值报错" + ex.Message, exc:ex);
            }
        }

        /// <summary>
        /// 递归遍历控件及其子控件，获取值
        /// </summary>
        private static void GetControlValues(Control parentControl, Dictionary<string, string> Key, Dictionary<string, string> values)
        {
            foreach (Control control in parentControl.Controls)
            {
                // 处理当前控件的值
                if (!string.IsNullOrEmpty(control.Name) && Key.ContainsKey(control.Name)) // 忽略无名称的控件
                {
                    string value = GetControlValue(control);
                    if (value != null)
                    {
                        values[control.Name] = value;
                    }
                }

                // 递归处理子控件（如果是容器控件）
                if (control.HasChildren)
                {
                    GetControlValues(control, Key, values);
                }
            }
        }

        // 获取所有ToolStrip及其子控件的值
        private static void GetToolStripValues(Control mainControl, Dictionary<string, string> Key, Dictionary<string, string> values)
        {
            // 查找所有ToolStrip控件
            foreach (Control control in mainControl.Controls)
            {
                if (control is ToolStrip toolStrip)
                {
                    foreach (ToolStripItem item in toolStrip.Items)
                    {
                        string value = GetToolStripItemValue(item);
                        if (!string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(item.Name) && Key.ContainsKey(item.Name))
                        {
                            values[item.Name] = value;
                        }
                    }
                }
                // 递归检查子控件中是否包含ToolStrip
                else if (control.HasChildren)
                {
                    GetToolStripValues(control, Key, values);
                }
            }
        }

        /// <summary>
        /// 根据控件类型获取对应的值
        /// </summary>
        private static string GetControlValue(Control control)
        {
            switch (control)
            {
                case TextBox textBox:
                    return textBox.Text;
                case ComboBox comboBox:
                    return comboBox.SelectedItem?.ToString() ?? "";
                case CheckBox checkBox:
                    return checkBox.Checked.ToString();
                case RadioButton radioButton:
                    return radioButton.Checked.ToString();
                case NumericUpDown numericUpDown:
                    return numericUpDown.Value.ToString();
                case DateTimePicker dateTimePicker:
                    return dateTimePicker.Value.ToString();
                case CheckedListBox checkedListBox:
                    return GetCheckedListBoxValues(checkedListBox);
                // 可以根据需要添加更多控件类型
                default:
                    return "";
            }
        }
        /// <summary>
        /// 获取ToolStrip子项的值
        /// </summary>
        private static string GetToolStripItemValue(ToolStripItem item)
        {
            switch (item)
            {
                case ToolStripTextBox toolStripTextBox:
                    return toolStripTextBox.Text;
                case ToolStripComboBox toolStripComboBox:
                    return toolStripComboBox.SelectedItem?.ToString() ?? "";
                case ToolStripButton toolStripButton:
                    return toolStripButton.Checked.ToString();
                case ToolStripLabel toolStripLabel:
                    return toolStripLabel.Text;
                default:
                    return "";
            }
        }
        /// <summary>
        /// 处理 CheckedListBox 的勾选值
        /// </summary>
        private static string GetCheckedListBoxValues(CheckedListBox listBox)
        {
            List<string> values = new List<string>();
            for (int i = 0; i < listBox.Items.Count; i++)
            {
                if (listBox.GetItemChecked(i))
                {
                    values.Add(listBox.Items[i].ToString());
                }
            }
            return values.Count > 0 ? string.Join(", ", values) : "无选中项";
        }

        /// <summary>
        /// 记录审计追踪,
        /// </summary>
        /// <param name="from"></param>
        /// <param name="IsNew">新增时原值为空</param>
        public static void CheckChangesAndLog(Form from, bool IsNew = false)
        {
            try
            {
                if (DicAuditTrai.ContainsKey(from.Name))
                {

                    string fromName = DicAuditTraiFormName.ContainsKey(from.Name) ? DicAuditTraiFormName[from.Name] : from.Name;
                    // 收集当前值
                    // 递归遍历所有控件
                    GetControlValues(from, DicAuditTrai[from.Name], DicAuditTraiNewValue[from.Name]);

                    // 获取ToolStrip控件的值
                    GetToolStripValues(from, DicAuditTrai[from.Name], DicAuditTraiNewValue[from.Name]);

                    bool hasChanges = false;
                    StringBuilder changeLogs = new StringBuilder();

                    // 对比初始值和当前值
                    foreach (var initial in DicAuditTrai[from.Name])
                    {
                        if (DicAuditTraiNewValue[from.Name][initial.Key] != DicAuditTraiOldValue[from.Name][initial.Key] || IsNew)
                        {
                            hasChanges = true;
                            changeLogs.Append((IsNew ? "新增 " : " ") + $"{initial.Value} ");
                            changeLogs.Append($"原始值: {(IsNew ? "" : DicAuditTraiOldValue[from.Name][initial.Key])}, ");
                            changeLogs.Append($"修改后: {DicAuditTraiNewValue[from.Name][initial.Key]} \n");
                        }
                    }

                    // 如果有变化则输出日志
                    if (hasChanges)
                    {
                        SqliteHelper.ExecuteScalar($"INSERT into audit_log(Username,FormName,Detail,Created_At) VALUES ('{GlobalPara.CurrentUser.UserName}','{fromName}','{changeLogs.ToString()}',CURRENT_TIMESTAMP) ");
                    }
                }
            }
            catch (Exception ex)
            {
                Loger.Error("审计追踪记录日志报错 页面名称" + from.Name + ex.Message, exc:ex);
            }
        }

        public static void AuditLog(string msg)
        {
            try
            {
                SqliteHelper.ExecuteScalar($"INSERT into audit_log(Username,FormName,Detail,Created_At) VALUES ('{GlobalPara.CurrentUser.UserName}','核素库','{msg}',CURRENT_TIMESTAMP) ");
            }
            catch (Exception ex)
            {
                Loger.Error("删除核素库失败"+ ex.Message, exc:ex);
            }
        }

        #endregion

    }
}
