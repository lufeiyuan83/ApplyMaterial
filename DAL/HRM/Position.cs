using SQLHelp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DAL.HRM
{
    public class Position :IDAL.HRM.IPosition
    {
        private const string tableName = "HRM.dbo.Position";
        /// <summary>
        /// 查询Position
        /// </summary>
        /// <returns>Position</returns>
        public DataTable Select()
        {
            using (DataAccess da = new DataAccess())
            {
                return da.GetDataTable("Select * From " + tableName + " Where IsDeleted=0 Order By id");
            }
        }
        /// <summary>
        /// 查询单条Position
        /// </summary>
        /// <param name="m_id">主键</param>
        /// <returns>Position</returns>
        public Model.HRM.Position Select(long m_id)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@id", m_id));
                DataRow dr = da.GetDataRow("Select * From " + tableName + " Where id=@id And IsDeleted=0", da.sqlParameters);
                if (dr != null)
                {
                    Model.HRM.Position position = new Model.HRM.Position();
                    position.Id = Convert.ToInt64(dr["Id"]);
                    position.ApplicationOrg = dr["ApplicationOrg"].ToString().Trim(); 
                    position.DepartmentId = Convert.ToInt64(dr["DepartmentId"]);
                    position.PositionCode = dr["PositionCode"].ToString().Trim();
                    position.PositionName = dr["PositionName"].ToString().Trim();
                    position.IsDeleted = Convert.ToBoolean(dr["IsDeleted"]);
                    position.CreateUserID = dr["CreateUserID"].ToString().Trim();
                    position.CreateDateTime = Convert.ToDateTime(dr["CreateDateTime"]);
                    position.LastUpdateUserID = dr["LastUpdateUserID"].ToString().Trim();
                    position.LastUpdateDateTime = Convert.ToDateTime(dr["LastUpdateDateTime"]);
                    position.Remark = dr["Remark"].ToString().Trim();
                    return position;
                }
                else
                    return null;
            }
        }
        /// <summary>
        /// 查询单条Position
        /// </summary>
        /// <param name="m_ApplicationOrg">组织代码</param>
        /// <returns>Position</returns>
        public DataTable Select(string m_ApplicationOrg)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@ApplicationOrg", m_ApplicationOrg.Trim().ToUpper()));
                return da.GetDataTable("Select * From " + tableName + " Where Upper(ApplicationOrg)=@ApplicationOrg And IsDeleted=0", da.sqlParameters);
            }
        }
        /// <summary>
        /// 查询单条Position
        /// </summary>
        /// <param name="m_ApplicationOrg">组织代码</param>
        /// <param name="m_DepartmentId">部门主键</param>
        /// <returns>Position</returns>
        public DataTable Select(string m_ApplicationOrg, long m_DepartmentId)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@ApplicationOrg", m_ApplicationOrg.Trim().ToUpper()));
                da.sqlParameters.Add(da.CreateSqlParameter("@DepartmentId", m_DepartmentId));
                return da.GetDataTable("Select * From " + tableName + " Where Upper(ApplicationOrg)=@ApplicationOrg And DepartmentId=@DepartmentId And IsDeleted=0", da.sqlParameters);
            }
        }
        /// <summary>
        /// 查询Position
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
                    strWhere += " And Upper(ApplicationOrg) in(";
                for (int i = 0; i < m_ApplicationOrg.Count; i++)
                    strWhere += "'" + m_ApplicationOrg[i].Trim().ToUpper() + "',";
                strWhere = strWhere.Substring(0, strWhere.Length - 1) + ")";
                return da.GetDataTable("Select * From " + tableName + strWhere + " Order By id", da.sqlParameters);
            }
        }
        /// <summary>
        /// 查询Position
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
        /// 增加Position
        /// </summary>
        /// <param name="m_Position">Position</param>
        /// <returns>返回增加结果</returns>
        public bool Add(Model.HRM.Position m_Position)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@ApplicationOrg", m_Position.ApplicationOrg.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@DepartmentId", m_Position.DepartmentId));
                da.sqlParameters.Add(da.CreateSqlParameter("@PositionCode", m_Position.PositionCode.Trim())); 
                da.sqlParameters.Add(da.CreateSqlParameter("@PositionName", m_Position.PositionName.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@IsDeleted", m_Position.IsDeleted));
                da.sqlParameters.Add(da.CreateSqlParameter("@CreateUserID", m_Position.CreateUserID.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@LastUpdateUserID", m_Position.LastUpdateUserID.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@Remark", m_Position.Remark.Trim()));
                return da.ExecuteCommand("Insert Into " + tableName + "(ApplicationOrg,DepartmentId,PositionCode,PositionName,CreateUserID,LastUpdateUserID,Remark) Values(@ApplicationOrg,@DepartmentId,@PositionCode,@PositionName,@CreateUserID,@LastUpdateUserID,@Remark)", da.sqlParameters) != 0 ? true : false;
            }
        }
        /// <summary>
        /// 更新Position
        /// </summary>
        /// <param name="m_Position">Position</param>
        /// <returns>返回更新结果</returns>
        public bool Update(Model.HRM.Position m_Position)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@id", m_Position.Id));
                da.sqlParameters.Add(da.CreateSqlParameter("@ApplicationOrg", m_Position.ApplicationOrg.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@PositionCode", m_Position.PositionCode.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@PositionName", m_Position.PositionName.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@LastUpdateUserID", m_Position.LastUpdateUserID.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@Remark", m_Position.Remark.Trim()));
                return da.ExecuteCommand("Update " + tableName + " Set ApplicationOrg=@ApplicationOrg,PositionCode=@PositionCode,PositionName=@PositionName,LastUpdateUserID=@LastUpdateUserID,LastUpdateDateTime=GetDate(),Remark=@Remark Where id=@id", da.sqlParameters) != 0 ? true : false;
            }
        }
        /// <summary>
        /// 删除Position
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
        /// <param name="m_ApplicationOrg">组织代码</param>
        /// <param name="m_DepartmentId">部门主键</param>
        /// <param name="m_PositionCode">岗位代码</param>
        /// <returns>返回结果</returns>
        public bool IsExist(string m_ApplicationOrg, long m_DepartmentId, string m_PositionCode)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@ApplicationOrg", m_ApplicationOrg.Trim().ToUpper()));
                da.sqlParameters.Add(da.CreateSqlParameter("@DepartmentId", m_DepartmentId));
                da.sqlParameters.Add(da.CreateSqlParameter("@PositionCode", m_PositionCode.Trim().ToUpper()));
                object isExist = da.GetScalar("Select Count(1) From " + tableName + " Where Upper(ApplicationOrg)=@ApplicationOrg And DepartmentId=@DepartmentId And Upper(PositionCode)=@PositionCode And IsDeleted=0", da.sqlParameters);
                if (isExist != null)
                    return Convert.ToInt32(isExist) > 0;
                else
                {
                    Exception ex = new Exception("Not find ApplicationOrg=" + m_ApplicationOrg + ",DepartmentId=" + m_DepartmentId + ",PositionCode=" + m_PositionCode + " information");
                    ex.Source = "Class name:" + tableName + ",Error method:public bool IsExist(m_ApplicationOrg=" + m_ApplicationOrg + ",m_DepartmentId=" + m_DepartmentId + ",m_PositionCode=" + m_PositionCode + ")";
                    throw ex;
                }
            }
        }
        /// <summary>
        /// 获得岗位信息中的m_FieldName的值
        /// </summary>
        /// <param name="m_ApplicationOrg">组织代码</param>
        /// <param name="m_DepartmentId">部门主键</param>
        /// <param name="m_PositionCode">岗位代码</param>
        /// <param name="m_FieldName">字段名称，以逗号分隔</param>
        /// <returns>字段值</returns>
        public DataTable GetFieldValue(string m_ApplicationOrg, long m_DepartmentId, string m_PositionCode, string m_FieldName)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@ApplicationOrg", m_ApplicationOrg.Trim().ToUpper()));
                da.sqlParameters.Add(da.CreateSqlParameter("@DepartmentId", m_DepartmentId));
                da.sqlParameters.Add(da.CreateSqlParameter("@PositionCode", m_PositionCode.Trim().ToUpper()));
                da.sqlParameters.Add(da.CreateSqlParameter("@FieldName", m_FieldName.Trim()));
                return da.GetDataTable("select " + m_FieldName.Trim() + " From " + tableName + " Where Upper(ApplicationOrg)=@ApplicationOrg And DepartmentId=@DepartmentId And Upper(PositionCode)=@PositionCode And IsDeleted=0", da.sqlParameters);
            }
        }
        /// <summary>
        /// 获得岗位信息中的m_FieldName的值
        /// </summary>
        /// <param name="m_ApplicationOrg">组织代码</param>
        /// <param name="m_DepartmentId">部门主键</param>
        /// <param name="m_PositionCode">岗位代码</param>
        /// <param name="m_FieldName">字段名称，以逗号分隔</param>
        /// <returns>字段值</returns>
        public DataTable GetFieldValue(List<string> m_ApplicationOrg, long m_DepartmentId, string m_PositionCode, string m_FieldName)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@DepartmentId", m_DepartmentId));
                da.sqlParameters.Add(da.CreateSqlParameter("@PositionCode", m_PositionCode.Trim().ToUpper()));
                string strWhere = " Where IsDeleted=0 And DepartmentId=@DepartmentId And Upper(PositionCode)=@PositionCode";
                if (m_ApplicationOrg.Count > 0)
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
