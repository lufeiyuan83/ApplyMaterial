using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace IDAL.HRM
{
    public interface IDepartment
    {
        /// <summary>
        /// 查询Department
        /// </summary>
        /// <returns>Department</returns>
        DataTable Select();
        /// <summary>
        /// 查询单条Department
        /// </summary>
        /// <param name="m_id">主键</param>
        /// <returns>Department</returns>
        Model.HRM.Department Select(long m_id);
        /// <summary>
        /// 查询单条Department
        /// </summary>
        /// <param name="m_ApplicationOrg">组织代码</param>
        /// <returns>Department</returns>
        DataTable Select(string m_ApplicationOrg);
        /// <summary>
        /// 查询Department
        /// </summary>
        /// <param name="m_ApplicationOrg">组织代码</param>
        /// <returns></returns>
        DataTable Select(List<string> m_ApplicationOrg);
        /// <summary>
        /// 查询Department
        /// </summary>
        /// <param name="m_id">主键</param>
        /// <returns></returns>
        DataTable Select(List<long> m_id);
        /// <summary>
        /// 增加Department
        /// </summary>
        /// <param name="m_Department">Department</param>
        /// <returns>返回增加结果</returns>
        bool Add(Model.HRM.Department m_Department);
        /// <summary>
        /// 更新Department
        /// </summary>
        /// <param name="m_Department">Department</param>
        /// <returns>返回更新结果</returns>
        bool Update(Model.HRM.Department m_Department);
        /// <summary>
        /// 删除Department
        /// </summary>
        /// <param name="m_id">主键</param>
        /// <param name="m_DeleteUserID">删除人</param>
        /// <returns>返回删除结果</returns>
        bool Delete(long m_id, string m_DeleteUserID);
        /// <summary>
        /// 是否已经存在该部门
        /// </summary>
        /// <param name="m_ApplicationOrg">组织代码</param>
        /// <param name="m_DepartmentCode">部门代码</param>
        /// <returns>返回结果</returns>
        bool IsExist(string m_ApplicationOrg, string m_DepartmentCode);
        /// <summary>
        /// 获得部门信息中的m_FieldName的值
        /// </summary>
        /// <param name="m_ApplicationOrg">组织代码</param>
        /// <param name="m_DepartmentCode">部门代码</param>
        /// <param name="m_FieldName">字段名称，以逗号分隔</param>
        /// <returns>字段值</returns>
        DataTable GetFieldValue(string m_ApplicationOrg, string m_DepartmentCode, string m_FieldName);
        /// <summary>
        /// 获得部门信息中的m_FieldName的值
        /// </summary>
        /// <param name="m_ApplicationOrg">组织代码</param>
        /// <param name="m_DepartmentCode">部门代码</param>
        /// <param name="m_FieldName">字段名称，以逗号分隔</param>
        /// <returns>字段值</returns>
        DataTable GetFieldValue(List<string> m_ApplicationOrg, string m_DepartmentCode, string m_FieldName);
    }
}
