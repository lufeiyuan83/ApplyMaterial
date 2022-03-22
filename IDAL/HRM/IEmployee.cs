using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace IDAL.HRM
{
    public interface IEmployee
    {
        /// <summary>
        /// 查询Employee
        /// </summary>
        /// <returns>Employee</returns>
        DataTable Select();
        /// <summary>
        /// 查询单条员工信息
        /// </summary>
        /// <param name="m_id">主键</param>
        /// <returns></returns>
        Model.HRM.Employee Select(long m_id);
        /// <summary>
        /// 查询Employee
        /// </summary>
        /// <param name="m_ApplicationOrg">组织</param>
        /// <returns></returns>
        DataTable Select(string m_ApplicationOrg);
        /// <summary>
        /// 查询单条Employee
        /// </summary>
        /// <param name="m_ApplicationOrg">ApplicationOrg</param>
        /// <param name="m_EmployeeID">EmployeeID</param>
        /// <returns>Employee</returns>
        Model.HRM.Employee Select(string m_ApplicationOrg, string m_EmployeeID);
        /// <summary>
        /// 查询单条Employee
        /// </summary>
        /// <param name="m_ApplicationOrg">ApplicationOrg</param>
        /// <param name="m_EmployeeID">EmployeeID</param>
        /// <returns>Employee</returns>
        DataTable Select(string m_ApplicationOrg, string[] m_EmployeeID);
        /// <summary>
        /// 查询该部门下的员工信息
        /// </summary>
        /// <param name="m_ApplicationOrg">ApplicationOrg</param>
        /// <param name="m_DepartmentCode">部门代码</param>
        /// <returns></returns>
        DataTable SearchByDepartmentCode(string m_ApplicationOrg, string m_DepartmentCode);
        /// <summary>
        /// 查询Employee
        /// </summary>
        /// <param name="m_ApplicationOrg">ApplicationOrg</param>
        /// <param name="m_EmployeeID">EmployeeID</param>
        /// <returns>Employee</returns>
        DataTable Search(string m_ApplicationOrg, string m_EmployeeID);
        /// <summary>
        /// 增加Employee
        /// </summary>
        /// <param name="m_Employee">Employee</param>
        /// <returns>返回增加结果</returns>
        bool Add(Model.HRM.Employee m_Employee);
        /// <summary>
        /// 更新Employee
        /// </summary>
        /// <param name="m_Employee">Employee</param>
        /// <returns>返回更新结果</returns>
        bool Update(Model.HRM.Employee m_Employee);
        /// <summary>
        /// 删除员工信息
        /// </summary>
        /// <param name="m_id">主键</param>
        /// <param name="m_DeleteUserID">删除人</param>
        /// <returns>返回删除结果</returns>
        bool Delete(long m_id, string m_DeleteUserID);
        /// <summary>
        /// 获得员工信息中的m_FieldName的值
        /// </summary>
        /// <param name="m_ApplicationOrg">ApplicationOrg</param>
        /// <param name="m_EmployeeID">EmployeeID</param>
        /// <param name="m_FieldName">FieldName</param>
        /// <returns>FieldValue</returns>
        DataTable GetFieldValue(string m_ApplicationOrg, string m_EmployeeID, string m_FieldName);
        /// <summary>
        /// 获得员工信息中的m_FieldName的值
        /// </summary>
        /// <param name="m_ApplicationOrg">ApplicationOrg</param>
        /// <param name="m_EmployeeID">EmployeeID</param>
        /// <param name="m_FieldName">FieldName</param>
        /// <returns>FieldValue</returns>
        DataTable GetFieldValue(string m_ApplicationOrg, string[] m_EmployeeID, string m_FieldName);
        /// <summary>
        /// 是否已经存在该员工信息
        /// </summary>
        /// <param name="m_ApplicationOrg">应用组织</param>
        /// <param name="m_EmployeeID">工号</param>
        /// <returns>返回结果</returns>
        bool IsExist(string m_ApplicationOrg, string m_EmployeeID);
    }
}
