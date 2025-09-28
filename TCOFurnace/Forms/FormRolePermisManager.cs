using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using TCOFurnace.Models;

namespace TCOFurnace.Forms
{
    public class FormRolePermisManager : BaseForm
    {
        // 数据模型
        private List<UserRole> _users = new List<UserRole>();
        private List<Role> _roles = new List<Role>();
        private List<Permission> _permissions = new List<Permission>();

        // 当前选中的用户
        private UserRole _selectedUser = null;
        private Panel pnlPermission;
        private TreeView tvPermissions;
        private Label lblPermissionTree;

        // 当前选中的角色
        private Role _selectedRole = null;

        public FormRolePermisManager()
        {
            InitializeComponent();
            InitializeUI();
            LoadData();
            BindData();
        }

        #region 初始化UI
        private void InitializeUI()
        {
            // 设置窗体样式
            this.Text = "用户角色权限管理";
            this.StartPosition = FormStartPosition.CenterScreen;

            // 设置SplitContainer样式
            //splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.BorderStyle = BorderStyle.FixedSingle;
            splitContainer1.SplitterWidth = 6;

            splitContainer2.Dock = DockStyle.Fill;
            splitContainer2.BorderStyle = BorderStyle.FixedSingle;
            splitContainer2.SplitterWidth = 6;

            // 设置列表标题样式
            lblUserList.Text = "用户列表";
            lblRoleList.Text = "角色列表";
            lblPermissionTree.Text = "权限列表";

            foreach (var label in new Label[] { lblUserList, lblRoleList, lblPermissionTree })
            {
                label.Dock = DockStyle.Top;
                label.Height = 35;
                label.Font = new Font("微软雅黑", 10, FontStyle.Bold);
                label.ForeColor = Color.White;
                label.BackColor = Color.FromArgb(54, 79, 107);
                label.TextAlign = ContentAlignment.MiddleCenter;
            }

            // 设置用户列表样式
            lstUsers.Dock = DockStyle.Fill;
            lstUsers.Font = new Font("微软雅黑", 9);
            lstUsers.ItemHeight = 30;
            lstUsers.DrawMode = DrawMode.OwnerDrawFixed;
            lstUsers.BorderStyle = BorderStyle.None;

            // 设置角色列表样式
            clbRoles.Dock = DockStyle.Fill;
            clbRoles.Font = new Font("微软雅黑", 9);
            clbRoles.ItemHeight = 28;
            clbRoles.CheckOnClick = true;
            clbRoles.BorderStyle = BorderStyle.None;
            clbRoles.DisplayMember = "name"; // 显示名称的字段
            clbRoles.ValueMember = "id";     // 实际值字段

            // 设置权限树样式
            tvPermissions.Dock = DockStyle.Fill;
            tvPermissions.Font = new Font("微软雅黑", 9);
            tvPermissions.CheckBoxes = true;
            tvPermissions.BorderStyle = BorderStyle.None;
            tvPermissions.ShowLines = true;
            tvPermissions.ShowPlusMinus = true;
            tvPermissions.ShowRootLines = true;


            //btnSaveUserRoles.Dock = DockStyle.Top;
            //btnSaveRolePermissions.Dock = DockStyle.Top;
            //btnAddRole.Dock = DockStyle.Top;

            // 添加事件处理
            lstUsers.DrawItem += lstUsers_DrawItem;
            lstUsers.SelectedIndexChanged += lstUsers_SelectedIndexChanged;
            clbRoles.ItemCheck += clbRoles_ItemCheck;
            clbRoles.MouseDown += clbRoles_MouseDown;

            tvPermissions.AfterCheck += tvPermissions_AfterCheck;

            // 添加面板和容器
            pnlUser.Controls.Add(lstUsers);
            pnlUser.Controls.Add(lblUserList);
            pnlUser.Controls.SetChildIndex(lblUserList, 1);
            pnlUser.Controls.SetChildIndex(lstUsers, 0);
            pnlUser.Controls.SetChildIndex(lstUsers, 0);

            pnlRole.Controls.Add(clbRoles);
            pnlRole.Controls.Add(lblRoleList);
            //pnlRole.Controls.Add(btnSaveUserRoles);
            //pnlRole.Controls.Add(btnAddRole);
            pnlRole.Controls.SetChildIndex(lblRoleList, 1);
            pnlRole.Controls.SetChildIndex(clbRoles, 0);
            //pnlRole.Controls.SetChildIndex(btnSaveUserRoles, 2);
            // pnlRole.Controls.SetChildIndex(btnAddRole, 3);

            pnlPermission.Controls.Add(tvPermissions);
            pnlPermission.Controls.Add(lblPermissionTree);
            // pnlPermission.Controls.Add(btnSaveRolePermissions);
            pnlPermission.Controls.SetChildIndex(lblPermissionTree, 1);
            pnlPermission.Controls.SetChildIndex(tvPermissions, 0);
            // pnlPermission.Controls.SetChildIndex(btnSaveRolePermissions, 2);

            //splitContainer2.Panel1.Controls.Add(pnlRole);
            //splitContainer2.Panel2.Controls.Add(pnlPermission);
            //splitContainer1.Panel1.Controls.Add(pnlUser);
            //splitContainer1.Panel2.Controls.Add(splitContainer2);
        }
        #endregion

