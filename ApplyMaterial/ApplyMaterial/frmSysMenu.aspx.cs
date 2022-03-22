using FineUI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ApplyMaterial.ApplyMaterial
{
    public partial class frmSysMenu : System.Web.UI.Page
    {
        private static bool AddIsSubNode = true;
        private void LoadSysMenuCode()
        {
            try
            {
                BLL.ApplyMaterial.System m_System = new BLL.ApplyMaterial.System();
                cmbESystemCode.DataTextField = "SystemName";
                cmbESystemCode.DataValueField = "SystemCode";
                cmbESystemCode.DataSource = m_System.Select(); 
                cmbESystemCode.DataBind();
            }
            catch (Exception ex)
            {
                BLL.Global.WriteLog(ex, this.ClientTarget, Util.LogType.Exception.ToString(), new System.Diagnostics.StackTrace(true).GetFrame(0).GetMethod().ToString(), Util.Util.UserName, Environment.MachineName, "Line:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileLineNumber().ToString() + " Column:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileColumnNumber().ToString());
                Alert.Show(ex.Message, "错误", MessageBoxIcon.Error);
            }
        }
        private void LoadMenu(string m_SystemCode)
        {
            try
            {
                tData.Nodes.Clear();
                BLL.ApplyMaterial.SysMenu m_SysMenu = new BLL.ApplyMaterial.SysMenu();
                DataTable dtSysMenu = m_SysMenu.Select(m_SystemCode);

                DataSet ds = new DataSet();
                ds.Tables.Add(dtSysMenu);
                ds.Relations.Add("TreeRelation", ds.Tables[0].Columns["Id"], ds.Tables[0].Columns["ParentId"]);
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    if (row["Id"].ToString() == row["ParentId"].ToString())
                    {
                        FineUI.TreeNode node = new FineUI.TreeNode();
                        node.NodeID = row["Id"].ToString();
                        node.Text = row["MenuName"].ToString();
                        //node.NavigateUrl = row["NavigateUrl"].ToString();
                        node.IconUrl = row["Icon"].ToString();
                        node.Expanded = Convert.ToBoolean(row["Expanded"]);
                        node.EnableClickEvent = true;
                        tData.Nodes.Add(node);

                        ResolveSubTree(row, node);
                    }
                }
            }
            catch (Exception ex)
            {
                BLL.Global.WriteLog(ex, this.ClientTarget, Util.LogType.Exception.ToString(), new System.Diagnostics.StackTrace(true).GetFrame(0).GetMethod().ToString(), Util.Util.UserName, Environment.MachineName, "Line:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileLineNumber().ToString() + " Column:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileColumnNumber().ToString());
                Alert.Show(ex.Message, "错误", MessageBoxIcon.Error);
            }
        }
        private void ResolveSubTree(DataRow dataRow, FineUI.TreeNode treeNode)
        {
            DataRow[] rows = dataRow.GetChildRows("TreeRelation");
            if (rows.Length > 0)
            {
                foreach (DataRow row in rows)
                { 
                    if (row["Id"].ToString() != row["ParentId"].ToString())
                    {
                        FineUI.TreeNode node = new FineUI.TreeNode();
                        node.NodeID = row["Id"].ToString();
                        node.Text = row["MenuName"].ToString();
                        //node.NavigateUrl = row["NavigateUrl"].ToString();
                        node.IconUrl = row["Icon"].ToString();
                        node.Expanded = Convert.ToBoolean(row["Expanded"]);
                        node.EnableClickEvent = true;
                        treeNode.Nodes.Add(node);

                        ResolveSubTree(row, node);
                    }
                }
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadSysMenuCode();
                LoadMenu(cmbESystemCode.SelectedValue);
            }
        }
        protected void cmbESystemCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadMenu(cmbESystemCode.SelectedValue);
        }
        private void Clear()
        {
            txtNodeCode.Text = "";
            txtMenuName.Text = "";
            txtNavigateUrl.Text = "";
            txtIcon.Text = "";
            cbExpanded.Checked = false;
            txtSortNo.Text = "";
        }
        protected void btnAddSiblingNode_Click(object sender, EventArgs e)
        {
            Clear();
            if (tData.Nodes.Count > 0)
            {
                if (tData.SelectedNode != null)
                {
                    BLL.ApplyMaterial.SysMenu m_SysMenu = new BLL.ApplyMaterial.SysMenu();
                    Model.ApplyMaterial.SysMenu sysMenu = m_SysMenu.Select(Convert.ToInt64(tData.SelectedNode.NodeID));
                    txtSortNo.Text = (m_SysMenu.GetMaxSortNo(cmbESystemCode.SelectedValue, sysMenu.ParentId == null ? 0 : sysMenu.ParentId.Value) + 1).ToString();
                    AddIsSubNode = false;
                    pEdit.Title = "新增";
                    pEdit.Hidden = false;
                }
                else
                    Alert.Show("请选在一个菜单节点", "警告", MessageBoxIcon.Warning);
            }
            else
            {
                txtSortNo.Text = "1";
                pEdit.Title = "新增";
                pEdit.Hidden = false;
            }
        }
        protected void btnAddSubNode_Click(object sender, EventArgs e)
        {
            try
            {
                Clear();
                if (tData.Nodes.Count > 0)
                {
                    if (tData.SelectedNode != null)
                    {
                        BLL.ApplyMaterial.SysMenu m_SysMenu = new BLL.ApplyMaterial.SysMenu();
                        Model.ApplyMaterial.SysMenu sysMenu = m_SysMenu.Select(Convert.ToInt64(tData.SelectedNode.NodeID));
                        txtSortNo.Text = (m_SysMenu.GetMaxSortNo(cmbESystemCode.SelectedValue, sysMenu.Id) + 1).ToString();
                        AddIsSubNode = true;
                        pEdit.Title = "新增";
                        pEdit.Hidden = false;
                    }
                    else
                        Alert.Show("请选在一个菜单节点", "警告", MessageBoxIcon.Warning);
                }
                else
                {
                    txtSortNo.Text = "1";
                    pEdit.Title = "新增";
                    pEdit.Hidden = false;
                }
            }
            catch (Exception ex)
            {
                BLL.Global.WriteLog(ex, this.ClientTarget, Util.LogType.Exception.ToString(), new System.Diagnostics.StackTrace(true).GetFrame(0).GetMethod().ToString(), Util.Util.UserName, Environment.MachineName, "Line:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileLineNumber().ToString() + " Column:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileColumnNumber().ToString());
                Alert.Show(ex.Message, "错误", MessageBoxIcon.Error);
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                Clear();
                if (tData.SelectedNode != null)
                {
                    BLL.ApplyMaterial.SysMenu m_SysMenu = new BLL.ApplyMaterial.SysMenu();
                    Model.ApplyMaterial.SysMenu sysMenu = m_SysMenu.Select(Convert.ToInt64(tData.SelectedNode.NodeID));
                    if (sysMenu != null)
                    {
                        txtNodeCode.Text = sysMenu.NodeCode;
                        txtMenuName.Text = sysMenu.MenuName;
                        txtNavigateUrl.Text = sysMenu.NavigateUrl;
                        txtIcon.Text = sysMenu.Icon;
                        cbExpanded.Checked = sysMenu.Expanded;
                        txtSortNo.Text = sysMenu.SortNo.ToString();
                    }

                    pEdit.Title = "修改";
                    pEdit.Hidden = false;
                }
                else
                    Alert.Show("请选在一个菜单节点", "警告", MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                BLL.Global.WriteLog(ex, this.ClientTarget, Util.LogType.Exception.ToString(), new System.Diagnostics.StackTrace(true).GetFrame(0).GetMethod().ToString(), Util.Util.UserName, Environment.MachineName, "Line:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileLineNumber().ToString() + " Column:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileColumnNumber().ToString());
                Alert.Show(ex.Message, "错误", MessageBoxIcon.Error);
            }
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (tData.SelectedNode.Leaf)
                {
                    Alert.Show("请从叶子节点开始删除", "警告", MessageBoxIcon.Warning);
                    return;
                }
                if (tData.SelectedNode != null)
                {
                    BLL.ApplyMaterial.SysMenu m_SysMenu = new BLL.ApplyMaterial.SysMenu();
                    if (m_SysMenu.Delete(Convert.ToInt64(tData.SelectedNode.NodeID), Util.Util.UserName))
                    {
                        LoadMenu(cmbESystemCode.SelectedValue);
                        Alert.Show("删除成功", "提示", MessageBoxIcon.Information);
                    }
                    else
                        Alert.Show("删除失败", "错误", MessageBoxIcon.Error);

                }
                else
                    Alert.Show("请选在一个菜单节点", "警告", MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                BLL.Global.WriteLog(ex, this.ClientTarget, Util.LogType.Exception.ToString(), new System.Diagnostics.StackTrace(true).GetFrame(0).GetMethod().ToString(), Util.Util.UserName, Environment.MachineName, "Line:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileLineNumber().ToString() + " Column:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileColumnNumber().ToString());
                Alert.Show(ex.Message, "错误", MessageBoxIcon.Error);
            }
        }
        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                if (pEdit.Title == "新增")
                {
                    BLL.ApplyMaterial.SysMenu m_SysMenu = new BLL.ApplyMaterial.SysMenu();
                    if (m_SysMenu.IsExist(cmbESystemCode.SelectedValue, txtNodeCode.Text.Trim()))
                    {
                        Alert.Show("该节点代码已经存在", "警告", MessageBoxIcon.Warning);
                        return;
                    }
                    if (tData.SelectedNode != null)
                    {
                        if (m_SysMenu.IsExist(cmbESystemCode.SelectedValue, Convert.ToInt32(tData.SelectedNode.NodeID), txtMenuName.Text.Trim()))
                        {
                            Alert.Show("该菜单名称已经存在", "警告", MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    Model.ApplyMaterial.SysMenu sysMenu = new Model.ApplyMaterial.SysMenu();
                    sysMenu.SystemCode = cmbESystemCode.SelectedValue;
                    sysMenu.NodeCode = txtNodeCode.Text.Trim();
                    sysMenu.MenuName = txtMenuName.Text.Trim();
                    if (tData.Nodes.Count > 0)
                    {
                        Model.ApplyMaterial.SysMenu sysMenu1 = m_SysMenu.Select(Convert.ToInt64(tData.SelectedNode.NodeID));
                        if (AddIsSubNode)
                            sysMenu.ParentId = sysMenu1.Id;
                        else
                            sysMenu.ParentId = null;
                    }
                    else
                        sysMenu.ParentId = null;
                    sysMenu.NavigateUrl = txtNavigateUrl.Text.Trim();
                    sysMenu.Icon = txtIcon.Text.Trim();
                    sysMenu.Expanded = cbExpanded.Checked;
                    sysMenu.SortNo = Convert.ToInt32(txtSortNo.Text);
                    if (m_SysMenu.Add(sysMenu))
                    {
                        LoadMenu(cmbESystemCode.SelectedValue);
                        Clear();
                        pEdit.Hidden = true;
                    }
                    else
                        Alert.Show("新增失败", "错误", MessageBoxIcon.Error);
                }
                else if (pEdit.Title == "修改" && tData.SelectedNode!=null)
                {
                    BLL.ApplyMaterial.SysMenu m_SysMenu = new BLL.ApplyMaterial.SysMenu();
                    Model.ApplyMaterial.SysMenu sysMenu = m_SysMenu.Select(Convert.ToInt64(tData.SelectedNode.NodeID));
                    if (sysMenu.NodeCode != txtNodeCode.Text.Trim())
                    {
                        if (m_SysMenu.IsExist(cmbESystemCode.SelectedValue, txtNodeCode.Text.Trim()))
                        {
                            Alert.Show("该节点代码已经存在", "警告", MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    if (sysMenu.MenuName != txtMenuName.Text.Trim())
                    {
                        if (m_SysMenu.IsExist(cmbESystemCode.SelectedValue, Convert.ToInt32(tData.SelectedNode.NodeID), txtMenuName.Text.Trim()))
                        {
                            Alert.Show("该菜单名称已经存在", "警告", MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    sysMenu.SystemCode = cmbESystemCode.SelectedValue;
                    sysMenu.NodeCode = txtNodeCode.Text.Trim();
                    sysMenu.MenuName = txtMenuName.Text.Trim();
                    sysMenu.NavigateUrl = txtNavigateUrl.Text.Trim();
                    sysMenu.Icon = txtIcon.Text.Trim();
                    sysMenu.Expanded = cbExpanded.Checked;
                    sysMenu.SortNo = Convert.ToInt32(txtSortNo.Text);
                    if (m_SysMenu.Update(sysMenu))
                    {
                        LoadMenu(cmbESystemCode.SelectedValue);
                        Clear();
                        pEdit.Hidden = true;
                    }
                    else
                        Alert.Show("修改失败", "错误", MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                BLL.Global.WriteLog(ex, this.ClientTarget, Util.LogType.Exception.ToString(), new System.Diagnostics.StackTrace(true).GetFrame(0).GetMethod().ToString(), Util.Util.UserName, Environment.MachineName, "Line:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileLineNumber().ToString() + " Column:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileColumnNumber().ToString());
                Alert.Show(ex.Message, "错误", MessageBoxIcon.Error);
            }
        }
    }
}