using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace TCOFurnace.Common
{
    public static class LanguageManager
    {
        // 存储所有语言的字典，键为语言代码，值为该语言的文本字典
        private static Dictionary<string, Dictionary<string, Dictionary<string, string>>> _languages = new Dictionary<string, Dictionary<string, Dictionary<string, string>>>();
        private static Dictionary<string, string> _messages_zh_CN = new Dictionary<string, string>();
        private static Dictionary<string, string> _messages_zh_US = new Dictionary<string, string>();
        /// <summary>
        /// 中英切换默认中文。 中文：zh-CN , 英文: en-US
        /// </summary>
        /// 
        public static string CurrentLanguage = "zh-CN";
        /// <summary>
        /// 初始化语言数据
        /// </summary>
        public static void Initialize()
        {
            //清空以前的配置
            _languages.Clear();

            #region 所有页面中文语言 配置
            //所有页面中文语言字典
            Dictionary<string, Dictionary<string, string>> _languagesCNFroms = new Dictionary<string, Dictionary<string, string>>();
            string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"XML\\{"zh-CN"}.xml");
            _languages["zh-CN"] = ReadFormXML(configPath);
            if (_languages["zh-CN"] != null)
            {
                Loger.Info("页面中文语言配置成功");
            }
            else
            {
                Loger.Info("页面中文语言配置失败");
            }
            #endregion

            #region 所有页面英文语言 配置
            //所有页面英文语言字典
            Dictionary<string, Dictionary<string, string>> _languagesUSFroms = new Dictionary<string, Dictionary<string, string>>();
            string configPathUS = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"XML\\{"zh-US"}.xml");
            _languages["en-US"] = ReadFormXML(configPathUS);
            if (_languages["en-US"] != null)
            {
                Loger.Info("页面英文语言配置成功");
            }
            else
            {
                Loger.Info("页面英文语言配置失败");
            }
            #endregion

            #region 文字信息中英文切换   
            //中文弹框信息
            string MsgPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"XML\\{"zh-CN"}.xml");
            _messages_zh_CN = ReadMsgXML(MsgPath);
            if (_messages_zh_CN != null)
            {
                Loger.Info("页面中文弹框信息配置成功");
            }
            else
            {
                Loger.Info("页面中文弹框信息语言配置失败");
            }
            //英文弹框信息
            string MsgPathUS = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"XML\\{"zh-US"}.xml");
            _messages_zh_US = ReadMsgXML(MsgPathUS);
            if (_messages_zh_US != null)
            {
                Loger.Info("页面英文弹框信息配置成功");
            }
            else
            {
                Loger.Info("页面英文弹框信息语言配置失败");
            }
            #endregion

            #region 权限配置

            #endregion
        }

        /// <summary>
        /// 切换语言
        /// </summary>
        /// <param name="languageCode">语言代码，如zh-CN, en-US</param>
        /// <param name="form">需要更新的窗体</param>
        public static void SwitchLanguage(string languageCode)
        {
            if (!_languages.ContainsKey(languageCode))
                throw new ArgumentException("不支持的语言代码");

            CurrentLanguage = languageCode;
            foreach (Form form in Application.OpenForms)
            {
                //SetData.langue = 0;
                UpdateFormLanguage(form);
            }
        }

        /// <summary>
        /// 更新窗体语言
        /// </summary>
        /// <param name="form">需要更新的窗体</param>
        public static void UpdateFormLanguage(Form form)
        {
            // 更新窗体标题
            if (_languages.ContainsKey(CurrentLanguage) && _languages[CurrentLanguage].ContainsKey(form.Name) && _languages[CurrentLanguage][form.Name].ContainsKey(form.Name))
            {
                form.Text = _languages[CurrentLanguage][form.Name][form.Name];
            }
            else
            {
                return;
            }
            // 递归更新所有控件
            UpdateControlLanguage(_languages[CurrentLanguage][form.Name], form);
        }

        /// <summary>
        /// 中英文翻译处理 递归更新控件语言
        /// </summary>
        /// <param name="controls">控件集合</param>
        private static void UpdateControlLanguage(Dictionary<string, string> formDic, Control control)
        {
            //对菜单的处理
            if (control is ToolStrip)
            {
                if (formDic.ContainsKey(control.Name))
                {
                    control.Text = formDic[control.Name];
                }

                ToolStrip ms = (ToolStrip)control;
                if (ms.Items.Count > 0)
                {
                    foreach (var cc in ms.Items)
                    {
                        if (cc is ToolStripMenuItem ccT)
                        {
                            if (formDic.ContainsKey(ccT.Name))
                                ccT.Text = formDic[ccT.Name];
                            //遍历菜单
                            UpdateControlLanguage(formDic, ccT);
                        }
                        else if (cc is ToolStripButton ccTb)
                        {
                            if (formDic.ContainsKey(ccTb.Name))
                                ccTb.Text = formDic[ccTb.Name];
                        }
                    }
                }
            }

            foreach (Control ctl in control.Controls)
            {
                // 如果控件名称在语言字典中存在，则更新文本
                if (formDic.ContainsKey(ctl.Name))
                {
                    ctl.Text = formDic[ctl.Name];
                }
                // 递归处理子控件
                if (control.Controls.Count > 0)
                {
                    UpdateControlLanguage(formDic, ctl);
                }
            }
        }

        /// <summary>
        /// 中英文翻译处理 ToolStrip 控件
        /// </summary>
        /// <param name="formDic"></param>
        /// <param name="item"></param>
        private static void UpdateControlLanguage(Dictionary<string, string> formDic, ToolStripMenuItem item)
        {
            if (item is ToolStripMenuItem)
            {
                if (formDic.ContainsKey(item.Name))
                {
                    item.Text = formDic[item.Name];
                }

                ToolStripMenuItem tsmi = (ToolStripMenuItem)item;
                if (tsmi.DropDownItems.Count > 0)
                {
                    foreach (ToolStripMenuItem c in tsmi.DropDownItems)
                    {
                        if (formDic.ContainsKey(c.Name))
                        {
                            c.Text = formDic[c.Name];
                        }
                        UpdateControlLanguage(formDic, c);
                    }
                }
            }
        }

        /// <summary>
        /// 获取所有页面控件名称
        /// </summary>
        /// <param name="xmlPath"></param>
        /// /// <param name="type">判断处理翻译还是权限</param>
        /// <returns></returns>
        private static Dictionary<string, Dictionary<string, string>> ReadFormXML(string xmlPath)
        {
            Dictionary<string, Dictionary<string, string>> dic = new Dictionary<string, Dictionary<string, string>>();
            if (!File.Exists(xmlPath))
            {
                Loger.Error($"语言配置文件不存在: {xmlPath}");
                return null;
            }

            // 加载并解析XML
            XDocument doc = XDocument.Load(xmlPath);
            XElement root = doc.Root;

            // 加载所有窗体的控件文本
            foreach (XElement formElement in root.Element("Forms").Elements("Form"))
            {
                string formName = formElement.Attribute("Name").Value;
                var controlsDict = new Dictionary<string, string>();

                // 添加所有控件翻译
                foreach (XElement control in formElement.Elements("Control"))
                {
                    string controlName = control.Attribute("Name").Value;
                    string text= control.Attribute("Text").Value;
                    controlsDict[controlName] = text;
                }
                dic.Add(formName, controlsDict);
            }
            return dic;
        }

        /// <summary>
        /// 读取提示中英文内容
        /// </summary>
        /// <param name="xmlPath"></param>
        /// <returns></returns>
        private static Dictionary<string, string> ReadMsgXML(string xmlPath)
        {
            if (!File.Exists(xmlPath))
            {
                Loger.Error($"语言配置文件不存在: {xmlPath}");
                return null;
            }
            // 加载并解析XML
            XDocument doc = XDocument.Load(xmlPath);
            XElement root = doc.Root;

            var MsgDict = new Dictionary<string, string>();
            // 加载所有窗体的控件文本
            foreach (XElement formElement in root.Element("Messages").Elements("Message"))
            {
                string Key = formElement.Attribute("Key").Value;
                string text = formElement.Value;
                MsgDict.Add(Key, text);
            }
            return MsgDict;
        }

        /// <summary>
        /// 获取文本信息中英文内用
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        public static string GetMsg(string Code)
        {
            Dictionary<string, string> keyValues = LanguageManager.CurrentLanguage == "zh-CN" ? _messages_zh_CN : _messages_zh_US;
            if (keyValues != null && keyValues.ContainsKey(Code))
            {
                return keyValues[Code];
            }
            return "";
        }
    }
}
