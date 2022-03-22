using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace IDAL.Auth
{
    public interface ILogin
    {
        /// <summary>
        /// 查询Login
        /// </summary>
        /// <returns>返回Login</returns>
        DataTable Select();
        /// <summary>
        /// 查询Login
        /// </summary>
        /// <param name="m_UserName">UserName</param>
        /// <returns>返回Login</returns>
        Model.Auth.Login Select(string m_UserName);
        /// <summary>
        /// 增加Login
        /// </summary>
        /// <param name="m_Login">Login</param>
        /// <returns>返回增加结果</returns>
        bool Add(Model.Auth.Login m_Login);
        /// <summary>
        /// 更新Login
        /// </summary>
        /// <param name="m_Login">Login</param>
        /// <returns>返回增加结果</returns>
        bool Update(Model.Auth.Login m_Login);
        /// <summary>
        /// 更新Login
        /// </summary>
        /// <param name="m_Login">Login</param>
        /// <param name="m_OldUserName">OldUserName</param>
        /// <returns>返回增加结果</returns>
        bool Update(Model.Auth.Login m_Login, string m_OldUserName);
        /// <summary>
        /// 删除Login
        /// </summary>
        /// <param name="m_UserName">UserName</param>
        /// <returns>返回删除结果</returns>
        bool Delete(string m_UserName);
        /// <summary>
        /// 获得该账号信息
        /// </summary>
        /// <param name="m_UserName">UserName</param>
        /// <returns>账号信息</returns>
        DataTable GetLogin(string m_UserName);
        /// <summary>
        /// 是否存在该账号
        /// </summary>
        /// <param name="m_UserName">UserName</param>
        /// <returns>返回结果</returns>
        bool IsExist(string m_UserName);
        /// <summary>
        /// 密码延期
        /// </summary>
        /// <param name="m_UserName">UserName</param>
        /// <returns></returns>
        bool Postpone(string m_UserName);
    }
}
