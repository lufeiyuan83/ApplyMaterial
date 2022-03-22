using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using DAL;
using SQLHelp;

namespace DAL.HRM
{
    public class Employee : IDAL.HRM.IEmployee
    {
        private const string tableName = "HRM.dbo.Employee";
        /// <summary>
        /// 查询Employee
        /// </summary>
        /// <returns>Employee</returns>
        public DataTable Select()
        {
            using (DataAccess da = new DataAccess())
            {
                return da.GetDataTable("Select a.*,b.DepartmentName From " + tableName + " a,HRM.dbo.Department b Where a.IsDeleted=0 And b.IsDeleted=0 And a.ApplicationOrg=b.ApplicationOrg And a.DepartmentCode=b.DepartmentCode Order By a.EmployeeID");
            }
        }
        /// <summary>
        /// 查询单条员工信息
        /// </summary>
        /// <param name="m_id">主键</param>
        /// <returns></returns>
        public Model.HRM.Employee Select(long m_id)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@id", m_id));
                DataRow dr = da.GetDataRow("Select * From " + tableName + " Where id=@id And IsDeleted=0", da.sqlParameters);
                if (dr != null)
                {
                    Model.HRM.Employee employee = new Model.HRM.Employee
                    {
                        Id = Convert.ToInt64(dr["id"]),
                        ApplicationOrg = dr["ApplicationOrg"].ToString().Trim(),
                        EmployeeID = dr["EmployeeID"].ToString().Trim(),
                        EmployeeName = dr["EmployeeName"].ToString().Trim(),
                        EmployeeEnglishName = dr["EmployeeEnglishName"].ToString().Trim(),
                        EmpoyeeGender = dr["EmpoyeeGender"].ToString().Trim(),
                        EmployeeEmail = dr["EmployeeEmail"].ToString().Trim(),
                        EmployeePhone = dr["EmployeePhone"].ToString().Trim(),
                        DepartmentCode = dr["DepartmentCode"].ToString().Trim(),
                        IsDeleted = Convert.ToBoolean(dr["IsDeleted"]),
                        CreateUserID = dr["CreateUserID"].ToString().Trim(),
                        CreateDateTime = Convert.ToDateTime(dr["CreateDateTime"]),
                        LastUpdateUserID = dr["LastUpdateUserID"].ToString().Trim(),
                        LastUpdateDateTime = Convert.ToDateTime(dr["LastUpdateDateTime"]),
                        Remark = dr["Remark"].ToString().Trim()
                    };
                    return employee;
                }
                else
                    return null;
            }
        }
        /// <summary>
        /// 查询Employee
        /// </summary>
        /// <param name="m_ApplicationOrg">组织</param>
        /// <returns></returns>
        public DataTable Select(string m_ApplicationOrg)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@ApplicationOrg", m_ApplicationOrg));
                return da.GetDataTable("Select a.*,b.DepartmentName From " + tableName + " a,HRM.dbo.Department b Where a.IsDeleted=0 And b.IsDeleted=0 And a.ApplicationOrg=b.ApplicationOrg And a.DepartmentCode=b.DepartmentCode And a.ApplicationOrg=@ApplicationOrg Order By a.EmployeeID", da.sqlParameters);
            }
        }
        /// <summary>
        /// 查询单条Employee
        /// </summary>
        /// <param name="m_ApplicationOrg">ApplicationOrg</param>
        /// <param name="m_EmployeeID">EmployeeID</param>
        /// <returns>Employee</returns>
        public Model.HRM.Employee Select(string m_ApplicationOrg, string m_EmployeeID)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                DataRow dr;
                string strWhere = " Where IsDeleted=0";
                if (!String.IsNullOrWhiteSpace(m_ApplicationOrg))
                { 
                    da.sqlParameters.Add(da.CreateSqlParameter("@ApplicationOrg", m_ApplicationOrg));
                    strWhere += " And ApplicationOrg=@ApplicationOrg";
                }
                if (!String.IsNullOrWhiteSpace(m_EmployeeID))
                {
                    da.sqlParameters.Add(da.CreateSqlParameter("@EmployeeID", m_EmployeeID));
                    strWhere += " And EmployeeID=@EmployeeID";                    
                }
                dr = da.GetDataRow("Select * From " + tableName + strWhere, da.sqlParameters);
                if (dr != null)
                {
                    Model.HRM.Employee employee = new Model.HRM.Employee
                    {
                        Id = Convert.ToInt64(dr["id"]),
                        ApplicationOrg = dr["ApplicationOrg"].ToString().Trim(),
                        EmployeeID = dr["EmployeeID"].ToString().Trim(),
                        EmployeeName = dr["EmployeeName"].ToString().Trim(),
                        EmployeeEnglishName = dr["EmployeeEnglishName"].ToString().Trim(),
                        EmpoyeeGender = dr["EmpoyeeGender"].ToString().Trim(),
                        EmployeeEmail = dr["EmployeeEmail"].ToString().Trim(),
                        EmployeePhone = dr["EmployeePhone"].ToString().Trim(),
                        DepartmentCode = dr["DepartmentCode"].ToString().Trim(),
                        IsDeleted = Convert.ToBoolean(dr["IsDeleted"]),
                        CreateUserID = dr["CreateUserID"].ToString().Trim(),
                        CreateDateTime = Convert.ToDateTime(dr["CreateDateTime"]),
                        LastUpdateUserID = dr["LastUpdateUserID"].ToString().Trim(),
                        LastUpdateDateTime = Convert.ToDateTime(dr["LastUpdateDateTime"]),
                        Remark = dr["Remark"].ToString().Trim()
                    };
                    return employee;
                }
                else
                    return null;
            }
        }
        /// <summary>
        /// 查询单条Employee
        /// </summary>
        /// <param name="m_ApplicationOrg">ApplicationOrg</param>
        /// <param name="m_EmployeeID">EmployeeID</param>
        /// <returns>Employee</returns>
        public DataTable Select(string m_ApplicationOrg, string[] m_EmployeeID)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@ApplicationOrg", m_ApplicationOrg));
                string strWhere = " Where a.IsDeleted=0 And b.IsDeleted=0 And a.ApplicationOrg=b.ApplicationOrg And a.DepartmentCode=b.DepartmentCode And a.ApplicationOrg=@ApplicationOrg";
                if(m_EmployeeID.Length>0)
                {
                    strWhere += " And a.EmployeeID in(";
                    for (int i = 0; i < m_EmployeeID.Length; i++)
                    {
                        strWhere += "'" + m_EmployeeID[i]+ "',";
                    }
                    strWhere = strWhere.Substring(0, strWhere.Length - 1) + ")";
                }
                return da.GetDataTable("Select a.*,b.DepartmentName From " + tableName + " a,HRM.dbo.Department b " + strWhere +" Order By a.EmployeeID", da.sqlParameters);
            }
        }
        /// <summary>
        /// 查询该部门下的员工信息
        /// </summary>
        /// <param name="m_ApplicationOrg">ApplicationOrg</param>
        /// <param name="m_DepartmentCode">部门代码</param>
        /// <returns></returns>
        public DataTable SearchByDepartmentCode(string m_ApplicationOrg, string m_DepartmentCode)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@ApplicationOrg", m_ApplicationOrg));
                da.sqlParameters.Add(da.CreateSqlParameter("@DepartmentCode", m_DepartmentCode));
                return da.GetDataTable("Select a.*,b.DepartmentName From " + tableName + " a,HRM.dbo.Department b Where a.IsDeleted=0 And b.IsDeleted=0 And a.ApplicationOrg=b.ApplicationOrg And a.DepartmentCode=b.DepartmentCode And b.DepartmentCode=@DepartmentCode And a.ApplicationOrg=@ApplicationOrg Order By a.EmployeeID", da.sqlParameters);
            }
        }
        /// <summary>
        /// 查询Employee
        /// </summary>
        /// <param name="m_ApplicationOrg">ApplicationOrg</param>
        /// <param name="m_EmployeeID">EmployeeID</param>
        /// <returns>Employee</returns>
        public DataTable Search(string m_ApplicationOrg, string m_EmployeeID)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@ApplicationOrg", m_ApplicationOrg));
                if (!String.IsNullOrWhiteSpace(m_EmployeeID))
                {
                    da.sqlParameters.Add(da.CreateSqlParameter("@EmployeeID", m_EmployeeID));
                    return da.GetDataTable("Select a.*,b.DepartmentName From " + tableName + " a,HRM.dbo.Department b Where a.IsDeleted=0 And b.IsDeleted=0 And a.ApplicationOrg=b.ApplicationOrg And a.DepartmentCode=b.DepartmentCode And a.ApplicationOrg=@ApplicationOrg And EmployeeID=@EmployeeID Order By a.EmployeeID", da.sqlParameters);
                }
                else
                    return da.GetDataTable("Select a.*,b.DepartmentName From " + tableName + " a,HRM.dbo.Department b Where a.IsDeleted=0 And b.IsDeleted=0 And a.ApplicationOrg=b.ApplicationOrg And a.DepartmentCode=b.DepartmentCode And a.ApplicationOrg=@ApplicationOrg Order By a.EmployeeID", da.sqlParameters);
            }
        }
        /// <summary>
        /// 增加Employee
        /// </summary>
        /// <param name="m_Employee">Employee</param>
        /// <returns>返回增加结果</returns>
        public bool Add(Model.HRM.Employee m_Employee)
        {
            using (DataAccess da = new DataAccess())
            {
                try
                {
                    da.sqlParameters.Clear();
                    da.sqlParameters.Add(da.CreateSqlParameter("@ApplicationOrg", m_Employee.ApplicationOrg.Trim()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@EmployeeID", m_Employee.EmployeeID.Trim()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@EmployeeName", m_Employee.EmployeeName.Trim()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@EmployeeEnglishName", m_Employee.EmployeeEnglishName.Trim()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@EmpoyeeGender", m_Employee.EmpoyeeGender.Trim()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@EmployeeEmail", m_Employee.EmployeeEmail.Trim()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@EmployeePhone", m_Employee.EmployeePhone.Trim()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@DepartmentCode", m_Employee.DepartmentCode.Trim()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@IsDeleted", false));
                    da.sqlParameters.Add(da.CreateSqlParameter("@CreateUserID", m_Employee.CreateUserID.Trim()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@LastUpdateUserID", m_Employee.LastUpdateUserID.Trim()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@Remark", m_Employee.Remark.Trim()));
                    return da.ExecuteCommand("Insert Into " + tableName + "(ApplicationOrg,EmployeeID,EmployeeName,EmployeeEnglishName,EmpoyeeGender,EmployeeEmail,EmployeePhone,DepartmentCode,IsDeleted,CreateUserID,CreateDateTime,LastUpdateUserID,LastUpdateDateTime,Remark) Values(@ApplicationOrg,@EmployeeID,@EmployeeName,@EmployeeEnglishName,@EmpoyeeGender,@EmployeeEmail,@EmployeePhone,@DepartmentCode,@IsDeleted,@CreateUserID,GetDate(),@LastUpdateUserID,GetDate(),@Remark)", da.sqlParameters) != 0;
                }
                catch
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// 更新Employee
        /// </summary>
        /// <param name="m_Employee">Employee</param>
        /// <returns>返回更新结果</returns>
        public bool Update(Model.HRM.Employee m_Employee)
        {
            using (DataAccess da = new DataAccess())
            {
                try
                {
                    da.sqlParameters.Clear();
                    da.sqlParameters.Add(da.CreateSqlParameter("@id", m_Employee.Id));
                    da.sqlParameters.Add(da.CreateSqlParameter("@ApplicationOrg", m_Employee.ApplicationOrg.Trim()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@EmployeeID", m_Employee.EmployeeID.Trim()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@EmployeeName", m_Employee.EmployeeName.Trim()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@EmployeeEnglishName", m_Employee.EmployeeEnglishName.Trim()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@EmpoyeeGender", m_Employee.EmpoyeeGender.Trim()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@EmployeeEmail", m_Employee.EmployeeEmail.Trim()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@EmployeePhone", m_Employee.EmployeePhone.Trim()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@DepartmentCode", m_Employee.DepartmentCode.Trim()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@LastUpdateUserID", m_Employee.LastUpdateUserID.Trim()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@Remark", m_Employee.Remark.Trim()));
                    return da.ExecuteCommand("Update " + tableName + " Set ApplicationOrg=@ApplicationOrg,EmployeeID=@EmployeeID,EmployeeName=@EmployeeName,EmployeeEnglishName=@EmployeeEnglishName,EmpoyeeGender=@EmpoyeeGender,EmployeeEmail=@EmployeeEmail,EmployeePhone=@EmployeePhone,DepartmentCode=@DepartmentCode,LastUpdateUserID=@LastUpdateUserID,LastUpdateDateTime=GetDate(),Remark=@Remark Where id=@id", da.sqlParameters) != 0;
                }
                catch
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// 删除员工信息
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
        /// 获得员工信息中的m_FieldName的值
        /// </summary>
        /// <param name="m_ApplicationOrg">ApplicationOrg</param>
        /// <param name="m_EmployeeID">EmployeeID</param>
        /// <param name="m_FieldName">FieldName</param>
        /// <returns>FieldValue</returns>
        public DataTable GetFieldValue(string m_ApplicationOrg, string m_EmployeeID, string m_FieldName)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@ApplicationOrg", m_ApplicationOrg));
                da.sqlParameters.Add(da.CreateSqlParameter("@EmployeeID", m_EmployeeID.Trim().ToUpper()));
                return da.GetDataTable("select " + m_FieldName.Trim() + " From " + tableName + " Where ApplicationOrg=@ApplicationOrg And Upper(EmployeeID)=@EmployeeID", da.sqlParameters);
            }
        }
        /// <summary>
        /// 获得员工信息中的m_FieldName的值
        /// </summary>
        /// <param name="m_ApplicationOrg">ApplicationOrg</param>
        /// <param name="m_EmployeeID">EmployeeID</param>
        /// <param name="m_FieldName">FieldName</param>
        /// <returns>FieldValue</returns>
        public DataTable GetFieldValue(string m_ApplicationOrg, string[] m_EmployeeID, string m_FieldName)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@ApplicationOrg", m_ApplicationOrg));
                string strWhere = " Where ApplicationOrg=@ApplicationOrg And Upper(EmployeeID) In(";
                for (int i = 0; i < m_EmployeeID.Length; i++)
                    strWhere += "'" + m_EmployeeID[i] + "',";
                strWhere = strWhere.Substring(0, strWhere.Length - 1) + ")";
                return da.GetDataTable("select " + m_FieldName.Trim() + " From " + tableName + strWhere, da.sqlParameters);
            }
        }
        /// <summary>
        /// 是否已经存在该员工信息
        /// </summary>
        /// <param name="m_ApplicationOrg">应用组织</param>
        /// <param name="m_EmployeeID">工号</param>
        /// <returns>返回结果</returns>
        public bool IsExist(string m_ApplicationOrg, string m_EmployeeID)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@ApplicationOrg", m_ApplicationOrg.Trim().ToUpper()));
                da.sqlParameters.Add(da.CreateSqlParameter("@EmployeeID", m_EmployeeID.Trim().ToUpper()));
                object isExist = da.GetScalar("Select Count(1) From " + tableName + " Where Upper(ApplicationOrg)=@ApplicationOrg And Upper(EmployeeID)=@EmployeeID And IsDeleted=0", da.sqlParameters);
                if (isExist != null)
                    return Convert.ToInt32(isExist) > 0;
                else
                {
                    Exception ex = new Exception("Not find ApplicationOrg=" + m_ApplicationOrg + ",EmployeeID=" + m_EmployeeID + " information");
                    ex.Source = "Class name:" + tableName + ",Error method:public bool IsExist(ApplicationOrg=" + m_ApplicationOrg + ",m_EmployeeID=" + m_EmployeeID + ")";
                    throw ex;
                }
            }
        }
    }
}
