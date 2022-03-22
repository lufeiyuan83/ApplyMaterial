using SQLHelp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DAL.ApplyMaterial
{
    public class WorkList :IDAL.ApplyMaterial.IWorkList
    {
        private const string tableName = "PLM.dbo.WorkList";
        /// <summary>
        /// 查询工作列表 
        /// </summary>
        /// <returns></returns>
        public DataTable Select()
        {
            using (DataAccess da = new DataAccess())
            {
                return da.GetDataTable("Select * From " + tableName);
            }
        }
        /// <summary>
        /// 查询该id的工作列表
        /// <param name="m_id"></param>
        /// </summary>
        /// <returns></returns>
        public Model.ApplyMaterial.WorkList Select(long m_id)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@id", m_id));
                DataRow dr = da.GetDataRow("Select * From " + tableName + " Where id=@id", da.sqlParameters);
                if (dr != null)
                {
                    Model.ApplyMaterial.WorkList workList = new Model.ApplyMaterial.WorkList();
                    workList.Id = Convert.ToInt64(dr["Id"]);
                    workList.TableName = dr["TableName"].ToString().Trim();
                    workList.FormId = dr["FormId"].ToString().Trim();
                    workList.SystemCode = dr["SystemCode"].ToString().Trim();
                    workList.Title = dr["Title"].ToString().Trim();
                    workList.URL = dr["URL"].ToString().Trim();
                    workList.FlowName = dr["FlowName"].ToString().Trim();
                    workList.Approver = dr["Approver"].ToString().Trim();
                    workList.Status = Convert.ToBoolean(dr["Status"].ToString().Trim());
                    workList.CreateUserID = dr["CreateUserID"].ToString().Trim();
                    workList.CreateDateTime = Convert.ToDateTime(dr["CreateDateTime"]);
                    return workList;
                }
                else
                    return null;
            }
        }
        /// <summary>
        /// 查询该操作员的工作列表
        /// <param name="m_SystemCode">系统代码</param>
        /// <param name="m_EmployeeID">操作员</param>
        /// </summary>
        /// <returns></returns>
        public DataTable Select(string m_SystemCode,string m_EmployeeID)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@SystemCode", m_SystemCode.Trim().ToUpper()));
                da.sqlParameters.Add(da.CreateSqlParameter("@Approver", m_EmployeeID));
                return da.GetDataTable("Select * From " + tableName + " Where Upper(SystemCode)=@SystemCode And Approver=@Approver Order By CreateDateTime desc", da.sqlParameters);
            }
        }
        /// <summary>
        /// 查询该操作员的工作列表
        /// <param name="m_SystemCode">系统代码</param>
        /// <param name="m_EmployeeID">操作员</param>
        /// <param name="m_Status">工作列表状态</param>
        /// </summary>
        /// <returns></returns>
        public DataTable Select(string m_SystemCode, string m_EmployeeID, string m_Status)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@SystemCode", m_SystemCode.Trim().ToUpper()));
                da.sqlParameters.Add(da.CreateSqlParameter("@Approver", m_EmployeeID));
                da.sqlParameters.Add(da.CreateSqlParameter("@Status", m_Status));
                return da.GetDataTable("Select * From " + tableName + " Where Upper(SystemCode)=@SystemCode And Approver=@Approver And Status=@Status Order By CreateDateTime asc", da.sqlParameters);
            }
        }
        /// <summary>
        /// 查询该表单的工作列表
        /// <param name="m_SystemCode">系统代码</param>
        /// <param name="m_TableName">表名</param>
        /// <param name="m_FormID">表单号</param>
        /// </summary>
        /// <returns></returns>
        public Model.ApplyMaterial.WorkList Search(string m_SystemCode,string m_TableName, string m_FormID)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@SystemCode", m_SystemCode.Trim().ToUpper()));
                da.sqlParameters.Add(da.CreateSqlParameter("@TableName", m_TableName.Trim().ToUpper()));
                da.sqlParameters.Add(da.CreateSqlParameter("@FormID", m_FormID));
                DataTable dt= da.GetDataTable("Select * From " + tableName + " Where Upper(SystemCode)=@SystemCode And Upper(TableName)=@TableName And FormID=@FormID Order By CreateDateTime desc", da.sqlParameters);
                if (dt != null && dt.Rows.Count>0)
                {
                    Model.ApplyMaterial.WorkList workList = new Model.ApplyMaterial.WorkList();
                    workList.Id = Convert.ToInt64(dt.Rows[0]["Id"]);
                    workList.TableName = dt.Rows[0]["TableName"].ToString().Trim();
                    workList.FormId = dt.Rows[0]["FormId"].ToString().Trim();
                    workList.SystemCode = dt.Rows[0]["SystemCode"].ToString().Trim();
                    workList.Title = dt.Rows[0]["Title"].ToString().Trim();
                    workList.URL = dt.Rows[0]["URL"].ToString().Trim();
                    workList.FlowName = dt.Rows[0]["FlowName"].ToString().Trim();
                    workList.Approver = dt.Rows[0]["Approver"].ToString().Trim();
                    workList.Status = Convert.ToBoolean(dt.Rows[0]["Status"].ToString().Trim());
                    workList.CreateUserID = dt.Rows[0]["CreateUserID"].ToString().Trim();
                    workList.CreateDateTime = Convert.ToDateTime(dt.Rows[0]["CreateDateTime"]);
                    return workList;
                }
                else
                    return null;
            }
        }
        /// <summary>
        /// 记录工作列表
        /// <param name="m_WorkList">工作列表</param>
        /// </summary>
        /// <returns></returns>
        public bool Add(Model.ApplyMaterial.WorkList m_WorkList)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@TableName", m_WorkList.TableName.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@FormId", m_WorkList.FormId.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@SystemCode", m_WorkList.SystemCode.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@Title", m_WorkList.Title.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@URL", m_WorkList.URL.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@FlowName", m_WorkList.FlowName.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@Approver", m_WorkList.Approver.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@Status", m_WorkList.Status));
                da.sqlParameters.Add(da.CreateSqlParameter("@CreateUserID", m_WorkList.CreateUserID.Trim()));
                return da.ExecuteCommand("Insert Into " + tableName + "(TableName,FormId,SystemCode,Title,URL,FlowName,Approver,Status,CreateUserID,CreateDateTime) Values(@TableName,@FormId,@SystemCode,@Title,@URL,@FlowName,@Approver,@Status,@CreateUserID,GetDate())", da.sqlParameters) != 0 ? true : false;
            }
        }
        /// <summary>
        /// 记录工作列表
        /// <param name="m_WorkList">工作列表</param>
        /// </summary>
        /// <returns></returns>
        public bool Add(List<Model.ApplyMaterial.WorkList> m_WorkList)
        {
            using (DataAccess da = new DataAccess())
            {
                try
                {
                    da.BeginTransaction();
                    for (int i = 0; i < m_WorkList.Count; i++)
                    {
                        da.sqlParameters.Clear();
                        da.sqlParameters.Add(da.CreateSqlParameter("@TableName", m_WorkList[i].TableName.Trim()));
                        da.sqlParameters.Add(da.CreateSqlParameter("@FormId", m_WorkList[i].FormId.Trim()));
                        da.sqlParameters.Add(da.CreateSqlParameter("@SystemCode", m_WorkList[i].SystemCode.Trim()));
                        da.sqlParameters.Add(da.CreateSqlParameter("@Title", m_WorkList[i].Title.Trim()));
                        da.sqlParameters.Add(da.CreateSqlParameter("@URL", m_WorkList[i].URL.Trim()));
                        da.sqlParameters.Add(da.CreateSqlParameter("@FlowName", m_WorkList[i].FlowName.Trim()));
                        da.sqlParameters.Add(da.CreateSqlParameter("@Approver", m_WorkList[i].Approver.Trim()));
                        da.sqlParameters.Add(da.CreateSqlParameter("@Status", m_WorkList[i].Status));
                        da.sqlParameters.Add(da.CreateSqlParameter("@CreateUserID", m_WorkList[i].CreateUserID.Trim()));
                        da.ExecuteCommand("Insert Into " + tableName + "(TableName,FormId,SystemCode,Title,URL,FlowName,Approver,Status,CreateUserID,CreateDateTime) Values(@TableName,@FormId,@SystemCode,@Title,@URL,@FlowName,@Approver,@Status,@CreateUserID,GetDate())", da.sqlParameters);
                    }
                    da.CommitTransaction();
                    return true;
                }
                catch (Exception ex)
                {
                    da.RollbackTransaction();
                    da.sqlParameters.Clear();
                    da.sqlParameters.Add(da.CreateSqlParameter("@Source", ex.Source.Trim()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@Message", ex.Message.Trim()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@StackTrace", ex.StackTrace.Trim()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@UserDescription", System.Reflection.MethodBase.GetCurrentMethod()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@CreateUserID", Util.Util.UserName));
                    da.sqlParameters.Add(da.CreateSqlParameter("@ComputerName", Environment.MachineName));
                    da.ExecuteCommand("Insert Into PLM.dbo.log Values('" + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + "','" + Util.LogType.Exception.ToString() + "',@Source,@Message,@StackTrace,@UserDescription,GetDate(),@CreateUserID,@ComputerName,'')", da.sqlParameters);
                    throw ex;
                }
            }
        }
        /// <summary>
        /// 更新工作列表的状态
        /// <param name="m_WorkList">代办列表</param>
        /// </summary>
        /// <returns></returns>
        public bool Update(Model.ApplyMaterial.WorkList m_WorkList)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear(); 
                da.sqlParameters.Add(da.CreateSqlParameter("@TableName", m_WorkList.TableName));
                da.sqlParameters.Add(da.CreateSqlParameter("@FormId", m_WorkList.FormId));
                da.sqlParameters.Add(da.CreateSqlParameter("@SystemCode", m_WorkList.SystemCode));
                da.sqlParameters.Add(da.CreateSqlParameter("@Title", m_WorkList.Title));
                da.sqlParameters.Add(da.CreateSqlParameter("@URL", m_WorkList.URL));
                da.sqlParameters.Add(da.CreateSqlParameter("@FlowName", m_WorkList.FlowName));
                da.sqlParameters.Add(da.CreateSqlParameter("@Status", m_WorkList.Status));
                return da.ExecuteCommand("Update " + tableName + " Set TableName=@TableName,SystemCode=@SystemCode,Title=@Title,URL=@URL,FlowName=@FlowName,Status=@Status Where FormId=@FormId", da.sqlParameters) != 0 ? true : false;
            }
        }
        /// <summary>
        /// 更新工作列表的状态
        /// <param name="m_FormId">表单编号</param>
        /// <param name="m_Status">工作列表状态</param>
        /// </summary>
        /// <returns></returns>
        public bool Update(string m_FormId,string m_Status)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@FormId", m_FormId));
                da.sqlParameters.Add(da.CreateSqlParameter("@Status", m_Status));
                return da.ExecuteCommand("Update " + tableName + " Set Status=@Status Where FormId=@FormId", da.sqlParameters) != 0 ? true : false;
            }
        }
        /// <summary>
        /// 更新工作列表的状态
        /// <param name="m_FormId">表单编号</param>
        /// <param name="m_URL">URL</param>
        /// <param name="m_Status">工作列表状态</param>
        /// </summary>
        /// <returns></returns>
        public bool Update(List<string> m_FormId, string m_URL, string m_Status)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@URL", m_URL));
                da.sqlParameters.Add(da.CreateSqlParameter("@Status", m_Status));
                string strWhere = " Where 1=1";
                if (m_FormId.Count > 0)
                {
                    strWhere += " And FormId in(";
                    for (int i = 0; i < m_FormId.Count; i++)
                    {
                        strWhere += "'" + m_FormId[i].Trim() + "',";
                    }
                    strWhere = strWhere.Substring(0, strWhere.Length - 1) + ")";
                }
                return da.ExecuteCommand("Update " + tableName + " Set Status=@Status,URL=@URL" + strWhere, da.sqlParameters) != 0 ? true : false;
            }
        }
        /// <summary>
        /// 更新工作列表的状态
        /// <param name="m_FormId">表单编号</param>
        /// <param name="m_Title">Title</param>
        /// <param name="m_URL">URL</param>
        /// <param name="m_Status">工作列表状态</param>
        /// </summary>
        /// <returns></returns>
        public bool Update(List<string> m_FormId,string m_Title,string m_URL, string m_Status)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@Title", m_Title));
                da.sqlParameters.Add(da.CreateSqlParameter("@URL", m_URL));
                da.sqlParameters.Add(da.CreateSqlParameter("@Status", m_Status));
                string strWhere = " Where 1=1";
                if (m_FormId.Count > 0)
                {
                    strWhere += " And FormId in(";
                    for (int i = 0; i < m_FormId.Count; i++)
                    {
                        strWhere += "'" + m_FormId[i].Trim() + "',";
                    }
                    strWhere = strWhere.Substring(0, strWhere.Length - 1) + ")";
                }
                return da.ExecuteCommand("Update " + tableName + " Set Title=@Title,Status=@Status,URL=@URL" + strWhere, da.sqlParameters) != 0 ? true : false;
            }
        }
    }
}
