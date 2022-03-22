using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BLL.Tenement
{
    public class Login
    {
        private readonly IDAL.Tenement.ILogin dal = DALFactory.DALFactory.GetInstance().GetLogin();
        /// <summary>
        /// 查询Login
        /// </summary>
        /// <returns>返回Login</returns>
        public DataTable Select()
        {
            return dal.Select();
        }
        /// <summary>
        /// 查询Login
        /// </summary>
        /// <param name="m_UserName">账号</param>
        /// <returns>返回Login</returns>
        public Model.Tenement.Login Select(string m_UserName)
        {
            return dal.Select(m_UserName);
        }
        /// <summary>
        /// 增加Login
        /// </summary>
        /// <param name="m_Login">Login</param>
        /// <returns>返回增加结果</returns>
        public bool Add(Model.Tenement.Login m_Login)
        {
            return dal.Add(m_Login);
        }
        /// <summary>
        /// 更新Login
        /// </summary>
        /// <param name="m_Login">Login</param>
        /// <returns>返回更新结果</returns>
        public bool Update(Model.Tenement.Login m_Login)
        {
            return dal.Update(m_Login);
        }
        /// <summary>
        /// 更新Login
        /// </summary>
        /// <param name="m_Login">Login</param>
        /// <param name="m_OldUserName">OldUserName</param>
        /// <returns>返回增加结果</returns>
        public bool Update(Model.Tenement.Login m_Login, string m_OldUserName)
        {
            return dal.Update(m_Login, m_OldUserName);
        }
        /// <summary>
        /// 删除Login
        /// </summary>
        /// <param name="m_UserName">UserName</param>
        /// <returns>返回删除结果</returns>
        public bool Delete(string m_UserName)
        {
            return dal.Delete(m_UserName);
        }
        /// <summary>
        /// 获得该账号信息
        /// </summary>
        /// <param name="m_UserName">UserName</param>
        /// <returns>账号信息</returns>
        public DataTable GetLogin(string m_UserName)
        {
            return dal.GetLogin(m_UserName);
        }
        /// <summary>
        /// 是否存在该账号
        /// </summary>
        /// <param name="m_UserName">UserName</param>
        /// <returns>返回结果</returns>
        public bool IsExist(string m_UserName)
        {
            return dal.IsExist(m_UserName);
        }
        /// <summary>
        /// 密码延期
        /// </summary>
        /// <param name="m_UserName">UserName</param>
        /// <returns></returns>
        public bool Postpone(string m_UserName)
        {
            return dal.Postpone(m_UserName);
        }
    }
}
