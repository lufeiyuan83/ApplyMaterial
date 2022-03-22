using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BLL.ApplyMaterial
{
    public class FlowInstance
    {
        private readonly IDAL.ApplyMaterial.IFlowInstance dal = DALFactory.DALFactory.GetInstance().GetFlowInstance();

        /// <summary>
        /// 查询审批实例
        /// </summary>
        /// <returns></returns>
        public DataTable Select()
        {
            return dal.Select();
        }
        /// <summary>
        /// 查询审批实例
        /// <param name="m_id"></param>
        /// </summary>
        /// <returns></returns>
        public Model.ApplyMaterial.FlowInstance Select(long m_id)
        {
            return dal.Select(m_id);
        }
        /// <summary>
        /// 查詢該表單的工作流信息
        /// </summary>
        /// <param name="m_FlowName">工作流</param>
        /// <param name="m_FormID">表單</param>
        /// <returns></returns>
        public DataTable Select(string m_FlowName, string m_FormID)
        {
            return dal.Select(m_FlowName, m_FormID);
        }
        /// <summary>
        /// 获得该表单该步序的工作流
        /// </summary>
        /// <param name="m_FlowName">工作流</param>
        /// <param name="m_FormID">表单编号</param>
        /// <param name="m_Step">步序</param>
        /// <returns></returns>
        public DataTable Select(string m_FlowName, string m_FormID, int m_Step)
        {
            return dal.Select(m_FlowName, m_FormID, m_Step);
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
            return dal.Select(m_FlowName, m_FormID, m_Step);
        }
        /// <summary>
        /// 獲得當前的工作流
        /// </summary>
        /// <param name="m_FlowName">工作流</param>
        /// <returns></returns>
        public DataTable GetCurrentWorkFlow(string m_FlowName)
        {
            return dal.GetCurrentWorkFlow(m_FlowName);
        }
        /// <summary>
        /// 獲得當前的工作流
        /// </summary>
        /// <param name="m_FlowName">工作流</param>
        /// <param name="m_OperatorID">可審批人</param>
        /// <returns></returns>
        public DataTable GetCurrentWorkFlow(string m_FlowName, string m_OperatorID)
        {
            return dal.GetCurrentWorkFlow(m_FlowName, m_OperatorID);
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
            return dal.GetCurrentWorkFlow(m_FlowName, m_FormID,m_OperatorID);
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
            return dal.GetCurrentWorkFlow(m_FlowName, m_FormID[0], m_OperatorID);
        }
        /// <summary>
        /// 是否已经审批完成
        /// </summary>
        /// <param name="m_FlowName"></param>
        /// <param name="m_FormID"></param>
        /// <returns></returns>
        public bool FlowIsFinished(string m_FlowName, string m_FormID)
        {
            return dal.FlowIsFinished(m_FlowName, m_FormID);
        }
        /// <summary>
        /// 是否已经审批完成
        /// </summary>
        /// <param name="m_FlowName"></param>
        /// <param name="m_FormID"></param>
        /// <returns></returns>
        public bool FlowIsFinished(string m_FlowName, List<string> m_FormID)
        {
            return dal.FlowIsFinished(m_FlowName, m_FormID[0]);
        }
        /// <summary>
        /// 是否所有节点都已经审批通过且完成了
        /// </summary>
        /// <param name="m_FlowName"></param>
        /// <param name="m_FormID"></param>
        /// <returns></returns>
        public bool AllFlowIsApproved(string m_FlowName, string m_FormID)
        {
            return dal.AllFlowIsApproved(m_FlowName, m_FormID);
        }
        /// <summary>
        /// 是否所有节点都已经审批通过且完成了
        /// </summary>
        /// <param name="m_FlowName"></param>
        /// <param name="m_FormID"></param>
        /// <returns></returns>
        public bool AllFlowIsApproved(string m_FlowName, List<string> m_FormID)
        {
            return dal.AllFlowIsApproved(m_FlowName, m_FormID[0]);
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
        public bool Approve(string m_FlowName, string m_FormID, string m_Approver, string m_Remark, out string m_Message)
        {
            return dal.Approve(m_FlowName,m_FormID, m_Approver, m_Remark, out m_Message);
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
            return dal.Approve(m_FlowName, m_FormID, m_Approver, m_Remark, out m_Message);
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
            return dal.Reject(m_FlowName, m_FormID, m_Approver, m_Remark, out m_Message, out m_Step);
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
            return dal.Reject(m_FlowName, m_FormID, m_Approver, m_Remark, out m_Message, out m_Step);
        }
    }
}
