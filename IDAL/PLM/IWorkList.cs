using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace IDAL.PLM
{
    public interface IWorkList
    {
        /// <summary>
        /// 查询工作列表 
        /// </summary>
        /// <returns></returns>
        DataTable Select();
        /// <summary>
        /// 查询该id的工作列表
        /// <param name="m_id"></param>
        /// </summary>
        /// <returns></returns>
        Model.PLM.WorkList Select(long m_id);
        /// <summary>
        /// 查询该操作员的工作列表
        /// <param name="m_SystemCode">系统代码</param>
        /// <param name="m_EmployeeID">操作员</param>
        /// </summary>
        /// <returns></returns>
        DataTable Select(string m_SystemCode, string m_EmployeeID);
        /// <summary>
        /// 查询该操作员的工作列表
        /// <param name="m_SystemCode">系统代码</param>
        /// <param name="m_EmployeeID">操作员</param>
        /// <param name="m_Status">工作列表状态</param>
        /// </summary>
        /// <returns></returns>
        DataTable Select(string m_SystemCode, string m_EmployeeID, string m_Status);
        /// <summary>
        /// 查询该表单的工作列表
        /// <param name="m_SystemCode">系统代码</param>
        /// <param name="m_TableName">表名</param>
        /// <param name="m_FormID">表单号</param>
        /// </summary>
        /// <returns></returns>
        Model.PLM.WorkList Search(string m_SystemCode, string m_TableName, string m_FormID);
        /// <summary>
        /// 记录工作列表
        /// <param name="m_WorkList">工作列表</param>
        /// </summary>
        /// <returns></returns>
        bool Add(Model.PLM.WorkList m_WorkList);
        /// <summary>
        /// 记录工作列表
        /// <param name="m_WorkList">工作列表</param>
        /// </summary>
        /// <returns></returns>
        bool Add(List<Model.PLM.WorkList> m_WorkList);
        /// <summary>
        /// 更新工作列表的状态
        /// <param name="m_WorkList">代办列表</param>
        /// </summary>
        /// <returns></returns>
        bool Update(Model.PLM.WorkList m_WorkList);
        /// <summary>
        /// 更新工作列表的状态
        /// <param name="m_FormId">表单编号</param>
        /// <param name="m_Status">工作列表状态</param>
        /// </summary>
        /// <returns></returns>
        bool Update(string m_FormId, string m_Status);
        /// <summary>
        /// 更新工作列表的状态
        /// <param name="m_FormId">表单编号</param>
        /// <param name="m_URL">URL</param>
        /// <param name="m_Status">工作列表状态</param>
        /// </summary>
        /// <returns></returns>
        bool Update(List<string> m_FormId, string m_URL, string m_Status);
        /// <summary>
        /// 更新工作列表的状态
        /// <param name="m_FormId">表单编号</param>
        /// <param name="m_Title">Title</param>
        /// <param name="m_URL">URL</param>
        /// <param name="m_Status">工作列表状态</param>
        /// </summary>
        /// <returns></returns>
        bool Update(List<string> m_FormId, string m_Title, string m_URL, string m_Status);
    }
}
