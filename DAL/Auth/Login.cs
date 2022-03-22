using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using DAL;
using SQLHelp;

namespace DAL.Auth
{
    public class Login : IDAL.Auth.ILogin
    {
        private const string tableName = "Auth.dbo.Login";
        /// <summary>
        /// 查询Login
        /// </summary>
        /// <returns>返回Login</returns>
        public DataTable Select()
        {
            using (DataAccess da = new DataAccess())
            {
                return da.GetDataTable("Select a.*,Employee.EmployeeName From " + tableName + " a,[HRM].[dbo].[Employee] Where a.UserName=Employee.EmployeeID");
            }
        }       
        /// <summary>
        /// 查询Login
        /// </summary>
        /// <param name="m_TenementCode">租户</param>
        /// <param name="m_UserName">UserName</param>
        /// <returns>返回Login</returns>
        public Model.Auth.Login Select(string m_UserName)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@UserName", m_UserName.Trim().ToUpper()));
                DataRow dr = da.GetDataRow("Select * From " + tableName + " Where Upper(UserName)=@UserName", da.sqlParameters, 0);
                if (dr != null)
                {
                    Model.Auth.Login login = new Model.Auth.Login();
                    login.UserName = dr["UserName"].ToString();
                    login.Password = dr["Password"].ToString();
                    login.LoginCount = Convert.ToInt32(dr["LoginCount"]);
                    login.LastLoginDate = Util.Util.ConvertToDateTime(dr["LastLoginDate"].ToString());
                    login.IsLock = Convert.ToBoolean(dr["IsLock"]);
                    login.Validity = Convert.ToInt32(dr["Validity"]);
                    login.ValidDate = Util.Util.ConvertToDateTime(dr["ValidDate"].ToString());
                    login.IsChange = Convert.ToBoolean(dr["IsChange"]);
                    login.LoginErrorCount = Convert.ToInt32(dr["LoginErrorCount"]);
                    login.LoginIP = dr["LoginIP"].ToString();
                    login.LoginComputer = dr["LoginComputer"].ToString();

                    return login;
                }
                else
                    return null;
            }
        }
        /// <summary>
        /// 增加Login
        /// </summary>
        /// <param name="m_Login">Login</param>
        /// <returns>返回增加结果</returns>
        public bool Add(Model.Auth.Login m_Login)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@UserName", m_Login.UserName));
                da.sqlParameters.Add(da.CreateSqlParameter("@Password", m_Login.Password));
                da.sqlParameters.Add(da.CreateSqlParameter("@LoginCount", m_Login.LoginCount));
                da.sqlParameters.Add(da.CreateSqlParameter("@IsLock", m_Login.IsLock));
                da.sqlParameters.Add(da.CreateSqlParameter("@Validity", m_Login.Validity));
                da.sqlParameters.Add(da.CreateSqlParameter("@IsChange", m_Login.IsChange));
                da.sqlParameters.Add(da.CreateSqlParameter("@LoginErrorCount", m_Login.LoginErrorCount));
                return da.ExecuteCommand("Insert Into " + tableName + "(UserName,Password,LoginCount,IsLock,Validity,IsChange,LoginErrorCount) Values(@UserName,@Password,@LoginCount,@IsLock,@Validity,@IsChange,@LoginErrorCount)", da.sqlParameters) != 0 ? true : false;
            }
        }
        /// <summary>
        /// 更新Login
        /// </summary>
        /// <param name="m_Login">Login</param>
        /// <returns>返回更新结果</returns>
        public bool Update(Model.Auth.Login m_Login)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@UserName", m_Login.UserName));
                da.sqlParameters.Add(da.CreateSqlParameter("@Password", m_Login.Password));
                da.sqlParameters.Add(da.CreateSqlParameter("@LoginCount", m_Login.LoginCount));
                da.sqlParameters.Add(da.CreateSqlParameter("@LastLoginDate", m_Login.LastLoginDate));
                da.sqlParameters.Add(da.CreateSqlParameter("@IsLock", m_Login.IsLock));
                da.sqlParameters.Add(da.CreateSqlParameter("@Validity", m_Login.Validity));
                da.sqlParameters.Add(da.CreateSqlParameter("@ValidDate", m_Login.ValidDate));
                da.sqlParameters.Add(da.CreateSqlParameter("@IsChange", m_Login.IsChange));
                da.sqlParameters.Add(da.CreateSqlParameter("@LoginErrorCount", m_Login.LoginErrorCount));
                da.sqlParameters.Add(da.CreateSqlParameter("@LoginIP", m_Login.LoginIP));
                da.sqlParameters.Add(da.CreateSqlParameter("@LoginComputer", m_Login.LoginComputer));
                return da.ExecuteCommand("Update " + tableName + " Set Password=@Password,LoginCount=@LoginCount,LastLoginDate=@LastLoginDate,IsLock=@IsLock,Validity=@Validity,ValidDate=@ValidDate,IsChange=@IsChange,LoginErrorCount=@LoginErrorCount,LoginIP=@LoginIP,LoginComputer=@LoginComputer Where UserName=@UserName", da.sqlParameters) != 0 ? true : false;
            }
        }
        /// <summary>
        /// 更新Login
        /// </summary>
        /// <param name="m_Login">Login</param>
        /// <param name="m_OldEmployeeID">OldEmployeeID</param>
        /// <returns>返回增加结果</returns>
        public bool Update(Model.Auth.Login m_Login, string m_OldEmployeeID)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@UserName", m_OldEmployeeID.Trim().ToUpper()));
                da.sqlParameters.Add(da.CreateSqlParameter("@OldEmployeeID", m_Login.UserName));
                da.sqlParameters.Add(da.CreateSqlParameter("@Password", m_Login.Password));
                da.sqlParameters.Add(da.CreateSqlParameter("@LoginCount", m_Login.LoginCount));
                da.sqlParameters.Add(da.CreateSqlParameter("@LastLoginDate", m_Login.LastLoginDate.ToString("yyyy-MM-dd HH:mm:ss")));
                da.sqlParameters.Add(da.CreateSqlParameter("@IsLock", m_Login.IsLock));
                da.sqlParameters.Add(da.CreateSqlParameter("@Validity", m_Login.Validity));
                da.sqlParameters.Add(da.CreateSqlParameter("@ValidDate", m_Login.ValidDate.ToString("yyyy-MM-dd HH:mm:ss")));
                da.sqlParameters.Add(da.CreateSqlParameter("@IsChange", m_Login.IsChange));
                da.sqlParameters.Add(da.CreateSqlParameter("@LoginErrorCount", m_Login.LoginErrorCount));
                da.sqlParameters.Add(da.CreateSqlParameter("@LoginIP", m_Login.LoginIP));
                da.sqlParameters.Add(da.CreateSqlParameter("@LoginComputer", m_Login.LoginComputer));
                return da.ExecuteCommand("Update " + tableName + " Set UserName=@UserName,Password=@Password,LoginCount=@LoginCount,LastLoginDate=@LastLoginDate,IsLock=@IsLock,Validity=@Validity,ValidDate=@ValidDate,IsChange=@IsChange,LoginErrorCount=@LoginErrorCount,LoginIP=@LoginIP,LoginComputer=@LoginComputer Where UserName=@OldEmployeeID", da.sqlParameters) != 0 ? true : false;
            }
        }
        /// <summary>
        /// 删除Login
        /// </summary>
        /// <param name="m_UserName">UserName</param>
        /// <returns>返回删除结果</returns>
        public bool Delete(string m_UserName)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@UserName", m_UserName.Trim().ToUpper()));
                return da.ExecuteCommand("Update " + tableName + " Set IsLock=1 Where Upper(UserName)=@UserName", da.sqlParameters) != 0 ? true : false;
            }
        }
        /// <summary>
        /// 获得该账号信息
        /// </summary>
        /// <param name="m_UserName">UserName</param>
        /// <returns>账号信息</returns>
        public DataTable GetLogin(string m_UserName)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                string strWhere = " Where 1=1";
                if (!string.IsNullOrEmpty(m_UserName))
                {
                    da.sqlParameters.Add(da.CreateSqlParameter("@UserName", m_UserName.Trim().ToUpper()));
                    strWhere += " And Upper(UserName)=@UserName";
                }
                return da.GetDataTable("Select * From " + tableName + strWhere, da.sqlParameters);
            }
        }
        /// <summary>
        /// 是否存在该账号
        /// </summary>
        /// <param name="m_UserName">UserName</param>
        /// <returns>返回结果</returns>
        public bool IsExist(string m_UserName)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@UserName", m_UserName.Trim().ToUpper()));
                object isExist = da.GetScalar("Select Count(UserName) From " + tableName + " Where Upper(UserName)=@UserName", da.sqlParameters);
                if (isExist != null)
                    return Convert.ToInt32(isExist) > 0 ? true : false;
                else
                {
                    Exception ex = new Exception("Not find UserName=" + m_UserName + " information");
                    ex.Source = "Class name:" + tableName + ",Error method:public bool IsExist(m_UserName=" + m_UserName + ")";
                    throw ex;
                }
            }
        }
        /// <summary>
        /// 密码延期
        /// </summary>
        /// <param name="m_UserName">UserName</param>
        /// <returns></returns>
        public bool Postpone(string m_UserName)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@UserName", m_UserName.Trim().ToUpper()));
                return da.ExecuteCommand("Update " + tableName + " set ValidDate=GETDATE()+3 where Upper(UserName)=@UserName", da.sqlParameters) != 0 ? true : false;
            }
        }
    }
}
