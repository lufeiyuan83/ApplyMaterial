using FineUI;
using System;
using System.Data;
using System.Web;

namespace ERP
{
    public partial class Login : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                System.Windows.Forms.Screen screen = System.Windows.Forms.Screen.PrimaryScreen; 
                int width = screen.WorkingArea.Width;
                int height = screen.WorkingArea.Height;
                Window1.Left = Convert.ToInt32(width * 0.7);
                Window1.Top = Convert.ToInt32(height * 0.3);
                if (!Object.Equals(Request.Cookies["UserName"], null))
                {
                    //创建一个Cookie对象，实现记住用户名
                    HttpCookie userName = Request.Cookies["UserName"];
                    txtUserName.Text = userName.Value;
                    txtPassword.Focus();
                }
                else
                {
                    txtUserName.Focus();
                }
                InitCaptchaCode();
            }
        }
        /// <summary>
        /// 初始化验证码
        /// </summary>
        private void InitCaptchaCode()
        {
            // 创建一个 6 位的随机数并保存在 Session 对象中
            string s = String.Empty;
            Random random = new Random();
            for (int i = 0; i < 6; i++)
            {
                s += random.Next(10).ToString();
            }
            Session["CaptchaImageText"] = s;
            imgCaptcha.ImageUrl = "~/Captcha/Captcha.ashx?w=100&h=30&t=" + DateTime.Now.Ticks;
        }
        protected void BtnRefresh_Click(object sender, EventArgs e)
        {
            InitCaptchaCode();
        }
        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            if (Session["CaptchaImageText"] == null)
                InitCaptchaCode();
            if (txtCaptcha.Text != Session["CaptchaImageText"].ToString())
            {
                Alert.ShowInTop(GetLocalResourceObject("CaptchaIsError").ToString(), GetGlobalResourceObject("Language", "Warning").ToString());
                return;
            }

            try
            {
                BLL.Auth.Login m_Login = new BLL.Auth.Login();
                Model.Auth.Login login = m_Login.Select(txtUserName.Text.Trim().ToUpper());
                if (login != null)
                {
                    #region 该账号是否被锁定
                    if (login.IsLock)
                    {
                        Alert.ShowInTop(GetLocalResourceObject("AccountIsLocked").ToString(), GetGlobalResourceObject("Language", "Warning").ToString(), MessageBoxIcon.Error);
                        return;
                    }
                    #endregion
                    if (login.Password == Util.MD5.MDString(txtPassword.Text))
                    {
                        txtPassword.Text = "";
                        Util.Util.SystemCode = "ERP";
                        Util.Util.UserName = txtUserName.Text.Trim();
                        HttpCookie UserName = new HttpCookie("UserName")
                        {
                            Value = txtUserName.Text.Trim(),
                            Expires = DateTime.MaxValue
                        };
                        Response.AppendCookie(UserName);                        
                        //System.Web.Security.FormsAuthentication.RedirectFromLoginPage(Util.Util.UserName, false);
                        System.Web.Security.FormsAuthentication.SetAuthCookie(Util.Util.UserName, false);
                        Response.Redirect("Default.aspx", false);
                    }
                    else if (login.LoginErrorCount < 4)
                    {
                        login.LoginErrorCount += 1;
                        m_Login.Update(login);
                        txtPassword.Focus();
                        Alert.ShowInTop(GetLocalResourceObject("UsernameOrPasswordIncorrect").ToString(), GetGlobalResourceObject("Language", "Error").ToString(), MessageBoxIcon.Error);
                        txtPassword.Text = "";
                    }
                    else
                    {
                        login.IsLock = true;
                        login.LoginErrorCount += 1;
                        m_Login.Update(login);
                        Alert.ShowInTop(GetLocalResourceObject("TooManyError").ToString(), GetGlobalResourceObject("Language", "Error").ToString(), MessageBoxIcon.Error);
                    }
                }
                else
                {
                    Alert.ShowInTop(GetLocalResourceObject("UsernameOrPasswordIncorrect").ToString(), GetGlobalResourceObject("Language", "Error").ToString(), MessageBoxIcon.Error);
                    txtPassword.Text = "";
                }
            }
            catch (Exception ex)
            {
                BLL.Global.WriteLog(ex, this.ClientTarget, Util.LogType.Exception.ToString(), new System.Diagnostics.StackTrace(true).GetFrame(0).GetMethod().ToString(), txtUserName.Text.Trim().ToUpper(), Environment.MachineName, "Line:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileLineNumber().ToString() + " Column:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileColumnNumber().ToString());
                Alert.ShowInTop(ex.Message, GetGlobalResourceObject("Language", "Error").ToString(), MessageBoxIcon.Error);
            }
        }
        protected void BtnForgotPassword_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUserName.Text.Trim().Length > 0)
                {
                    BLL.HRM.Employee m_Employee = new BLL.HRM.Employee();
                    DataTable dtEmployee = m_Employee.Select(txtUserName.Text.Trim());
                    if (dtEmployee != null && dtEmployee.Rows.Count > 0)
                    {
                        if (dtEmployee.Rows[0]["EmployeeEmail"].ToString().Length>0)
                            txtEmail.Text = dtEmployee.Rows[0]["EmployeeEmail"].ToString();
                        else
                            Alert.ShowInTop(GetLocalResourceObject("NoConfigEmail").ToString(), GetGlobalResourceObject("Language", "Error").ToString(), MessageBoxIcon.Error);
                    }
                }
                else
                    Alert.ShowInTop(GetLocalResourceObject("InputUsername").ToString(), GetGlobalResourceObject("Language", "Warning").ToString(), MessageBoxIcon.Warning);
                wForgotPassword.Hidden = false;
            }
            catch (Exception ex)
            {
                BLL.Global.WriteLog(ex, this.ClientTarget, Util.LogType.Exception.ToString(), new System.Diagnostics.StackTrace(true).GetFrame(0).GetMethod().ToString(), txtUserName.Text.Trim().ToUpper(), Environment.MachineName, "Line:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileLineNumber().ToString() + " Column:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileColumnNumber().ToString());
                Alert.ShowInTop(ex.Message, GetGlobalResourceObject("Language", "Error").ToString(), MessageBoxIcon.Error);
            }
        }
        protected void BtnSendCiphertext_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtEmail.Text.Trim().Length > 0)
                {
                    string strCryptograph = System.Guid.NewGuid().ToString();
                    if (Util.MailSending.Send(txtEmail.Text.Trim(), "", GetLocalResourceObject("AMSFindPassword").ToString(),string.Format(GetLocalResourceObject("YourSecretKey").ToString(), strCryptograph)))
                    {
                        BLL.Auth.Ciphertext m_Ciphertext = new BLL.Auth.Ciphertext();
                        Model.Auth.Ciphertext ciphertext = new Model.Auth.Ciphertext 
                        { 
                            SystemName = "ERP",
                            EmployeeID = txtUserName.Text.Trim(),
                            Cryptograph = strCryptograph
                        };
                        if (m_Ciphertext.Add(ciphertext))
                            Alert.ShowInTop(GetLocalResourceObject("SendSuccessfully").ToString(), GetGlobalResourceObject("Language", "Error").ToString(), MessageBoxIcon.Error);
                    }
                    else
                        Alert.ShowInTop(GetLocalResourceObject("SendFailed").ToString(), GetGlobalResourceObject("Language", "Error").ToString(), MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                BLL.Global.WriteLog(ex, this.ClientTarget, Util.LogType.Exception.ToString(), new System.Diagnostics.StackTrace(true).GetFrame(0).GetMethod().ToString(), txtUserName.Text.Trim().ToUpper(), Environment.MachineName, "Line:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileLineNumber().ToString() + " Column:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileColumnNumber().ToString());
                Alert.ShowInTop(ex.Message, GetGlobalResourceObject("Language", "Error").ToString(), MessageBoxIcon.Error);
            }
        }
        protected void BtnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCiphertext.Text.Trim().Length > 0 && txtNewPassword.Text.Length > 0 && txtConfirmPassword.Text.Length > 0)
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
                    BLL.Auth.Ciphertext m_Ciphertext = new BLL.Auth.Ciphertext();
                    string strCiphertext = m_Ciphertext.GetCiphertext("ERP", txtUserName.Text.Trim());
                    if (strCiphertext == txtCiphertext.Text.Trim())
                    {
                        BLL.Auth.Login m_Login = new BLL.Auth.Login();
                        Model.Auth.Login login = m_Login.Select(txtUserName.Text.Trim());
                        if (login != null)
                        {
                            login.Password = Util.MD5.MDString(txtNewPassword.Text);
                            login.ValidDate = DateTime.Now.AddDays(login.Validity);
                            if (m_Login.Update(login))
                            {
                                m_Ciphertext.Delete("ERP", txtUserName.Text.Trim());
                                txtCiphertext.Text = "";
                                txtNewPassword.Text = "";
                                txtConfirmPassword.Text = "";
                                Alert.Show(GetLocalResourceObject("UpdateSuccessfully").ToString(), GetGlobalResourceObject("Language", "Information").ToString(), MessageBoxIcon.Information);
                            }
                            else
                            {
                                Alert.Show(GetLocalResourceObject("UpdateFailed").ToString(), GetGlobalResourceObject("Language", "Error").ToString(), MessageBoxIcon.Error);
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
                BLL.Global.WriteLog(ex, this.ClientTarget, Util.LogType.Exception.ToString(), new System.Diagnostics.StackTrace(true).GetFrame(0).GetMethod().ToString(), txtUserName.Text.Trim().ToUpper(), Environment.MachineName, "Line:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileLineNumber().ToString() + " Column:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileColumnNumber().ToString());
                Alert.ShowInTop(ex.Message, GetGlobalResourceObject("Language", "Error").ToString(), MessageBoxIcon.Error);
            }
        }
    }
}
