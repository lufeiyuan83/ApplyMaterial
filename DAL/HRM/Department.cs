using SQLHelp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DAL.HRM
{
    public class Department :IDAL.HRM.IDepartment
    {
        private const string tableName = "HRM.dbo.Department";

        /// <summary>
        /// 查询Department
        /// </summary>
        /// <returns>Department</returns>
        public DataTable Select()
        {
            using (DataAccess da = new DataAccess())
            {
                return da.GetDataTable("Select * From " + tableName + " Where IsDeleted=0 Order By id");
            }
        }
        /// <summary>
        /// 查询单条Department
        /// </summary>
        /// <param name="m_id">主键</param>
        /// <returns>Department</returns>
        public Model.HRM.Department Select(long m_id)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@id", m_id));
                DataRow dr = da.GetDataRow("Select * From " + tableName + " Where id=@id And IsDeleted=0", da.sqlParameters);
                if (dr != null)
                {
                    Model.HRM.Department department = new Model.HRM.Department();
                    department.Id = Convert.ToInt64(dr["Id"]);
                    department.ApplicationOrg = dr["ApplicationOrg"].ToString().Trim();
                    department.DepartmentCode = dr["DepartmentCode"].ToString().Trim();
                    department.DepartmentName = dr["DepartmentName"].ToString().Trim();
                    department.IsDeleted = Convert.ToBoolean(dr["IsDeleted"]);
                    department.CreateUserID = dr["CreateUserID"].ToString().Trim();
                    department.CreateDateTime = Convert.ToDateTime(dr["CreateDateTime"]);
                    department.LastUpdateUserID = dr["LastUpdateUserID"].ToString().Trim();
                    department.LastUpdateDateTime = Convert.ToDateTime(dr["LastUpdateDateTime"]);
                    department.Remark = dr["Remark"].ToString().Trim();
                    return department;
                }
                else
                    return null;
            }
        }
        /// <summary>
        /// 查询单条Department
        /// </summary>
        /// <param name="m_ApplicationOrg">组织代码</param>
        /// <returns>Department</returns>
        public DataTable Select(string m_ApplicationOrg)
        {
            using (DataAccess da = new DataAccess())
            {
                if (!String.IsNullOrWhiteSpace(m_ApplicationOrg))
                {
                    da.sqlParameters.Clear();
                    da.sqlParameters.Add(da.CreateSqlParameter("@ApplicationOrg", m_ApplicationOrg.Trim().ToUpper()));
                    return da.GetDataTable("Select * From " + tableName + " Where Upper(ApplicationOrg)=@ApplicationOrg And IsDeleted=0", da.sqlParameters);
                }
                else
                    return null;
            }
        }
        /// <summary>
        /// 查询Department
        /// </summary>
        /// <param name="m_ApplicationOrg">组织代码</param>
        /// <returns></returns>
        public DataTable Select(List<string> m_ApplicationOrg)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                string strWhere = " Where IsDeleted=0";
                if (m_ApplicationOrg.Count > 0)
                    strWhere += " And Upper(OrgCode) in(";
                for (int i = 0; i < m_ApplicationOrg.Count; i++)
                    strWhere += "'" + m_ApplicationOrg[i].Trim().ToUpper() + "',";
                strWhere = strWhere.Substring(0, strWhere.Length - 1) + ")";
                return da.GetDataTable("Select * From " + tableName + strWhere + " Order By id", da.sqlParameters);
            }
        }
        /// <summary>
        /// 查询Department
        /// </summary>
        /// <param name="m_id">主键</param>
        /// <returns></returns>
        public DataTable Select(List<long> m_id)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                string strWhere = " Where IsDeleted=0";
                if (m_id.Count > 0)
                {
                    strWhere += " And id In(";
                    for (int i = 0; i < m_id.Count; i++)
                        strWhere += m_id[i] + ",";
                }
                strWhere = strWhere.Substring(0, strWhere.Length - 1) + ")";
                return da.GetDataTable("Select * From " + tableName + strWhere + " Order By id", da.sqlParameters);
            }
        }
        /// <summary>
        /// 增加Department
        /// </summary>
        /// <param name="m_Department">Department</param>
        /// <returns>返回增加结果</returns>
        public bool Add(Model.HRM.Department m_Department)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@ApplicationOrg", m_Department.ApplicationOrg.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@DepartmentCode", m_Department.DepartmentCode.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@DepartmentName", m_Department.DepartmentName.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@IsDeleted", m_Department.IsDeleted));
                da.sqlParameters.Add(da.CreateSqlParameter("@CreateUserID", m_Department.CreateUserID.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@LastUpdateUserID", m_Department.LastUpdateUserID.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@Remark", m_Department.Remark.Trim()));
                return da.ExecuteCommand("Insert Into " + tableName + "(ApplicationOrg,DepartmentCode,DepartmentName,CreateUserID,LastUpdateUserID,Remark) Values(@ApplicationOrg,@DepartmentCode,@DepartmentName,@CreateUserID,@LastUpdateUserID,@Remark)", da.sqlParameters) != 0 ? true : false;
            }
        }
        /// <summary>
        /// 更新Department
        /// </summary>
        /// <param name="m_Department">Department</param>
        /// <returns>返回更新结果</returns>
        public bool Update(Model.HRM.Department m_Department)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@id", m_Department.Id));
                da.sqlParameters.Add(da.CreateSqlParameter("@ApplicationOrg", m_Department.ApplicationOrg.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@DepartmentCode", m_Department.DepartmentCode.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@DepartmentName", m_Department.DepartmentName.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@LastUpdateUserID", m_Department.LastUpdateUserID.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@Remark", m_Department.Remark.Trim()));
                return da.ExecuteCommand("Update " + tableName + " Set ApplicationOrg=@ApplicationOrg,DepartmentCode=@DepartmentCode,DepartmentName=@DepartmentName,LastUpdateUserID=@LastUpdateUserID,LastUpdateDateTime=GetDate(),Remark=@Remark Where id=@id", da.sqlParameters) != 0 ? true : false;
            }
        }
        /// <summary>
        /// 删除Department
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
        /// 是否已经存在该部门
        /// </summary>
        /// <param name="m_ApplicationOrg">组织代码</param>
        /// <param name="m_DepartmentCode">部门代码</param>
        /// <returns>返回结果</returns>
        public bool IsExist(string m_ApplicationOrg, string m_DepartmentCode)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@ApplicationOrg", m_ApplicationOrg.Trim().ToUpper()));
                da.sqlParameters.Add(da.CreateSqlParameter("@DepartmentCode", m_DepartmentCode.Trim().ToUpper()));
                object isExist = da.GetScalar("Select Count(1) From " + tableName + " Where Upper(ApplicationOrg)=@ApplicationOrg And Upper(DepartmentCode)=@DepartmentCode And IsDeleted=0", da.sqlParameters);
                if (isExist != null)
                    return Convert.ToInt32(isExist) > 0;
                else
                {
                    Exception ex = new Exception("Not find ApplicationOrg=" + m_ApplicationOrg + ",DepartmentCode=" + m_DepartmentCode + " information");
                    ex.Source = "Class name:" + tableName + ",Error method:public bool IsExist(m_ApplicationOrg=" + m_ApplicationOrg + ",m_DepartmentCode=" + m_DepartmentCode + ")";
                    throw ex;
                }
            }
        }
        /// <summary>
        /// 获得部门信息中的m_FieldName的值
        /// </summary>
        /// <param name="m_ApplicationOrg">组织代码</param>
        /// <param name="m_DepartmentCode">部门代码</param>
        /// <param name="m_FieldName">字段名称，以逗号分隔</param>
        /// <returns>字段值</returns>
        public DataTable GetFieldValue(string m_ApplicationOrg,string m_DepartmentCode, string m_FieldName)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@ApplicationOrg", m_ApplicationOrg.Trim().ToUpper()));
                da.sqlParameters.Add(da.CreateSqlParameter("@DepartmentCode", m_DepartmentCode.Trim().ToUpper()));
                da.sqlParameters.Add(da.CreateSqlParameter("@FieldName", m_FieldName.Trim()));
                return da.GetDataTable("select " + m_FieldName.Trim() + " From " + tableName + " Where Upper(ApplicationOrg)=@ApplicationOrg And Upper(DepartmentCode)=@DepartmentCode And IsDeleted=0", da.sqlParameters);
            }
        }
        /// <summary>
        /// 获得部门信息中的m_FieldName的值
        /// </summary>
        /// <param name="m_ApplicationOrg">组织代码</param>
        /// <param name="m_DepartmentCode">部门代码</param>
        /// <param name="m_FieldName">字段名称，以逗号分隔</param>
        /// <returns>字段值</returns>
        public DataTable GetFieldValue(List<string> m_ApplicationOrg, string m_DepartmentCode, string m_FieldName)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear(); 
                da.sqlParameters.Add(da.CreateSqlParameter("@DepartmentCode", m_DepartmentCode.Trim().ToUpper()));
                string strWhere = " Where IsDeleted=0 And Upper(DepartmentCode)=@DepartmentCode";
                if(m_ApplicationOrg.Count>0)
                {
                    strWhere += " And Upper(ApplicationOrg) In(";
                    for (int i = 0; i < m_ApplicationOrg.Count; i++)
                        strWhere += "'" + m_ApplicationOrg[i].Trim().ToUpper() + "',";
                    strWhere = strWhere.Substring(0, strWhere.Length - 1) + ")";
                }
                return da.GetDataTable("select " + m_FieldName.Trim() + " From " + tableName + strWhere, da.sqlParameters);
            }
        }
    }
}
