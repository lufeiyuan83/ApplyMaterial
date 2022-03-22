using DAL;
using SQLHelp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DAL.Tenement
{
    public class Ciphertext : IDAL.Tenement.ICiphertext
    {
        private const string tableName = "Tenement.dbo.Ciphertext";
        /// <summary>
        /// 查询单条Ciphertext
        /// </summary>
        /// <param name="m_SystemName">系统名</param>
        /// <param name="m_Cryptograph">密文</param>
        /// <returns>AdditionalItems</returns>
        public Model.Tenement.Ciphertext Select(string m_SystemName, string m_Cryptograph)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@SystemName", m_SystemName.Trim().ToUpper()));
                da.sqlParameters.Add(da.CreateSqlParameter("@Cryptograph", m_Cryptograph));
                DataRow dr = da.GetDataRow("Select * From " + tableName + " Where Upper(SystemName)=@SystemName And Cryptograph=@Cryptograph", da.sqlParameters);
                if (dr != null)
                {
                    Model.Tenement.Ciphertext ciphertext = new Model.Tenement.Ciphertext();
                    ciphertext.SystemName = dr["SystemName"].ToString().Trim();
                    ciphertext.EmployeeID = dr["EmployeeID"].ToString().Trim();
                    ciphertext.Cryptograph = dr["Cryptograph"].ToString().Trim();
                    return ciphertext;
                }
                else
                    return null;
            }
        }
        /// <summary>
        /// 获得密文
        /// </summary>
        /// <param name="m_SystemName">系统名</param>
        /// <param name="m_EmployeeID">工号</param>
        /// <returns></returns>
        public string GetCiphertext(string m_SystemName, string m_EmployeeID)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@SystemName", m_SystemName.Trim().ToUpper()));
                da.sqlParameters.Add(da.CreateSqlParameter("@EmployeeID", m_EmployeeID.Trim().ToUpper()));
                object objCryptograph = da.GetScalar("Select Cryptograph From " + tableName + " Where Upper(SystemName)=@SystemName And Upper(EmployeeID)=@EmployeeID", da.sqlParameters);
                if (objCryptograph != null)
                    return objCryptograph.ToString();
                else
                    return null;
            }
        }
        /// <summary>
        /// 是否存在该员工的密文
        /// </summary>
        /// <param name="m_SystemName">系统名</param>
        /// <param name="m_EmployeeID">EmployeeID</param>
        /// <returns></returns>
        public bool IsExist(string m_SystemName,string m_EmployeeID)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@SystemName", m_SystemName.Trim().ToUpper()));
                da.sqlParameters.Add(da.CreateSqlParameter("@EmployeeID", m_EmployeeID.Trim().ToUpper()));
                object isExist = da.GetScalar("Select Count(1) From " + tableName + " Where Upper(SystemName)=@SystemName And Upper(EmployeeID)=@EmployeeID", da.sqlParameters);
                if (isExist != null)
                    return Convert.ToInt32(isExist) > 0 ? true : false;
                else
                {
                    Exception ex = new Exception("Not find SystemName=" + m_SystemName + ",EmployeeID=" + m_EmployeeID + " information");
                    ex.Source = "Class name:" + tableName + ",Error method:public bool IsExist(m_SystemName=" + m_SystemName + ",m_EmployeeID=" + m_EmployeeID + ")";
                    throw ex;
                }
            }
        }
        /// <summary>
        /// 增加Ciphertext
        /// </summary>
        /// <param name="m_Ciphertext">Ciphertext</param>
        /// <returns>返回增加结果</returns>
        public bool Add(Model.Tenement.Ciphertext m_Ciphertext)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@SystemName", m_Ciphertext.SystemName.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@EmployeeID", m_Ciphertext.EmployeeID.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@Cryptograph", m_Ciphertext.Cryptograph.Trim()));
                return da.ExecuteCommand("Insert Into " + tableName + "(SystemName,EmployeeID,Cryptograph) Values(@SystemName,@EmployeeID,@Cryptograph)", da.sqlParameters) != 0 ? true : false;
            }
        }
        /// <summary>
        /// 删除Ciphertext
        /// </summary>
        /// <param name="m_SystemName">系统名</param>
        /// <param name="m_EmployeeID">EmployeeID</param>
        /// <returns>返回删除结果</returns>
        public bool Delete(string m_SystemName,string m_EmployeeID)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@SystemName", m_SystemName));
                da.sqlParameters.Add(da.CreateSqlParameter("@EmployeeID", m_EmployeeID));
                return da.ExecuteCommand("Delete " + tableName + " Where SystemName=@SystemName And EmployeeID=@EmployeeID", da.sqlParameters) != 0 ? true : false;
            }
        }
    }
}
