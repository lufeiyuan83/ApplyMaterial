using FineUI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ERP.HRM
{
    public partial class Organization : System.Web.UI.Page
    {
        private readonly string strClassName = new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().ReflectedType.Name + ".aspx";
        private static bool AddIsSubNode = true;
        private void LoadData()
        {
            try
            {
                tData.Nodes.Clear();
                BLL.HRM.Organization m_Organization = new BLL.HRM.Organization();
                DataTable dtOrganization = m_Organization.Select();
                if (dtOrganization != null && dtOrganization.Rows.Count > 0)
                {
                    DataSet ds = new DataSet();
                    ds.Tables.Add(dtOrganization);
                    ds.Relations.Add("TreeRelation", ds.Tables[0].Columns["Id"], ds.Tables[0].Columns["OrgHierarchy"]);
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        if (row["Id"].ToString() == row["OrgHierarchy"].ToString())
                        {
                            FineUI.TreeNode node = new FineUI.TreeNode
                            {
                                NodeID = row["Id"].ToString(),
                                Text = row["OrgName"].ToString() + "(" + row["OrgCode"].ToString() + ")",
                                Icon = Convert.ToBoolean(row["DefaultValue"]) ? Icon.Star : Icon.None,
                                EnableClickEvent = true
                            };

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
        private void ResolveSubTree(DataRow dataRow, FineUI.TreeNode treeNode)
        {
            DataRow[] rows = dataRow.GetChildRows("TreeRelation");
            if (rows.Length > 0)
            {
                foreach (DataRow row in rows)
                {
                    if (row["Id"].ToString() != row["OrgHierarchy"].ToString())
                    {
                        FineUI.TreeNode node = new FineUI.TreeNode
                        {
                            NodeID = row["Id"].ToString(),
                            Text = row["OrgName"].ToString() + "(" + row["OrgCode"].ToString() + ")",
                            Icon = Convert.ToBoolean(row["DefaultValue"]) ? Icon.Star : Icon.None,
                            EnableClickEvent = true
                        };
                        treeNode.Nodes.Add(node);

                        ResolveSubTree(row, node);
                    }
                }
            }
        }
        private void LoadOrgType()
        {
            try
            {
                BLL.PLM.Mapping m_Mapping = new BLL.PLM.Mapping();
                cmbOrgType.DataTextField = "MappingValue2"; 
                cmbOrgType.DataValueField = "MappingValue2";
                cmbOrgType.DataSource = m_Mapping.Select("Organization", "OrgType");
                cmbOrgType.DataBind();
            }
            catch (Exception ex)
            {
                BLL.Global.WriteLog(ex, this.ClientTarget, Util.LogType.Exception.ToString(), new System.Diagnostics.StackTrace(true).GetFrame(0).GetMethod().ToString(), Util.Util.UserName, Environment.MachineName, "Line:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileLineNumber().ToString() + " Column:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileColumnNumber().ToString());
                Alert.ShowInTop(ex.Message, GetGlobalResourceObject("Language", "Error").ToString(), MessageBoxIcon.Error);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
                LoadOrgType();
                BtnSave.OnClientClick = wData.GetHideReference();
            }
        }
        protected void BtnAddSiblingNode_Click(object sender, EventArgs e)
        {
            try
            {
                Clear();
                if (tData.Nodes.Count > 0)
                {
                    //long lngParentId = 0;
                    //BLL.HRM.Organization m_Organization = new BLL.HRM.Organization();
                    //if (tData.SelectedNode != null)
                    //{
                    //    Model.HRM.Organization organization = m_Organization.Select(Convert.ToInt64(tData.SelectedNode.NodeID));

                    //    if (organization != null)
                    //    {
                    //        lngParentId = organization.OrgHierarchy;
                    //    }
                    //}
                    //txtOrgHierarchy.Text = lngParentId.ToString();

                    AddIsSubNode = false;
                }
                wData.Title = GetGlobalResourceObject(strClassName, "Add").ToString();
                wData.Hidden = false;
            }
            catch (Exception ex)
            {
                BLL.Global.WriteLog(ex, this.ClientTarget, Util.LogType.Exception.ToString(), new System.Diagnostics.StackTrace(true).GetFrame(0).GetMethod().ToString(), Util.Util.UserName, Environment.MachineName, "Line:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileLineNumber().ToString() + " Column:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileColumnNumber().ToString());
                Alert.ShowInTop(ex.Message, GetGlobalResourceObject("Language", "Error").ToString(), MessageBoxIcon.Error);
            }
        }
        protected void BtnAddSubNode_Click(object sender, EventArgs e)
        {
            try
            {
                Clear();
                if (tData.Nodes.Count > 0)
                {
                    //long lngParentId = 0;
                    //BLL.HRM.Organization m_Organization = new BLL.HRM.Organization();
                    //if (tData.SelectedNode != null)
                    //{
                    //    Model.HRM.Organization organization = m_Organization.Select(Convert.ToInt64(tData.SelectedNode.NodeID));
                    //    if (organization != null)
                    //    {
                    //        lngParentId = organization.Id;
                    //    }
                    //}
                    //txtOrgHierarchy.Text = lngParentId.ToString();

                    AddIsSubNode = true;
                }
                wData.Title = GetGlobalResourceObject(strClassName, "Add").ToString();
                wData.Hidden = false;
            }
            catch (Exception ex)
            {
                BLL.Global.WriteLog(ex, this.ClientTarget, Util.LogType.Exception.ToString(), new System.Diagnostics.StackTrace(true).GetFrame(0).GetMethod().ToString(), Util.Util.UserName, Environment.MachineName, "Line:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileLineNumber().ToString() + " Column:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileColumnNumber().ToString());
                Alert.ShowInTop(ex.Message, GetGlobalResourceObject("Language", "Error").ToString(), MessageBoxIcon.Error);
            }
        }
        private void Clear()
        {           
            txtOrgCode.Text = "";
            txtOrgName.Text = "";
            txtOrgShortName.Text = "";
            txtOrgDescription.Text = "";
            txtAffiliation.Text = "";
            cmbOrgType.SelectedIndex=-1;
            cbEnableSelect.Checked = true;
            cbDefaultValue.Checked = true;
            txtPhone.Text = "";
            txtMobilePhone.Text = "";
            txtEmail.Text = "";
            txtFax.Text = "";
            txtAddress.Text = "";
            txtRemark.Text = "";
        }
        protected void BtnUpdateNode_Click(object sender, EventArgs e)
        {
            try
            {
                Clear();
                if (tData.SelectedNode != null)
                {
                    BLL.HRM.Organization m_Organization = new BLL.HRM.Organization();
                    Model.HRM.Organization organization = m_Organization.Select(Convert.ToInt64(tData.SelectedNode.NodeID));
                    if (organization != null)
                    {
                        txtOrgCode.Text = organization.OrgCode;
                        txtOrgName.Text = organization.OrgName;
                        txtOrgShortName.Text = organization.OrgShortName;
                        txtOrgDescription.Text = organization.OrgDescription;
                        txtAffiliation.Text = organization.Affiliation;
                        cmbOrgType.SelectedValue = organization.OrgType;
                        cbEnableSelect.Checked = organization.EnableSelect;
                        cbDefaultValue.Checked = organization.DefaultValue;
                        txtPhone.Text = organization.Phone;
                        txtMobilePhone.Text = organization.MobilePhone;
                        txtEmail.Text = organization.Email;
                        txtFax.Text = organization.Fax;
                        txtAddress.Text = organization.Address;
                        txtRemark.Text = organization.Remark;
                    }

                    wData.Title = GetGlobalResourceObject(strClassName, "Update").ToString();
                    wData.Hidden = false;
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
        protected void BtnDeleteNode_Click(object sender, EventArgs e)
        {
            try
            {
                if (!tData.SelectedNode.Leaf)
                {
                    Alert.Show(GetGlobalResourceObject(strClassName, "DeleteFromLeafNode").ToString(), GetGlobalResourceObject("Language", "Warning").ToString(), MessageBoxIcon.Warning);
                    return;
                }
                if (tData.SelectedNode != null)
                {
                    BLL.HRM.Organization m_Organization = new BLL.HRM.Organization();
                    Model.HRM.Organization organization = m_Organization.Select(Convert.ToInt64(tData.SelectedNode.NodeID));
                    if (m_Organization.Delete(Convert.ToInt64(tData.SelectedNode.NodeID), Util.Util.UserName))
                    {
                        LoadData();
                        Alert.Show(GetGlobalResourceObject(strClassName, "DeleteSuccessfully").ToString(), GetGlobalResourceObject("Language", "Information").ToString(), MessageBoxIcon.Information);
                    }
                    else
                        Alert.Show(GetGlobalResourceObject(strClassName, "DeleteFailed").ToString(), GetGlobalResourceObject("Language", "Error").ToString(), MessageBoxIcon.Error);

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
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                BLL.HRM.Organization m_Organization = new BLL.HRM.Organization();
                if (wData.Title == GetGlobalResourceObject(strClassName, "Add").ToString())
                {
                    if (m_Organization.IsExist(txtOrgCode.Text.Trim()))
                    {
                        Alert.Show(GetGlobalResourceObject(strClassName, "NodeCodeExisted").ToString(), GetGlobalResourceObject("Language", "Warning").ToString(), MessageBoxIcon.Warning);
                        return;
                    }
                    if (tData.SelectedNode != null)
                    {
                        if (m_Organization.IsExist(txtOrgCode.Text.Trim()))
                        {
                            Alert.Show(GetGlobalResourceObject(strClassName, "NodeExisted").ToString(), GetGlobalResourceObject("Language", "Warning").ToString(), MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    Model.HRM.Organization organization = new Model.HRM.Organization
                    {
                        OrgCode = txtOrgCode.Text.Trim(),
                        OrgName = txtOrgName.Text.Trim(),
                        OrgShortName = txtOrgShortName.Text.Trim(),
                        OrgDescription = txtOrgDescription.Text.Trim(),
                        Affiliation = txtAffiliation.Text.Trim(),
                        OrgType = cmbOrgType.SelectedText,
                        EnableSelect = cbEnableSelect.Checked,
                        DefaultValue = cbDefaultValue.Checked,
                        Phone = txtPhone.Text.Trim(),
                        MobilePhone = txtMobilePhone.Text.Trim(),
                        Email = txtEmail.Text.Trim(),
                        Fax = txtFax.Text.Trim(),
                        Address = txtAddress.Text.Trim(),
                        CreateUserID = Util.Util.UserName,
                        LastUpdateUserID = Util.Util.UserName,
                        Remark = txtRemark.Text.Trim()
                    };
                    if (tData.SelectedNode != null)
                    {
                        Model.HRM.Organization organization1 = m_Organization.Select(Convert.ToInt64(tData.SelectedNode.NodeID));
                        if (AddIsSubNode)
                        {
                            organization.OrgHierarchy = organization1.Id;
                        }
                        else
                        {
                            organization.OrgHierarchy = 0;
                        }
                    }
                    else
                    {
                        organization.OrgHierarchy = 0;
                    }
                    if (m_Organization.Add(organization))
                    {
                        LoadData();
                        Clear();
                        wData.Hidden = true;
                    }
                    else
                        Alert.Show(GetGlobalResourceObject(strClassName, "AddFailed").ToString(), GetGlobalResourceObject("Language", "Error").ToString(), MessageBoxIcon.Error);
                }
                else if (wData.Title == GetGlobalResourceObject(strClassName, "Update").ToString() && tData.SelectedNode != null)
                {
                    Model.HRM.Organization organization = m_Organization.Select(Convert.ToInt64(tData.SelectedNode.NodeID));
                    if (organization != null && organization.OrgCode != txtOrgCode.Text.Trim())
                    {
                        if (m_Organization.IsExist(txtOrgCode.Text.Trim()))
                        {
                            Alert.Show(GetGlobalResourceObject(strClassName, "NodeCodeExisted").ToString(), GetGlobalResourceObject("Language", "Warning").ToString(), MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    organization.OrgCode = txtOrgCode.Text.Trim();
                    organization.OrgName = txtOrgName.Text.Trim();
                    organization.OrgShortName = txtOrgShortName.Text.Trim();
                    organization.OrgDescription = txtOrgDescription.Text.Trim();
                    organization.Affiliation = txtAffiliation.Text.Trim();
                    organization.OrgType = cmbOrgType.SelectedText;
                    organization.EnableSelect = cbEnableSelect.Checked;
                    organization.DefaultValue = cbDefaultValue.Checked;
                    organization.Phone = txtPhone.Text.Trim();
                    organization.MobilePhone = txtMobilePhone.Text.Trim();
                    organization.Email = txtEmail.Text.Trim();
                    organization.Fax = txtFax.Text.Trim();
                    organization.Address = txtAddress.Text.Trim();
                    organization.LastUpdateUserID = Util.Util.UserName;
                    organization.Remark = txtRemark.Text.Trim();
                    if (m_Organization.Update(organization))
                    {
                        LoadData();
                        Clear();
                        wData.Hidden = true;
                    }
                    else
                        Alert.Show(GetGlobalResourceObject(strClassName, "UpdateFailed").ToString(), GetGlobalResourceObject("Language", "Error").ToString(), MessageBoxIcon.Error);
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