        #region 数据加载和绑定
        private void LoadData()
        {
            _users = new List<UserRole>();
            _roles = new List<Role>();
            _permissions = new List<Permission>();

            //_users = new List<UserRole>
            //{
            //    new UserRole { Id = 1, Name = "张三", Department = "技术部"},
            //};
            //加载用户数据
            List<UserRole> users = SqliteHelper.Query<UserRole>(
                " select a.UserCode,a.UserName ,b.RoleCode from users a left join users_roles b on a.UserCode=b.UserCode  ").ToList();
            foreach (var item in users)
            {
                UserRole UR = _users.FirstOrDefault(c => c.UserCode == item.UserCode);
                if (UR == null)
                {
                    item.RoleCodes = new List<int>();
                    item.RoleCodes.Add(item.RoleCode);
                    _users.Add(item);
                }
                else
                {
                    UR.RoleCodes.Add(item.RoleCode);
                }
            }



            //_roles = new List<Role>
            //{
            //        new Role { Id = 1, Name = "管理员", Description = "系统管理员，拥有所有权限" },
            //};
            //加载角色数据
            List<Role> RoList = SqliteHelper.Query<Role>(
                " select  a.code,a.name,b.PermissionCode PermissionCode from roles a left join role_permissions b on a.code = b.RoleCode ").ToList();
            foreach (var item in RoList)
            {
                Role UR = _roles.FirstOrDefault(c => c.Code == item.Code);
                if (UR == null)
                {
                    item.Permissions = new List<Permission>();
                    item.Permissions.Add(new Permission() { Code = item.PermissionCode });
                    _roles.Add(item);
                }
                else
                {
                    UR.Permissions.Add(new Permission() { Code = item.PermissionCode });
                }
            }

            //加载权限数据 _permissions = new List<Permission>();
            List<Permission> PermissioList = SqliteHelper.Query<Permission>(" select * from permissions  ").ToList();
            _permissions.AddRange(PermissioList.Where(c => c.ParentCode == null || c.ParentCode == 0));
            foreach (var item in _permissions)
            {
                if (item.Children == null)
                {
                    item.Children = new List<Permission>();
                }
                item.Children.AddRange(PermissioList.Where(c => c.ParentCode == item.Code));

                foreach (var item1 in item.Children)
                {
                    if (item1.Children == null)
                    {
                        item1.Children = new List<Permission>();
                    }
                    item1.Children.AddRange(PermissioList.Where(c => c.ParentCode == item.Code));
                }

            }
        }

        private void BindData()
        {
            // 绑定用户列表
            lstUsers.Items.Clear();
            foreach (var user in _users)
            {
                lstUsers.Items.Add(user);
            }

            // 绑定角色列表
            clbRoles.Items.Clear();
            foreach (var role in _roles)
            {
                clbRoles.Items.Add(role);
            }

            // 绑定权限树
            tvPermissions.Nodes.Clear();
            foreach (var permission in _permissions)
            {
                var node = CreateTreeNode(permission);
                tvPermissions.Nodes.Add(node);
            }
            tvPermissions.ExpandAll();
        }

        private TreeNode CreateTreeNode(Permission permission)
        {
            var node = new TreeNode(permission.Description + $"[{permission.Code}]")
            {
                Tag = permission,
                Checked = false
            };

            foreach (var child in permission.Children)
            {
                node.Nodes.Add(CreateTreeNode(child));
            }

            return node;
        }
        #endregion

