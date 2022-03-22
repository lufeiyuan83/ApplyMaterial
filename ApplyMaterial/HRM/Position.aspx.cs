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
    public partial class Position : BasePage
    {
        private readonly string strClassName = new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().ReflectedType.Name + ".aspx";
        private static DataTable dtDepartment;
        private static DataTable dtPosition;
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
        private void LoadDepartment(string m_ApplicationOrg)
        {
            try
            {
                BLL.HRM.Department m_Department = new BLL.HRM.Department();
                dtDepartment = m_Department.Select(m_ApplicationOrg);
                dgvDepartment.DataSource = dtDepartment;
                dgvDepartment.DataBind();
            }
            catch (Exception ex)
            {
                BLL.Global.WriteLog(ex, this.ClientTarget, Util.LogType.Exception.ToString(), new System.Diagnostics.StackTrace(true).GetFrame(0).GetMethod().ToString(), Util.Util.UserName, Environment.MachineName, "Line:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileLineNumber().ToString() + " Column:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileColumnNumber().ToString());
                Alert.ShowInTop(ex.Message, GetGlobalResourceObject("Language", "Error").ToString(), MessageBoxIcon.Error);
            }
        }
        private void LoadPosition(string m_ApplicationOrg,long m_DepartmentId)
        {
            try
            {
                BLL.HRM.Position m_Position = new BLL.HRM.Position();
                dtPosition = m_Position.Select(m_ApplicationOrg, m_DepartmentId);
                dgvPosition.DataSource = dtPosition;
                dgvPosition.DataBind();
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
                if (organization != null)
                    LoadDepartment(organization.OrgCode);
                else
                    LoadDepartment(null);
            }
            catch (Exception ex)
            {
                BLL.Global.WriteLog(ex, this.ClientTarget, Util.LogType.Exception.ToString(), new System.Diagnostics.StackTrace(true).GetFrame(0).GetMethod().ToString(), Util.Util.UserName, Environment.MachineName, "Line:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileLineNumber().ToString() + " Column:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileColumnNumber().ToString());
                Alert.ShowInTop(ex.Message, GetGlobalResourceObject("Language", "Error").ToString(), MessageBoxIcon.Error);
            }
        }
        protected void DgvDepartment_RowSelect(object sender, FineUI.GridRowSelectEventArgs e)
        {
            LoadPosition(dgvDepartment.SelectedRow.Values[1].ToString(),Convert.ToInt64(dgvDepartment.DataKeys[e.RowIndex][0]));
        }
        protected void DgvDepartment_Sort(object sender, FineUI.GridSortEventArgs e)
        {
            if (dtDepartment != null)
            {
                string sortField = dgvDepartment.SortField;
                string sortDirection = dgvDepartment.SortDirection;

                DataView dv = dtDepartment.DefaultView;
                if (!string.IsNullOrEmpty(sortField))
                {
                    dv.Sort = String.Format("{0} {1}", sortField, sortDirection);
                }

                dgvDepartment.DataSource = dv;
                dgvDepartment.DataBind();
            }
        }
        protected void DgvPosition_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            try
            {
                BLL.HRM.Position m_Position = new BLL.HRM.Position();
                Model.HRM.Position position = m_Position.Select(Convert.ToInt64(dgvPosition.DataKeys[e.RowIndex][0]));
                if (position != null)
                {
                    txtPositionCode.Text = position.PositionCode;
                    txtPositionName.Text = position.PositionName;
                    txtRemark.Text = position.Remark;

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
        protected void DgvPosition_Sort(object sender, FineUI.GridSortEventArgs e)
        {
            if (dtPosition != null)
            {
                string sortField = dgvPosition.SortField;
                string sortDirection = dgvPosition.SortDirection;

                DataView dv = dtPosition.DefaultView;
                if (!string.IsNullOrEmpty(sortField))
                {
                    dv.Sort = String.Format("{0} {1}", sortField, sortDirection);
                }

                dgvPosition.DataSource = dv;
                dgvPosition.DataBind();
            }
        }
        private void Clear()
        {
            txtPositionCode.Text = "";
            txtPositionName.Text = "";
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
                BLL.HRM.Position m_Position = new BLL.HRM.Position();
                Model.HRM.Position position;
                if (wData.Title == GetGlobalResourceObject(strClassName, "Add").ToString())
                {
                    position = new Model.HRM.Position();
                    position.ApplicationOrg = dgvDepartment.Rows[dgvDepartment.SelectedRow.RowIndex].Values[1].ToString();
                    position.DepartmentId = Convert.ToInt64(dgvDepartment.DataKeys[dgvDepartment.SelectedRow.RowIndex][0]);
                    position.PositionCode = txtPositionCode.Text.Trim();
                    position.PositionName = txtPositionName.Text.Trim();
                    position.CreateUserID = Util.Util.UserName;
                    position.LastUpdateUserID = Util.Util.UserName;
                    position.Remark = txtRemark.Text;
                    if (m_Position.Add(position))
                    {
                        Clear();
                        Alert.ShowInTop(GetGlobalResourceObject(strClassName, "SaveSuccessfully").ToString(), GetGlobalResourceObject("Language", "Information").ToString(), MessageBoxIcon.Information);
                        LoadPosition(position.ApplicationOrg,position.DepartmentId);
                    }
                    else
                    {
                        Alert.Show(GetGlobalResourceObject(strClassName, "SaveFailed").ToString(), GetGlobalResourceObject("Language", "Error").ToString(), MessageBoxIcon.Error);
                    }
                }
                else if (wData.Title == GetGlobalResourceObject(strClassName, "Update").ToString())
                {
                    position = m_Position.Select(Convert.ToInt64(dgvPosition.DataKeys[dgvPosition.SelectedRow.RowIndex][0]));
                    position.PositionCode = txtPositionCode.Text.Trim();
                    position.PositionName = txtPositionName.Text.Trim();
                    position.LastUpdateUserID = Util.Util.UserName;
                    position.Remark = txtRemark.Text;
                    if (m_Position.Update(position))
                    {
                        Clear();
                        Alert.ShowInTop(GetGlobalResourceObject(strClassName, "SaveSuccessfully").ToString(), GetGlobalResourceObject("Language", "Information").ToString(), MessageBoxIcon.Information);
                        LoadPosition(position.ApplicationOrg,position.DepartmentId);
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