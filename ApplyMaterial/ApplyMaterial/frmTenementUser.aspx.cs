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
    public partial class frmTenementUser : System.Web.UI.Page
    {
        private static DataTable dtDataSource;
        private void LoadData(string m_UserName)
        {
            try
            {
                BLL.ApplyMaterial.Login m_Login = new BLL.ApplyMaterial.Login();
                DataTable dtLogin = m_Login.GetLogin(Util.Util.TenementCode, m_UserName);
                cmbEUserName.DataTextField = "UserName";
                cmbEUserName.DataValueField = "UserName";
                cmbEUserName.DataSource = dtLogin;
                cmbEUserName.DataBind();

                dtDataSource = dtLogin;
                dgvData.DataSource = dtLogin;
                dgvData.DataBind();
            }
            catch (Exception ex)
            {
                Alert.Show(ex.Message, "错误", MessageBoxIcon.Error);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData("");
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData(cmbEUserName.SelectedValue);
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            cmbEUserName.SelectedIndex = -1;
        }
        protected void dgvData_RowCommand(object sender, FineUI.GridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                try
                {
                    if (dgvData.Rows[e.RowIndex].Values[0] != null)
                    {
                        BLL.ApplyMaterial.Login m_Login = new BLL.ApplyMaterial.Login();
                        if (m_Login.Delete(Util.Util.TenementCode,dgvData.SelectedRow.Values[2].ToString()))
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
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            txtUserName.Text = "";
            pEdit.Title = "新增";
            pEdit.Hidden = false;
        }
        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                if (pEdit.Title == "新增")
                {
                    BLL.ApplyMaterial.Login m_Login = new BLL.ApplyMaterial.Login();
                    if (m_Login.IsExist(Util.Util.TenementCode, txtUserName.Text.Trim()))
                    {
                        Alert.Show("该账号已经存在", "警告", MessageBoxIcon.Warning);
                        return;
                    }

                    Model.ApplyMaterial.Login login = new Model.ApplyMaterial.Login();
                    login.TenementCode = Util.Util.TenementCode;
                    login.UserName = txtUserName.Text.Trim();
                    login.Password = Util.MD5.MDString(txtUserName.Text.Trim());
                    if (m_Login.Add(login))
                    {
                        LoadData("");
                        txtUserName.Text = "";
                        pEdit.Hidden = true;
                    }
                    else
                        Alert.Show("新增失败", "错误", MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                BLL.Global.WriteLog(ex, this.ClientTarget, Util.LogType.Exception.ToString(), new System.Diagnostics.StackTrace(true).GetFrame(0).GetMethod().ToString(), Util.Util.UserName, Environment.MachineName, "Line:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileLineNumber().ToString() + " Column:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileColumnNumber().ToString());
                Alert.Show(ex.Message, "错误", MessageBoxIcon.Error);
            }
        }
        protected void btnLocked_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvData.SelectedRow != null)
                {
                    BLL.ApplyMaterial.Login m_Login = new BLL.ApplyMaterial.Login();
                    Model.ApplyMaterial.Login login = m_Login.Select(Util.Util.TenementCode, dgvData.SelectedRow.Values[2].ToString());
                    if (login.IsLock)
                    {
                        Alert.Show("该账号已经是锁定状态，无需锁定", "警告", MessageBoxIcon.Warning);
                        return;
                    }
                    login.IsLock = true;
                    if (m_Login.Update(login))
                    {
                        LoadData("");
                        Alert.Show("该账号已经锁定", "提示", MessageBoxIcon.Information);
                    }
                    else
                        Alert.Show("锁定失败", "错误", MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                BLL.Global.WriteLog(ex, this.ClientTarget, Util.LogType.Exception.ToString(), new System.Diagnostics.StackTrace(true).GetFrame(0).GetMethod().ToString(), Util.Util.UserName, Environment.MachineName, "Line:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileLineNumber().ToString() + " Column:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileColumnNumber().ToString());
                Alert.Show(ex.Message, "错误", MessageBoxIcon.Error);
            }
        }
        protected void btnUnlocked_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvData.SelectedRow != null)
                {
                    BLL.ApplyMaterial.Login m_Login = new BLL.ApplyMaterial.Login();
                    Model.ApplyMaterial.Login login = m_Login.Select(Util.Util.TenementCode, dgvData.SelectedRow.Values[2].ToString());
                    if (!login.IsLock)
                    {
                        Alert.Show("该账号已经是解锁状态，无需解锁", "警告", MessageBoxIcon.Warning);
                        return;
                    }
                    login.IsLock = false;
                    if (m_Login.Update(login))
                    {
                        LoadData("");
                        Alert.Show("该账号已经解锁", "提示", MessageBoxIcon.Information);
                    }
                    else
                        Alert.Show("解锁失败", "错误", MessageBoxIcon.Error);
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