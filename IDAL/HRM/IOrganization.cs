using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace IDAL.HRM
{
    public interface IOrganization
    {
        /// <summary>
        /// 查询Organization
        /// </summary>
        /// <returns>Organization</returns>
        DataTable Select();
        /// <summary>
        /// 查询单条Organization
        /// </summary>
        /// <param name="m_id">主键</param>
        /// <returns>Organization</returns>
        Model.HRM.Organization Select(long m_id);
        /// <summary>
        /// 查询单条Organization
        /// </summary>
        /// <param name="m_OrgCode">组织代码</param>
        /// <returns>Organization</returns>
        Model.HRM.Organization Select( string m_OrgCode);
        /// <summary>
        /// 查询Organization
        /// </summary>
        /// <param name="m_id">组织主键</param>
        /// <returns></returns>
        DataTable Select(List<long> m_id);
        /// <summary>
        /// 查询Organization
        /// </summary>
        /// <param name="m_OrgCode">组织代码</param>
        /// <returns></returns>
        DataTable Select(List<string> m_OrgCode);
        /// <summary>
        /// 增加Organization
        /// </summary>
        /// <param name="m_Organization">Organization</param>
        /// <returns>返回增加结果</returns>
        bool Add(Model.HRM.Organization m_Organization);
        /// <summary>
        /// 更新Organization
        /// </summary>
        /// <param name="m_Organization">Organization</param>
        /// <returns>返回更新结果</returns>
        bool Update(Model.HRM.Organization m_Organization);
        /// <summary>
        /// 删除Organization
        /// </summary>
        /// <param name="m_id">主键</param>
        /// <param name="m_DeleteUserID">删除人</param>
        /// <returns>返回删除结果</returns>
        bool Delete(long m_id, string m_DeleteUserID);
        /// <summary>
        /// 是否已经存在该组织
        /// </summary>
        /// <param name="m_OrgCode">组织代码</param>
        /// <returns>返回结果</returns>
        bool IsExist(string m_OrgCode);
        /// <summary>
        /// 获得组织信息中的m_FieldName的值
        /// </summary>
        /// <param name="m_OrgCode">组织代码</param>
        /// <param name="m_FieldName">字段名称，以逗号分隔</param>
        /// <returns>字段值</returns>
        DataTable GetFieldValue(string m_OrgCode, string m_FieldName);
        /// <summary>
        /// 获得组织信息中的m_FieldName的值
        /// </summary>
        /// <param name="m_OrgCode">组织代码</param>
        /// <param name="m_FieldName">字段名称，以逗号分隔</param>
        /// <returns>字段值</returns>
        DataTable GetFieldValue(List<string> m_OrgCode, string m_FieldName); 
    }
}
