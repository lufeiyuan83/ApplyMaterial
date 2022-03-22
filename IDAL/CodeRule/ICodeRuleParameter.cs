using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace IDAL.CodeRule
{
    public interface ICodeRuleParameter
    {
        /// <summary>
        /// 查询编码原则参数信息
        /// </summary>
        /// <returns></returns>
        DataTable Select();
        /// <summary>
        /// 查询编码原则参数信息
        /// </summary>
        /// <param name="m_Id">Id</param>
        /// <returns></returns>
        Model.CodeRule.CodeRuleParameter Select(long m_Id);
        /// <summary>
        /// 通过编码原则主键查询编码原则参数信息
        /// </summary>
        /// <param name="m_CodeRuleId">编码原则主键</param>
        /// <returns></returns>
        DataTable Search(long m_CodeRuleId);
        /// <summary>
        /// 增加编码原则参数信息
        /// </summary>
        /// <param name="m_CodeRuleParameter">编码原则参数信息</param>
        /// <returns>返回增加结果</returns>
        bool Add(Model.CodeRule.CodeRuleParameter m_CodeRuleParameter);
        /// <summary>
        /// 更新编码原则参数信息
        /// <param name="m_CodeRuleParameter">编码原则参数信息</param>
        /// </summary>
        /// <returns></returns>
        bool Update(Model.CodeRule.CodeRuleParameter m_CodeRuleParameter);
        /// <summary>
        /// 删除编码原则参数信息
        /// </summary>
        /// <param name="m_Id">主键</param>
        /// <param name="m_DeleteUserID">删除人</param>
        /// <returns>返回删除结果</returns>
        bool Delete(long m_Id, string m_DeleteUserID);
        /// <summary>
        /// 获得最大SortNo
        /// </summary>
        /// <param name="m_CodeRuleId">编码原则主键</param>
        /// <param name="m_ParentId"></param>
        /// <param name="m_IsSubNodeSortNo">是否查询子节点的最大排序号</param>
        /// <param name="m_IsSubNodeSortNo">是否顶层节点</param>
        /// <returns></returns>
        int GetMaxSortNo(long m_CodeRuleId, long m_ParentId, bool m_IsSubNodeSortNo, bool m_IsTopNode);
        /// <summary>
        /// 是否已经存在该编码参数信息
        /// </summary>
        /// <param name="m_CodeRuleId">编码原则主键</param>
        /// <param name="m_ParameterCode">参数代码</param>
        /// <returns>返回结果</returns>
        bool IsExist(long m_CodeRuleId, string m_ParameterCode);
        /// <summary>
        /// 是否已经存在该编码参数信息
        /// </summary>
        /// <param name="m_CodeRuleId">编码原则主键</param>
        /// <param name="m_ParentId">父节点编号</param>
        /// <param name="m_ParameterCode">参数代码</param>
        /// <returns>返回结果</returns>
        bool IsExist(long m_CodeRuleId, long m_ParentId, string m_ParameterCode);
    }
}
