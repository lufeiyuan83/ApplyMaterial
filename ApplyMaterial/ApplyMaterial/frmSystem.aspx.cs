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
    public partial class frmSystem : System.Web.UI.Page
    {
        private static DataTable dtDataSource;
        private void LoadData(string m_SystemName)
        {
            try
            {
                BLL.ApplyMaterial.System m_System = new BLL.ApplyMaterial.System();
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
            pEdit.Title = "新增";
            pEdit.Hidden = false;
        }
        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                if (pEdit.Title == "新增")
                {
                    BLL.ApplyMaterial.System m_System = new BLL.ApplyMaterial.System();
                    if (m_System.IsExist(txtSystemCode.Text.Trim()))
                    {
                        txtSystemCode.Focus();
                        Alert.Show("该系统代码已经存在", "警告", MessageBoxIcon.Warning);
                        return;
                    }

                    Model.ApplyMaterial.System system = new Model.ApplyMaterial.System();
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
                        Alert.Show("新增失败", "错误",  MessageBoxIcon.Error);
                }
                else if (pEdit.Title == "修改" && dgvData.SelectedRow!=null)
                {
                    BLL.ApplyMaterial.System m_System = new BLL.ApplyMaterial.System();
                    if (txtSystemCode.Text.Trim().ToUpper() != dgvData.SelectedRow.Values[2].ToString().ToUpper())
                    {
                        txtSystemCode.Focus();
                        if (m_System.IsExist(txtSystemCode.Text.Trim()))
                        {
                            Alert.Show("该系统代码已经存在", "警告", MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    Model.ApplyMaterial.System system = m_System.Select(Convert.ToInt64(dgvData.DataKeys[dgvData.SelectedRow.RowIndex][0]));
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
                        BLL.ApplyMaterial.System m_System = new BLL.ApplyMaterial.System();
                        if (m_System.Delete(Convert.ToInt64(dgvData.DataKeys[e.RowIndex][0]), Util.Util.UserName))
                        {
                            LoadData("");
                            Alert.Show("删除成功", "提示",MessageBoxIcon.Information);
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

            txtSystemCode.Text = dgvData.Rows[e.RowIndex].Values[2].ToString();
            txtSystemName.Text = dgvData.Rows[e.RowIndex].Values[3].ToString();
            txtSystemDescription.Text = dgvData.Rows[e.RowIndex].Values[4].ToString();
            txtRemark.Text = dgvData.Rows[e.RowIndex].Values[7].ToString();

            pEdit.Title = "修改";
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