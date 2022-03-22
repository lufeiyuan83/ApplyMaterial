using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace IDAL.Tenement
{
    public interface ISystem
    {
        /// <summary>
        /// 查询System
        /// </summary>
        /// <returns>System</returns>
        DataTable Select();
        /// <summary>
        /// 查询单条System
        /// </summary>
        /// <param name="m_id">主键</param>
        /// <returns>System</returns>
        Model.Tenement.System Select(long m_id);
        /// <summary>
        /// 查询System
        /// </summary>
        /// <param name="m_id">主键</param>
        /// <returns></returns>
        DataTable Select(List<long> m_id);
        /// <summary>
        /// 查询单条System
        /// </summary>
        /// <param name="m_SystemCode">系统代码</param>
        /// <returns>System</returns>
        Model.Tenement.System Select(string m_SystemCode);
        /// <summary>
        /// 查询单条System
        /// </summary>
        /// <param name="m_SystemCode">系统代码</param>
        /// <param name="m_SystemName">系统名称</param>
        /// <returns>System</returns>
        Model.Tenement.System Select(string m_SystemCode, string m_SystemName);
        /// <summary>
        /// 查询System
        /// </summary>
        /// <param name="m_SystemCode">系统代码</param>
        /// <returns></returns>
        DataTable Select(List<string> m_SystemCode);
        /// <summary>
        /// 增加System
        /// </summary>
        /// <param name="m_System">System</param>
        /// <returns>返回增加结果</returns>
        bool Add(Model.Tenement.System m_System);
        /// <summary>
        /// 更新System
        /// </summary>
        /// <param name="m_System">System</param>
        /// <returns>返回更新结果</returns>
        bool Update(Model.Tenement.System m_System);
        /// <summary>
        /// 删除System
        /// </summary>
        /// <param name="m_id">主键</param>
        /// <param name="m_DeleteUserID">删除人</param>
        /// <returns>返回删除结果</returns>
        bool Delete(long m_id, string m_DeleteUserID);
        /// <summary>
        /// 是否已经存在该系统信息
        /// </summary>
        /// <param name="m_SystemCode">系统代码</param>
        /// <returns>返回结果</returns>
        bool IsExist(string m_SystemCode);
        /// <summary>
        /// 获得系统信息中的m_FieldName的值
        /// </summary>
        /// <param name="m_SystemCode">系统代码</param>
        /// <param name="m_FieldName">字段名称，以逗号分隔</param>
        /// <returns>字段值</returns>
        DataTable GetFieldValue(string m_SystemCode, string m_FieldName);
        /// <summary>
        /// 获得系统信息中的m_FieldName的值
        /// </summary>
        /// <param name="m_SystemCode">系统代码</param>
        /// <param name="m_FieldName">字段名称，以逗号分隔</param>
        /// <returns>字段值</returns>
        DataTable GetFieldValue(List<string> m_SystemCode, string m_FieldName);
        /// <summary>
        /// 获得系统信息
        /// </summary>
        /// <param name="m_SystemName">系统名称</param>
        /// <returns>字段值</returns>
        DataTable GetSystem(string m_SystemName);  
    }
}
