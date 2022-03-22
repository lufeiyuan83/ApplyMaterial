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
    public partial class Sys : BasePage
    {
        private readonly string strClassName = new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().ReflectedType.Name + ".aspx";
        private static DataTable dtDataSource;
        private void LoadData(string m_SystemName)
        {
            try
            {
                BLL.Auth.System m_System = new BLL.Auth.System();
                cmbESystemName.DataTextField = "SystemName";
                cmbESystemName.DataValueField = "SystemName";
                cmbESystemName.DataSource = m_System.Select();
                cmbESystemName.DataBind();

                dtDataSource = m_System.GetSystem(m_SystemName);
                dgvData.DataSource = dtDataSource;
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
                LoadData("");
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData(cmbESystemName.SelectedValue);
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            cmbESystemName.SelectedIndex = -1;
        }
        private void Clear()
        {
            txtSystemCode.Text = "";
            txtSystemName.Text = "";
            txtSystemDescription.Text = "";
            txtRemark.Text = "";
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Clear();
            pEdit.Title = GetGlobalResourceObject(strClassName, "Add").ToString();
            pEdit.Hidden = false;
        }
        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                BLL.Auth.System m_System = new BLL.Auth.System();
                if (pEdit.Title == GetGlobalResourceObject(strClassName, "Add").ToString())
                {
                    if (m_System.IsExist(txtSystemCode.Text.Trim()))
                    {
                        txtSystemCode.Focus();
                        Alert.Show(GetGlobalResourceObject(strClassName, "SystemCodeExist").ToString(), GetGlobalResourceObject("Language", "Warning").ToString(), MessageBoxIcon.Warning);
                        return;
                    }

                    Model.Auth.System system = new Model.Auth.System();
                    system.SystemCode = txtSystemCode.Text.Trim();
                    system.SystemName = txtSystemName.Text.Trim();
                    system.SystemDescription = txtSystemDescription.Text.Trim();
                    system.IsDeleted = false;
                    system.CreateUserID = Util.Util.UserName;
                    system.LastUpdateUserID = Util.Util.UserName;
                    system.Remark = txtRemark.Text;
                    if (m_System.Add(system))
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
                    if (txtSystemCode.Text.Trim().ToUpper() != dgvData.SelectedRow.Values[2].ToString().ToUpper())
                    {
                        txtSystemCode.Focus();
                        if (m_System.IsExist(txtSystemCode.Text.Trim()))
                        {
                            Alert.Show(GetGlobalResourceObject(strClassName, "SystemCodeExist").ToString(), GetGlobalResourceObject("Language", "Warning").ToString(), MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    Model.Auth.System system = m_System.Select(Convert.ToInt64(dgvData.DataKeys[dgvData.SelectedRow.RowIndex][0]));
                    system.SystemCode = txtSystemCode.Text.Trim();
                    system.SystemName = txtSystemName.Text.Trim();
                    system.SystemDescription = txtSystemDescription.Text.Trim();
                    system.LastUpdateUserID = Util.Util.UserName;
                    system.Remark = txtRemark.Text.Trim();

                    if (m_System.Update(system))
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
        protected void dgvData_RowCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                try
                {
                    if (e.RowIndex >= 0)
                    {
                        BLL.Auth.System m_System = new BLL.Auth.System();                        
                        if (m_System.Delete(Convert.ToInt64(dgvData.DataKeys[e.RowIndex][0]), Util.Util.UserName))
                        {
                            LoadData("");
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
            Clear();

            txtSystemCode.Text = dgvData.Rows[e.RowIndex].Values[2].ToString();
            txtSystemName.Text = dgvData.Rows[e.RowIndex].Values[3].ToString();
            txtSystemDescription.Text = dgvData.Rows[e.RowIndex].Values[4].ToString();
            txtRemark.Text = dgvData.Rows[e.RowIndex].Values[7].ToString();

            pEdit.Title = GetGlobalResourceObject(strClassName, "Update").ToString();
            pEdit.Hidden = false;
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