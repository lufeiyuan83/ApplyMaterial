using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace IDAL.PMS
{
    public interface IRole
    {
        /// <summary>
        /// 查询Role
        /// </summary>
        /// <returns>Role</returns>
        DataTable Select();
        /// <summary>
        /// 查询单条Role
        /// </summary>
        /// <param name="m_id">主键</param>
        /// <returns>Role</returns>
        Model.PMS.Role Select(long m_id);
        /// <summary>
        /// 查询Role
        /// </summary>
        /// <param name="m_id">主键</param>
        /// <returns></returns>
        DataTable Select(List<long> m_id);
        /// <summary>
        /// 查询单条Role
        /// </summary>
        /// <param name="m_RoleCode">角色代码</param>
        /// <returns>Role</returns>
        Model.PMS.Role Select(string m_RoleCode);
        /// <summary>
        /// 查询单条Role
        /// </summary>
        /// <param name="m_RoleCode">角色代码</param>
        /// <param name="m_RoleName">角色名称</param>
        /// <returns>Role</returns>
        Model.PMS.Role Select(string m_RoleCode, string m_RoleName);
        /// <summary>
        /// 查询Role
        /// </summary>
        /// <param name="m_RoleCode">角色代码</param>
        /// <returns></returns>
        DataTable Select(List<string> m_RoleCode);
        /// <summary>
        /// 增加Role
        /// </summary>
        /// <param name="m_Role">Role</param>
        /// <returns>返回增加结果</returns>
        bool Add(Model.PMS.Role m_Role);
        /// <summary>
        /// 更新Role
        /// </summary>
        /// <param name="m_Role">Role</param>
        /// <returns>返回更新结果</returns>
        bool Update(Model.PMS.Role m_Role);
        /// <summary>
        /// 删除Role
        /// </summary>
        /// <param name="m_id">主键</param>
        /// <param name="m_DeleteUserID">删除人</param>
        /// <returns>返回删除结果</returns>
        bool Delete(long m_id, string m_DeleteUserID);
        /// <summary>
        /// 是否已经存在该角色信息
        /// </summary>
        /// <param name="m_RoleCode">角色代码</param>
        /// <returns>返回结果</returns>
        bool IsExist(string m_RoleCode);
        /// <summary>
        /// 获得角色信息中的m_FieldName的值
        /// </summary>
        /// <param name="m_RoleCode">角色代码</param>
        /// <param name="m_FieldName">字段名称，以逗号分隔</param>
        /// <returns>字段值</returns>
        DataTable GetFieldValue(string m_RoleCode, string m_FieldName);
        /// <summary>
        /// 获得角色信息中的m_FieldName的值
        /// </summary>
        /// <param name="m_RoleCode">角色代码</param>
        /// <param name="m_FieldName">字段名称，以逗号分隔</param>
        /// <returns>字段值</returns>
        DataTable GetFieldValue(List<string> m_RoleCode, string m_FieldName);
        /// <summary>
        /// 获得角色信息
        /// </summary>
        /// <param name="m_RoleName">角色名称</param>
        /// <returns>字段值</returns>
        DataTable GetRole(string m_RoleName);  
    }
}
