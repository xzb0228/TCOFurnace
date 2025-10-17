using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using TCOFurnace.Models;

namespace TCOFurnace.Common
{
    public static class PermissionManager
    {
        // 页面控件权限配置信息
        private static Dictionary<string, Dictionary<string, string>> _Manager = new Dictionary<string, Dictionary<string, string>>();
        /// <summary>
        /// 当前用户所拥有的权限列表
        /// </summary>
        public static List<Permission> PermissionList = new List<Permission>();
        /// <summary>
        /// 初始化权限数据
        /// </summary>
        public static void Initialize()
        {
            //是否启用页面控件权限
            if (GlobalPara.isOpenPermission)
            {
                //清空以前的权限
                _Manager.Clear();

                #region 所有页面权限配置
                string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"XML\\ControlPermission.xml");
                _Manager = ReadFormXML(configPath);
                #endregion

                #region MyRegion
                InitPermission();
                #endregion
            }
        }

        //获取当前登录人员的所有权限
        public static void InitPermission()
        {
            //用户权限赋值
            PermissionList = SqliteHelper.Query<Permission>($" select c.* from users a inner join users_roles b on a.UserCode=b.UserCode inner join role_permissions c on b.RoleCode=c.RoleCode inner join permissions d on c.PermissionCode=d.code where a.UserCode='{GlobalPara.CurrentUser.UserCode}' ").ToList();
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
                Loger.Error($"权限配置文件不存在: {xmlPath}");
                return null;
            }
            try
            {
                // 加载并解析XML
                XDocument doc = XDocument.Load(xmlPath);
                XElement root = doc.Root;

                // 加载所有窗体的控件文本
                foreach (XElement formElement in root.Elements("Form"))
                {
                    string formName = formElement.Attribute("Name").Value;
                    var controlsDict = new Dictionary<string, string>();

                    // 添加所有控件翻译
                    foreach (XElement control in formElement.Elements("Control"))
                    {
                        string controlName = control.Attribute("Name").Value;
                        string text = control.Attribute("PermissionsCode").Value;
                        controlsDict[controlName] = text;
                    }
                    dic.Add(formName, controlsDict);
                }
                return dic;
            }
            catch (Exception ex)
            {
                Loger.Error($"权限配置文件读取报错:" + ex.Message, exc:ex);
                return null;
            }
        }

        public static void UpdateFormConPermissions(Form form)
        {
            try
            {
                //超级管理员不控制权限
                if (GlobalPara.CurrentUser.UserName == "admin")
                {
                    return;
                }
                // 更新窗体标题
                if (_Manager.ContainsKey(form.Name))
                {
                    // 递归更新所有控件
                    UpdateControlPermissions(_Manager[form.Name], form);
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                Loger.Error("控制页面控件的可点击状态报错："+ ex.Message, exc:ex);
            }

        }
        /// <summary>
        /// 递归更新控件是否可编辑
        /// </summary>
        /// <param name="controls">控件集合</param>
        private static void UpdateControlPermissions(Dictionary<string, string> formDic, Control control)
        {
            if (control is ToolStrip)
            {
                //将资源与控件对应
                //resources.ApplyResources(control, control.Name);
                // 如果控件名称在语言字典中存在，则更新文本
                if (formDic.ContainsKey(control.Name))
                {
                    //有操作权限
                    if (PermissionList.FirstOrDefault(c => c.Code.ToString() == formDic[control.Name]) == null)
                    {
                        control.Enabled = false;
                    }
                    else
                    {
                        control.Enabled = true;
                    }
                }

                ToolStrip ms = (ToolStrip)control;
                if (ms.Items.Count > 0)
                {
                    foreach (var cc in ms.Items)
                    {
                        if (cc is ToolStripMenuItem ccT)
                        {
                            //有操作权限
                            if (formDic.ContainsKey(control.Name) && PermissionList.FirstOrDefault(c => c.Code.ToString() == formDic[ccT.Name]) == null)
                            {
                                ccT.Enabled = false;
                            }
                            else
                            {
                                ccT.Enabled = true;
                            }
                            //遍历菜单
                            UpdateControlPermissions(formDic, ccT);
                        }
                        else if (cc is ToolStripButton ccTb)
                        {
                            //有操作权限
                            if (formDic.ContainsKey(ccTb.Name) && PermissionList.FirstOrDefault(c => c.Code.ToString() == formDic[ccTb.Name]) == null)
                            {
                                ccTb.Enabled = false;
                            }
                            else
                            {
                                ccTb.Enabled = true;
                            }
                        }
                    }
                }
            }

            foreach (Control cl in control.Controls)
            {

                //resources.ApplyResources(c, c.Name);
                // 如果控件名称在语言字典中存在，则更新文本
                if (formDic.ContainsKey(cl.Name))
                {
                    //有操作权限
                    if (PermissionList.FirstOrDefault(c => c.Code.ToString() == formDic[cl.Name]) == null)
                    {
                        cl.Enabled = false;
                    }
                    else
                    {
                        cl.Enabled = true;
                    }
                }

                UpdateControlPermissions(formDic, cl);
            }
        }
        /// <summary>
        /// 递归更新控件是否可编辑
        /// </summary>
        /// <param name="formDic"></param>
        /// <param name="item"></param>

        private static void UpdateControlPermissions(Dictionary<string, string> formDic, ToolStripMenuItem item)
        {

            if (item is ToolStripMenuItem)
            {

                if (formDic.ContainsKey(item.Name))
                {
                    //有操作权限
                    if (PermissionList.FirstOrDefault(c => c.Code.ToString() == formDic[item.Name]) == null)
                    {
                        item.Visible = false;
                    }
                    else
                    {
                        item.Visible = true;
                    }
                }

                ToolStripMenuItem tsmi = (ToolStripMenuItem)item;
                if (tsmi.DropDownItems.Count > 0)
                {
                    foreach (ToolStripMenuItem c in tsmi.DropDownItems)
                    {
                        if (formDic.ContainsKey(c.Name))
                        {
                            //有操作权限
                            if (PermissionList.FirstOrDefault(cc => cc.Code.ToString() == formDic[c.Name]) == null)
                            {
                                c.Visible = false;
                            }
                            else
                            {
                                c.Visible = true;
                            }
                        }
                        UpdateControlPermissions(formDic, c);
                    }
                }
            }
        }

    }
}
