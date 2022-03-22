using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace IDAL.PMS
{
    public interface IRoleRelation
    {
        /// <summary>
        /// 查询RoleRelation
        /// </summary>
        /// <returns>RoleRelation</returns>
        DataTable Select();
        /// <summary>
        /// 查询单条角色关系表
        /// </summary>
        /// <param name="m_Id">主键</param>
        /// <returns></returns>
        Model.PMS.RoleRelation Select(long m_Id);
        /// <summary>
        /// 查询该角色下的员工
        /// </summary>
        /// <param name="m_RoleId">角色主键</param>
        /// <returns></returns>
        DataTable SearchByRoleId(long m_RoleId);
        /// <summary>
        /// 增加RoleRelation
        /// </summary>
        /// <param name="m_RoleRelation">RoleRelation</param>
        /// <returns>返回增加结果</returns>
        bool Add(Model.PMS.RoleRelation m_RoleRelation);
        /// <summary>
        /// 更新RoleRelation
        /// </summary>
        /// <param name="m_RoleRelation">RoleRelation</param>
        /// <returns>返回更新结果</returns>
        bool Update(Model.PMS.RoleRelation m_RoleRelation);
        /// <summary>
        /// 删除RoleRelation
        /// </summary>
        /// <param name="m_Id">主键</param>
        /// <returns>返回删除结果</returns>
        bool Delete(long m_Id);
        /// <summary>
        /// 是否已经存在该角色关系表
        /// </summary>
        /// <param name="m_RoleID">角色代码</param>
        /// <param name="m_EmployeeID">工号</param>
        /// <returns>返回结果</returns>
        bool IsExist(string m_RoleID, string m_EmployeeID);
        /// <summary>
        /// 获得角色关系表中的m_FieldName的值
        /// </summary>
        /// <param name="m_EmployeeID">工号</param>
        /// <param name="m_FieldName">字段名称，以逗号分隔</param>
        /// <returns>字段值</returns>
        DataTable GetFieldValue(string m_EmployeeID, string m_FieldName);
        /// <summary>
        /// 获得角色关系表中的m_FieldName的值
        /// </summary>
        /// <param name="m_EmployeeID">工号</param>
        /// <param name="m_FieldName">字段名称，以逗号分隔</param>
        /// <returns>字段值</returns>
        DataTable GetFieldValue(List<string> m_EmployeeID, string m_FieldName);
    }
}
