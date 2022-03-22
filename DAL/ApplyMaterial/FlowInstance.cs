using SQLHelp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DAL.ApplyMaterial
{
    public class FlowInstance : IDAL.ApplyMaterial.IFlowInstance
    {
        private const string tableName = "[PLM].[dbo].[FlowInstance]";

        /// <summary>
        /// 查询审批实例 
        /// </summary>
        /// <returns></returns>
        public DataTable Select()
        {
            using (DataAccess da = new DataAccess())
            {
                return da.GetDataTable("Select * From " + tableName + " Where IsDeleted=0");
            }
        }
        /// <summary>
        /// 查询审批实例
        /// <param name="m_id"></param>
        /// </summary>
        /// <returns></returns>
        public Model.ApplyMaterial.FlowInstance Select(long m_id)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@id", m_id));
                DataRow dr = da.GetDataRow("Select * From " + tableName + " Where id=@id", da.sqlParameters);
                if (dr != null)
                {
                    Model.ApplyMaterial.FlowInstance flowInstance = new Model.ApplyMaterial.FlowInstance();
                    flowInstance.Id = Convert.ToInt64(dr["id"]);
                    flowInstance.FlowName = dr["FlowName"].ToString().Trim();
                    flowInstance.FlowDescription = dr["FlowDescription"].ToString().Trim();
                    flowInstance.Step = Convert.ToInt32(dr["Step"]);
                    flowInstance.FlowNode = dr["FlowNode"].ToString().Trim();
                    flowInstance.FlowNodeName = dr["FlowNodeName"].ToString().Trim();
                    flowInstance.OperatorID = dr["OperatorID"].ToString().Trim();
                    flowInstance.Approver = dr["Approver"].ToString().Trim();
                    flowInstance.ApproverName = dr["ApproverName"].ToString().Trim();
                    flowInstance.Result = dr["Result"].ToString().Trim();
                    flowInstance.RejectToStep = Convert.ToInt32(dr["RejectToStep"]);
                    flowInstance.ApproveDateTime = Convert.ToDateTime(dr["ApproveDateTime"]);
                    flowInstance.IsDeleted = Convert.ToBoolean(dr["IsDeleted"]);
                    flowInstance.CreateUserID = dr["CreateUserID"].ToString().Trim();
                    flowInstance.CreateDateTime = Convert.ToDateTime(dr["CreateDateTime"]);
                    flowInstance.LastUpdateUserID = dr["LastUpdateUserID"].ToString().Trim();
                    flowInstance.LastUpdateDateTime = Convert.ToDateTime(dr["LastUpdateDateTime"]);
                    flowInstance.Remark = dr["Remark"].ToString().Trim();
                    return flowInstance;
                }
                else
                    return null;
            }
        }
        /// <summary>
        /// 查詢該表單的工作流信息
        /// </summary>
        /// <param name="m_FlowName">工作流</param>
        /// <param name="m_FormID">表單</param>
        /// <returns></returns>
        public DataTable Select(string m_FlowName, string m_FormID)
        {
            using (DataAccess da = new DataAccess())
            {
                string strFilter = " Where 1=1";
                da.sqlParameters.Clear();
                if (!string.IsNullOrEmpty(m_FlowName))
                {
                    strFilter += " And FlowName like '" + m_FlowName.Trim() + "%'";
                }
                if (!string.IsNullOrEmpty(m_FormID))
                {
                    da.sqlParameters.Add(da.CreateSqlParameter("@FormID", m_FormID.Trim()));
                    strFilter += " And FormID=@FormID";
                }
                return da.GetDataTable("Select * From " + tableName + strFilter, da.sqlParameters);
            }
        }
        /// <summary>
        /// 获得该表单该步序的工作流
        /// </summary>
        /// <param name="m_FlowName">工作流</param>
        /// <param name="m_FormID">表单编号</param>
        /// <param name="m_Step">步序</param>
        /// <returns></returns>
        public DataTable Select(string m_FlowName,string m_FormID,int m_Step)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@FormID", m_FormID.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@Step", m_Step));
                return da.GetDataTable("SELECT * FROM " + tableName + " Where IsDeleted=0 And FlowName like '" + m_FlowName.Trim() + "%' And FormID=@FormID And Step=@Step", da.sqlParameters);
            }
        }
        /// <summary>
        /// 获得该表单该步序的工作流
        /// </summary>
        /// <param name="m_FlowName">工作流</param>
        /// <param name="m_FormID">表单编号</param>
        /// <param name="m_Step">步序</param>
        /// <returns></returns>
        public DataTable Select(string m_FlowName, List<string> m_FormID, int m_Step)
        {
            using (DataAccess da = new DataAccess())
            {
                string strWhere = " Where IsDeleted=0 And FlowName like '" + m_FlowName.Trim() + "%' And Step=@Step";
                if (m_FormID.Count > 0)
                {
                    strWhere += " And FormID in(";
                    for (int i = 0; i < m_FormID.Count; i++)
                    {
                        strWhere += "'" + m_FormID[i].Trim() + "',";
                    }
                    strWhere = strWhere.Substring(0, strWhere.Length - 1) + ")";
                }
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@Step", m_Step));
                return da.GetDataTable("SELECT * FROM " + tableName + strWhere, da.sqlParameters);
            }
        }
        /// <summary>
        /// 獲得當前的工作流
        /// </summary>
        /// <param name="m_FlowName">工作流</param>
        /// <returns></returns>
        public DataTable GetCurrentWorkFlow(string m_FlowName)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                return da.GetDataTable("SELECT t.* FROM (select FormID,Step,FlowName,OperatorID,rank()over(partition by FormID order by Step) rownumber from " + tableName + " Where IsDeleted=0 And (Result is null or Result='') And FlowName like '" + m_FlowName.Trim() + "%') t where t.rownumber = 1", da.sqlParameters);
            }
        }
        /// <summary>
        /// 獲得當前的工作流
        /// </summary>
        /// <param name="m_FlowName">工作流</param>
        /// <param name="m_OperatorID">可審批人</param>
        /// <returns></returns>
        public DataTable GetCurrentWorkFlow(string m_FlowName,string m_OperatorID)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@OperatorID", m_OperatorID.Trim()));
                return da.GetDataTable("SELECT t.* FROM (select FormID,Step,FlowName,OperatorID,rank()over(partition by FormID order by Step) rownumber from " + tableName + " Where IsDeleted=0 And (Result is null or Result='') And FlowName like '" + m_FlowName.Trim() + "%') t where t.rownumber = 1 And OperatorID=@OperatorID", da.sqlParameters);
            }
        }
        /// <summary>
        /// 獲得當前的工作流
        /// </summary>
        /// <param name="m_FlowName">工作流</param>
        /// <param name="m_FormID">表单编号</param>
        /// <param name="m_OperatorID">可審批人</param>
        /// <returns></returns>
        public DataTable GetCurrentWorkFlow(string m_FlowName, string m_FormID, string m_OperatorID)
        {
            using (DataAccess da = new DataAccess())
            {
                if (!String.IsNullOrWhiteSpace(m_OperatorID))
                {
                    da.sqlParameters.Clear();
                    da.sqlParameters.Add(da.CreateSqlParameter("@FormID", m_FormID.Trim()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@OperatorID", m_OperatorID.Trim()));
                    return da.GetDataTable("SELECT t.* FROM (select FormID,Step,FlowName,OperatorID,rank()over(partition by FormID order by Step) rownumber from " + tableName + " Where IsDeleted=0 And (Result is null or Result='') And FlowName like '" + m_FlowName.Trim() + "%' And FormID=@FormID) t where t.rownumber = 1 And OperatorID=@OperatorID", da.sqlParameters);
                }
                else
                {
                    da.sqlParameters.Clear();
                    da.sqlParameters.Add(da.CreateSqlParameter("@FormID", m_FormID.Trim()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@OperatorID", m_OperatorID.Trim()));
                    return da.GetDataTable("SELECT t.* FROM (select FormID,Step,FlowName,OperatorID,rank()over(partition by FormID order by Step) rownumber from " + tableName + " Where IsDeleted=0 And (Result is null or Result='') And FlowName like '" + m_FlowName.Trim() + "%' And FormID=@FormID) t where t.rownumber = 1", da.sqlParameters);
                }
            }
        }
        /// <summary>
        /// 獲得當前的工作流
        /// </summary>
        /// <param name="m_FlowName">工作流</param>
        /// <param name="m_FormID">表单编号</param>
        /// <param name="m_OperatorID">可審批人</param>
        /// <returns></returns>
        public DataTable GetCurrentWorkFlow(string m_FlowName, List<string> m_FormID, string m_OperatorID)
        {
            using (DataAccess da = new DataAccess())
            {
                if (!String.IsNullOrWhiteSpace(m_OperatorID))
                {
                    da.sqlParameters.Clear();
                    da.sqlParameters.Add(da.CreateSqlParameter("@FormID", m_FormID[0].Trim()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@OperatorID", m_OperatorID.Trim()));
                    return da.GetDataTable("SELECT t.* FROM (select FormID,Step,FlowName,OperatorID,rank()over(partition by FormID order by Step) rownumber from " + tableName + " Where IsDeleted=0 And (Result is null or Result='') And FlowName like '" + m_FlowName.Trim() + "%' And FormID=@FormID) t where t.rownumber = 1 And OperatorID=@OperatorID", da.sqlParameters);
                }
                else
                {
                    da.sqlParameters.Clear();
                    da.sqlParameters.Add(da.CreateSqlParameter("@FormID", m_FormID[0].Trim()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@OperatorID", m_OperatorID.Trim()));
                    return da.GetDataTable("SELECT t.* FROM (select FormID,Step,FlowName,OperatorID,rank()over(partition by FormID order by Step) rownumber from " + tableName + " Where IsDeleted=0 And (Result is null or Result='') And FlowName like '" + m_FlowName.Trim() + "%' And FormID=@FormID) t where t.rownumber = 1", da.sqlParameters);
                }
            }
        }
        /// <summary>
        /// 是否已经审批完成
        /// </summary>
        /// <param name="m_FlowName"></param>
        /// <param name="m_FormID"></param>
        /// <returns></returns>
        public bool FlowIsFinished(string m_FlowName,string m_FormID)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@FormID", m_FormID.Trim().ToUpper()));
                object objCount = da.GetScalar("Select Count(1) From " + tableName + " Where FlowName Like '" + m_FlowName.Trim() + "%' And Upper(FormID)=@FormID And (Result='' Or Result is null) And IsDeleted=0", da.sqlParameters);
                if (objCount.ToString() == "0")
                    return true;
                else
                    return false;
            }
        }
        /// <summary>
        /// 是否所有节点都已经审批通过且完成了
        /// </summary>
        /// <param name="m_FlowName"></param>
        /// <param name="m_FormID"></param>
        /// <returns></returns>
        public bool AllFlowIsApproved(string m_FlowName, string m_FormID)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@FormID", m_FormID.Trim().ToUpper()));
                da.sqlParameters.Add(da.CreateSqlParameter("@Result", Util.ApproveResult.Approved.ToString()));
                object objCount = da.GetScalar("Select Count(1) From " + tableName + " Where FlowName Like '" + m_FlowName.Trim() + "%' And Upper(FormID)=@FormID And Result<>@Result And IsDeleted=0", da.sqlParameters);
                if (objCount.ToString() == "0")
                    return true;
                else
                    return false;
            }  
        }
        /// <summary>
        /// 审批通过
        /// </summary>
        /// <param name="m_FlowName">审批流</param>
        /// <param name="m_FormID">审批的表单号</param>
        /// <param name="m_Approver">审批人</param>
        /// <param name="m_Remark">备注</param>
        /// <param name="m_Message">输出信息</param>
        /// <returns></returns>
        public bool Approve(string m_FlowName,string m_FormID, string m_Approver, string m_Remark,out string m_Message)
        {
            using (DataAccess da = new DataAccess())
            {
                m_Message = "";
                try
                {
                    da.BeginTransaction();
                    da.sqlParameters.Clear();
                    da.sqlParameters.Add(da.CreateSqlParameter("@FormID", m_FormID.Trim().ToUpper()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@Approver", m_Approver));
                    object objStep = da.GetScalar("Select Step From " + tableName + " Where FlowName Like '" + m_FlowName.Trim() + "%' And Upper(FormID)=@FormID And charindex(@Approver,OperatorID)>0 And (Result='' Or Result is null) And IsDeleted=0", da.sqlParameters);
                    if (objStep == null)
                    {
                        m_Message = "你无权审批，请联系管理员！";
                        da.RollbackTransaction();
                        return false;
                    }
                    da.sqlParameters.Clear();
                    da.sqlParameters.Add(da.CreateSqlParameter("@FormID", m_FormID.Trim().ToUpper()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@Step", Convert.ToInt32(objStep)));
                    da.sqlParameters.Add(da.CreateSqlParameter("@Result", Util.ApproveResult.Approved.ToString()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@Approver", m_Approver));                 
                    da.sqlParameters.Add(da.CreateSqlParameter("@LastUpdateUserID", m_Approver));
                    da.sqlParameters.Add(da.CreateSqlParameter("@Remark", m_Remark.Trim()));
                    da.ExecuteCommand("Update " + tableName + " Set Result=@Result,Approver=@Approver,ApproverName=(SELECT EmployeeName FROM [HRM].[dbo].[Employee] where EmployeeID=@Approver And IsDeleted=0),ApproveDateTime=GetDate(),LastUpdateUserID=@LastUpdateUserID,LastUpdateDateTime=GetDate(),Remark=@Remark Where FlowName Like '" + m_FlowName.Trim() + "%' And Upper(FormID)=@FormID And Step=@Step And IsDeleted=0", da.sqlParameters);
                    
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
        /// 审批通过
        /// </summary>
        /// <param name="m_FlowName">审批流</param>
        /// <param name="m_FormID">审批的表单号</param>
        /// <param name="m_Approver">审批人</param>
        /// <param name="m_Remark">备注</param>
        /// <param name="m_Message">输出信息</param>
        /// <returns></returns>
        public bool Approve(string m_FlowName, List<string> m_FormID, string m_Approver, string m_Remark, out string m_Message)
        {
            using (DataAccess da = new DataAccess())
            {
                m_Message = "";
                try
                {
                    da.BeginTransaction();
                    for(int i=0;i< m_FormID.Count;i++)
                    { 
                        da.sqlParameters.Clear();
                        da.sqlParameters.Add(da.CreateSqlParameter("@FormID", m_FormID[i].Trim().ToUpper()));
                        da.sqlParameters.Add(da.CreateSqlParameter("@Approver", m_Approver));
                        object objStep = da.GetScalar("Select Step From " + tableName + " Where FlowName Like '" + m_FlowName.Trim() + "%' And Upper(FormID)=@FormID And charindex(@Approver,OperatorID)>0 And (Result='' Or Result is null) And IsDeleted=0", da.sqlParameters);
                        if (objStep == null)
                        {
                            m_Message = "你无权审批，请联系管理员！";
                            da.RollbackTransaction();
                            return false;
                        }
                        da.sqlParameters.Clear();
                        da.sqlParameters.Add(da.CreateSqlParameter("@FormID", m_FormID[i].Trim().ToUpper()));
                        da.sqlParameters.Add(da.CreateSqlParameter("@Step", Convert.ToInt32(objStep)));
                        da.sqlParameters.Add(da.CreateSqlParameter("@Result", Util.ApproveResult.Approved.ToString()));
                        da.sqlParameters.Add(da.CreateSqlParameter("@Approver", m_Approver));
                        da.sqlParameters.Add(da.CreateSqlParameter("@LastUpdateUserID", m_Approver));
                        da.sqlParameters.Add(da.CreateSqlParameter("@Remark", m_Remark.Trim()));
                        da.ExecuteCommand("Update " + tableName + " Set Result=@Result,Approver=@Approver,ApproverName=(SELECT EmployeeName FROM [HRM].[dbo].[Employee] where EmployeeID=@Approver And IsDeleted=0),ApproveDateTime=GetDate(),LastUpdateUserID=@LastUpdateUserID,LastUpdateDateTime=GetDate(),Remark=@Remark Where FlowName Like '" + m_FlowName.Trim() + "%' And Upper(FormID)=@FormID And Step=@Step And IsDeleted=0", da.sqlParameters);
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
        /// 审批退回
        /// </summary>
        /// <param name="m_FlowName">审批流</param>
        /// <param name="m_FormID">审批的表单号</param>
        /// <param name="m_Approver">审批人</param>
        /// <param name="m_Remark">备注</param>
        /// <param name="m_Message">输出信息</param>
        /// <param name="m_Step">退回步序</param>
        /// <returns></returns>
        public bool Reject(string m_FlowName, string m_FormID, string m_Approver, string m_Remark, out string m_Message, out int m_Step)
        {
            using (DataAccess da = new DataAccess())
            {
                m_Message = "";
                m_Step = -1;
                try
                {
                    da.BeginTransaction();
                    da.sqlParameters.Clear();
                    da.sqlParameters.Add(da.CreateSqlParameter("@FormID", m_FormID.Trim().ToUpper()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@Approver", m_Approver));
                    object objStep = da.GetScalar("Select Step From " + tableName + " Where FlowName Like '" + m_FlowName.Trim() + "%' And Upper(FormID)=@FormID And charindex(@Approver,OperatorID)>0 And (Result='' Or Result is null) And IsDeleted=0", da.sqlParameters);
                    if (objStep == null)
                    {
                        m_Message = "你无权审批，请联系管理员！";
                        return false;
                    }
                    da.sqlParameters.Clear();
                    da.sqlParameters.Add(da.CreateSqlParameter("@FormID", m_FormID.Trim().ToUpper()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@Step", Convert.ToInt32(objStep)));
                    da.sqlParameters.Add(da.CreateSqlParameter("@Approver", m_Approver));
                    da.sqlParameters.Add(da.CreateSqlParameter("@Result", Util.ApproveResult.Reject.ToString()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@LastUpdateUserID", m_Approver));
                    da.sqlParameters.Add(da.CreateSqlParameter("@Remark", m_Remark.Trim()));
                    da.ExecuteCommand("Update " + tableName + " Set Approver=@Approver,ApproverName=(SELECT EmployeeName FROM [HRM].[dbo].[Employee] where EmployeeID=@Approver And IsDeleted=0),Result=@Result,ApproveDateTime=GetDate(),LastUpdateUserID=@LastUpdateUserID,LastUpdateDateTime=GetDate(),Remark=@Remark Where FlowName Like '" + m_FlowName.Trim() + "%' And Upper(FormID)=@FormID And Step=@Step And IsDeleted=0", da.sqlParameters);

                    da.sqlParameters.Clear();
                    da.sqlParameters.Add(da.CreateSqlParameter("@FormID", m_FormID.Trim().ToUpper()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@Step", Convert.ToInt32(objStep)));
                    object objRejectToStep = da.GetScalar("Select RejectToStep From " + tableName + " Where FlowName Like '" + m_FlowName.Trim() + "%' And Upper(FormID)=@FormID And Step=@Step And IsDeleted=0", da.sqlParameters);
                    if (objRejectToStep != null && objRejectToStep.ToString()!="")
                    m_Step = Convert.ToInt32(objRejectToStep);

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
        /// 审批退回
        /// </summary>
        /// <param name="m_FlowName">审批流</param>
        /// <param name="m_FormID">审批的表单号</param>
        /// <param name="m_Approver">审批人</param>
        /// <param name="m_Remark">备注</param>
        /// <param name="m_Message">输出信息</param>
        /// <param name="m_Step">退回步序</param>
        /// <returns></returns>
        public bool Reject(string m_FlowName, List<string> m_FormID, string m_Approver, string m_Remark, out string m_Message, out int m_Step)
        {
            using (DataAccess da = new DataAccess())
            {
                m_Message = "";
                m_Step = -1;
                try
                {
                    da.BeginTransaction();
                    for (int i = 0; i < m_FormID.Count; i++)
                    { 
                        da.sqlParameters.Clear();
                        da.sqlParameters.Add(da.CreateSqlParameter("@FormID", m_FormID[i].Trim().ToUpper()));
                        da.sqlParameters.Add(da.CreateSqlParameter("@Approver", m_Approver));
                        object objStep = da.GetScalar("Select Step From " + tableName + " Where FlowName Like '" + m_FlowName.Trim() + "%' And Upper(FormID)=@FormID And charindex(@Approver,OperatorID)>0 And (Result='' Or Result is null) And IsDeleted=0", da.sqlParameters);
                        if (objStep == null)
                        {
                            m_Message = "你无权审批，请联系管理员！";
                            return false;
                        }
                        da.sqlParameters.Clear();
                        da.sqlParameters.Add(da.CreateSqlParameter("@FormID", m_FormID[i].Trim().ToUpper()));
                        da.sqlParameters.Add(da.CreateSqlParameter("@Step", Convert.ToInt32(objStep)));
                        da.sqlParameters.Add(da.CreateSqlParameter("@Approver", m_Approver));
                        da.sqlParameters.Add(da.CreateSqlParameter("@Result", Util.ApproveResult.Reject.ToString()));
                        da.sqlParameters.Add(da.CreateSqlParameter("@LastUpdateUserID", m_Approver));
                        da.sqlParameters.Add(da.CreateSqlParameter("@Remark", m_Remark.Trim()));
                        da.ExecuteCommand("Update " + tableName + " Set Approver=@Approver,ApproverName=(SELECT EmployeeName FROM [HRM].[dbo].[Employee] where EmployeeID=@Approver And IsDeleted=0),Result=@Result,ApproveDateTime=GetDate(),LastUpdateUserID=@LastUpdateUserID,LastUpdateDateTime=GetDate(),Remark=@Remark Where FlowName Like '" + m_FlowName.Trim() + "%' And Upper(FormID)=@FormID And Step=@Step And IsDeleted=0", da.sqlParameters);

                        da.sqlParameters.Clear();
                        da.sqlParameters.Add(da.CreateSqlParameter("@FormID", m_FormID[i].Trim().ToUpper()));
                        da.sqlParameters.Add(da.CreateSqlParameter("@Step", Convert.ToInt32(objStep)));
                        object objRejectToStep = da.GetScalar("Select RejectToStep From " + tableName + " Where FlowName Like '" + m_FlowName.Trim() + "%' And Upper(FormID)=@FormID And Step=@Step And IsDeleted=0", da.sqlParameters);
                        if (objRejectToStep != null && objRejectToStep.ToString() != "")
                            m_Step = Convert.ToInt32(objRejectToStep);
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
    }
}
