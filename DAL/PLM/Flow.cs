﻿using SQLHelp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DAL.PLM
{
    public class Flow: IDAL.PLM.IFlow
    {
        private const string tableName = "[PLM].[dbo].[Flow]";

        /// <summary>
        /// 查询审批流
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
        /// 查询审批流
        /// <param name="m_id"></param>
        /// </summary>
        /// <returns></returns>
        public Model.PLM.Flow Select(long m_id)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@id", m_id));
                DataRow dr = da.GetDataRow("Select * From " + tableName + " Where id=@id", da.sqlParameters);
                if (dr != null)
                {
                    Model.PLM.Flow flow = new Model.PLM.Flow();
                    flow.Id = Convert.ToInt64(dr["id"]);
                    flow.FlowName = dr["FlowName"].ToString().Trim();
                    flow.FlowGroup = Convert.ToInt32(dr["FlowGroup"]);
                    flow.FlowCode = dr["FlowCode"].ToString().Trim();
                    flow.FlowDescription = dr["FlowDescription"].ToString().Trim();
                    flow.Step = Convert.ToInt32(dr["Step"]);
                    flow.FlowNode = dr["FlowNode"].ToString().Trim();
                    flow.FlowNodeName = dr["FlowNodeName"].ToString().Trim();
                    flow.OperatorID = dr["OperatorID"].ToString().Trim();
                    flow.RejectToStep = Convert.ToInt32(dr["RejectToStep"]);
                    flow.IsDeleted = Convert.ToBoolean(dr["IsDeleted"]);
                    flow.CreateUserID = dr["CreateUserID"].ToString().Trim();
                    flow.CreateDateTime = Convert.ToDateTime(dr["CreateDateTime"]);
                    flow.LastUpdateUserID = dr["LastUpdateUserID"].ToString().Trim();
                    flow.LastUpdateDateTime = Convert.ToDateTime(dr["LastUpdateDateTime"]);
                    flow.Remark = dr["Remark"].ToString().Trim();
                    return flow;
                }
                else
                    return null;
            }
        }
        /// <summary>
        /// 获得工作流名称
        /// </summary>
        /// <param name="m_FlowName">工作流</param>
        /// <param name="m_Operator">申请人</param>
        /// <returns></returns>
        public DataTable Select(string m_FlowName, string m_Operator)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@FlowName", m_FlowName.Trim()));
                return da.GetDataTable("Select * From " + tableName + " Where FlowName=@FlowName And OperatorID Like '%" + m_Operator + "%' And IsDeleted=0", da.sqlParameters);
            }
        }
        /// <summary>
        /// 获得工作流名称
        /// </summary>
        /// <param name="m_FlowName">工作流前缀名称</param>
        /// <param name="m_FlowCode">流程代码</param>
        /// <param name="m_Operator">申请人</param>
        /// <returns></returns>
        public DataTable Select(string m_FlowName,string m_FlowCode, string m_Operator)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@FlowName", m_FlowName.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@FlowCode", m_FlowCode.Trim()));
                return da.GetDataTable("Select * From " + tableName + " Where FlowName=@FlowName And FlowCode=@FlowCode And OperatorID Like '%" + m_Operator + "%' And IsDeleted=0", da.sqlParameters);
            }
        }
        /// <summary>
        /// 获得工作流名称
        /// </summary>
        /// <param name="m_FlowName">工作流</param>
        /// <param name="m_Operator">申请人</param>
        /// <returns></returns>
        public string GetFlowName(string m_FlowName, string m_Operator)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@FlowName", m_FlowName.Trim()));
                //da.sqlParameters.Add(da.CreateSqlParameter("@Operator", m_Operator.Trim()));
                object objFlowName = da.GetScalar("Select distinct FlowName From " + tableName + " Where FlowName=@FlowName And OperatorID Like '%" + m_Operator + "%' And IsDeleted=0", da.sqlParameters);
                if (objFlowName == null || objFlowName.ToString()=="")
                    return "";
                else
                    return objFlowName.ToString();
            }
        }
        /// <summary>
        /// 检查是否有审批权限
        /// </summary>
        /// <param name="m_FlowName">审批流</param>
        /// <param name="m_FlowCode">审批流编码</param>
        /// <param name="m_Approver">审批人</param>
        /// <returns></returns>
        public bool CheckApproveAccess(string m_FlowName, string m_FlowCode, string m_Approver)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@FlowName", m_FlowName.Trim().ToUpper()));
                da.sqlParameters.Add(da.CreateSqlParameter("@FlowCode", m_FlowCode.Trim().ToUpper()));
                return Convert.ToInt32(da.GetScalar("Select Count(1) From " + tableName + " Where Upper(FlowName)=@FlowName And Upper(FlowCode)=@FlowCode And IsDeleted=0 And Upper(OperatorID) Like '%" + m_Approver.Trim().ToUpper() + "%'", da.sqlParameters)) > 0;
            }
        }
        /// <summary>
        /// 发起审批
        /// </summary>
        /// <param name="m_FlowName">审批流</param>
        /// <param name="m_FlowCode">审批流编码</param>
        /// <param name="m_FormID">业务表单号</param>
        /// <param name="m_Approver">审批人</param>
        /// <param name="m_Message">输出信息</param>
        /// <returns></returns>
        public bool StartFlow(string m_FlowName,string m_FlowCode, string m_FormID, string m_Approver, out string m_Message)
        {
            using (DataAccess da = new DataAccess())
            {
                m_Message = "";
                try
                {
                    da.BeginTransaction();
                    da.sqlParameters.Clear();
                    da.sqlParameters.Add(da.CreateSqlParameter("@FlowName", m_FlowName.Trim().ToUpper()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@FlowCode", m_FlowCode.Trim().ToUpper()));
                    DataTable dtFlow = da.GetDataTable("Select FlowGroup,Step From " + tableName + " Where Upper(FlowName)=@FlowName And Upper(FlowCode)=@FlowCode And IsDeleted=0 And Upper(OperatorID) Like '%" + m_Approver.Trim().ToUpper() + "%'", da.sqlParameters);
                    if (dtFlow == null || dtFlow.Rows.Count==0)
                    {
                        m_Message = "你无权发起审批，请联系管理员！";
                        return false;
                    }
                    da.sqlParameters.Clear();
                    da.sqlParameters.Add(da.CreateSqlParameter("@FlowName", m_FlowName.Trim().ToUpper()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@FlowGroup", dtFlow.Rows[0]["FlowGroup"].ToString().ToUpper()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@FlowCode", m_FlowCode.Trim().ToUpper()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@FormID", m_FormID));
                    da.ExecuteCommand("Insert Into [PLM].[dbo].[FlowInstance](FormID,FlowName,FlowGroup,FlowCode,Step,FlowNode,FlowNodeName,Approver,ApproverName,RejectToStep,IsDeleted,CreateUserID,CreateDateTime,LastUpdateUserID,LastUpdateDateTime,Remark) Select @FormID,FlowName,FlowGroup,FlowCode,Step,FlowNode,FlowNodeName,OperatorID,(SELECT EmployeeName FROM [HRM].[dbo].[Employee] where EmployeeID=OperatorID And IsDeleted=0),RejectToStep,IsDeleted,CreateUserID,CreateDateTime,LastUpdateUserID,LastUpdateDateTime,Remark From " + tableName + " Where Upper(FlowName)=@FlowName And Upper(FlowGroup)=@FlowGroup And Upper(FlowCode)=@FlowCode And IsDeleted=0", da.sqlParameters);
                    
                    da.sqlParameters.Clear();
                    da.sqlParameters.Add(da.CreateSqlParameter("@FlowName", m_FlowName.Trim().ToUpper()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@FlowGroup", dtFlow.Rows[0]["FlowGroup"].ToString().ToUpper()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@FlowCode", m_FlowCode.Trim().ToUpper()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@FormID", m_FormID));
                    da.sqlParameters.Add(da.CreateSqlParameter("@Step", Convert.ToInt32(dtFlow.Rows[0]["Step"])));
                    da.sqlParameters.Add(da.CreateSqlParameter("@Result", Util.ApproveResult.Approved.ToString()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@Approver", m_Approver));            
                    da.sqlParameters.Add(da.CreateSqlParameter("@LastUpdateUserID", m_Approver));
                    da.ExecuteCommand("Update [PLM].[dbo].[FlowInstance] Set Result=@Result,Approver=@Approver,ApproverName=(SELECT EmployeeName FROM [HRM].[dbo].[Employee] where EmployeeID=@Approver And IsDeleted=0),ApproveDateTime=GetDate(),LastUpdateUserID=@LastUpdateUserID,LastUpdateDateTime=GetDate() Where Upper(FlowName)=@FlowName And Upper(FlowGroup)=@FlowGroup And FormID=@FormID And Approver=@Approver And Step<>(Select Min(Step) From " + tableName + " Where Upper(FlowName)=@FlowName And Upper(FlowGroup)=@FlowGroup And Upper(FlowCode)=@FlowCode And FormID=@FormID And IsDeleted=0) And IsDeleted=0", da.sqlParameters);

                    da.sqlParameters.Clear();
                    da.sqlParameters.Add(da.CreateSqlParameter("@FlowName", m_FlowName.Trim().ToUpper()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@FlowGroup", dtFlow.Rows[0]["FlowGroup"].ToString().ToUpper()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@FlowCode", m_FlowCode.Trim().ToUpper()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@FormID", m_FormID));
                    da.sqlParameters.Add(da.CreateSqlParameter("@Step", Convert.ToInt32(dtFlow.Rows[0]["Step"])));
                    da.sqlParameters.Add(da.CreateSqlParameter("@Result", Util.ApproveResult.Approved.ToString()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@Approver", m_Approver));
                    da.sqlParameters.Add(da.CreateSqlParameter("@LastUpdateUserID", m_Approver));
                    da.ExecuteCommand("Update [PLM].[dbo].[FlowInstance] Set Result=@Result,Approver=@Approver,ApproverName=(SELECT EmployeeName FROM [HRM].[dbo].[Employee] where EmployeeID=@Approver And IsDeleted=0),ApproveDateTime=GetDate(),LastUpdateUserID=@LastUpdateUserID,LastUpdateDateTime=GetDate() Where Upper(FlowName)=@FlowName And Upper(FlowGroup)=@FlowGroup And FormID=@FormID And Step=(Select Min(Step) From " + tableName + " Where Upper(FlowName)=@FlowName And Upper(FlowGroup)=@FlowGroup And Upper(FlowCode)=@FlowCode And FormID=@FormID And IsDeleted=0) And IsDeleted=0", da.sqlParameters);

                    da.sqlParameters.Clear();
                    da.sqlParameters.Add(da.CreateSqlParameter("@FlowName", m_FlowName.Trim().ToUpper()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@FlowGroup", dtFlow.Rows[0]["FlowGroup"].ToString().ToUpper()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@FlowCode", m_FlowCode.Trim().ToUpper()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@FormID", m_FormID));
                    DataTable dtFlowInstance = da.GetDataTable("Select Top 1 * From [PLM].[dbo].[FlowInstance] Where Upper(FlowName)=@FlowName And Upper(FlowGroup)=@FlowGroup And Upper(FlowCode)=@FlowCode And FormID=@FormID And IsDeleted=0 And Result is null Order By Step", da.sqlParameters);
                    if (dtFlowInstance != null && dtFlowInstance.Rows.Count > 0)
                        m_Message = dtFlowInstance.Rows[0]["Approver"].ToString();

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
                    throw;
                }
            }
        }
    }
}
