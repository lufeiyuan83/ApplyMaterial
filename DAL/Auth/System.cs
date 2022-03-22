using SQLHelp;
using System;
using System.Collections.Generic;
using System.Data;

namespace DAL.Auth
{
    public class System : IDAL.Auth.ISystem
    {
        private const string tableName = "Auth.dbo.System";

        /// <summary>
        /// 查询System
        /// </summary>
        /// <returns>System</returns>
        public DataTable Select()
        {
            using (DataAccess da = new DataAccess())
            {
                return da.GetDataTable("Select * From " + tableName + " Where IsDeleted=0 Order By Id");
            }
        }
        /// <summary>
        /// 查询单条System
        /// </summary>
        /// <param name="m_Id">主键</param>
        /// <returns>System</returns>
        public Model.Auth.System Select(long m_Id)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@Id", m_Id));
                DataRow dr = da.GetDataRow("Select * From " + tableName + " Where Id=@Id And IsDeleted=0", da.sqlParameters);
                if (dr != null)
                {
                    Model.Auth.System system = new Model.Auth.System();
                    system.Id = Convert.ToInt64(dr["Id"]);
                    system.SystemCode = dr["SystemCode"].ToString().Trim();
                    system.SystemName = dr["SystemName"].ToString().Trim();
                    system.SystemDescription = dr["SystemDescription"].ToString().Trim();
                    system.IsDeleted = Convert.ToBoolean(dr["IsDeleted"]);
                    system.CreateUserID = dr["CreateUserID"].ToString().Trim();
                    system.CreateDateTime = Convert.ToDateTime(dr["CreateDateTime"]);
                    system.LastUpdateUserID = dr["LastUpdateUserID"].ToString().Trim();
                    system.LastUpdateDateTime = Convert.ToDateTime(dr["LastUpdateDateTime"]);
                    system.Remark = dr["Remark"].ToString().Trim();
                    return system;
                }
                else
                    return null;
            }
        }
        /// <summary>
        /// 查询System
        /// </summary>
        /// <param name="m_Id">主键</param>
        /// <returns></returns>
        public DataTable Select(List<long> m_Id)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                string strWhere = " Where IsDeleted=0";
                if (m_Id.Count > 0)
                    strWhere += " And Id in(";
                for (int i = 0; i < m_Id.Count; i++)
                    strWhere += m_Id[i] + ",";
                strWhere = strWhere.Substring(0, strWhere.Length - 1) + ")";
                return da.GetDataTable("Select * From " + tableName + strWhere + " Order By Id", da.sqlParameters);
            }
        }
        /// <summary>
        /// 查询单条System
        /// </summary>
        /// <param name="m_SystemCode">系统代码</param>
        /// <returns>System</returns>
        public Model.Auth.System Select(string m_SystemCode)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@SystemCode", m_SystemCode.Trim().ToUpper()));
                DataRow dr = da.GetDataRow("Select * From " + tableName + " Where Upper(SystemCode)=@SystemCode And IsDeleted=0", da.sqlParameters);
                if (dr != null)
                {
                    Model.Auth.System system = new Model.Auth.System();
                    system.Id = Convert.ToInt64(dr["Id"]);
                    system.SystemCode = dr["SystemCode"].ToString().Trim();
                    system.SystemName = dr["SystemName"].ToString().Trim();
                    system.SystemDescription = dr["SystemDescription"].ToString().Trim();
                    system.IsDeleted = Convert.ToBoolean(dr["IsDeleted"]);
                    system.CreateUserID = dr["CreateUserID"].ToString().Trim();
                    system.CreateDateTime = Convert.ToDateTime(dr["CreateDateTime"]);
                    system.LastUpdateUserID = dr["LastUpdateUserID"].ToString().Trim();
                    system.LastUpdateDateTime = Convert.ToDateTime(dr["LastUpdateDateTime"]);
                    system.Remark = dr["Remark"].ToString().Trim();
                    return system;
                }
                else
                    return null;
            }
        }
        /// <summary>
        /// 查询单条System
        /// </summary>
        /// <param name="m_SystemCode">系统代码</param>
        /// <param name="m_SystemName">系统名称</param>
        /// <returns>System</returns>
        public Model.Auth.System Select(string m_SystemCode, string m_SystemName)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                string strWhere = " Where IsDeleted=0";
                if (!String.IsNullOrEmpty(m_SystemCode))
                {
                    da.sqlParameters.Add(da.CreateSqlParameter("@SystemCode", m_SystemCode.Trim().ToUpper()));
                    strWhere += " And Upper(SystemCode)=@SystemCode";
                }
                if (!String.IsNullOrEmpty(m_SystemName))
                {
                    da.sqlParameters.Add(da.CreateSqlParameter("@SystemName", m_SystemName.Trim().ToUpper()));
                    strWhere += " And Upper(SystemName)=@SystemName";
                }
                DataRow dr = da.GetDataRow("Select * From " + tableName + strWhere, da.sqlParameters);
                if (dr != null)
                {
                    Model.Auth.System system = new Model.Auth.System();
                    system.Id = Convert.ToInt64(dr["Id"]);
                    system.SystemCode = dr["SystemCode"].ToString().Trim();
                    system.SystemName = dr["SystemName"].ToString().Trim();
                    system.SystemDescription = dr["SystemDescription"].ToString().Trim();
                    system.IsDeleted = Convert.ToBoolean(dr["IsDeleted"]);
                    system.CreateUserID = dr["CreateUserID"].ToString().Trim();
                    system.CreateDateTime = Convert.ToDateTime(dr["CreateDateTime"]);
                    system.LastUpdateUserID = dr["LastUpdateUserID"].ToString().Trim();
                    system.LastUpdateDateTime = Convert.ToDateTime(dr["LastUpdateDateTime"]);
                    system.Remark = dr["Remark"].ToString().Trim();
                    return system;
                }
                else
                    return null;
            }
        }
        /// <summary>
        /// 查询System
        /// </summary>
        /// <param name="m_SystemCode">系统代码</param>
        /// <returns></returns>
        public DataTable Select(List<string> m_SystemCode)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                string strWhere = " Where IsDeleted=0";
                if (m_SystemCode.Count != 1 || !String.IsNullOrEmpty(m_SystemCode[0]))
                {
                    if (m_SystemCode.Count > 0)
                        strWhere += " And Upper(SystemCode) in(";
                    for (int i = 0; i < m_SystemCode.Count; i++)
                        strWhere += "'" + m_SystemCode[i].Trim().ToUpper() + "',";
                    strWhere = strWhere.Substring(0, strWhere.Length - 1) + ")";
                }
                return da.GetDataTable("Select * From " + tableName + strWhere + " Order By Id", da.sqlParameters);
            }
        }
        /// <summary>
        /// 增加System
        /// </summary>
        /// <param name="m_System">System</param>
        /// <returns>返回增加结果</returns>
        public bool Add(Model.Auth.System m_System)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@SystemCode", m_System.SystemCode.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@SystemName", m_System.SystemName.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@SystemDescription", m_System.SystemDescription.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@IsDeleted", m_System.IsDeleted));
                da.sqlParameters.Add(da.CreateSqlParameter("@CreateUserID", m_System.CreateUserID.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@LastUpdateUserID", m_System.LastUpdateUserID.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@Remark", m_System.Remark.Trim()));
                return da.ExecuteCommand("Insert Into " + tableName + "(SystemCode,SystemName,SystemDescription,CreateUserID,LastUpdateUserID,Remark) Values(@SystemCode,@SystemName,@SystemDescription,@CreateUserID,@LastUpdateUserID,@Remark)", da.sqlParameters) != 0 ? true : false;
            }
        }
        /// <summary>
        /// 更新System
        /// </summary>
        /// <param name="m_System">System</param>
        /// <returns>返回更新结果</returns>
        public bool Update(Model.Auth.System m_System)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@Id", m_System.Id));
                da.sqlParameters.Add(da.CreateSqlParameter("@SystemCode", m_System.SystemCode.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@SystemName", m_System.SystemName.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@SystemDescription", m_System.SystemDescription.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@LastUpdateUserID", m_System.LastUpdateUserID.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@Remark", m_System.Remark.Trim()));
                return da.ExecuteCommand("Update " + tableName + " Set SystemCode=@SystemCode,SystemName=@SystemName,SystemDescription=@SystemDescription,LastUpdateUserID=@LastUpdateUserID,LastUpdateDateTime=GetDate(),Remark=@Remark Where Id=@Id", da.sqlParameters) != 0 ? true : false;
            }
        }
        /// <summary>
        /// 删除System
        /// </summary>
        /// <param name="m_Id">主键</param>
        /// <param name="m_DeleteUserID">删除人</param>
        /// <returns>返回删除结果</returns>
        public bool Delete(long m_Id, string m_DeleteUserID)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@Id", m_Id));
                da.sqlParameters.Add(da.CreateSqlParameter("@LastUpdateUserID", m_DeleteUserID.Trim()));
                return da.ExecuteCommand("Update " + tableName + " Set IsDeleted=1,LastUpdateDateTime=GetDate(),LastUpdateUserID=@LastUpdateUserID Where Id=@Id", da.sqlParameters) != 0 ? true : false;
            }
        }
        /// <summary>
        /// 是否已经存在该系统信息
        /// </summary>
        /// <param name="m_SystemCode">系统代码</param>
        /// <returns>返回结果</returns>
        public bool IsExist(string m_SystemCode)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@SystemCode", m_SystemCode.Trim().ToUpper()));
                object isExist = da.GetScalar("Select Count(1) From " + tableName + " Where Upper(SystemCode)=@SystemCode And IsDeleted=0", da.sqlParameters);
                if (isExist != null)
                    return Convert.ToInt32(isExist) > 0;
                else
                {
                    Exception ex = new Exception("Not find SystemCode=" + m_SystemCode + " information");
                    ex.Source = "Class name:" + tableName + ",Error method:public bool IsExist(m_SystemCode=" + m_SystemCode + ")";
                    throw ex;
                }
            }
        }
        /// <summary>
        /// 获得系统信息中的m_FieldName的值
        /// </summary>
        /// <param name="m_SystemCode">系统代码</param>
        /// <param name="m_FieldName">字段名称，以逗号分隔</param>
        /// <returns>字段值</returns>
        public DataTable GetFieldValue(string m_SystemCode, string m_FieldName)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@SystemCode", m_SystemCode.Trim().ToUpper()));
                return da.GetDataTable("select " + m_FieldName.Trim() + " From " + tableName + " Where Upper(SystemCode)=@SystemCode And IsDeleted=0", da.sqlParameters);
            }
        }
        /// <summary>
        /// 获得系统信息中的m_FieldName的值
        /// </summary>
        /// <param name="m_SystemCode">系统代码</param>
        /// <param name="m_FieldName">字段名称，以逗号分隔</param>
        /// <returns>字段值</returns>
        public DataTable GetFieldValue(List<string> m_SystemCode, string m_FieldName)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                string strWhere = " Where IsDeleted=0";
                if (m_SystemCode.Count != 1 || !String.IsNullOrEmpty(m_SystemCode[0]))
                {
                    if (m_SystemCode.Count > 0)
                        strWhere = " And Upper(SystemCode) In(";
                    for (int i = 0; i < m_SystemCode.Count; i++)
                        strWhere += "'" + m_SystemCode[i].Trim().ToUpper() + "',";
                    strWhere = strWhere.Substring(0, strWhere.Length - 1) + ")";
                }
                return da.GetDataTable("select " + m_FieldName.Trim() + " From " + tableName + strWhere, da.sqlParameters);
            }
        } 
        /// <summary>
        /// 获得系统信息
        /// </summary>
        /// <param name="m_SystemName">系统名称</param>
        /// <returns>字段值</returns>
        public DataTable GetSystem(string m_SystemName)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                if (!String.IsNullOrWhiteSpace(m_SystemName))
                {
                    da.sqlParameters.Add(da.CreateSqlParameter("@SystemName", m_SystemName.Trim().ToUpper()));
                    return da.GetDataTable("select * From " + tableName + " Where Upper(SystemName)=@SystemName And IsDeleted=0", da.sqlParameters);
                }
                else
                {
                    return da.GetDataTable("select * From " + tableName + " Where IsDeleted=0");
                }
            }
        }
    }
}
