using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDAL.Auth
{
    public interface ICiphertext
    {
        /// <summary>
        /// 查询单条Ciphertext
        /// </summary>
        /// <param name="m_SystemName">系统名</param>
        /// <param name="m_Cryptograph">密文</param>
        /// <returns>AdditionalItems</returns>
        Model.Auth.Ciphertext Select(string m_SystemName, string m_Cryptograph);
        /// <summary>
        /// 获得密文
        /// </summary>
        /// <param name="m_SystemName">系统名</param>
        /// <param name="m_EmployeeID">工号</param>
        /// <returns></returns>
        string GetCiphertext(string m_SystemName, string m_EmployeeID);
        /// <summary>
        /// 是否存在该员工的密文
        /// </summary>
        /// <param name="m_SystemName">系统名</param>
        /// <param name="m_EmployeeID">EmployeeID</param>
        /// <returns></returns>
        bool IsExist(string m_SystemName, string m_EmployeeID);
        /// <summary>
        /// 增加Ciphertext
        /// </summary>
        /// <param name="m_Ciphertext">Ciphertext</param>
        /// <returns>返回增加结果</returns>
        bool Add(Model.Auth.Ciphertext m_Ciphertext);
        /// <summary>
        /// 删除Ciphertext
        /// </summary>
        /// <param name="m_SystemName">系统名</param>
        /// <param name="m_EmployeeID">EmployeeID</param>
        /// <returns>返回删除结果</returns>
        bool Delete(string m_SystemName, string m_EmployeeID);
    }
}
