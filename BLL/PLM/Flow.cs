using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BLL.PLM
{
    public class Flow
    {
        private readonly IDAL.PLM.IFlow dal = DALFactory.DALFactory.GetInstance().GetFlow();

        /// <summary>
        /// 查询审批流
        /// </summary>
        /// <returns></returns>
        public DataTable Select()
        {
            return dal.Select();
        }
        /// <summary>
        /// 查询审批流
        /// <param name="m_id"></param>
        /// </summary>
        /// <returns></returns>
        public Model.PLM.Flow Select(long m_id)
        {
            return dal.Select(m_id);
        }
        /// <summary>
        /// 获得工作流名称
        /// </summary>
        /// <param name="m_FlowName">工作流</param>
        /// <param name="m_Operator">申请人</param>
        /// <returns></returns>
        public DataTable Select(string m_FlowName, string m_Operator)
        {
            return dal.Select(m_FlowName, m_Operator);
        }
        /// <summary>
        /// 获得工作流名称
        /// </summary>
        /// <param name="m_FlowName">工作流前缀名称</param>
        /// <param name="m_FlowCode">流程代码</param>
        /// <param name="m_Operator">申请人</param>
        /// <returns></returns>
        public DataTable Select(string m_FlowName, string m_FlowCode, string m_Operator)
        {
            return dal.Select(m_FlowName, m_FlowCode, m_Operator);
        }
        /// <summary>
        /// 获得工作流名称
        /// </summary>
        /// <param name="m_FlowName">工作流</param>
        /// <param name="m_Operator">申请人</param>
        /// <returns></returns>
        public string GetFlowName(string m_FlowName, string m_Operator)
        {
            return dal.GetFlowName(m_FlowName, m_Operator);
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
            return dal.CheckApproveAccess(m_FlowName, m_FlowCode, m_Approver);
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
        public bool StartFlow(string m_FlowName, string m_FlowCode, string m_FormID, string m_Approver, out string m_Message)
        {
            return dal.StartFlow(m_FlowName, m_FlowCode, m_FormID, m_Approver, out m_Message);
        }
    }
}
