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
    public partial class Employee : BasePage
    {
        private readonly string strClassName = new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().ReflectedType.Name + ".aspx";
        private static DataTable dtDepartment;
        private static DataTable dtEmployee;

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

                dgvEmployee.DataSource = null;
                dgvEmployee.DataBind();
            }
            catch (Exception ex)
            {
                BLL.Global.WriteLog(ex, this.ClientTarget, Util.LogType.Exception.ToString(), new System.Diagnostics.StackTrace(true).GetFrame(0).GetMethod().ToString(), Util.Util.UserName, Environment.MachineName, "Line:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileLineNumber().ToString() + " Column:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileColumnNumber().ToString());
                Alert.ShowInTop(ex.Message, GetGlobalResourceObject("Language", "Error").ToString(), MessageBoxIcon.Error);
            }
        }
        private void LoadPosition()
        {
            try
            {
                BLL.HRM.Position m_Position = new BLL.HRM.Position();
                cmbPositionName.DataTextField = "PositionName";
                cmbPositionName.DataValueField = "Id";
                cmbPositionName.DataSource = m_Position.Select();
                cmbPositionName.DataBind();
            }
            catch (Exception ex)
            {
                BLL.Global.WriteLog(ex, this.ClientTarget, Util.LogType.Exception.ToString(), new System.Diagnostics.StackTrace(true).GetFrame(0).GetMethod().ToString(), Util.Util.UserName, Environment.MachineName, "Line:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileLineNumber().ToString() + " Column:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileColumnNumber().ToString());
                Alert.ShowInTop(ex.Message, GetGlobalResourceObject("Language", "Error").ToString(), MessageBoxIcon.Error);
            }
        }
        private void LoadEmployee(string m_ApplicationOrg, string m_DepartmentCode)
        {
            try
            {
                BLL.HRM.Employee m_Employee = new BLL.HRM.Employee();
                dtEmployee = m_Employee.SearchByDepartmentCode(m_ApplicationOrg, m_DepartmentCode);
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
                LoadOrganization();
                LoadPosition();
                BtnSave.OnClientClick = wData.GetHideReference();
            }
        }
        protected void DgvEmployee_RowDoubleClick(object sender, FineUI.GridRowClickEventArgs e)
        {
            try
            {
                BLL.HRM.Employee m_Employee = new BLL.HRM.Employee();
                Model.HRM.Employee employee = m_Employee.Select(Convert.ToInt64(dgvEmployee.DataKeys[e.RowIndex][0]));
                if (employee != null)
                {
                    txtEmployeeID.Text = employee.EmployeeID;
                    txtEmployeeName.Text = employee.EmployeeName;
                    txtEmployeeEnglishName.Text = employee.EmployeeEnglishName;
                    cmbEmpoyeeGender.Text = employee.EmpoyeeGender;
                    txtEmployeeEmail.Text = employee.EmployeeEmail;
                    txtEmployeePhone.Text = employee.EmployeePhone;
                    txtRemark.Text = employee.Remark;

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
        protected void DgvEmployee_Sort(object sender, FineUI.GridSortEventArgs e)
        {
            string sortField = dgvEmployee.SortField;
            string sortDirection = dgvEmployee.SortDirection;

            DataView dv = dtEmployee.DefaultView;
            if (!string.IsNullOrEmpty(sortField))
            {
                dv.Sort = String.Format("{0} {1}", sortField, sortDirection);
            }

            dgvEmployee.DataSource = dv;
            dgvEmployee.DataBind();
        }
        protected void DgvDepartment_Sort(object sender, FineUI.GridSortEventArgs e)
        {
            string sortField = dgvDepartment.SortField;
            string sortDirection = dgvDepartment.SortDirection;

            DataView dv = dtEmployee.DefaultView;
            if (!string.IsNullOrEmpty(sortField))
            {
                dv.Sort = String.Format("{0} {1}", sortField, sortDirection);
            }

            dgvDepartment.DataSource = dv;
            dgvDepartment.DataBind();
        }
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                BLL.HRM.Employee m_Employee = new BLL.HRM.Employee();
                Model.HRM.Employee employee;
                if(wData.Title == GetGlobalResourceObject(strClassName, "Add").ToString())
                {
                    employee = new Model.HRM.Employee(); 
                    employee.ApplicationOrg = dgvDepartment.Rows[dgvDepartment.SelectedRow.RowIndex].Values[1].ToString();
                    employee.EmployeeID = txtEmployeeID.Text;
                    employee.EmployeeName = txtEmployeeName.Text;
                    employee.EmployeeEnglishName = txtEmployeeEnglishName.Text;
                    employee.EmpoyeeGender = cmbEmpoyeeGender.SelectedValue;
                    employee.EmployeeEmail = txtEmployeeEmail.Text;
                    employee.EmployeePhone = txtEmployeePhone.Text;
                    employee.DepartmentCode = dgvDepartment.SelectedRow.Values[2].ToString();
                    employee.CreateUserID = Util.Util.UserName;
                    employee.LastUpdateUserID= Util.Util.UserName;
                    employee.Remark = txtRemark.Text;
                    if (m_Employee.Add(employee))
                    {
                        Clear();
                        Alert.ShowInTop(GetGlobalResourceObject(strClassName, "SaveSuccessfully").ToString(), GetGlobalResourceObject("Language", "Information").ToString(), MessageBoxIcon.Information);
                        LoadEmployee(employee.ApplicationOrg, employee.DepartmentCode);
                    }
                    else
                    {
                        Alert.Show(GetGlobalResourceObject(strClassName, "SaveFailed").ToString(), GetGlobalResourceObject("Language", "Error").ToString(), MessageBoxIcon.Error);
                    }
                }
                else if (wData.Title == GetGlobalResourceObject(strClassName, "Update").ToString())
                {
                    employee = m_Employee.Select(Convert.ToInt64(dgvEmployee.DataKeys[dgvEmployee.SelectedRow.RowIndex][0]));
                    employee.ApplicationOrg = dgvDepartment.Rows[dgvDepartment.SelectedRow.RowIndex].Values[1].ToString();
                    employee.EmployeeID = txtEmployeeID.Text;
                    employee.EmployeeName = txtEmployeeName.Text;
                    employee.EmployeeEnglishName = txtEmployeeEnglishName.Text;
                    employee.EmpoyeeGender = cmbEmpoyeeGender.SelectedValue;
                    employee.EmployeeEmail = txtEmployeeEmail.Text;
                    employee.EmployeePhone = txtEmployeePhone.Text;
                    employee.DepartmentCode = dgvDepartment.SelectedRow.Values[2].ToString();
                    employee.LastUpdateUserID = Util.Util.UserName;
                    employee.Remark = txtRemark.Text;
                    if(m_Employee.Update(employee))
                    {
                        Clear();
                        Alert.ShowInTop(GetGlobalResourceObject(strClassName, "SaveSuccessfully").ToString(), GetGlobalResourceObject("Language", "Information").ToString(), MessageBoxIcon.Information);
                        LoadEmployee(employee.ApplicationOrg, employee.DepartmentCode);
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
        private void Clear()
        {
            txtEmployeeID.Text = "";
            txtEmployeeName.Text = "";
            txtEmployeeEnglishName.Text = "";
            cmbEmpoyeeGender.Text = "";
            txtEmployeeEmail.Text = "";
            txtEmployeePhone.Text = "";
            txtRemark.Text = "";
        }
        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            Clear();
            wData.Title = GetGlobalResourceObject(strClassName, "Add").ToString();
            wData.Hidden = false;
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
        protected void DgvDepartment_RowSelect(object sender, GridRowSelectEventArgs e)
        {
            LoadEmployee(dgvDepartment.SelectedRow.Values[1].ToString(), dgvDepartment.SelectedRow.Values[2].ToString());
        }
        protected void DgvEmployee_RowCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                try
                {
                    if (e.RowIndex >= 0)
                    {
                        BLL.HRM.Employee m_Employee = new BLL.HRM.Employee();
                        if (m_Employee.Delete(Convert.ToInt64(dgvEmployee.DataKeys[e.RowIndex][0]), Util.Util.UserName))
                        {
                            LoadEmployee(dgvDepartment.SelectedRow.Values[1].ToString(), dgvDepartment.SelectedRow.Values[2].ToString());
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