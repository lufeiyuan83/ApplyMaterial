using FineUI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ERP.SystemManagement
{
    public partial class SysMenu : BasePage
    {
        private readonly string strClassName = new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().ReflectedType.Name + ".aspx";
        private static DataTable dtDataSource;
        private static bool AddIsSubNode = true;
        private void LoadSysMenuCode()
        {
            try
            {
                BLL.Auth.System m_System = new BLL.Auth.System();
                cmbESystemCode.DataTextField = "SystemName";
                cmbESystemCode.DataValueField = "SystemCode";
                cmbESystemCode.DataSource = m_System.Select();
                cmbESystemCode.DataBind();
            }
            catch (Exception ex)
            {
                BLL.Global.WriteLog(ex, this.ClientTarget, Util.LogType.Exception.ToString(), new System.Diagnostics.StackTrace(true).GetFrame(0).GetMethod().ToString(), Util.Util.UserName, Environment.MachineName, "Line:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileLineNumber().ToString() + " Column:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileColumnNumber().ToString());
                Alert.ShowInTop(ex.Message, GetGlobalResourceObject("Language", "Error").ToString(), MessageBoxIcon.Error);
            }
        }
        private void LoadMenu(string m_SystemCode)
        {
            try
            {
                tData.Nodes.Clear();
                BLL.Auth.SysMenu m_SysMenu = new BLL.Auth.SysMenu();
                DataTable dtSysMenu = m_SysMenu.Select(m_SystemCode);
                if (dtSysMenu != null && dtSysMenu.Rows.Count > 0)
                {
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
                            node.IconUrl = row["Icon"].ToString();
                            node.Expanded = Convert.ToBoolean(row["Expanded"]);
                            node.EnableClickEvent = true;
                            tData.Nodes.Add(node);

                            ResolveSubTree(row, node);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                BLL.Global.WriteLog(ex, this.ClientTarget, Util.LogType.Exception.ToString(), new System.Diagnostics.StackTrace(true).GetFrame(0).GetMethod().ToString(), Util.Util.UserName, Environment.MachineName, "Line:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileLineNumber().ToString() + " Column:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileColumnNumber().ToString());
                Alert.ShowInTop(ex.Message, GetGlobalResourceObject("Language", "Error").ToString(), MessageBoxIcon.Error);
            }
        }
        private void LoadElement(long m_SysMenuIdId)
        {
            try
            {
                BLL.PMS.Element m_Element = new BLL.PMS.Element();
                dtDataSource = m_Element.SearchBySysMenuId(m_SysMenuIdId);
                dgvData.DataSource = dtDataSource;
                dgvData.DataBind();
            }
            catch (Exception ex)
            {
                BLL.Global.WriteLog(ex, this.ClientTarget, Util.LogType.Exception.ToString(), new System.Diagnostics.StackTrace(true).GetFrame(0).GetMethod().ToString(), Util.Util.UserName, Environment.MachineName, "Line:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileLineNumber().ToString() + " Column:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileColumnNumber().ToString());
                Alert.ShowInTop(ex.Message, GetGlobalResourceObject("Language", "Error").ToString(), MessageBoxIcon.Error);
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
                btnSave.OnClientClick = wAccess.GetHideReference();
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
            txtMenuNameEn.Text = "";
            txtMenuNameTh.Text = "";
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
                int m_ParentId = 0;
                BLL.Auth.SysMenu m_SysMenu = new BLL.Auth.SysMenu();
                if (tData.SelectedNode != null)
                {
                    Model.Auth.SysMenu sysMenu = m_SysMenu.Select(Convert.ToInt64(tData.SelectedNode.NodeID)); 
                    if (sysMenu != null)
                    {
                        m_ParentId = sysMenu.ParentId;
                    }
                }
                int intMaxSortNo = m_SysMenu.GetMaxSortNo(cmbESystemCode.SelectedValue, m_ParentId);

                txtSortNo.Text = (intMaxSortNo + 1).ToString();
                AddIsSubNode = false;
                pEdit.Title = GetGlobalResourceObject(strClassName, "Add").ToString();
                pEdit.Hidden = false;
            }
            else
            {
                txtSortNo.Text = "1";
                pEdit.Title = GetGlobalResourceObject(strClassName, "Add").ToString();
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
                    int intParentId = 0;
                    BLL.Auth.SysMenu m_SysMenu = new BLL.Auth.SysMenu();
                    if (tData.SelectedNode != null)
                    {
                        Model.Auth.SysMenu sysMenu = m_SysMenu.Select(Convert.ToInt32(tData.SelectedNode.NodeID));
                        if (sysMenu != null)
                        {
                            intParentId = sysMenu.Id;
                        }
                    }

                    int intMaxSortNo = m_SysMenu.GetMaxSortNo(cmbESystemCode.SelectedValue, intParentId);

                    txtSortNo.Text = (intMaxSortNo + 1).ToString();
                    AddIsSubNode = true;
                    pEdit.Title = GetGlobalResourceObject(strClassName, "Add").ToString();
                    pEdit.Hidden = false;
                }
                else
                {
                    txtSortNo.Text = "1";
                    pEdit.Title = GetGlobalResourceObject(strClassName, "Add").ToString();
                    pEdit.Hidden = false;
                }
            }
            catch (Exception ex)
            {
                BLL.Global.WriteLog(ex, this.ClientTarget, Util.LogType.Exception.ToString(), new System.Diagnostics.StackTrace(true).GetFrame(0).GetMethod().ToString(), Util.Util.UserName, Environment.MachineName, "Line:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileLineNumber().ToString() + " Column:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileColumnNumber().ToString());
                Alert.ShowInTop(ex.Message, GetGlobalResourceObject("Language", "Error").ToString(), MessageBoxIcon.Error);
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                Clear();
                if (tData.SelectedNode != null)
                {
                    BLL.Auth.SysMenu m_SysMenu = new BLL.Auth.SysMenu();
                    Model.Auth.SysMenu sysMenu = m_SysMenu.Select(Convert.ToInt32(tData.SelectedNode.NodeID));
                    if (sysMenu != null)
                    {
                        txtNodeCode.Text = sysMenu.NodeCode;
                        txtMenuName.Text = sysMenu.MenuName;
                        txtMenuNameEn.Text = sysMenu.MenuNameEn;
                        txtMenuNameTh.Text = sysMenu.MenuNameTh;
                        txtNavigateUrl.Text = sysMenu.NavigateUrl;
                        txtIcon.Text = sysMenu.Icon;
                        cbExpanded.Checked = sysMenu.Expanded;
                        txtSortNo.Text = sysMenu.SortNo.ToString();
                    }

                    pEdit.Title = GetGlobalResourceObject(strClassName, "Update").ToString();
                    pEdit.Hidden = false;
                }
                else
                    Alert.Show(GetGlobalResourceObject(strClassName, "SelectMenuNode").ToString(), GetGlobalResourceObject("Language", "Warning").ToString(), MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                BLL.Global.WriteLog(ex, this.ClientTarget, Util.LogType.Exception.ToString(), new System.Diagnostics.StackTrace(true).GetFrame(0).GetMethod().ToString(), Util.Util.UserName, Environment.MachineName, "Line:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileLineNumber().ToString() + " Column:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileColumnNumber().ToString());
                Alert.ShowInTop(ex.Message, GetGlobalResourceObject("Language", "Error").ToString(), MessageBoxIcon.Error);
            }
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (!tData.SelectedNode.Leaf)
                {
                    Alert.Show(GetGlobalResourceObject(strClassName, "DeleteFromLeaf").ToString(), GetGlobalResourceObject("Language", "Warning").ToString(), MessageBoxIcon.Warning);
                    return;
                }
                if (tData.SelectedNode != null)
                {
                    BLL.Auth.SysMenu m_SysMenu = new BLL.Auth.SysMenu();
                    if (m_SysMenu.Delete(Convert.ToInt32(tData.SelectedNode.NodeID), Util.Util.UserName))
                    {
                        LoadMenu(cmbESystemCode.SelectedValue);
                        Alert.Show(GetGlobalResourceObject(strClassName, "DeleteSuccessfully").ToString(), GetGlobalResourceObject("Language", "Information").ToString(), MessageBoxIcon.Information);
                    }
                    else
                        Alert.Show(GetGlobalResourceObject(strClassName, "DeleteSuccessfully").ToString(), GetGlobalResourceObject("Language", "Error").ToString(), MessageBoxIcon.Error);

                }
                else
                    Alert.Show(GetGlobalResourceObject(strClassName, "SelectMenuNode").ToString(), GetGlobalResourceObject("Language", "Warning").ToString(), MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                BLL.Global.WriteLog(ex, this.ClientTarget, Util.LogType.Exception.ToString(), new System.Diagnostics.StackTrace(true).GetFrame(0).GetMethod().ToString(), Util.Util.UserName, Environment.MachineName, "Line:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileLineNumber().ToString() + " Column:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileColumnNumber().ToString());
                Alert.ShowInTop(ex.Message, GetGlobalResourceObject("Language", "Error").ToString(), MessageBoxIcon.Error);
            }
        }
        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                BLL.Auth.SysMenu m_SysMenu = new BLL.Auth.SysMenu();
                if (pEdit.Title == GetGlobalResourceObject(strClassName, "Add").ToString())
                {
                    if (m_SysMenu.IsExist(cmbESystemCode.SelectedValue, txtNodeCode.Text.Trim()))
                    {
                        Alert.Show(GetGlobalResourceObject(strClassName, "NodeExist").ToString(), GetGlobalResourceObject("Language", "Warning").ToString(), MessageBoxIcon.Warning);
                        return;
                    }
                    if (tData.SelectedNode != null)
                    {
                        if (m_SysMenu.IsExist(cmbESystemCode.SelectedValue,Convert.ToInt32(tData.SelectedNode.NodeID), txtMenuName.Text.Trim()))
                        {
                            Alert.Show(GetGlobalResourceObject(strClassName, "MenuNameExist").ToString(), GetGlobalResourceObject("Language", "Warning").ToString(), MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    Model.Auth.SysMenu sysMenu = new Model.Auth.SysMenu();
                    sysMenu.SystemCode = cmbESystemCode.SelectedValue;
                    sysMenu.NodeCode = txtNodeCode.Text.Trim();
                    sysMenu.MenuName = txtMenuName.Text.Trim();
                    sysMenu.MenuNameEn = txtMenuNameEn.Text.Trim();
                    sysMenu.MenuNameTh = txtMenuNameTh.Text.Trim();
                    if (tData.Nodes.Count > 0)
                    {
                        Model.Auth.SysMenu sysMenu1 = m_SysMenu.Select(Convert.ToInt64(tData.SelectedNode.NodeID));
                        if (AddIsSubNode)
                            sysMenu.ParentId = sysMenu1.Id;
                        else
                            sysMenu.ParentId = 0;
                    }
                    else
                        sysMenu.ParentId = 0;
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
                        Alert.Show(GetGlobalResourceObject(strClassName, "SaveFailed").ToString(), GetGlobalResourceObject("Language", "Error").ToString(), MessageBoxIcon.Error);
                }
                else if (pEdit.Title == GetGlobalResourceObject(strClassName, "Update").ToString() && tData.SelectedNode != null)
                {
                    Model.Auth.SysMenu sysMenu = m_SysMenu.Select(Convert.ToInt64(tData.SelectedNode.NodeID));
                    if (sysMenu != null && sysMenu.NodeCode != txtNodeCode.Text.Trim())
                    {
                        if (m_SysMenu.IsExist(cmbESystemCode.SelectedValue,txtNodeCode.Text.Trim()))
                        {
                            Alert.Show(GetGlobalResourceObject(strClassName, "NodeExist").ToString(), GetGlobalResourceObject("Language", "Warning").ToString(), MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    if (sysMenu.MenuName != txtMenuName.Text.Trim())
                    {
                        if (m_SysMenu.IsExist(cmbESystemCode.SelectedValue,Convert.ToInt32(tData.SelectedNode.NodeID),txtMenuName.Text.Trim()))
                        {
                            Alert.Show(GetGlobalResourceObject(strClassName, "MenuNameExist").ToString(), GetGlobalResourceObject("Language", "Warning").ToString(), MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    sysMenu.SystemCode = cmbESystemCode.SelectedValue;
                    sysMenu.NodeCode = txtNodeCode.Text.Trim();
                    sysMenu.MenuName = txtMenuName.Text.Trim();
                    sysMenu.MenuNameEn = txtMenuNameEn.Text.Trim();
                    sysMenu.MenuNameTh = txtMenuNameTh.Text.Trim();
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
                        Alert.Show(GetGlobalResourceObject(strClassName, "SaveFailed").ToString(), GetGlobalResourceObject("Language", "Error").ToString(), MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                BLL.Global.WriteLog(ex, this.ClientTarget, Util.LogType.Exception.ToString(), new System.Diagnostics.StackTrace(true).GetFrame(0).GetMethod().ToString(), Util.Util.UserName, Environment.MachineName, "Line:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileLineNumber().ToString() + " Column:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileColumnNumber().ToString());
                Alert.ShowInTop(ex.Message, GetGlobalResourceObject("Language", "Error").ToString(), MessageBoxIcon.Error);
            }
        }
        protected void tData_NodeCommand(object sender, TreeCommandEventArgs e)
        {
            try
            {
                if(tData.SelectedNode!=null && tData.SelectedNode.Leaf)
                {
                    BLL.PMS.Element m_Element = new BLL.PMS.Element();
                    dgvData.DataSource = m_Element.SearchBySysMenuId(Convert.ToInt64(e.NodeID));
                    dgvData.DataBind();
                }
            }
            catch (Exception ex)
            {
                BLL.Global.WriteLog(ex, this.ClientTarget, Util.LogType.Exception.ToString(), new System.Diagnostics.StackTrace(true).GetFrame(0).GetMethod().ToString(), Util.Util.UserName, Environment.MachineName, "Line:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileLineNumber().ToString() + " Column:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileColumnNumber().ToString());
                Alert.ShowInTop(ex.Message, GetGlobalResourceObject("Language", "Error").ToString(), MessageBoxIcon.Error);
            }
        }
        protected void dgvData_RowCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                try
                {
                    if (e.RowIndex >= 0)
                    {
                        BLL.PMS.Element m_Element = new BLL.PMS.Element();
                        if (m_Element.Delete(Convert.ToInt64(dgvData.DataKeys[e.RowIndex][0])))
                        {
                            LoadElement(Convert.ToInt64(tData.SelectedNodeID));
                            Alert.Show(GetGlobalResourceObject(strClassName, "DeleteSuccessfully").ToString(), GetGlobalResourceObject("Language", "Information").ToString(), MessageBoxIcon.Information);
                        }
                        else
                            Alert.Show(GetGlobalResourceObject(strClassName, "DeleteFailed").ToString(), GetGlobalResourceObject("Language", "Error").ToString(), MessageBoxIcon.Error);
                    }
                    else
                        Alert.Show(GetGlobalResourceObject(strClassName, "SaveDataFirst").ToString(), GetGlobalResourceObject("Language", "Warning").ToString(), MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    BLL.Global.WriteLog(ex, this.ClientTarget, Util.LogType.Exception.ToString(), new System.Diagnostics.StackTrace(true).GetFrame(0).GetMethod().ToString(), Util.Util.UserName, Environment.MachineName, "Line:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileLineNumber().ToString() + " Column:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileColumnNumber().ToString());
                    Alert.ShowInTop(ex.Message, GetGlobalResourceObject("Language", "Error").ToString(), MessageBoxIcon.Error);
                }
            }
        }
        protected void dgvData_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {

        }
        protected void dgvData_Sort(object sender, GridSortEventArgs e)
        {
            string sortField = dgvData.SortField;
            string sortDirection = dgvData.SortDirection;

            DataView dv = dtDataSource.DefaultView;
            if (!String.IsNullOrEmpty(sortField))
            {
                dv.Sort = String.Format("{0} {1}", sortField, sortDirection);
            }

            dgvData.DataSource = dv;
            dgvData.DataBind();
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            wAccess.Title = GetGlobalResourceObject(strClassName, "Add").ToString();
            wAccess.Hidden = false;
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCode.Text.Trim().Length > 0 && txtName.Text.Trim().Length > 0)
                {
                    BLL.PMS.Element m_Element = new BLL.PMS.Element();
                    Model.PMS.Element element = new Model.PMS.Element();
                    element.SysMenuId = Convert.ToInt64(tData.SelectedNodeID);
                    element.Code = txtCode.Text.Trim();
                    element.Name = txtName.Text.Trim();
                    if (m_Element.Add(element))
                    {
                        LoadElement(Convert.ToInt64(tData.SelectedNodeID));
                    }
                }
            }
            catch (Exception ex)
            {
                BLL.Global.WriteLog(ex, this.ClientTarget, Util.LogType.Exception.ToString(), new System.Diagnostics.StackTrace(true).GetFrame(0).GetMethod().ToString(), Util.Util.UserName, Environment.MachineName, "Line:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileLineNumber().ToString() + " Column:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileColumnNumber().ToString());
                Alert.ShowInTop(ex.Message, GetGlobalResourceObject("Language", "Error").ToString(), MessageBoxIcon.Error);
            }
        }
    }
}