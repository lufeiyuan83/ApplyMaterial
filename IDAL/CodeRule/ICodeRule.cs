using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace IDAL.CodeRule
{
    public interface ICodeRule
    {
        /// <summary>
        /// 查询编码原则
        /// </summary>
        /// <returns></returns>
        DataTable Select();
        /// <summary>
        /// 查询编码原则
        /// </summary>
        /// <param name="m_Id">Id</param>
        /// <returns></returns>
        Model.CodeRule.CodeRule Select(long m_Id);
        /// <summary>
        /// 通过名称查询编码原则信息
        /// </summary>
        /// <param name="m_Code">编码原则代码</param>
        /// <returns></returns>
        DataTable Select(string m_Code);
        /// <summary>
        /// 通过名称查询编码原则信息
        /// </summary>
        /// <param name="m_Code">编码原则代码</param>
        /// <param name="m_Code">编码原则对应类别</param>
        /// <returns></returns>
        DataTable Select(string m_Code,string m_CodeRuleClass);
        /// <summary>
        /// 增加编码原则信息
        /// </summary>
        /// <param name="m_CodeRule">编码原则</param>
        /// <returns>返回增加结果</returns>
        bool Add(Model.CodeRule.CodeRule m_CodeRule);
        /// <summary>
        /// 更新编码原则信息
        /// <param name="m_CodeRule">编码原则</param>
        /// </summary>
        /// <returns></returns>
        bool Update(Model.CodeRule.CodeRule m_CodeRule);
    }
}
