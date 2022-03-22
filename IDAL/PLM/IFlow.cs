using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace IDAL.PLM
{
    public interface IFlow
    {        
        /// <summary>
        /// 查询审批流
        /// </summary>
        /// <returns></returns>
        DataTable Select();
        /// <summary>
        /// 查询审批流
        /// <param name="m_id"></param>
        /// </summary>
        /// <returns></returns>
        Model.PLM.Flow Select(long m_id);
        /// <summary>
        /// 获得工作流名称
        /// </summary>
        /// <param name="m_FlowName">工作流</param>
        /// <param name="m_Operator">申请人</param>
        /// <returns></returns>
        DataTable Select(string m_FlowName, string m_Operator);
        /// <summary>
        /// 获得工作流名称
        /// </summary>
        /// <param name="m_FlowName">工作流前缀名称</param>
        /// <param name="m_FlowCode">流程代码</param>
        /// <param name="m_Operator">申请人</param>
        /// <returns></returns>
        DataTable Select(string m_FlowName, string m_FlowCode, string m_Operator);
        /// <summary>
        /// 获得工作流名称
        /// </summary>
        /// <param name="m_FlowName">工作流</param>
        /// <param name="m_Operator">申请人</param>
        /// <returns></returns>
        string GetFlowName(string m_FlowName, string m_Operator);
        /// <summary>
        /// 检查是否有审批权限
        /// </summary>
        /// <param name="m_FlowName">审批流</param>
        /// <param name="m_FlowCode">审批流编码</param>
        /// <param name="m_Approver">审批人</param>
        /// <returns></returns>
        bool CheckApproveAccess(string m_FlowName, string m_FlowCode, string m_Approver);
        /// <summary>
        /// 发起审批
        /// </summary>
        /// <param name="m_FlowName">审批流</param>
        /// <param name="m_FlowCode">审批流编码</param>
        /// <param name="m_FormID">业务表单号</param>
        /// <param name="m_Approver">审批人</param>
        /// <param name="m_Message">输出信息</param>
        /// <returns></returns>
        bool StartFlow(string m_FlowName, string m_FlowCode, string m_FormID, string m_Approver, out string m_Message);
    }
}
