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
    public partial class frmTenement : System.Web.UI.Page
    {
        private static DataTable dtDataSource;
        private void LoadData(string m_TenementName)
        {
            try
            {
                BLL.ApplyMaterial.ApplyMaterial m_Tenement = new BLL.ApplyMaterial.ApplyMaterial();
                cmbETenementName.DataTextField = "TenementName";
                cmbETenementName.DataValueField = "TenementCode";
                cmbETenementName.DataSource = m_Tenement.Select("", "");
                cmbETenementName.DataBind();

                dtDataSource = m_Tenement.Select("", m_TenementName);
                dgvData.DataSource = dtDataSource;
                dgvData.DataBind();

                dgvAccount.DataSource = null;
                dgvAccount.DataBind();
            }
            catch (Exception ex)
            {
                BLL.Global.WriteLog(ex, this.ClientTarget, Util.LogType.Exception.ToString(), new System.Diagnostics.StackTrace(true).GetFrame(0).GetMethod().ToString(), Util.Util.UserName, Environment.MachineName, "Line:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileLineNumber().ToString() + " Column:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileColumnNumber().ToString());
                Alert.Show(ex.Message, "错误", MessageBoxIcon.Error);
            }
        }
        private void LoadAccount(string m_TenementCode)
        {
            try
            {
                BLL.ApplyMaterial.TenementAccount m_TenementAccount = new BLL.ApplyMaterial.TenementAccount();
                dgvAccount.DataSource = m_TenementAccount.Select(m_TenementCode, "");
                dgvAccount.DataBind();
            }
            catch (Exception ex)
            {
                BLL.Global.WriteLog(ex, this.ClientTarget, Util.LogType.Exception.ToString(), new System.Diagnostics.StackTrace(true).GetFrame(0).GetMethod().ToString(), Util.Util.UserName, Environment.MachineName, "Line:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileLineNumber().ToString() + " Column:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileColumnNumber().ToString());
                Alert.Show(ex.Message, "错误", MessageBoxIcon.Error);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData("");
                btnConfirmAccount.OnClientClick = pAccount.GetHideReference();
                btnSave.OnClientClick = wAuthConfig.GetHideReference();
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData(cmbETenementName.SelectedText);
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            cmbETenementName.SelectedIndex = -1;
        }
        protected void dgvData_RowSelect(object sender, GridRowSelectEventArgs e)
        {
            if (dgvData.SelectedRow != null)
                LoadAccount(dgvData.SelectedRow.Values[2].ToString());
            else
            {
                dgvAccount.DataSource = null;
                dgvAccount.DataBind();
            }
        }
        private void Clear()
        {
            txtTenementCode.Text = "";
            txtTenementName.Text = "";
            txtContact.Text = "";
            txtMobilePhone.Text = "";
            txtTelephone.Text = "";
            txtEmail.Text = "";
            txtAddress.Text = "";
            txtPrice.Text = "0";
            cmbUnit.SelectedIndex = -1;
            txtRemark.Text = "";
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Clear();
            pEdit.Title = "新增";
            pEdit.Hidden = false;
        }
        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                if (pEdit.Title == "新增")
                {
                    BLL.ApplyMaterial.ApplyMaterial m_Tenement = new BLL.ApplyMaterial.ApplyMaterial();
                    if (m_Tenement.IsExist(txtTenementName.Text.Trim()))
                    {
                        Alert.Show("该租户已经存在", "警告", MessageBoxIcon.Warning);
                        return;
                    }

                    Model.ApplyMaterial.ApplyMaterial tenement = new Model.ApplyMaterial.ApplyMaterial();
                    tenement.TenementCode = txtTenementCode.Text.Trim();
                    tenement.TenementName = txtTenementName.Text.Trim();
                    tenement.Contact = txtContact.Text.Trim();
                    tenement.MobilePhone = txtMobilePhone.Text.Trim();
                    tenement.Telephone = txtTelephone.Text.Trim();
                    tenement.Email = txtEmail.Text.Trim();
                    tenement.Address = txtAddress.Text.Trim();
                    tenement.Price = Convert.ToDouble(txtPrice.Text);
                    tenement.Unit = cmbUnit.SelectedValue;
                    tenement.IsDeleted = false;
                    tenement.CreateUserID = Util.Util.UserName;
                    tenement.LastUpdateUserID = Util.Util.UserName;
                    tenement.Remark = txtRemark.Text;
                    if (m_Tenement.Add(tenement))
                    {
                        LoadData("");
                        Clear();
                        pEdit.Hidden = true;
                    }
                    else
                        Alert.Show("新增失败", "错误", MessageBoxIcon.Error);
                }
                else if (pEdit.Title == "修改" && dgvData.SelectedRow!=null)
                {
                    BLL.ApplyMaterial.ApplyMaterial m_Tenement = new BLL.ApplyMaterial.ApplyMaterial();
                    if (txtTenementCode.Text.Trim().ToUpper() != dgvData.SelectedRow.Values[2].ToString().ToUpper())
                    {
                        if (m_Tenement.IsExist(txtTenementName.Text.Trim()))
                        {
                            Alert.Show("该租户已经存在", "警告", MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    Model.ApplyMaterial.ApplyMaterial tenement = m_Tenement.Select(Convert.ToInt64(dgvData.DataKeys[dgvData.SelectedRow.RowIndex][0]));
                    tenement.TenementCode = txtTenementCode.Text.Trim();
                    tenement.TenementName = txtTenementName.Text.Trim();
                    tenement.Contact = txtContact.Text.Trim();
                    tenement.MobilePhone = txtMobilePhone.Text.Trim();
                    tenement.Telephone = txtTelephone.Text.Trim();
                    tenement.Email = txtEmail.Text.Trim();
                    tenement.Address = txtAddress.Text.Trim();
                    tenement.Price = Convert.ToDouble(txtPrice.Text);
                    tenement.Unit = cmbUnit.SelectedValue;
                    tenement.LastUpdateUserID = Util.Util.UserName;
                    tenement.Remark = txtRemark.Text.Trim();
                    if (m_Tenement.Update(tenement))
                    {
                        LoadData("");
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
        protected void dgvData_RowCommand(object sender, FineUI.GridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                try
                {
                    if (e.RowIndex>=0)
                    {
                        BLL.ApplyMaterial.ApplyMaterial m_Tenement = new BLL.ApplyMaterial.ApplyMaterial();
                        if (m_Tenement.Delete(Convert.ToInt64(dgvData.DataKeys[e.RowIndex][0]), Util.Util.UserName))
                        {
                            LoadData("");
                            Alert.Show("删除成功", "提示", MessageBoxIcon.Information);
                        }
                        else
                            Alert.Show("删除失败", "错误", MessageBoxIcon.Error);
                    }
                    else
                        Alert.Show("请先保存数据", "警告", MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    BLL.Global.WriteLog(ex, this.ClientTarget, Util.LogType.Exception.ToString(), new System.Diagnostics.StackTrace(true).GetFrame(0).GetMethod().ToString(), Util.Util.UserName, Environment.MachineName, "Line:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileLineNumber().ToString() + " Column:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileColumnNumber().ToString());
                    Alert.Show(ex.Message, "错误", MessageBoxIcon.Error);
                }
            }
        }
        protected void dgvData_RowDoubleClick(object sender, FineUI.GridRowClickEventArgs e)
        {
            Clear();

            txtTenementCode.Text = dgvData.Rows[e.RowIndex].Values[2].ToString();
            txtTenementName.Text = dgvData.Rows[e.RowIndex].Values[3].ToString();
            txtContact.Text = dgvData.Rows[e.RowIndex].Values[4].ToString();
            txtMobilePhone.Text = dgvData.Rows[e.RowIndex].Values[5].ToString();
            txtTelephone.Text = dgvData.Rows[e.RowIndex].Values[6].ToString();
            txtEmail.Text = dgvData.Rows[e.RowIndex].Values[7].ToString();
            txtAddress.Text = dgvData.Rows[e.RowIndex].Values[8].ToString();
            txtPrice.Text = dgvData.Rows[e.RowIndex].Values[9].ToString();
            cmbUnit.SelectedValue = dgvData.Rows[e.RowIndex].Values[10].ToString();
            txtRemark.Text = dgvData.Rows[e.RowIndex].Values[11].ToString();

            pEdit.Title = "修改";
            pEdit.Hidden = false;
        }
        protected void btnAddAccount_Click(object sender, EventArgs e)
        {
            if (dgvData.SelectedRow != null)
            {
                txtUserName.Text = "";
                txtPassword.Text = "";
                txtConfirmPassword.Text = "";
                txtConnectionString.Text = "";
                pAccount.Hidden = false;
            }
            else
                Alert.Show("请先选择租户信息", "警告", MessageBoxIcon.Warning);
        }
        protected void dgvAccount_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            try
            {
                if (dgvData.SelectedRow != null)
                {
                    txtUserName.Text = dgvAccount.SelectedRow.Values[1].ToString();
                    BLL.ApplyMaterial.TenementAccount m_TenementAccount = new BLL.ApplyMaterial.TenementAccount();
                    DataTable dtTenementAccount = m_TenementAccount.Select(dgvData.SelectedRow.Values[2].ToString(), dgvAccount.SelectedRow.Values[1].ToString());
                    if (dtTenementAccount != null && dtTenementAccount.Rows.Count > 0)
                    {
                        txtPassword.Text = dtTenementAccount.Rows[0]["Password"].ToString();
                        txtConfirmPassword.Text = txtPassword.Text;
                        txtConnectionString.Text = dtTenementAccount.Rows[0]["ConnectionString"].ToString();
                    }
                    pAccount.Hidden = false;
                }
            }
            catch (Exception ex)
            {
                BLL.Global.WriteLog(ex, this.ClientTarget, Util.LogType.Exception.ToString(), new System.Diagnostics.StackTrace(true).GetFrame(0).GetMethod().ToString(), Util.Util.UserName, Environment.MachineName, "Line:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileLineNumber().ToString() + " Column:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileColumnNumber().ToString());
                Alert.Show(ex.Message, "错误", MessageBoxIcon.Error);
            }
        }
        protected void btnConfirmAccount_Click(object sender, EventArgs e)
        {
            try
            {
                if(dgvData.SelectedRow!=null)
                {
                    if (pAccount.Title == "新增")
                    {
                        BLL.ApplyMaterial.TenementAccount m_TenementAccount = new BLL.ApplyMaterial.TenementAccount();
                        if (m_TenementAccount.IsExist(dgvData.SelectedRow.Values[2].ToString(), txtUserName.Text.Trim()))
                        {
                            Alert.Show("该租户已经存在", "警告", MessageBoxIcon.Warning);
                            return;
                        }

                        Model.ApplyMaterial.TenementAccount tenementAccount = new Model.ApplyMaterial.TenementAccount();
                        tenementAccount.TenementCode = dgvData.SelectedRow.Values[2].ToString();
                        tenementAccount.UserName = txtUserName.Text.Trim();
                        tenementAccount.Password = txtPassword.Text.Trim();
                        tenementAccount.ConnectionString = txtConnectionString.Text.Trim();
                        if (m_TenementAccount.Add(tenementAccount))
                        {
                            LoadAccount(dgvData.SelectedRow.Values[2].ToString());
                        }
                    }
                    else if (pAccount.Title == "修改" && dgvAccount.SelectedRow!=null)
                    {
                        BLL.ApplyMaterial.TenementAccount m_TenementAccount = new BLL.ApplyMaterial.TenementAccount();
                        if (txtUserName.Text.Trim().ToUpper() != dgvAccount.SelectedRow.Values[1].ToString().ToUpper())
                        {
                            if (m_TenementAccount.IsExist(dgvData.SelectedRow.Values[2].ToString(), txtUserName.Text.Trim()))
                            {
                                Alert.Show("该租户已经存在", "警告", MessageBoxIcon.Warning);
                                return;
                            }
                        }
                        Model.ApplyMaterial.TenementAccount tenementAccount = new Model.ApplyMaterial.TenementAccount();
                        tenementAccount.TenementCode = dgvData.SelectedRow.Values[2].ToString();
                        tenementAccount.UserName = txtUserName.Text.Trim();
                        tenementAccount.Password = txtPassword.Text.Trim();
                        tenementAccount.ConnectionString = txtConnectionString.Text.Trim();
                        if (m_TenementAccount.Add(tenementAccount))
                        {
                            LoadAccount(dgvData.SelectedRow.Values[2].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                BLL.Global.WriteLog(ex, this.ClientTarget, Util.LogType.Exception.ToString(), new System.Diagnostics.StackTrace(true).GetFrame(0).GetMethod().ToString(), Util.Util.UserName, Environment.MachineName, "Line:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileLineNumber().ToString() + " Column:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileColumnNumber().ToString());
                Alert.Show(ex.Message, "错误", MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// 加载树
        /// </summary>
        /// <param name="tvData">TreeView</param>
        /// <param name="m_DataSource">加载的数据源</param>
        /// <param name="m_ParentId">父节点id</param>
        /// <param name="m_ParentName">父节点字段名称</param>
        /// <param name="m_Node">当前节点</param>
        /// <param name="m_Text">当前节点的Text字段名</param>
        /// <param name="m_Tag">当前节点的Tag字段名</param>
        /// <param name="m_ToolTipText">当前节点的ToolTipText字段名</param>
        private void AddTree(FineUI.Tree tvData, DataTable m_DataSource, string m_ParentId, string m_ParentName,long m_ModuleId, FineUI.TreeNode m_Node, string m_Text, string m_Tag, string m_ToolTipText)
        {
            if (m_DataSource != null && m_DataSource.Rows.Count > 0)
            {
                DataView dvTree = new DataView(m_DataSource);
                if (!String.IsNullOrEmpty(m_ParentName))
                {
                    if (m_ParentId == "SYSTEM")
                        dvTree.RowFilter = m_ParentName + "='" + m_ParentId.ToString() + "'";
                    else
                        dvTree.RowFilter = m_ParentName + "='" + m_ParentId.ToString() + "' And SystemId=" + m_ModuleId.ToString();
                }
                foreach (DataRowView row in dvTree)
                {
                    FineUI.TreeNode node = new FineUI.TreeNode();
                    node.Text = row[m_Text].ToString();
                    if(row["ElementType"].ToString()=="SYSTEM")
                        node.NodeID = row[m_Tag].ToString();
                    else if (row["ElementType"].ToString() == "MODULE")
                        node.NodeID = row["ModuleId"].ToString();
                    node.ToolTip = row[m_ToolTipText].ToString();
                    node.EnableCheckBox = true;
                    node.EnableCheckEvent = true;
                    node.EnableClickEvent = true;
                    if (m_Node == null)
                        tvData.Nodes.Add(node);
                    else
                        m_Node.Nodes.Add(node);
                    AddTree(tvData, m_DataSource, "MODULE", m_ParentName, Convert.ToInt64(row["Id"].ToString()), node, m_Text, m_Tag, m_ToolTipText);
                }
            }
        }
        private void SetTreeViewParentCheckedValue(FineUI.Tree tvData, FineUI.TreeNode m_Node)
        {
            if (m_Node.Checked)
            {
                while (m_Node.ParentNode != null)
                {
                    m_Node = m_Node.ParentNode;
                    m_Node.Checked = true;
                }
            }
            else if (m_Node.ParentNode != null)
            {
                int intCount = 0;
                for (int i = 0; i < m_Node.ParentNode.Nodes.Count; i++)
                {
                    if (!m_Node.ParentNode.Nodes[i].Checked)
                        intCount++;
                }
                if (intCount == m_Node.ParentNode.Nodes.Count)
                    m_Node.ParentNode.Checked = false;
            }
        }
        private void SetCheckedNode(FineUI.Tree tvData, FineUI.TreeNodeCollection tnc, string m_ElementName)
        {
            foreach (FineUI.TreeNode node in tnc)
            {
                if (node.Text == m_ElementName)
                {
                    node.Checked = true;
                    SetTreeViewParentCheckedValue(tData, node);
                }
                else
                    SetCheckedNode(tvData, node.Nodes, m_ElementName);
            }
        }
        protected void btnGrant_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvAccount.SelectedRow != null)
                {
                    BLL.ApplyMaterial.System m_System = new BLL.ApplyMaterial.System();
                    cmbSystemName.DataTextField = "SystemName";
                    cmbSystemName.DataValueField = "id";
                    cmbSystemName.DataSource = m_System.Select();
                    cmbSystemName.DataBind();
                    tData.Nodes.Clear();
                    wGrant.Hidden = false;
                }
                else
                    Alert.Show("请先选择账号", "警告", MessageBoxIcon.Warning);
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
                    if (row.IsNull("ParentId"))
                    {
                        FineUI.TreeNode node = new FineUI.TreeNode();
                        node.NodeID = row["Id"].ToString();
                        node.Text = row["MenuName"].ToString();
                        //node.NavigateUrl = row["NavigateUrl"].ToString();
                        node.IconUrl = row["Icon"].ToString();
                        node.Expanded = Convert.ToBoolean(row["Expanded"]);
                        node.EnableCheckBox = true;
                        node.EnableCheckEvent = true;
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
                    FineUI.TreeNode node = new FineUI.TreeNode();
                    node.NodeID = row["Id"].ToString();
                    node.Text = row["MenuName"].ToString();
                    //node.NavigateUrl = row["NavigateUrl"].ToString();
                    node.IconUrl = row["Icon"].ToString();
                    node.Expanded = Convert.ToBoolean(row["Expanded"]);
                    node.EnableCheckBox = true;
                    node.EnableCheckEvent = true;
                    treeNode.Nodes.Add(node);

                    ResolveSubTree(row, node);
                }
            }
        }
        protected void cmbSystemName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbSystemName.SelectedText != null)
                {
                    BLL.ApplyMaterial.System m_System = new BLL.ApplyMaterial.System();
                    Model.ApplyMaterial.System system = m_System.Select(Convert.ToInt64(cmbSystemName.SelectedValue));
                    if (system != null)
                    {
                        LoadMenu(system.SystemCode);

                        BLL.ApplyMaterial.TenentServices m_TenentServices = new BLL.ApplyMaterial.TenentServices();
                        DataTable dtTenentServices = m_TenentServices.Select(Util.Util.TenementCode, system.SystemCode);
                        if (dtTenentServices != null && dtTenentServices.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtTenentServices.Rows.Count; i++)
                                SetCheckedNode(tData, tData.Nodes, dtTenentServices.Rows[i]["MenuName"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                BLL.Global.WriteLog(ex, this.ClientTarget, Util.LogType.Exception.ToString(), new System.Diagnostics.StackTrace(true).GetFrame(0).GetMethod().ToString(), Util.Util.UserName, Environment.MachineName, "Line:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileLineNumber().ToString() + " Column:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileColumnNumber().ToString());
                Alert.Show(ex.Message, "错误", MessageBoxIcon.Error);
            }
        }
        protected void tData_NodeCheck(object sender, TreeCheckEventArgs e)
        {
            try
            {
                BLL.ApplyMaterial.TenentServices m_TenentServices = new BLL.ApplyMaterial.TenentServices();
                if (!m_TenentServices.SetRelation(Util.Util.TenementCode, Convert.ToInt64(e.NodeID), e.Checked, Util.Util.UserName, ""))
                    Alert.Show("保存失败", "错误", MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                BLL.Global.WriteLog(ex, this.ClientTarget, Util.LogType.Exception.ToString(), new System.Diagnostics.StackTrace(true).GetFrame(0).GetMethod().ToString(), Util.Util.UserName, Environment.MachineName, "Line:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileLineNumber().ToString() + " Column:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileColumnNumber().ToString());
                Alert.Show(ex.Message, "错误", MessageBoxIcon.Error);
            }
        }
        protected void btnUpdatePrice_Click(object sender, EventArgs e)
        {
            try
            {
                if (tData.SelectedNode != null && tData.SelectedNode.Leaf && tData.SelectedNode.Checked)
                {
                    BLL.ApplyMaterial.TenentServices m_TenentServices = new BLL.ApplyMaterial.TenentServices();
                    Model.ApplyMaterial.TenentServices tenentServices = m_TenentServices.SelectBySysMenuId(Convert.ToInt32(tData.SelectedNodeID));
                    if (tenentServices != null)
                    {
                        if (tenentServices.StartDate != null)
                            dtpStartDate.SelectedDate = tenentServices.StartDate;
                        if (tenentServices.EndDate != null)
                            dtpEndDate.SelectedDate = tenentServices.EndDate;
                        txtDPrice.Text = tenentServices.Price.ToString();
                        cmbDUnit.SelectedValue = tenentServices.Unit;
                        wAuthConfig.Hidden = false;
                    }
                }
            }
            catch (Exception ex)
            {
                BLL.Global.WriteLog(ex, this.ClientTarget, Util.LogType.Exception.ToString(), new System.Diagnostics.StackTrace(true).GetFrame(0).GetMethod().ToString(), Util.Util.UserName, Environment.MachineName, "Line:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileLineNumber().ToString() + " Column:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileColumnNumber().ToString());
                Alert.Show(ex.Message, "错误", MessageBoxIcon.Error);
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (tData.SelectedNode != null)
                {
                    if (dtpStartDate.SelectedDate.Value > dtpEndDate.SelectedDate.Value)
                    {
                        Alert.Show("租期开始时间必须小于等于租期结束时间", "警告", MessageBoxIcon.Warning);
                        return;
                    }
                    BLL.ApplyMaterial.TenentServices m_TenentServices = new BLL.ApplyMaterial.TenentServices();
                    Model.ApplyMaterial.TenentServices tenentServices = m_TenentServices.SelectBySysMenuId(Convert.ToInt32(tData.SelectedNode.NodeID));
                    if (tenentServices != null)
                    {
                        tenentServices.StartDate = dtpStartDate.SelectedDate.Value;
                        tenentServices.EndDate = dtpEndDate.SelectedDate.Value;
                        tenentServices.Price = Convert.ToDouble(txtDPrice.Text);
                        tenentServices.Unit = cmbDUnit.SelectedValue;
                        m_TenentServices.Update(tenentServices);
                    }
                }
            }
            catch (Exception ex)
            {
                BLL.Global.WriteLog(ex, this.ClientTarget, Util.LogType.Exception.ToString(), new System.Diagnostics.StackTrace(true).GetFrame(0).GetMethod().ToString(), Util.Util.UserName, Environment.MachineName, "Line:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileLineNumber().ToString() + " Column:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileColumnNumber().ToString());
                Alert.Show(ex.Message, "错误", MessageBoxIcon.Error);
            }
        }
        protected void dgvAccount_RowCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                try
                {
                    if (e.RowIndex>=0)
                    {
                        BLL.ApplyMaterial.TenementAccount m_TenementAccount = new BLL.ApplyMaterial.TenementAccount();
                        if (m_TenementAccount.Delete(Util.Util.TenementCode,dgvAccount.Rows[e.RowIndex].Values[1].ToString()))
                        {
                            LoadAccount(dgvData.SelectedRow.Values[2].ToString());
                            Alert.Show("删除成功", "提示", MessageBoxIcon.Information);
                        }
                        else
                            Alert.Show("删除失败", "错误", MessageBoxIcon.Error);
                    }
                    else
                        Alert.Show("请先保存数据", "警告", MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    BLL.Global.WriteLog(ex, this.ClientTarget, Util.LogType.Exception.ToString(), new System.Diagnostics.StackTrace(true).GetFrame(0).GetMethod().ToString(), Util.Util.UserName, Environment.MachineName, "Line:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileLineNumber().ToString() + " Column:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileColumnNumber().ToString());
                    Alert.Show(ex.Message, "错误", MessageBoxIcon.Error);
                }
            }
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
    }
}