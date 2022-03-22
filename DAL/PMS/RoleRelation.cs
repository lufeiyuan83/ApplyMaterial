using SQLHelp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DAL.PMS
{
    public class RoleRelation : IDAL.PMS.IRoleRelation
    {
        private const string tableName = "PMS.dbo.RoleRelation";
        /// <summary>
        /// 查询RoleRelation
        /// </summary>
        /// <returns>RoleRelation</returns>
        public DataTable Select()
        {
            using (DataAccess da = new DataAccess())
            {
                return da.GetDataTable("Select * From " + tableName + " Order By Id");
            }
        }
        /// <summary>
        /// 查询单条角色关系表
        /// </summary>
        /// <param name="m_id">主键</param>
        /// <returns></returns>
        public Model.PMS.RoleRelation Select(long m_Id)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@Id", m_Id));
                DataRow dr = da.GetDataRow("Select * From " + tableName + " Where Id=@Id", da.sqlParameters);
                if (dr != null)
                {
                    Model.PMS.RoleRelation roleRelation = new Model.PMS.RoleRelation();
                    roleRelation.Id = Convert.ToInt64(dr["Id"]);
                    roleRelation.ApplicationOrg = dr["ApplicationOrg"].ToString().Trim();
                    roleRelation.RoleId = Convert.ToInt64(dr["RoleId"]);
                    roleRelation.EmployeeID = dr["EmployeeID"].ToString().Trim();
                    return roleRelation;
                }
                else
                    return null;
            }
        }
        /// <summary>
        /// 查询该角色下的员工
        /// </summary>
        /// <param name="m_RoleId">角色主键</param>
        /// <returns></returns>
        public DataTable SearchByRoleId(long m_RoleId)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@RoleId", m_RoleId));
                return da.GetDataTable("Select * From " + tableName + " Where RoleId=@RoleId Order By EmployeeID", da.sqlParameters);
            }
        }
        /// <summary>
        /// 增加RoleRelation
        /// </summary>
        /// <param name="m_RoleRelation">RoleRelation</param>
        /// <returns>返回增加结果</returns>
        public bool Add(Model.PMS.RoleRelation m_RoleRelation)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@ApplicationOrg", m_RoleRelation.ApplicationOrg.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@RoleId", m_RoleRelation.RoleId));
                da.sqlParameters.Add(da.CreateSqlParameter("@EmployeeID", m_RoleRelation.EmployeeID.Trim()));
                return da.ExecuteCommand("Insert Into " + tableName + "(ApplicationOrg,RoleId,EmployeeID) Values(@ApplicationOrg,@RoleId,@EmployeeID)", da.sqlParameters) != 0 ? true : false;
            }
        }
        /// <summary>
        /// 更新RoleRelation
        /// </summary>
        /// <param name="m_RoleRelation">RoleRelation</param>
        /// <returns>返回更新结果</returns>
        public bool Update(Model.PMS.RoleRelation m_RoleRelation)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@Id", m_RoleRelation.Id));
                da.sqlParameters.Add(da.CreateSqlParameter("@ApplicationOrg", m_RoleRelation.ApplicationOrg.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@RoleId", m_RoleRelation.RoleId));
                da.sqlParameters.Add(da.CreateSqlParameter("@EmployeeID", m_RoleRelation.EmployeeID.Trim()));
                return da.ExecuteCommand("Update " + tableName + " Set ApplicationOrg=@ApplicationOrg,RoleId=@RoleId,EmployeeID=@EmployeeID Where Id=@Id", da.sqlParameters) != 0 ? true : false;
            }
        }
        /// <summary>
        /// 删除RoleRelation
        /// </summary>
        /// <param name="m_Id">主键</param>
        /// <returns>返回删除结果</returns>
        public bool Delete(long m_Id)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@Id", m_Id));
                return da.ExecuteCommand("Delete From " + tableName + " Where Id=@Id", da.sqlParameters) != 0 ? true : false;
            }
        }
        /// <summary>
        /// 是否已经存在该角色关系表
        /// </summary>
        /// <param name="m_RoleID">角色代码</param>
        /// <param name="m_EmployeeID">工号</param>
        /// <returns>返回结果</returns>
        public bool IsExist(string m_RoleID, string m_EmployeeID)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@RoleId", m_RoleID.Trim().ToUpper()));
                da.sqlParameters.Add(da.CreateSqlParameter("@EmployeeID", m_EmployeeID.Trim().ToUpper()));
                object isExist = da.GetScalar("Select Count(1) From " + tableName + " Where Upper(RoleId)=@RoleId And Upper(EmployeeID)=@EmployeeID", da.sqlParameters);
                if (isExist != null)
                    return Convert.ToInt32(isExist) > 0;
                else
                {
                    Exception ex = new Exception("Not find RoleId=" + m_RoleID + ",EmployeeID=" + m_EmployeeID + " information");
                    ex.Source = "Class name:" + tableName + ",Error method:public bool IsExist(m_RoleID=" + m_RoleID + ",m_EmployeeID=" + m_EmployeeID + ")";
                    throw ex;
                }
            }
        }
        /// <summary>
        /// 获得角色关系表中的m_FieldName的值
        /// </summary>
        /// <param name="m_EmployeeID">工号</param>
        /// <param name="m_FieldName">字段名称，以逗号分隔</param>
        /// <returns>字段值</returns>
        public DataTable GetFieldValue(string m_EmployeeID, string m_FieldName)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@EmployeeID", m_EmployeeID.Trim().ToUpper()));
                return da.GetDataTable("select " + m_FieldName.Trim() + " From " + tableName + " Where Upper(EmployeeID)=@EmployeeID", da.sqlParameters);
            }
        }
        /// <summary>
        /// 获得角色关系表中的m_FieldName的值
        /// </summary>
        /// <param name="m_EmployeeID">工号</param>
        /// <param name="m_FieldName">字段名称，以逗号分隔</param>
        /// <returns>字段值</returns>
        public DataTable GetFieldValue(List<string> m_EmployeeID, string m_FieldName)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                string strWhere = " Where 1=1";
                if (m_EmployeeID.Count != 1 || !String.IsNullOrEmpty(m_EmployeeID[0]))
                {
                    if (m_EmployeeID.Count > 0)
                        strWhere = " And Upper(EmployeeID) In(";
                    for (int i = 0; i < m_EmployeeID.Count; i++)
                        strWhere += "'" + m_EmployeeID[i].Trim().ToUpper() + "',";
                    strWhere = strWhere.Substring(0, strWhere.Length - 1) + ")";
                }
                return da.GetDataTable("select " + m_FieldName.Trim() + " From " + tableName + strWhere, da.sqlParameters);
            }
        }
    }
}
