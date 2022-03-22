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
    public partial class Department : BasePage
    {
        private readonly string strClassName = new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().ReflectedType.Name + ".aspx";
        private static DataTable dtData;
        private void LoadOrganization()
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
        private void LoadData(string m_ApplicationOrg)
        {
            try
            {
                BLL.HRM.Department m_Department = new BLL.HRM.Department();
                dtData = m_Department.Select(m_ApplicationOrg);
                dgvData.DataSource = dtData;
                dgvData.DataBind();
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
                LoadOrganization();
                BtnSave.OnClientClick = wData.GetHideReference();
            }
        }
        protected void tData_NodeCommand(object sender, TreeCommandEventArgs e)
        {
            try
            {
                BLL.HRM.Organization m_Organization = new BLL.HRM.Organization();
                Model.HRM.Organization organization = m_Organization.Select(Convert.ToInt64(e.Node.NodeID));
                if(organization!=null)
                    LoadData(organization.OrgCode);
                else
                    LoadData(null);
            }
            catch (Exception ex)
            {
                BLL.Global.WriteLog(ex, this.ClientTarget, Util.LogType.Exception.ToString(), new System.Diagnostics.StackTrace(true).GetFrame(0).GetMethod().ToString(), Util.Util.UserName, Environment.MachineName, "Line:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileLineNumber().ToString() + " Column:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileColumnNumber().ToString());
                Alert.ShowInTop(ex.Message, GetGlobalResourceObject("Language", "Error").ToString(), MessageBoxIcon.Error);
            }
        }
        protected void DgvData_RowDoubleClick(object sender, FineUI.GridRowClickEventArgs e)
        {
            try
            {
                BLL.HRM.Department m_Department = new BLL.HRM.Department();
                Model.HRM.Department department = m_Department.Select(Convert.ToInt64(dgvData.DataKeys[e.RowIndex][0]));
                if (department != null)
                {
                    txtDepartmentCode.Text = department.DepartmentCode;
                    txtDepartmentName.Text = department.DepartmentName;
                    txtRemark.Text = department.Remark;

                    wData.Title = GetGlobalResourceObject(strClassName, "Update").ToString();
                    wData.Hidden = false;
                }
            }
            catch (Exception ex)
            {
                BLL.Global.WriteLog(ex, this.ClientTarget, Util.LogType.Exception.ToString(), new System.Diagnostics.StackTrace(true).GetFrame(0).GetMethod().ToString(), Util.Util.UserName, Environment.MachineName, "Line:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileLineNumber().ToString() + " Column:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileColumnNumber().ToString());
                Alert.ShowInTop(ex.Message, GetGlobalResourceObject("Language", "Error").ToString(), MessageBoxIcon.Error);
            }
        }
        protected void DgvData_Sort(object sender, FineUI.GridSortEventArgs e)
        {
            if(dtData!=null)
            { 
                string sortField = dgvData.SortField;
                string sortDirection = dgvData.SortDirection;

                DataView dv = dtData.DefaultView;
                if (!string.IsNullOrEmpty(sortField))
                {
                    dv.Sort = String.Format("{0} {1}", sortField, sortDirection);
                }

                dgvData.DataSource = dv;
                dgvData.DataBind();
            }
        }
        private void Clear()
        {
            txtDepartmentCode.Text = "";
            txtDepartmentName.Text = "";
            txtRemark.Text = "";
        }
        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            Clear();

            wData.Title = GetGlobalResourceObject(strClassName, "Add").ToString();
            wData.Hidden = false;
        }
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                BLL.HRM.Department m_Department = new BLL.HRM.Department();
                Model.HRM.Department department;
                if (wData.Title == GetGlobalResourceObject(strClassName, "Add").ToString())
                {
                    department = new Model.HRM.Department();
                    BLL.HRM.Organization m_Organization = new BLL.HRM.Organization();
                    Model.HRM.Organization organization = m_Organization.Select(Convert.ToInt64(tData.SelectedNodeID));
                    if(organization!=null)
                        department.ApplicationOrg = organization.OrgCode;
                    department.DepartmentCode = txtDepartmentCode.Text;
                    department.DepartmentName = txtDepartmentName.Text;
                    department.CreateUserID = Util.Util.UserName;
                    department.LastUpdateUserID = Util.Util.UserName;
                    department.Remark = txtRemark.Text;
                    if (m_Department.Add(department))
                    {
                        Clear();
                        Alert.ShowInTop(GetGlobalResourceObject(strClassName, "SaveSuccessfully").ToString(), GetGlobalResourceObject("Language", "Information").ToString(), MessageBoxIcon.Information);
                        LoadData(department.ApplicationOrg);
                    }
                    else
                    {
                        Alert.Show(GetGlobalResourceObject(strClassName, "SaveFailed").ToString(), GetGlobalResourceObject("Language", "Error").ToString(), MessageBoxIcon.Error);
                    }
                }
                else if (wData.Title == GetGlobalResourceObject(strClassName, "Update").ToString())
                {
                    department = m_Department.Select(Convert.ToInt64(dgvData.DataKeys[dgvData.SelectedRow.RowIndex][0]));
                    department.DepartmentCode = txtDepartmentCode.Text;
                    department.DepartmentName = txtDepartmentName.Text;
                    department.LastUpdateUserID = Util.Util.UserName;
                    department.Remark = txtRemark.Text;
                    if (m_Department.Update(department))
                    {
                        Clear();
                        Alert.ShowInTop(GetGlobalResourceObject(strClassName, "SaveSuccessfully").ToString(), GetGlobalResourceObject("Language", "Information").ToString(), MessageBoxIcon.Information);
                        LoadData(department.ApplicationOrg);
                    }
                    else
                    {
                        Alert.Show(GetGlobalResourceObject(strClassName, "SaveFailed").ToString(), GetGlobalResourceObject("Language", "Error").ToString(), MessageBoxIcon.Error);
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