using FineUI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ERP.PMS
{
    public partial class Role : BasePage
    {
        private readonly string strClassName = new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().ReflectedType.Name + ".aspx";
        private static DataTable dtDataSource;
        private static DataTable dtEmployee;
        private void LoadData(string m_RoleName)
        {
            try
            {
                BLL.PMS.Role m_Role = new BLL.PMS.Role();
                dtDataSource = m_Role.GetRole(m_RoleName);
                dgvData.DataSource = dtDataSource;
                dgvData.DataBind();
            }
            catch (Exception ex)
            {
                BLL.Global.WriteLog(ex, this.ClientTarget, Util.LogType.Exception.ToString(), new System.Diagnostics.StackTrace(true).GetFrame(0).GetMethod().ToString(), Util.Util.UserName, Environment.MachineName, "Line:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileLineNumber().ToString() + " Column:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileColumnNumber().ToString());
                Alert.ShowInTop(ex.Message, GetGlobalResourceObject("Language", "Error").ToString(), MessageBoxIcon.Error);
            }
        }
        private void LoadEmployee(long m_RoleId)
        {
            try
            {
                BLL.PMS.RoleRelation m_RoleRelation = new BLL.PMS.RoleRelation();
                dtEmployee = m_RoleRelation.SearchByRoleId(m_RoleId);
                dgvEmployee.DataSource = dtEmployee;
                dgvEmployee.DataBind();
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
                LoadData("");
            }
        }
        private void Clear()
        {
            txtRoleCode.Text = "";
            txtRoleName.Text = "";
            txtRoleDescription.Text = "";
            txtRemark.Text = "";
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Clear();
            pEdit.Title = GetGlobalResourceObject(strClassName, "Add").ToString();
            pEdit.Hidden = false;
        }
        protected void btnGrant_Click(object sender, EventArgs e)
        {

        }
        protected void btnAddAccount_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvData.SelectedRow != null)
                {
                    txtEmployeeID.Text = "";
                    LoadEmployee(Convert.ToInt64(dgvData.DataKeys[dgvData.SelectedRow.RowIndex][0]));
                    wAddEmployee.Title = GetGlobalResourceObject(strClassName, "AddAccount").ToString();
                    wAddEmployee.Hidden = false;
                }
            }
            catch (Exception ex)
            {
                BLL.Global.WriteLog(ex, this.ClientTarget, Util.LogType.Exception.ToString(), new System.Diagnostics.StackTrace(true).GetFrame(0).GetMethod().ToString(), Util.Util.UserName, Environment.MachineName, "Line:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileLineNumber().ToString() + " Column:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileColumnNumber().ToString());
                Alert.ShowInTop(ex.Message, GetGlobalResourceObject("Language", "Error").ToString(), MessageBoxIcon.Error);
            }
        }
        protected void dgvData_RowCommand(object sender, FineUI.GridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                try
                {
                    if (e.RowIndex >= 0)
                    {
                        BLL.PMS.Role m_Role = new BLL.PMS.Role();
                        if (m_Role.Delete(Convert.ToInt64(dgvData.DataKeys[e.RowIndex][0]), Util.Util.UserName))
                        {
                            LoadData(dgvData.SelectedRow.Values[3].ToString());
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
        protected void dgvData_RowDoubleClick(object sender, FineUI.GridRowClickEventArgs e)
        {
            Clear();

            txtRoleCode.Text = dgvData.Rows[e.RowIndex].Values[2].ToString();
            txtRoleName.Text = dgvData.Rows[e.RowIndex].Values[3].ToString();
            txtRoleDescription.Text = dgvData.Rows[e.RowIndex].Values[4].ToString();
            txtRemark.Text = dgvData.Rows[e.RowIndex].Values[7].ToString();

            pEdit.Title = GetGlobalResourceObject(strClassName, "Update").ToString();
            pEdit.Hidden = false;
        }
        protected void dgvData_Sort(object sender, FineUI.GridSortEventArgs e)
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
        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                BLL.PMS.Role m_Role = new BLL.PMS.Role();
                if (pEdit.Title == GetGlobalResourceObject(strClassName, "Add").ToString())
                {
                    if (m_Role.IsExist(txtRoleCode.Text.Trim()))
                    {
                        txtRoleCode.Focus();
                        Alert.Show(GetGlobalResourceObject(strClassName, "RoleCodeExist").ToString(), GetGlobalResourceObject("Language", "Warning").ToString(), MessageBoxIcon.Warning);
                        return;
                    }

                    Model.PMS.Role role = new Model.PMS.Role();
                    role.RoleCode = txtRoleCode.Text.Trim();
                    role.RoleName = txtRoleName.Text.Trim();
                    role.RoleDescription = txtRoleDescription.Text.Trim();
                    role.IsDeleted = false;
                    role.CreateUserID = Util.Util.UserName;
                    role.LastUpdateUserID = Util.Util.UserName;
                    role.Remark = txtRemark.Text;
                    if (m_Role.Add(role))
                    {
                        LoadData("");
                        Clear();
                        pEdit.Hidden = true;
                    }
                    else
                        Alert.Show(GetGlobalResourceObject(strClassName, "SaveFailed").ToString(), GetGlobalResourceObject("Language", "Error").ToString(), MessageBoxIcon.Error);
                }
                else if (pEdit.Title == GetGlobalResourceObject(strClassName, "Update").ToString() && dgvData.SelectedRow != null)
                {
                    if (txtRoleCode.Text.Trim().ToUpper() != dgvData.SelectedRow.Values[2].ToString().ToUpper())
                    {
                        txtRoleCode.Focus();
                        if (m_Role.IsExist(txtRoleCode.Text.Trim()))
                        {
                            Alert.Show(GetGlobalResourceObject(strClassName, "RoleCodeExist").ToString(), GetGlobalResourceObject("Language", "Warning").ToString(), MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    Model.PMS.Role role = m_Role.Select(Convert.ToInt64(dgvData.DataKeys[dgvData.SelectedRow.RowIndex][0]));
                    role.RoleCode = txtRoleCode.Text.Trim();
                    role.RoleName = txtRoleName.Text.Trim();
                    role.RoleDescription = txtRoleDescription.Text.Trim();
                    role.LastUpdateUserID = Util.Util.UserName;
                    role.Remark = txtRemark.Text.Trim();

                    if (m_Role.Update(role))
                    {
                        LoadData("");
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
        protected void BtnAddEmp_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtEmployeeID.Text.Trim().Length>0)
                {
                    BLL.HRM.Employee m_Employee = new BLL.HRM.Employee();
                    Model.HRM.Employee employee = m_Employee.Select("",txtEmployeeID.Text.Trim());
                    if(employee!=null)
                    { 
                        BLL.PMS.RoleRelation m_RoleRelation = new BLL.PMS.RoleRelation();
                        Model.PMS.RoleRelation roleRelation = new Model.PMS.RoleRelation();
                        roleRelation.ApplicationOrg = employee.ApplicationOrg;
                        roleRelation.RoleId = Convert.ToInt64(dgvData.DataKeys[dgvData.SelectedRow.RowIndex][0]);
                        roleRelation.EmployeeID = employee.EmployeeID;
                        if (m_RoleRelation.Add(roleRelation))
                        {
                            LoadEmployee(Convert.ToInt64(dgvData.DataKeys[dgvData.SelectedRow.RowIndex][0]));
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
        protected void DgvEmployee_Sort(object sender, GridSortEventArgs e)
        {
            string sortField = dgvEmployee.SortField;
            string sortDirection = dgvEmployee.SortDirection;

            DataView dv = dtEmployee.DefaultView;
            if (!String.IsNullOrEmpty(sortField))
            {
                dv.Sort = String.Format("{0} {1}", sortField, sortDirection);
            }

            dgvEmployee.DataSource = dv;
            dgvEmployee.DataBind();
        }
        protected void DgvEmployee_RowCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                try
                {
                    if (e.RowIndex >= 0)
                    {
                        BLL.PMS.RoleRelation m_RoleRelation = new BLL.PMS.RoleRelation();
                        if (m_RoleRelation.Delete(Convert.ToInt64(dgvEmployee.DataKeys[e.RowIndex][0])))
                        {
                            LoadEmployee(Convert.ToInt64(Convert.ToInt64(dgvData.DataKeys[e.RowIndex][0])));
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
    }
}