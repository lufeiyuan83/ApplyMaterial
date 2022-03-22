using FineUI;
using System;
using System.Collections.Generic;
using System.Data;

namespace ApplyMaterial.CodeRule
{
    public partial class CodeRule : System.Web.UI.Page
    {
        private static DataTable dtData;
        private static bool AddIsSubNode = true;
        private void LoadMenu(long m_CodeRuleID,string m_Name)
        {
            try
            {
                tData.Nodes.Clear();
                BLL.CodeRule.CodeRuleParameter m_CodeRuleParameter = new BLL.CodeRule.CodeRuleParameter();
                DataTable dtCodeRule = m_CodeRuleParameter.Search(m_CodeRuleID);
                if (dtCodeRule != null && dtCodeRule.Rows.Count > 0)
                {
                    DataSet ds = new DataSet();
                    ds.Tables.Add(dtCodeRule);
                    ds.Relations.Add("TreeRelation", ds.Tables[0].Columns["Id"], ds.Tables[0].Columns["ParentId"]);
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        if (row["Id"].ToString() == row["ParentId"].ToString())
                        {
                            FineUI.TreeNode node = new FineUI.TreeNode();
                            node.NodeID = row["Id"].ToString();
                            node.Text = row["ParameterName"].ToString()+"("+ row["ParameterValue"].ToString() + ")";
                            if (Convert.ToBoolean(row["IsDefault"]))
                                node.Icon = Icon.Star;
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
                        node.Text = row["ParameterName"].ToString() + "(" + row["ParameterValue"].ToString() + ")";
                        if (Convert.ToBoolean(row["IsDefault"]))
                            node.Icon = Icon.Star;
                        node.EnableClickEvent = true;
                        treeNode.Nodes.Add(node);

                        ResolveSubTree(row, node);
                    }
                }
            }
        }
        private void LoadFixedRule()
        {
            try
            {                
                BLL.CodeRule.CodeRule m_CodeRule = new BLL.CodeRule.CodeRule();
                DataTable dtCodeRule = m_CodeRule.Select("FixedCodeRule");
                if (dtCodeRule != null && dtCodeRule.Rows.Count > 0)
                {
                    BLL.CodeRule.CodeRuleParameter m_CodeRuleParameter = new BLL.CodeRule.CodeRuleParameter();
                    cmbFixedRule.DataTextField = "ParameterName";
                    cmbFixedRule.DataValueField = "ParameterValue";
                    cmbFixedRule.DataSource= m_CodeRuleParameter.Search(Convert.ToInt64(dtCodeRule.Rows[0]["Id"]));
                    cmbFixedRule.DataBind();
                }
            }
            catch (Exception ex)
            {
                BLL.Global.WriteLog(ex, this.ClientTarget, Util.LogType.Exception.ToString(), new System.Diagnostics.StackTrace(true).GetFrame(0).GetMethod().ToString(), Util.Util.UserName, Environment.MachineName, "Line:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileLineNumber().ToString() + " Column:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileColumnNumber().ToString());
                Alert.Show(ex.Message, "错误", MessageBoxIcon.Error);
            }
        }
        private void LoadData()
        {
            try
            {
                BLL.CodeRule.CodeRule m_CodeRule = new BLL.CodeRule.CodeRule();
                dtData = m_CodeRule.Select();
                dgvData.DataSource = dtData;
                dgvData.DataBind();
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
                LoadData();
                LoadFixedRule();
                btnSaveRelateBusiness.OnClientClick = wRelateBusiness.GetHideReference();
                btnSaveRule.OnClientClick = wCodeRule.GetHideReference();
                btnConfirm.OnClientClick = wCodeNode.GetHideReference();
            }
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            wCodeRule.Hidden = false;
            wCodeRule.Title = "新增";
        }
        protected void btnRelate_Click(object sender, EventArgs e)
        {
            if (dgvData.SelectedRow != null)
            {
                txtSchema.Text = dgvData.SelectedRow.Values[6].ToString();
                txtTableName.Text = dgvData.SelectedRow.Values[7].ToString();
                txtField.Text = dgvData.SelectedRow.Values[8].ToString();
                wRelateBusiness.Hidden = false;
                wRelateBusiness.Title = "新增";
            }
        }
        protected void btnSaveRelateBusiness_Click(object sender, EventArgs e)
        {
            try
            {
                BLL.CodeRule.CodeRule m_CodeRule = new BLL.CodeRule.CodeRule();
                Model.CodeRule.CodeRule codeRule = m_CodeRule.Select(Convert.ToInt64(dgvData.DataKeys[dgvData.SelectedRowIndex][0]));
                codeRule.Schema = txtSchema.Text.Trim();
                codeRule.TableName = txtTableName.Text.Trim();
                codeRule.Field = txtField.Text.Trim();
                if (m_CodeRule.Update(codeRule))
                {
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                BLL.Global.WriteLog(ex, this.ClientTarget, Util.LogType.Exception.ToString(), new System.Diagnostics.StackTrace(true).GetFrame(0).GetMethod().ToString(), Util.Util.UserName, Environment.MachineName, "Line:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileLineNumber().ToString() + " Column:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileColumnNumber().ToString());
                Alert.Show(ex.Message, "错误", MessageBoxIcon.Error);
            }
        }
        protected void btnSaveRule_Click(object sender, EventArgs e)
        {
            try
            {
                if (wCodeRule.Title == "新增")
                {
                    BLL.CodeRule.CodeRule m_CodeRule = new BLL.CodeRule.CodeRule();
                    Model.CodeRule.CodeRule codeRule = new Model.CodeRule.CodeRule();
                    codeRule.Code = txtCode.Text.Trim();
                    codeRule.Name = txtCodeRule.Text.Trim();
                    codeRule.Rule = txtRule.Text.Trim();
                    codeRule.RuleEN = txtRuleEN.Text.Trim();
                    codeRule.Rev = Convert.ToInt32(nbRev.Text);
                    codeRule.IsActive = cbIsActive.Checked;
                    codeRule.CreateUserID = Util.Util.UserName;
                    codeRule.LastUpdateUserID = Util.Util.UserName;
                    codeRule.Remark = txtRemark.Text.Trim();

                    if (m_CodeRule.Add(codeRule))
                    {
                        LoadData();
                        Alert.Show("保存成功", "信息", MessageBoxIcon.Information);
                    }
                    else
                        Alert.Show("保存失败", "错误", MessageBoxIcon.Error);
                }
                else
                {
                    BLL.CodeRule.CodeRule m_CodeRule = new BLL.CodeRule.CodeRule();
                    Model.CodeRule.CodeRule codeRule = m_CodeRule.Select(Convert.ToInt64(dgvData.DataKeys[dgvData.SelectedRowIndex][0]));
                    codeRule.Code = txtCode.Text.Trim();
                    codeRule.Name = txtCodeRule.Text.Trim();
                    codeRule.Rule = txtRule.Text.Trim();
                    codeRule.RuleEN = txtRuleEN.Text.Trim();
                    codeRule.Rev = Convert.ToInt32(nbRev.Text);
                    codeRule.Schema = "";
                    codeRule.TableName = "";
                    codeRule.Field = "";
                    codeRule.IsActive = cbIsActive.Checked;
                    codeRule.LastUpdateUserID = Util.Util.UserName;
                    codeRule.Remark = txtRemark.Text.Trim();

                    if (m_CodeRule.Update(codeRule))
                    {
                        LoadData();
                        Alert.Show("保存成功", "信息", MessageBoxIcon.Information);
                    }
                    else
                        Alert.Show("保存失败", "错误", MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                BLL.Global.WriteLog(ex, this.ClientTarget, Util.LogType.Exception.ToString(), new System.Diagnostics.StackTrace(true).GetFrame(0).GetMethod().ToString(), Util.Util.UserName, Environment.MachineName, "Line:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileLineNumber().ToString() + " Column:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileColumnNumber().ToString());
                Alert.Show(ex.Message, "错误", MessageBoxIcon.Error);
            }
        }
        protected void dgvData_RowSelect(object sender, GridRowSelectEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                LoadMenu(Convert.ToInt64(dgvData.DataKeys[dgvData.SelectedRow.RowIndex][0]),dgvData.SelectedRow.Values[1].ToString());
                Toolbar1.Enabled = (dgvData.SelectedRow.Values[1].ToString() != "FixedCodeRule");
                Menu1.Enabled = Toolbar1.Enabled;
            }
            else
                tData.Nodes.Clear();
        }
        protected void dgvData_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            try
            {
                BLL.CodeRule.CodeRule m_CodeRule = new BLL.CodeRule.CodeRule();
                Model.CodeRule.CodeRule codeRule = m_CodeRule.Select(Convert.ToInt64(dgvData.DataKeys[e.RowIndex][0]));
                if (codeRule != null)
                {
                    txtCode.Text = codeRule.Code;
                    txtCodeRule.Text = codeRule.Name;
                    txtRule.Text = codeRule.Rule;
                    txtRuleEN.Text = codeRule.RuleEN;
                    nbRev.Text = codeRule.Rev.ToString();
                    cbIsActive.Checked = codeRule.IsActive;
                    txtRemark.Text = codeRule.Remark;

                    btnSaveRule.Enabled = ("FixedCodeRule" != txtCode.Text);
                    wCodeRule.Title = "修改";
                    wCodeRule.Hidden = false;
                }
            }
            catch (Exception ex)
            {
                BLL.Global.WriteLog(ex, this.ClientTarget, Util.LogType.Exception.ToString(), new System.Diagnostics.StackTrace(true).GetFrame(0).GetMethod().ToString(), Util.Util.UserName, Environment.MachineName, "Line:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileLineNumber().ToString() + " Column:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileColumnNumber().ToString());
                Alert.Show(ex.Message, "错误", MessageBoxIcon.Error);
            }
        }
        protected void dgvData_Sort(object sender, GridSortEventArgs e)
        {
            string sortField = dgvData.SortField;
            string sortDirection = dgvData.SortDirection;

            DataView dv = dtData.DefaultView;
            if (!String.IsNullOrEmpty(sortField))
            {
                dv.Sort = String.Format("{0} {1}", sortField, sortDirection);
            }

            dgvData.DataSource = dv;
            dgvData.DataBind();
        }
        private void Clear()
        {
            txtParameterCode.Text = "";
            txtParameterName.Text = "";
            txtParameterValue.Text = "";
            cmbParameterType.Text = "";
            txtParameterDescription.Text = "";
            cbIsDefault.Checked = false;
            txtSortNo.Text = "";
            txtGroupNo.Text = "";
        }
        private void LoadParameterType()
        {
            string strRuleEN = dgvData.SelectedRow.Values[3].ToString();
            string[] arrRuleEN1 = strRuleEN.Split('[');
            List<string> lst = new List<string>();
            for (int i = 0; i < arrRuleEN1.Length; i++)
            {
                if (arrRuleEN1[i].Trim() != "")
                {
                    string[] arrRuleEN2 = arrRuleEN1[i].Split(']');
                    for (int j = 0; j < arrRuleEN2.Length; j++)
                    {
                        if (arrRuleEN2[j].Trim() != "" && strRuleEN.Contains("[" + arrRuleEN2[j] + "]"))
                            lst.Add(arrRuleEN2[j]);
                    }
                }
            }
            cmbParameterType.DataSource = lst;
            cmbParameterType.DataBind();
        }
        protected void btnAddSiblingNode_Click(object sender, EventArgs e)
        {
            if (dgvData.SelectedRow == null)
                return;
            Clear();
            LoadParameterType();
            if (tData.Nodes.Count > 0)
            {
                long lngParentId = 0;
                BLL.CodeRule.CodeRuleParameter m_CodeRuleParameter = new BLL.CodeRule.CodeRuleParameter();
                if (tData.SelectedNode != null)
                {
                    Model.CodeRule.CodeRuleParameter codeRuleParameter = m_CodeRuleParameter.Select(Convert.ToInt64(tData.SelectedNode.NodeID));

                    if (codeRuleParameter != null)
                    {
                        lngParentId = codeRuleParameter.ParentId; 
                        if(cmbParameterType.Items.Count> codeRuleParameter.Level)
                            cmbParameterType.SelectedValue = cmbParameterType.Items[codeRuleParameter.Level].Value;
                    }

                }
                int intMaxSortNo = m_CodeRuleParameter.GetMaxSortNo(Convert.ToInt64(dgvData.DataKeys[dgvData.SelectedRow.RowIndex][0]), lngParentId, false, tData.SelectedNode == null ? true : (tData.SelectedNode.ParentNode == null));

                txtSortNo.Text = (intMaxSortNo + 1).ToString();
                txtGroupNo.Text = txtSortNo.Text;

                AddIsSubNode = false;
                wCodeNode.Title = "新增";
                wCodeNode.Hidden = false;
                btnConfirm.Enabled = (dgvData.SelectedRow.Values[1].ToString() != "FixedCodeRule");
            }
            else
            {
                txtSortNo.Text = "1";
                txtGroupNo.Text = "1";
                wCodeNode.Title = "新增";
                wCodeNode.Hidden = false;
            }
        }
        protected void btnAddSubNode_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvData.SelectedRow == null)
                    return;
                Clear();
                LoadParameterType();
                if (tData.Nodes.Count > 0)
                {
                    long lngParentId = 0;
                    BLL.CodeRule.CodeRuleParameter m_CodeRuleParameter = new BLL.CodeRule.CodeRuleParameter();
                    if (tData.SelectedNode != null)
                    {
                        Model.CodeRule.CodeRuleParameter codeRuleParameter = m_CodeRuleParameter.Select(Convert.ToInt64(tData.SelectedNode.NodeID));
                        if (codeRuleParameter != null)
                        {
                            lngParentId = codeRuleParameter.Id;
                            if (cmbParameterType.Items.Count > codeRuleParameter.Level + 1)
                                cmbParameterType.SelectedValue = cmbParameterType.Items[codeRuleParameter.Level+1].Value;
                            else
                            {
                                Alert.Show("您不能再增加子节点了，与编码规则不符", "警告", MessageBoxIcon.Warning);
                                return;
                            }
                        }
                    }
                    int intMaxSortNo = m_CodeRuleParameter.GetMaxSortNo(Convert.ToInt64(dgvData.DataKeys[dgvData.SelectedRow.RowIndex][0]), lngParentId, true, false);
                                        
                    txtSortNo.Text = (intMaxSortNo + 1).ToString();
                    txtGroupNo.Text = txtSortNo.Text;
                    AddIsSubNode = true;
                    wCodeNode.Title = "新增";
                    wCodeNode.Hidden = false;
                    btnConfirm.Enabled = (dgvData.SelectedRow.Values[1].ToString() != "FixedCodeRule");
                }
                else
                {
                    txtSortNo.Text = "1";
                    txtGroupNo.Text = "1";
                    wCodeNode.Title = "新增";
                    wCodeNode.Hidden = false;
                }
            }
            catch (Exception ex)
            {
                BLL.Global.WriteLog(ex, this.ClientTarget, Util.LogType.Exception.ToString(), new System.Diagnostics.StackTrace(true).GetFrame(0).GetMethod().ToString(), Util.Util.UserName, Environment.MachineName, "Line:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileLineNumber().ToString() + " Column:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileColumnNumber().ToString());
                Alert.Show(ex.Message, "错误", MessageBoxIcon.Error);
            }
        }
        protected void btnUpdateNode_Click(object sender, EventArgs e)
        {
            try
            {
                Clear();
                if (tData.SelectedNode != null)
                {
                    LoadParameterType();
                    BLL.CodeRule.CodeRuleParameter m_CodeRuleParameter = new BLL.CodeRule.CodeRuleParameter();
                    Model.CodeRule.CodeRuleParameter codeRuleParameter = m_CodeRuleParameter.Select(Convert.ToInt64(tData.SelectedNode.NodeID));
                    if (codeRuleParameter != null)
                    {
                        txtParameterCode.Text = codeRuleParameter.ParameterCode;
                        txtParameterName.Text = codeRuleParameter.ParameterName;
                        txtParameterValue.Text = codeRuleParameter.ParameterValue;
                        cmbParameterType.SelectedValue = codeRuleParameter.ParameterType;
                        txtParameterDescription.Text = codeRuleParameter.ParameterDescription;
                        cbIsDefault.Checked = codeRuleParameter.IsDefault;
                        txtSortNo.Text = codeRuleParameter.SortNo.ToString();
                        txtGroupNo.Text = codeRuleParameter.GroupNo.ToString();
                    }

                    wCodeNode.Title = "修改";
                    wCodeNode.Hidden = false;
                    btnConfirm.Enabled = (dgvData.SelectedRow.Values[1].ToString() != "FixedCodeRule");
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
        protected void btnDeleteNode_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvData.SelectedRow.Values[1].ToString() == "FixedCodeRule")
                {
                    Alert.Show("您不能删除内置编码规则", "警告", MessageBoxIcon.Warning);
                    return;
                }
                if (!tData.SelectedNode.Leaf)
                {
                    Alert.Show("请从叶子节点开始删除", "警告", MessageBoxIcon.Warning);
                    return;
                }
                if (tData.SelectedNode != null)
                {
                    BLL.CodeRule.CodeRuleParameter m_CodeRuleParameter = new BLL.CodeRule.CodeRuleParameter();
                    Model.CodeRule.CodeRuleParameter codeRuleParameter = m_CodeRuleParameter.Select(Convert.ToInt64(tData.SelectedNode.NodeID));
                    if (m_CodeRuleParameter.Delete(Convert.ToInt64(tData.SelectedNode.NodeID), Util.Util.UserName))
                    {
                        LoadMenu(Convert.ToInt64(dgvData.DataKeys[dgvData.SelectedRow.RowIndex][0]),dgvData.SelectedRow.Values[1].ToString());
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
                BLL.CodeRule.CodeRuleParameter m_CodeRuleParameter = new BLL.CodeRule.CodeRuleParameter();
                if (wCodeNode.Title == "新增")
                {
                    if (m_CodeRuleParameter.IsExist(Convert.ToInt64(dgvData.DataKeys[dgvData.SelectedRow.RowIndex][0]), txtParameterCode.Text.Trim()))
                    {
                        Alert.Show("该节点代码已经存在", "警告", MessageBoxIcon.Warning);
                        return;
                    }
                    if (tData.SelectedNode != null)
                    {
                        if (m_CodeRuleParameter.IsExist(Convert.ToInt64(dgvData.DataKeys[dgvData.SelectedRow.RowIndex][0]), Convert.ToInt64(tData.SelectedNodeID), txtParameterCode.Text.Trim()))
                        {
                            Alert.Show("该节点已经存在", "警告", MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    Model.CodeRule.CodeRuleParameter codeRuleParameter = new Model.CodeRule.CodeRuleParameter();
                    codeRuleParameter.CodeRuleId = Convert.ToInt64(dgvData.DataKeys[dgvData.SelectedRow.RowIndex][0]);
                    codeRuleParameter.ParameterCode = txtParameterCode.Text.Trim();
                    codeRuleParameter.ParameterName = txtParameterName.Text.Trim();
                    codeRuleParameter.ParameterValue = txtParameterValue.Text.Trim();
                    codeRuleParameter.ParameterType = cmbParameterType.SelectedText;
                    codeRuleParameter.ParameterDescription = txtParameterDescription.Text.Trim();
                    if (tData.SelectedNode != null)
                    {
                        Model.CodeRule.CodeRuleParameter codeRuleParameter1 = m_CodeRuleParameter.Select(Convert.ToInt64(tData.SelectedNode.NodeID));
                        if (AddIsSubNode)
                        {
                            codeRuleParameter.ParentId = codeRuleParameter1.Id;
                            codeRuleParameter.Level = codeRuleParameter1.Level + 1;
                        }
                        else
                        {
                            codeRuleParameter.ParentId = 0;
                            codeRuleParameter.Level = 0;
                        }
                    }
                    else
                    {
                        codeRuleParameter.ParentId = 0;
                        codeRuleParameter.Level = 0;
                    }
                    codeRuleParameter.IsDefault = cbIsDefault.Checked;
                    codeRuleParameter.SortNo = Convert.ToInt32(txtSortNo.Text);
                    codeRuleParameter.GroupNo = Convert.ToInt32(txtGroupNo.Text);
                    codeRuleParameter.CreateUserID = Util.Util.UserName;
                    codeRuleParameter.LastUpdateUserID = Util.Util.UserName;
                    codeRuleParameter.Remark = "";
                    if (m_CodeRuleParameter.Add(codeRuleParameter))
                    {
                        LoadMenu(Convert.ToInt64(dgvData.DataKeys[dgvData.SelectedRow.RowIndex][0]),dgvData.SelectedRow.Values[1].ToString());
                        Clear();
                        wCodeNode.Hidden = true;
                    }
                    else
                        Alert.Show("新增失败", "错误", MessageBoxIcon.Error);
                }
                else if (wCodeNode.Title == "修改" && tData.SelectedNode != null)
                {
                    Model.CodeRule.CodeRuleParameter codeRuleParameter = m_CodeRuleParameter.Select(Convert.ToInt64(tData.SelectedNode.NodeID));
                    if (codeRuleParameter != null && codeRuleParameter.ParameterCode != txtParameterCode.Text.Trim())
                    {
                        if (m_CodeRuleParameter.IsExist(Convert.ToInt64(dgvData.DataKeys[dgvData.SelectedRow.RowIndex][0]), txtParameterCode.Text.Trim()))
                        {
                            Alert.Show("该节点代码已经存在", "警告", MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    if (codeRuleParameter.ParameterName != txtParameterName.Text.Trim())
                    {
                        if (m_CodeRuleParameter.IsExist(Convert.ToInt64(dgvData.DataKeys[dgvData.SelectedRow.RowIndex][0]), Convert.ToInt64(tData.SelectedNodeID), txtParameterCode.Text.Trim()))
                        {
                            Alert.Show("该节点代码已经存在", "警告", MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    codeRuleParameter.ParameterCode = txtParameterCode.Text.Trim();
                    codeRuleParameter.ParameterName = txtParameterName.Text.Trim();
                    codeRuleParameter.ParameterValue = txtParameterValue.Text.Trim();
                    codeRuleParameter.ParameterType = cmbParameterType.SelectedText;
                    codeRuleParameter.ParameterDescription = txtParameterDescription.Text.Trim();
                    codeRuleParameter.IsDefault = cbIsDefault.Checked;
                    codeRuleParameter.SortNo = Convert.ToInt32(txtSortNo.Text);
                    codeRuleParameter.GroupNo = Convert.ToInt32(txtGroupNo.Text);
                    codeRuleParameter.LastUpdateUserID = Util.Util.UserName;
                    if (m_CodeRuleParameter.Update(codeRuleParameter))
                    {
                        LoadMenu(Convert.ToInt64(dgvData.DataKeys[dgvData.SelectedRow.RowIndex][0]),dgvData.SelectedRow.Values[1].ToString());
                        Clear();
                        wCodeNode.Hidden = true;
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
        protected void btnAppend_Click(object sender, EventArgs e)
        {
            txtRule.Text += "["+ cmbFixedRule.SelectedText + "]";
            txtRuleEN.Text += "[" + cmbFixedRule.SelectedValue + "]";
        }
    }
}