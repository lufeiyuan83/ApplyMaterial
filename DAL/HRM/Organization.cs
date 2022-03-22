using SQLHelp;
using System;
using System.Collections.Generic;
using System.Data;

namespace DAL.HRM
{
    public class Organization :IDAL.HRM.IOrganization
    {
        private const string tableName = "HRM.dbo.Organization";

        /// <summary>
        /// 查询Organization
        /// </summary>
        /// <returns>Organization</returns>
        public DataTable Select()
        {
            using (DataAccess da = new DataAccess())
            {
                return da.GetDataTable("Select * From " + tableName + " Where IsDeleted=0 Order By id");
            }
        }
        /// <summary>
        /// 查询单条Organization
        /// </summary>
        /// <param name="m_id">主键</param>
        /// <returns>Organization</returns>
        public Model.HRM.Organization Select(long m_id)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@id", m_id));
                DataRow dr = da.GetDataRow("Select * From " + tableName + " Where id=@id And IsDeleted=0", da.sqlParameters);
                if (dr != null)
                {
                    Model.HRM.Organization organization = new Model.HRM.Organization
                    {
                        Id = Convert.ToInt64(dr["Id"]),
                        OrgCode = dr["OrgCode"].ToString().Trim(),
                        OrgName = dr["OrgName"].ToString().Trim(),
                        OrgShortName = dr["OrgShortName"].ToString().Trim(),
                        OrgDescription = dr["OrgDescription"].ToString().Trim(),
                        OrgHierarchy = Convert.ToInt64(dr["OrgHierarchy"]),
                        Affiliation = dr["Affiliation"].ToString().Trim(),
                        OrgType = dr["OrgType"].ToString().Trim(),
                        EnableSelect = Convert.ToBoolean(dr["EnableSelect"]),
                        DefaultValue = Convert.ToBoolean(dr["DefaultValue"]),
                        Phone = dr["Phone"].ToString().Trim(),
                        MobilePhone = dr["MobilePhone"].ToString().Trim(),
                        Email = dr["Email"].ToString().Trim(),
                        Fax = dr["Fax"].ToString().Trim(),
                        Address = dr["Address"].ToString().Trim(),
                        IsDeleted = Convert.ToBoolean(dr["IsDeleted"]),
                        CreateUserID = dr["CreateUserID"].ToString().Trim(),
                        CreateDateTime = Convert.ToDateTime(dr["CreateDateTime"]),
                        LastUpdateUserID = dr["LastUpdateUserID"].ToString().Trim(),
                        LastUpdateDateTime = Convert.ToDateTime(dr["LastUpdateDateTime"]),
                        Remark = dr["Remark"].ToString().Trim()
                    };
                    return organization;
                }
                else
                    return null;
            }
        }
        /// <summary>
        /// 查询单条Organization
        /// </summary>
        /// <param name="m_OrgCode">组织代码</param>
        /// <returns>Organization</returns>
        public Model.HRM.Organization Select(string m_OrgCode)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@OrgCode", m_OrgCode.Trim().ToUpper()));
                DataRow dr = da.GetDataRow("Select * From " + tableName + " Where Upper(OrgCode)=@OrgCode And IsDeleted=0", da.sqlParameters);
                if (dr != null)
                {
                    Model.HRM.Organization organization = new Model.HRM.Organization
                    {
                        OrgCode = dr["OrgCode"].ToString().Trim(),
                        OrgName = dr["OrgName"].ToString().Trim(),
                        OrgShortName = dr["OrgShortName"].ToString().Trim(),
                        OrgDescription = dr["OrgDescription"].ToString().Trim(),
                        OrgHierarchy = Convert.ToInt64(dr["OrgHierarchy"]),
                        Affiliation = dr["Affiliation"].ToString().Trim(),
                        OrgType = dr["OrgType"].ToString().Trim(),
                        EnableSelect = Convert.ToBoolean(dr["EnableSelect"]),
                        DefaultValue = Convert.ToBoolean(dr["DefaultValue"]),
                        Phone = dr["Phone"].ToString().Trim(),
                        MobilePhone = dr["MobilePhone"].ToString().Trim(),
                        Email = dr["Email"].ToString().Trim(),
                        Fax = dr["Fax"].ToString().Trim(),
                        Address = dr["Address"].ToString().Trim(),
                        IsDeleted = Convert.ToBoolean(dr["IsDeleted"]),
                        CreateUserID = dr["CreateUserID"].ToString().Trim(),
                        CreateDateTime = Convert.ToDateTime(dr["CreateDateTime"]),
                        LastUpdateUserID = dr["LastUpdateUserID"].ToString().Trim(),
                        LastUpdateDateTime = Convert.ToDateTime(dr["LastUpdateDateTime"]),
                        Remark = dr["Remark"].ToString().Trim()
                    };
                    return organization;
                }
                else
                    return null;
            }
        }
        /// <summary>
        /// 查询Organization
        /// </summary>
        /// <param name="m_OrgCode">组织代码</param>
        /// <returns></returns>
        public DataTable Select(List<string> m_OrgCode)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                string strWhere = " Where IsDeleted=0";
                if (m_OrgCode.Count > 0)
                    strWhere += " And Upper(OrgCode) in(";
                for (int i = 0; i < m_OrgCode.Count; i++)
                    strWhere += "'" + m_OrgCode[i].Trim().ToUpper() + "',";
                strWhere = strWhere.Substring(0, strWhere.Length - 1) + ")";
                return da.GetDataTable("Select * From " + tableName + strWhere + " Order By id", da.sqlParameters);
            }
        }
        /// <summary>
        /// 查询Organization
        /// </summary>
        /// <param name="m_id">组织主键</param>
        /// <returns></returns>
        public DataTable Select(List<long> m_id)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                string strWhere = " Where IsDeleted=0";
                if (m_id.Count > 0)
                    strWhere += " And OrgHierarchy in(";
                for (int i = 0; i < m_id.Count; i++)
                    strWhere += m_id[i] + ",";
                strWhere = strWhere.Substring(0, strWhere.Length - 1) + ")";
                return da.GetDataTable("Select * From " + tableName + strWhere + " Order By id", da.sqlParameters);
            }
        }
        /// <summary>
        /// 增加Organization
        /// </summary>
        /// <param name="m_Organization">Organization</param>
        /// <returns>返回增加结果</returns>
        public bool Add(Model.HRM.Organization m_Organization)
        {
            using (DataAccess da = new DataAccess())
            {
                try
                {
                    da.BeginTransaction();
                    da.sqlParameters.Clear();
                    da.sqlParameters.Add(da.CreateSqlParameter("@OrgCode", m_Organization.OrgCode.Trim()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@OrgName", m_Organization.OrgName.Trim()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@OrgShortName", m_Organization.OrgShortName.Trim()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@OrgDescription", m_Organization.OrgDescription.Trim()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@OrgHierarchy", m_Organization.OrgHierarchy));
                    da.sqlParameters.Add(da.CreateSqlParameter("@Affiliation", m_Organization.Affiliation.Trim()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@OrgType", m_Organization.OrgType.Trim()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@EnableSelect", m_Organization.EnableSelect));
                    da.sqlParameters.Add(da.CreateSqlParameter("@DefaultValue", m_Organization.DefaultValue));
                    da.sqlParameters.Add(da.CreateSqlParameter("@Phone", m_Organization.Phone.Trim()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@MobilePhone", m_Organization.MobilePhone.Trim()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@Email", m_Organization.Email.Trim()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@Fax", m_Organization.Fax.Trim()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@Address", m_Organization.Address.Trim()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@IsDeleted", m_Organization.IsDeleted));
                    da.sqlParameters.Add(da.CreateSqlParameter("@CreateUserID", m_Organization.CreateUserID.Trim()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@LastUpdateUserID", m_Organization.LastUpdateUserID.Trim()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@Remark", m_Organization.Remark.Trim()));
                    da.ExecuteCommand("Insert Into " + tableName + "(OrgCode,OrgName,OrgShortName,OrgDescription,OrgHierarchy,Affiliation,OrgType,EnableSelect,DefaultValue,Phone,MobilePhone,Email,Fax,Address,CreateUserID,LastUpdateUserID,Remark) Values(@OrgCode,@OrgName,@OrgShortName,@OrgDescription,@OrgHierarchy,@Affiliation,@OrgType,@EnableSelect,@DefaultValue,@Phone,@MobilePhone,@Email,@Fax,@Address,@CreateUserID,@LastUpdateUserID,@Remark)", da.sqlParameters);
                    if (m_Organization.OrgHierarchy == 0)
                        da.ExecuteCommand("Update " + tableName + " Set OrgHierarchy=id where OrgHierarchy=0");

                    da.CommitTransaction();
                    return true;
                }
                catch
                {
                    da.RollbackTransaction();
                    throw;
                }
            }
        }
        /// <summary>
        /// 更新Organization
        /// </summary>
        /// <param name="m_Organization">Organization</param>
        /// <returns>返回更新结果</returns>
        public bool Update(Model.HRM.Organization m_Organization)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@id", m_Organization.Id));
                da.sqlParameters.Add(da.CreateSqlParameter("@OrgCode", m_Organization.OrgCode.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@OrgName", m_Organization.OrgName.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@OrgShortName", m_Organization.OrgShortName.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@OrgDescription", m_Organization.OrgDescription.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@OrgHierarchy", m_Organization.OrgHierarchy));
                da.sqlParameters.Add(da.CreateSqlParameter("@Affiliation", m_Organization.Affiliation.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@OrgType", m_Organization.OrgType.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@EnableSelect", m_Organization.EnableSelect));
                da.sqlParameters.Add(da.CreateSqlParameter("@DefaultValue", m_Organization.DefaultValue));
                da.sqlParameters.Add(da.CreateSqlParameter("@Phone", m_Organization.Phone.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@MobilePhone", m_Organization.MobilePhone.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@Email", m_Organization.Email.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@Fax", m_Organization.Fax.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@Address", m_Organization.Address.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@LastUpdateUserID", m_Organization.LastUpdateUserID.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@Remark", m_Organization.Remark.Trim()));
                return da.ExecuteCommand("Update " + tableName + " Set OrgCode=@OrgCode,OrgName=@OrgName,OrgShortName=@OrgShortName,OrgDescription=@OrgDescription,OrgHierarchy=@OrgHierarchy,Affiliation=@Affiliation,OrgType=@OrgType,EnableSelect=@EnableSelect,DefaultValue=@DefaultValue,Phone=@Phone,MobilePhone=@MobilePhone,Email=@Email,Fax=@Fax,Address=@Address,LastUpdateUserID=@LastUpdateUserID,LastUpdateDateTime=GetDate(),Remark=@Remark Where id=@id", da.sqlParameters) != 0 ? true : false;
            }
        }
        /// <summary>
        /// 删除Organization
        /// </summary>
        /// <param name="m_id">主键</param>
        /// <param name="m_DeleteUserID">删除人</param>
        /// <returns>返回删除结果</returns>
        public bool Delete(long m_id, string m_DeleteUserID)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@id", m_id));
                da.sqlParameters.Add(da.CreateSqlParameter("@LastUpdateUserID", m_DeleteUserID.Trim()));
                return da.ExecuteCommand("Update " + tableName + " Set IsDeleted=1,LastUpdateDateTime=GetDate(),LastUpdateUserID=@LastUpdateUserID Where id=@id", da.sqlParameters) != 0 ? true : false;
            }
        }
        /// <summary>
        /// 是否已经存在该组织
        /// </summary>
        /// <param name="m_OrgCode">组织代码</param>
        /// <returns>返回结果</returns>
        public bool IsExist(string m_OrgCode)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@OrgCode", m_OrgCode.Trim().ToUpper()));
                object isExist = da.GetScalar("Select Count(1) From " + tableName + " Where Upper(OrgCode)=@OrgCode And IsDeleted=0", da.sqlParameters);
                if (isExist != null)
                    return Convert.ToInt32(isExist) > 0;
                else
                {
                    Exception ex = new Exception("Not find OrgCode=" + m_OrgCode + " information");
                    ex.Source = "Class name:" + tableName + ",Error method:public bool IsExist(m_OrgCode=" + m_OrgCode + ")";
                    throw ex;
                }
            }
        }
        /// <summary>
        /// 获得组织信息中的m_FieldName的值
        /// </summary>
        /// <param name="m_OrgCode">组织代码</param>
        /// <param name="m_FieldName">字段名称，以逗号分隔</param>
        /// <returns>字段值</returns>
        public DataTable GetFieldValue(string m_OrgCode, string m_FieldName)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@OrgName", m_OrgCode.Trim().ToUpper()));
                da.sqlParameters.Add(da.CreateSqlParameter("@FieldName", m_FieldName.Trim()));
                return da.GetDataTable("select " + m_FieldName.Trim() + " From " + tableName + " Where Upper(OrgName)=@OrgName And IsDeleted=0", da.sqlParameters);
            }
        }
        /// <summary>
        /// 获得组织信息中的m_FieldName的值
        /// </summary>
        /// <param name="m_OrgCode">组织代码</param>
        /// <param name="m_FieldName">字段名称，以逗号分隔</param>
        /// <returns>字段值</returns>
        public DataTable GetFieldValue(List<string> m_OrgCode, string m_FieldName)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                string strWhere = " Where IsDeleted=0";
                if (m_OrgCode.Count > 0)
                {
                    strWhere += " And Upper(OrgCode) In(";
                    for (int i = 0; i < m_OrgCode.Count; i++)
                    strWhere += "'" + m_OrgCode[i].Trim().ToUpper() + "',";
                }
                strWhere = strWhere.Substring(0, strWhere.Length - 1) + ")";
                return da.GetDataTable("select " + m_FieldName.Trim() + " From " + tableName + strWhere, da.sqlParameters);
            }
        }
    }
}
