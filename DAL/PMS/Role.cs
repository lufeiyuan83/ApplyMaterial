using SQLHelp;
using System;
using System.Collections.Generic;
using System.Data;

namespace DAL.PMS
{
    public class Role : IDAL.PMS.IRole
    {
        private const string tableName = "PMS.dbo.Role";

        /// <summary>
        /// 查询Role
        /// </summary>
        /// <returns>Role</returns>
        public DataTable Select()
        {
            using (DataAccess da = new DataAccess())
            {
                return da.GetDataTable("Select * From " + tableName + " Where IsDeleted=0 Order By Id");
            }
        }
        /// <summary>
        /// 查询单条Role
        /// </summary>
        /// <param name="m_Id">主键</param>
        /// <returns>Role</returns>
        public Model.PMS.Role Select(long m_Id)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@Id", m_Id));
                DataRow dr = da.GetDataRow("Select * From " + tableName + " Where Id=@Id And IsDeleted=0", da.sqlParameters);
                if (dr != null)
                {
                    Model.PMS.Role Role = new Model.PMS.Role();
                    Role.Id = Convert.ToInt64(dr["Id"]);
                    Role.RoleCode = dr["RoleCode"].ToString().Trim();
                    Role.RoleName = dr["RoleName"].ToString().Trim();
                    Role.RoleDescription = dr["RoleDescription"].ToString().Trim();
                    Role.IsDeleted = Convert.ToBoolean(dr["IsDeleted"]);
                    Role.CreateUserID = dr["CreateUserID"].ToString().Trim();
                    Role.CreateDateTime = Convert.ToDateTime(dr["CreateDateTime"]);
                    Role.LastUpdateUserID = dr["LastUpdateUserID"].ToString().Trim();
                    Role.LastUpdateDateTime = Convert.ToDateTime(dr["LastUpdateDateTime"]);
                    Role.Remark = dr["Remark"].ToString().Trim();
                    return Role;
                }
                else
                    return null;
            }
        }
        /// <summary>
        /// 查询Role
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
        /// 查询单条Role
        /// </summary>
        /// <param name="m_RoleCode">角色代码</param>
        /// <returns>Role</returns>
        public Model.PMS.Role Select(string m_RoleCode)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@RoleCode", m_RoleCode.Trim().ToUpper()));
                DataRow dr = da.GetDataRow("Select * From " + tableName + " Where Upper(RoleCode)=@RoleCode And IsDeleted=0", da.sqlParameters);
                if (dr != null)
                {
                    Model.PMS.Role Role = new Model.PMS.Role();
                    Role.Id = Convert.ToInt64(dr["Id"]);
                    Role.RoleCode = dr["RoleCode"].ToString().Trim();
                    Role.RoleName = dr["RoleName"].ToString().Trim();
                    Role.RoleDescription = dr["RoleDescription"].ToString().Trim();
                    Role.IsDeleted = Convert.ToBoolean(dr["IsDeleted"]);
                    Role.CreateUserID = dr["CreateUserID"].ToString().Trim();
                    Role.CreateDateTime = Convert.ToDateTime(dr["CreateDateTime"]);
                    Role.LastUpdateUserID = dr["LastUpdateUserID"].ToString().Trim();
                    Role.LastUpdateDateTime = Convert.ToDateTime(dr["LastUpdateDateTime"]);
                    Role.Remark = dr["Remark"].ToString().Trim();
                    return Role;
                }
                else
                    return null;
            }
        }
        /// <summary>
        /// 查询单条Role
        /// </summary>
        /// <param name="m_RoleCode">角色代码</param>
        /// <param name="m_RoleName">角色名称</param>
        /// <returns>Role</returns>
        public Model.PMS.Role Select(string m_RoleCode, string m_RoleName)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                string strWhere = " Where IsDeleted=0";
                if (!String.IsNullOrEmpty(m_RoleCode))
                {
                    da.sqlParameters.Add(da.CreateSqlParameter("@RoleCode", m_RoleCode.Trim().ToUpper()));
                    strWhere += " And Upper(RoleCode)=@RoleCode";
                }
                if (!String.IsNullOrEmpty(m_RoleName))
                {
                    da.sqlParameters.Add(da.CreateSqlParameter("@RoleName", m_RoleName.Trim().ToUpper()));
                    strWhere += " And Upper(RoleName)=@RoleName";
                }
                DataRow dr = da.GetDataRow("Select * From " + tableName + strWhere, da.sqlParameters);
                if (dr != null)
                {
                    Model.PMS.Role Role = new Model.PMS.Role();
                    Role.Id = Convert.ToInt64(dr["Id"]);
                    Role.RoleCode = dr["RoleCode"].ToString().Trim();
                    Role.RoleName = dr["RoleName"].ToString().Trim();
                    Role.RoleDescription = dr["RoleDescription"].ToString().Trim();
                    Role.IsDeleted = Convert.ToBoolean(dr["IsDeleted"]);
                    Role.CreateUserID = dr["CreateUserID"].ToString().Trim();
                    Role.CreateDateTime = Convert.ToDateTime(dr["CreateDateTime"]);
                    Role.LastUpdateUserID = dr["LastUpdateUserID"].ToString().Trim();
                    Role.LastUpdateDateTime = Convert.ToDateTime(dr["LastUpdateDateTime"]);
                    Role.Remark = dr["Remark"].ToString().Trim();
                    return Role;
                }
                else
                    return null;
            }
        }
        /// <summary>
        /// 查询Role
        /// </summary>
        /// <param name="m_RoleCode">角色代码</param>
        /// <returns></returns>
        public DataTable Select(List<string> m_RoleCode)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                string strWhere = " Where IsDeleted=0";
                if (m_RoleCode.Count != 1 || !String.IsNullOrEmpty(m_RoleCode[0]))
                {
                    if (m_RoleCode.Count > 0)
                        strWhere += " And Upper(RoleCode) in(";
                    for (int i = 0; i < m_RoleCode.Count; i++)
                        strWhere += "'" + m_RoleCode[i].Trim().ToUpper() + "',";
                    strWhere = strWhere.Substring(0, strWhere.Length - 1) + ")";
                }
                return da.GetDataTable("Select * From " + tableName + strWhere + " Order By Id", da.sqlParameters);
            }
        }
        /// <summary>
        /// 增加Role
        /// </summary>
        /// <param name="m_Role">Role</param>
        /// <returns>返回增加结果</returns>
        public bool Add(Model.PMS.Role m_Role)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@RoleCode", m_Role.RoleCode.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@RoleName", m_Role.RoleName.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@RoleDescription", m_Role.RoleDescription.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@IsDeleted", m_Role.IsDeleted));
                da.sqlParameters.Add(da.CreateSqlParameter("@CreateUserID", m_Role.CreateUserID.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@LastUpdateUserID", m_Role.LastUpdateUserID.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@Remark", m_Role.Remark.Trim()));
                return da.ExecuteCommand("Insert Into " + tableName + "(RoleCode,RoleName,RoleDescription,CreateUserID,LastUpdateUserID,Remark) Values(@RoleCode,@RoleName,@RoleDescription,@CreateUserID,@LastUpdateUserID,@Remark)", da.sqlParameters) != 0 ? true : false;
            }
        }
        /// <summary>
        /// 更新Role
        /// </summary>
        /// <param name="m_Role">Role</param>
        /// <returns>返回更新结果</returns>
        public bool Update(Model.PMS.Role m_Role)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@Id", m_Role.Id));
                da.sqlParameters.Add(da.CreateSqlParameter("@RoleCode", m_Role.RoleCode.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@RoleName", m_Role.RoleName.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@RoleDescription", m_Role.RoleDescription.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@LastUpdateUserID", m_Role.LastUpdateUserID.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@Remark", m_Role.Remark.Trim()));
                return da.ExecuteCommand("Update " + tableName + " Set RoleCode=@RoleCode,RoleName=@RoleName,RoleDescription=@RoleDescription,LastUpdateUserID=@LastUpdateUserID,LastUpdateDateTime=GetDate(),Remark=@Remark Where Id=@Id", da.sqlParameters) != 0 ? true : false;
            }
        }
        /// <summary>
        /// 删除Role
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
        /// 是否已经存在该角色信息
        /// </summary>
        /// <param name="m_RoleCode">角色代码</param>
        /// <returns>返回结果</returns>
        public bool IsExist(string m_RoleCode)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@RoleCode", m_RoleCode.Trim().ToUpper()));
                object isExist = da.GetScalar("Select Count(1) From " + tableName + " Where Upper(RoleCode)=@RoleCode And IsDeleted=0", da.sqlParameters);
                if (isExist != null)
                    return Convert.ToInt32(isExist) > 0;
                else
                {
                    Exception ex = new Exception("Not find RoleCode=" + m_RoleCode + " information");
                    ex.Source = "Class name:" + tableName + ",Error method:public bool IsExist(m_RoleCode=" + m_RoleCode + ")";
                    throw ex;
                }
            }
        }
        /// <summary>
        /// 获得角色信息中的m_FieldName的值
        /// </summary>
        /// <param name="m_RoleCode">角色代码</param>
        /// <param name="m_FieldName">字段名称，以逗号分隔</param>
        /// <returns>字段值</returns>
        public DataTable GetFieldValue(string m_RoleCode, string m_FieldName)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@RoleCode", m_RoleCode.Trim().ToUpper()));
                return da.GetDataTable("select " + m_FieldName.Trim() + " From " + tableName + " Where Upper(RoleCode)=@RoleCode And IsDeleted=0", da.sqlParameters);
            }
        }
        /// <summary>
        /// 获得角色信息中的m_FieldName的值
        /// </summary>
        /// <param name="m_RoleCode">角色代码</param>
        /// <param name="m_FieldName">字段名称，以逗号分隔</param>
        /// <returns>字段值</returns>
        public DataTable GetFieldValue(List<string> m_RoleCode, string m_FieldName)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                string strWhere = " Where IsDeleted=0";
                if (m_RoleCode.Count != 1 || !String.IsNullOrEmpty(m_RoleCode[0]))
                {
                    if (m_RoleCode.Count > 0)
                        strWhere = " And Upper(RoleCode) In(";
                    for (int i = 0; i < m_RoleCode.Count; i++)
                        strWhere += "'" + m_RoleCode[i].Trim().ToUpper() + "',";
                    strWhere = strWhere.Substring(0, strWhere.Length - 1) + ")";
                }
                return da.GetDataTable("select " + m_FieldName.Trim() + " From " + tableName + strWhere, da.sqlParameters);
            }
        } 
        /// <summary>
        /// 获得角色信息
        /// </summary>
        /// <param name="m_RoleName">角色名称</param>
        /// <returns>字段值</returns>
        public DataTable GetRole(string m_RoleName)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                if (!String.IsNullOrWhiteSpace(m_RoleName))
                {
                    da.sqlParameters.Add(da.CreateSqlParameter("@RoleName", m_RoleName.Trim().ToUpper()));
                    return da.GetDataTable("select * From " + tableName + " Where Upper(RoleName)=@RoleName And IsDeleted=0", da.sqlParameters);
                }
                else
                {
                    return da.GetDataTable("select * From " + tableName + " Where IsDeleted=0");
                }
            }
        }
    }
}