        #region 事件处理
        private void lstUsers_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            e.DrawBackground();

            // 绘制选中状态
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                using (var brush = new LinearGradientBrush(e.Bounds,
                    Color.FromArgb(150, 180, 210),
                    Color.FromArgb(100, 140, 180),
                    LinearGradientMode.Horizontal))
                {
                    e.Graphics.FillRectangle(brush, e.Bounds);
                }
            }
            else
            {
                e.Graphics.FillRectangle(Brushes.White, e.Bounds);
            }

            // 获取用户对象
            var user = (UserRole)lstUsers.Items[e.Index];

            // 绘制用户信息
            string displayText = $"{user.UserName}";

            using (var font = new Font("微软雅黑", 9, FontStyle.Bold))
            {
                e.Graphics.DrawString(displayText, font, Brushes.DimGray,
                    e.Bounds.Left + 10, e.Bounds.Top + 2);
            }



            // 绘制分隔线
            using (var pen = new Pen(Color.LightGray))
            {
                e.Graphics.DrawLine(pen, e.Bounds.Left, e.Bounds.Bottom - 1,
                    e.Bounds.Right, e.Bounds.Bottom - 1);
            }

            e.DrawFocusRectangle();
        }

        private void lstUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstUsers.SelectedIndex >= 0)
            {
                _selectedUser = (UserRole)lstUsers.SelectedItem;
                UpdateRoleCheckboxes();
            }
        }
        // 标记是否需要阻止勾选
        private bool _preventCheck = false;
        private void clbRoles_MouseDown(object sender, MouseEventArgs e)
        {
            CheckedListBox clb = sender as CheckedListBox;
            if (clb == null) return;

            int itemIndex = clb.IndexFromPoint(e.Location);
            if (itemIndex == -1) return;

            // 计算复选框区域（与方案一一致）
            Rectangle itemRect = clb.GetItemRectangle(itemIndex);
            Rectangle checkBoxRect = new Rectangle(
                itemRect.Left + 2,
                itemRect.Top + 2,
                16,
                itemRect.Height - 4
            );

            // 标记是否阻止勾选
            _preventCheck = !checkBoxRect.Contains(e.Location);
        }

        private void clbRoles_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (_preventCheck)
            {
                e.NewValue = e.CurrentValue; // 强制不改变勾选状态
                //_preventCheck = false;       // 重置标记
            }

            var role = (Role)clbRoles.Items[e.Index];
            _selectedRole = role;
            UpdatePermissionCheckboxes();

            if (!_preventCheck)
            {
                // 防止在加载时触发
                if (_selectedUser == null) return;
                // 更新当前选中用户的角色
                if (e.NewValue == CheckState.Checked)
                {
                    if (!_selectedUser.RoleCodes.Contains(role.Code))
                    {
                        _selectedUser.RoleCodes.Add(role.Code);

                        // 这里可以添加保存到数据库的逻辑
                        using (var conn = SqliteHelper.GetConn())
                        {
                            SqliteHelper.ExecuteScalar($"insert into users_roles(usercode,rolecode) values('{_selectedUser.UserCode}','{_selectedRole.Code}') ");
                        }
                    }
                }
                else
                {
                    _selectedUser.RoleCodes.Remove(role.Code);
                    // 这里可以添加保存到数据库的逻辑
                    using (var conn = SqliteHelper.GetConn())
                    {
                        SqliteHelper.ExecuteScalar($"delete from users_roles where username='{_selectedUser.UserCode}' and role_code='{_selectedRole.Code}'");

                    }
                }
            }

        }

        private void tvPermissions_AfterCheck(object sender, TreeViewEventArgs e)
        {
            // 防止在加载时触发
            if (_selectedRole == null) return;

            // 禁用事件，避免递归触发
            tvPermissions.AfterCheck -= tvPermissions_AfterCheck;

            try
            {
                var permission = (Permission)e.Node.Tag;
                bool isChecked = e.Node.Checked;

                // 更新子节点
                //UpdateChildNodes(e.Node, isChecked);

                // 更新父节点
                // UpdateParentNodes(e.Node.Parent);

                // 更新角色权限
                UpdateRolePermissions(permission, isChecked);

                // 这里可以添加保存到数据库的逻辑

                if (isChecked)
                {
                    SqliteHelper.ExecuteScalar($"insert into role_permissions(RoleCode,PermissionCode) values('{_selectedRole.Code}','{permission.Code}') ");
                }
                else
                {
                    SqliteHelper.ExecuteScalar($"delete from role_permissions where role_code='{_selectedRole.Code}' and permission_code='{permission.Code}'");
                }

            }
            finally
            {
                // 重新启用事件
                tvPermissions.AfterCheck += tvPermissions_AfterCheck;
            }
        }
        #endregion

        #region 辅助方法
        private void UpdateRoleCheckboxes()
        {
            // 禁用事件，避免触发不必要的更新
            clbRoles.ItemCheck -= clbRoles_ItemCheck;

            try
            {
                // 更新角色复选框状态
                for (int i = 0; i < clbRoles.Items.Count; i++)
                {
                    var role = (Role)clbRoles.Items[i];
                    clbRoles.SetItemChecked(i, _selectedUser.RoleCodes.Contains(role.Code));
                }

                _selectedRole = null;
                UpdatePermissionCheckboxes();
                //// 默认选中第一个角色
                //if (clbRoles.Items.Count > 0)
                //{
                //    clbRoles.SelectedIndex = 0;
                //    _selectedRole = (Role)clbRoles.SelectedItem;
                //    UpdatePermissionCheckboxes();
                //}
            }
            finally
            {
                // 重新启用事件
                clbRoles.ItemCheck += clbRoles_ItemCheck;
            }
        }

        private void UpdatePermissionCheckboxes()
        {
            // 禁用事件，避免触发不必要的更新
            tvPermissions.AfterCheck -= tvPermissions_AfterCheck;
            try
            {
                // 更新权限树复选框状态
                foreach (TreeNode node in tvPermissions.Nodes)
                {
                    UpdatePermissionNode(node);
                }
            }
            finally
            {
                // 重新启用事件
                tvPermissions.AfterCheck += tvPermissions_AfterCheck;
            }
        }

        private void UpdatePermissionNode(TreeNode node)
        {
            var permission = (Permission)node.Tag;
            node.Checked = _selectedRole == null ? false : (_selectedRole.Permissions.Where(c => c.Code == permission.Code).Count() > 0);

            foreach (TreeNode childNode in node.Nodes)
            {
                UpdatePermissionNode(childNode);
            }
        }

        private void UpdateChildNodes(TreeNode node, bool isChecked)
        {
            foreach (TreeNode childNode in node.Nodes)
            {
                childNode.Checked = isChecked;
                UpdateChildNodes(childNode, isChecked);
            }
        }

        private void UpdateParentNodes(TreeNode node)
        {
            if (node == null) return;

            bool allChecked = true;
            bool allUnchecked = true;

            foreach (TreeNode childNode in node.Nodes)
            {
                if (!childNode.Checked)
                    allChecked = false;
                else
                    allUnchecked = false;
            }

            if (allChecked)
                node.Checked = true;
            else if (allUnchecked)
                node.Checked = false;
            // 否则保持半选状态，TreeView会自动处理

            UpdateParentNodes(node.Parent);
        }

        private void UpdateRolePermissions(Permission permission, bool isChecked)
        {
            if (isChecked && !(_selectedRole.Permissions.Where(c => c.Code == permission.Code).Count() > 0))
            {
                _selectedRole.Permissions.Add(permission);
            }
            else if (!isChecked && (_selectedRole.Permissions.Where(c => c.Code == permission.Code).Count() > 0))
            {
                _selectedRole.Permissions.Remove(_selectedRole.Permissions.First(c => c.Code == permission.Code));
            }
        }
        #endregion

        #region 设计器生成的代码
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pnlUser = new System.Windows.Forms.Panel();
            this.lstUsers = new System.Windows.Forms.ListBox();
            this.lblUserList = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.pnlRole = new System.Windows.Forms.Panel();
            this.clbRoles = new System.Windows.Forms.CheckedListBox();
            this.lblRoleList = new System.Windows.Forms.Label();
            this.pnlPermission = new System.Windows.Forms.Panel();
            this.tvPermissions = new System.Windows.Forms.TreeView();
            this.lblPermissionTree = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.pnlUser.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.pnlRole.SuspendLayout();
            this.pnlPermission.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(0, 8);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.pnlUser);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(595, 497);
            this.splitContainer1.SplitterDistance = 176;
            this.splitContainer1.TabIndex = 0;
            // 
            // pnlUser
            // 
            this.pnlUser.Controls.Add(this.lstUsers);
            this.pnlUser.Controls.Add(this.lblUserList);
            this.pnlUser.Location = new System.Drawing.Point(0, 0);
            this.pnlUser.Name = "pnlUser";
            this.pnlUser.Size = new System.Drawing.Size(159, 485);
            this.pnlUser.TabIndex = 0;
            // 
            // lstUsers
            // 
            this.lstUsers.FormattingEnabled = true;
            this.lstUsers.ItemHeight = 12;
            this.lstUsers.Location = new System.Drawing.Point(3, 26);
            this.lstUsers.Name = "lstUsers";
            this.lstUsers.Size = new System.Drawing.Size(150, 448);
            this.lstUsers.TabIndex = 1;
            // 
            // lblUserList
            // 
            this.lblUserList.Location = new System.Drawing.Point(3, 0);
            this.lblUserList.Name = "lblUserList";
            this.lblUserList.Size = new System.Drawing.Size(100, 23);
            this.lblUserList.TabIndex = 2;
            this.lblUserList.Text = "用户列表";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.pnlRole);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.pnlPermission);
            this.splitContainer2.Size = new System.Drawing.Size(415, 497);
            this.splitContainer2.SplitterDistance = 167;
            this.splitContainer2.TabIndex = 0;
            // 
            // pnlRole
            // 
            this.pnlRole.Controls.Add(this.clbRoles);
            this.pnlRole.Controls.Add(this.lblRoleList);
            this.pnlRole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRole.Location = new System.Drawing.Point(0, 0);
            this.pnlRole.Name = "pnlRole";
            this.pnlRole.Size = new System.Drawing.Size(167, 497);
            this.pnlRole.TabIndex = 0;
            // 
            // clbRoles
            // 
            this.clbRoles.FormattingEnabled = true;
            this.clbRoles.Location = new System.Drawing.Point(3, 26);
            this.clbRoles.Name = "clbRoles";
            this.clbRoles.Size = new System.Drawing.Size(150, 452);
            this.clbRoles.TabIndex = 2;
            // 
            // lblRoleList
            // 
            this.lblRoleList.Location = new System.Drawing.Point(0, 0);
            this.lblRoleList.Name = "lblRoleList";
            this.lblRoleList.Size = new System.Drawing.Size(100, 23);
            this.lblRoleList.TabIndex = 3;
            // 
            // pnlPermission
            // 
            this.pnlPermission.Controls.Add(this.tvPermissions);
            this.pnlPermission.Controls.Add(this.lblPermissionTree);
            this.pnlPermission.Location = new System.Drawing.Point(0, 0);
            this.pnlPermission.Name = "pnlPermission";
            this.pnlPermission.Size = new System.Drawing.Size(216, 485);
            this.pnlPermission.TabIndex = 0;
            // 
            // tvPermissions
            // 
            this.tvPermissions.Location = new System.Drawing.Point(3, 26);
            this.tvPermissions.Name = "tvPermissions";
            this.tvPermissions.Size = new System.Drawing.Size(210, 452);
            this.tvPermissions.TabIndex = 1;
            // 
            // lblPermissionTree
            // 
            this.lblPermissionTree.Location = new System.Drawing.Point(1, 1);
            this.lblPermissionTree.Name = "lblPermissionTree";
            this.lblPermissionTree.Size = new System.Drawing.Size(100, 23);
            this.lblPermissionTree.TabIndex = 2;
            // 
            // FormRolePermisManager
            // 
            this.ClientSize = new System.Drawing.Size(595, 514);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FormRolePermisManager";
            this.Text = "用户角色权限管理";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.pnlUser.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.pnlRole.ResumeLayout(false);
            this.pnlPermission.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private SplitContainer splitContainer1;
        private Panel pnlUser;
        private ListBox lstUsers;
        private Label lblUserList;
        private SplitContainer splitContainer2;
        private Panel pnlRole;
        private CheckedListBox clbRoles;
        private Label lblRoleList;
        #endregion
    }

}
