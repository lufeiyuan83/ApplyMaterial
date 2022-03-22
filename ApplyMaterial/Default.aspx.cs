using FineUI;
using System;
using System.Data;

namespace ERP
{
    public partial class Default : BasePage
    {
        private const string title = "ERP";
        private void LoadData()
        {
            try
            {
                BLL.Auth.SysMenu m_SysMenu = new BLL.Auth.SysMenu();
                DataTable dtSysMenu = m_SysMenu.Select(title);

                DataSet ds = new DataSet();
                ds.Tables.Add(dtSysMenu);
                _ = ds.Relations.Add("TreeRelation", ds.Tables[0].Columns["id"], ds.Tables[0].Columns["ParentId"]);
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    if (row["Id"].ToString() == row["ParentId"].ToString())
                    {
                        if(Request.Cookies["Language"].Value.ToString()== "zh_CN")
                        { 
                            FineUI.TreeNode node = new FineUI.TreeNode
                            {
                                NodeID = row["Id"].ToString(),
                                Text = row["MenuName"].ToString(),
                                NavigateUrl = row["NavigateUrl"].ToString(),
                                IconUrl = row["Icon"].ToString(),
                                Expanded = Convert.ToBoolean(row["Expanded"]),
                                EnableClickEvent = true
                            };
                            leftMenuTree.Nodes.Add(node);
                            ResolveSubTree(row, node);
                        }
                        else if (Request.Cookies["Language"].Value.ToString() == "zh_TW")
                        {
                            FineUI.TreeNode node = new FineUI.TreeNode
                            {
                                NodeID = row["Id"].ToString(),
                                Text = row["MenuNameTh"].ToString(),
                                NavigateUrl = row["NavigateUrl"].ToString(),
                                IconUrl = row["Icon"].ToString(),
                                Expanded = Convert.ToBoolean(row["Expanded"]),
                                EnableClickEvent = true
                            };
                            leftMenuTree.Nodes.Add(node);
                            ResolveSubTree(row, node);
                        }
                        else if (Request.Cookies["Language"].Value.ToString() == "en")
                        {
                            FineUI.TreeNode node = new FineUI.TreeNode
                            {
                                NodeID = row["Id"].ToString(),
                                Text = row["MenuNameEn"].ToString(),
                                NavigateUrl = row["NavigateUrl"].ToString(),
                                IconUrl = row["Icon"].ToString(),
                                Expanded = Convert.ToBoolean(row["Expanded"]),
                                EnableClickEvent = true
                            };
                            leftMenuTree.Nodes.Add(node);
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
                    if (row["Id"].ToString() != row["ParentId"].ToString())
                    {
                        if (Request.Cookies["Language"].Value.ToString() == "zh_CN")
                        { 
                            FineUI.TreeNode node = new FineUI.TreeNode
                            {
                                NodeID = row["Id"].ToString(),
                                Text = row["MenuName"].ToString(),
                                NavigateUrl = row["NavigateUrl"].ToString(),
                                IconUrl = row["Icon"].ToString(),
                                Expanded = Convert.ToBoolean(row["Expanded"]),
                                EnableClickEvent = true
                            };
                            treeNode.Nodes.Add(node);

                            ResolveSubTree(row, node);
                        }
                        else if (Request.Cookies["Language"].Value.ToString() == "zh_TW")
                        {
                            FineUI.TreeNode node = new FineUI.TreeNode
                            {
                                NodeID = row["Id"].ToString(),
                                Text = row["MenuNameTh"].ToString(),
                                NavigateUrl = row["NavigateUrl"].ToString(),
                                IconUrl = row["Icon"].ToString(),
                                Expanded = Convert.ToBoolean(row["Expanded"]),
                                EnableClickEvent = true
                            };
                            treeNode.Nodes.Add(node);

                            ResolveSubTree(row, node);
                        }
                        else if (Request.Cookies["Language"].Value.ToString() == "en")
                        {
                            FineUI.TreeNode node = new FineUI.TreeNode
                            {
                                NodeID = row["Id"].ToString(),
                                Text = row["MenuNameEn"].ToString(),
                                NavigateUrl = row["NavigateUrl"].ToString(),
                                IconUrl = row["Icon"].ToString(),
                                Expanded = Convert.ToBoolean(row["Expanded"]),
                                EnableClickEvent = true
                            };
                            treeNode.Nodes.Add(node);

                            ResolveSubTree(row, node);
                        }
                    }
                }
            }
        }
        private void LoadToDoList(string m_EmployeeID)
        {
            try
            {
                BLL.PLM.WorkList m_WorkList = new BLL.PLM.WorkList();
                DataTable dtWorkList = m_WorkList.Select(title, m_EmployeeID, "0");
                if (dtWorkList != null && dtWorkList.Rows.Count > 0)
                {
                    for (int i = 0; i < dtWorkList.Rows.Count; i++)
                    {
                        FineUI.TreeNode tNode = new FineUI.TreeNode
                        {
                            NodeID = dtWorkList.Rows[i]["id"].ToString(),
                            Text = dtWorkList.Rows[i]["Title"].ToString(),
                            NavigateUrl = dtWorkList.Rows[i]["URL"].ToString() + "?Status=Processing",
                            Icon = FineUI.Icon.Mail,
                            EnableClickEvent = true
                        };
                        tToDoList.Nodes.Add(tNode);
                    }
                }
            }
            catch (Exception ex)
            {
                BLL.Global.WriteLog(ex, this.ClientTarget, Util.LogType.Exception.ToString(), new System.Diagnostics.StackTrace(true).GetFrame(0).GetMethod().ToString(), Util.Util.UserName, Environment.MachineName, "Line:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileLineNumber().ToString() + " Column:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileColumnNumber().ToString());
                Alert.ShowInTop(ex.Message, GetGlobalResourceObject("Language", "Error").ToString(), MessageBoxIcon.Error);
            }
        }
        private void LoadDone(string m_EmployeeID)
        {
            try
            {
                BLL.PLM.WorkList m_WorkList = new BLL.PLM.WorkList();
                DataTable dtWorkList = m_WorkList.Select(title, m_EmployeeID, "1");
                if (dtWorkList != null && dtWorkList.Rows.Count > 0)
                {
                    for (int i = 0; i < dtWorkList.Rows.Count; i++)
                    {
                        FineUI.TreeNode tNode = new FineUI.TreeNode
                        {
                            NodeID = dtWorkList.Rows[i]["id"].ToString(),
                            Text = dtWorkList.Rows[i]["Title"].ToString(),
                            NavigateUrl = dtWorkList.Rows[i]["URL"].ToString() + "?MaterialId=" + dtWorkList.Rows[i]["FormId"].ToString(),
                            Icon = FineUI.Icon.Mail,
                            EnableClickEvent = true
                        };
                        tDone.Nodes.Add(tNode);
                    }
                }
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
                if (!Equals(Request.Cookies["UserName"], null))
                {
                    lblLogon.Text = Util.Util.UserName;
                    LoadData();
                    LoadToDoList(Util.Util.UserName);
                    LoadDone(Util.Util.UserName);
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
                BtnOK.OnClientClick = wChangePassword.GetHideReference();
            }
        }
        protected void BtnExit_Click(object sender, EventArgs e)
        {
            System.Web.Security.FormsAuthentication.SignOut();
            System.Web.Security.FormsAuthentication.RedirectToLoginPage();
        }
        protected void BtnChangePassword_Click(object sender, EventArgs e)
        {
            wChangePassword.Hidden = false;
        }
        protected void BtnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtOldPassword.Text.Length>0 && txtNewPassword.Text.Length > 0 && txtConfirmPassword.Text.Length > 0)
                {
                    if (txtNewPassword.Text != txtConfirmPassword.Text)
                    {
                        Alert.Show(GetLocalResourceObject("NewPasswordIsNotEqualedToConfirm").ToString(), GetGlobalResourceObject("Language", "Error").ToString(), MessageBoxIcon.Error);
                        return;
                    }
                    if (txtNewPassword.Text.Length < 8)
                    {
                        Alert.Show(GetLocalResourceObject("PasswordLength").ToString(), GetGlobalResourceObject("Language", "Error").ToString(), MessageBoxIcon.Error);
                        return;
                    }
                    if (!System.Text.RegularExpressions.Regex.IsMatch(txtNewPassword.Text, @"[0-9]+") || !System.Text.RegularExpressions.Regex.IsMatch(txtNewPassword.Text, @"[A-Za-z]+"))
                    {
                        Alert.Show(GetLocalResourceObject("PasswordMustContainCharOrNumber").ToString(), GetGlobalResourceObject("Language", "Error").ToString(), MessageBoxIcon.Error);
                        return;
                    }
                    BLL.Auth.Login m_Login = new BLL.Auth.Login();
                    Model.Auth.Login login = m_Login.Select(Util.Util.UserName);
                    if (login != null)
                    {
                        if (login.Password == Util.MD5.MDString(txtOldPassword.Text))
                        {
                            login.Password = Util.MD5.MDString(txtNewPassword.Text);
                            login.ValidDate = DateTime.Now.AddDays(login.Validity);
                            if (m_Login.Update(login))
                            {
                                txtOldPassword.Text = "";
                                txtNewPassword.Text = "";
                                txtConfirmPassword.Text = "";
                                Alert.Show(GetLocalResourceObject("UpdateSuccessful").ToString(), GetGlobalResourceObject("Language", "Information").ToString(), MessageBoxIcon.Information);
                            }
                            else
                            {
                                Alert.Show(GetLocalResourceObject("UpdateFailure").ToString(), GetGlobalResourceObject("Language", "Error").ToString(), MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            Alert.Show(GetLocalResourceObject("OldPasswordIncorrect").ToString(), GetGlobalResourceObject("Language", "Error").ToString(), MessageBoxIcon.Error);
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
    }
}