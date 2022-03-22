using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Tenement
{
    public class Ciphertext
    {
        private readonly IDAL.Tenement.ICiphertext dal = DALFactory.DALFactory.GetInstance().GetCiphertext();
        /// <summary>
        /// 查询单条Ciphertext
        /// </summary>
        /// <param name="m_SystemName">系统名</param>
        /// <param name="m_Cryptograph">密文</param>
        /// <returns>AdditionalItems</returns>
        public Model.Tenement.Ciphertext Select(string m_SystemName, string m_Cryptograph)
        {
            return dal.Select(m_SystemName,m_Cryptograph);
        }
        /// <summary>
        /// 获得密文
        /// </summary>
        /// <param name="m_SystemName">系统名</param>
        /// <param name="m_EmployeeID">工号</param>
        /// <returns></returns>
        public string GetCiphertext(string m_SystemName, string m_EmployeeID)
        {
            return dal.GetCiphertext(m_SystemName, m_EmployeeID);
        }
        /// <summary>
        /// 是否存在该员工的密文
        /// </summary>
        /// <param name="m_SystemName">系统名</param>
        /// <param name="m_EmployeeID">EmployeeID</param>
        /// <returns></returns>
        public bool IsExist(string m_SystemName, string m_EmployeeID)
        {
            return dal.IsExist(m_SystemName,m_EmployeeID);
        }
        /// <summary>
        /// 增加Ciphertext
        /// </summary>
        /// <param name="m_Ciphertext">Ciphertext</param>
        /// <returns>返回增加结果</returns>
        public bool Add(Model.Tenement.Ciphertext m_Ciphertext)
        {
            return dal.Add(m_Ciphertext);
        }
        /// <summary>
        /// 删除Ciphertext
        /// </summary>
        /// <param name="m_SystemName">系统名</param>
        /// <param name="m_EmployeeID">EmployeeID</param>
        /// <returns>返回删除结果</returns>
        public bool Delete(string m_SystemName, string m_EmployeeID)
        {
            return dal.Delete(m_SystemName,m_EmployeeID);
        }
    }
}
