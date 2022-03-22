using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BLL.HRM
{
    public class Employee
    {
        private readonly IDAL.HRM.IEmployee dal = DALFactory.DALFactory.GetInstance().GetEmployee();

        /// <summary>
        /// 查询Employee
        /// </summary>
        /// <returns>Employee</returns>
        public DataTable Select()
        {
            return dal.Select();
        }
        /// <summary>
        /// 查询单条员工信息
        /// </summary>
        /// <param name="m_id">主键</param>
        /// <returns></returns>
        public Model.HRM.Employee Select(long m_id)
        {
            return dal.Select(m_id);
        }
        /// <summary>
        /// 查询Employee
        /// </summary>
        /// <param name="m_ApplicationOrg">组织</param>
        /// <returns></returns>
        public DataTable Select(string m_ApplicationOrg)
        {
            return dal.Select(m_ApplicationOrg);
        }
        /// <summary>
        /// 查询单条Employee
        /// </summary>
        /// <param name="m_ApplicationOrg">ApplicationOrg</param>
        /// <param name="m_EmployeeID">EmployeeID</param>
        /// <returns>Employee</returns>
        public Model.HRM.Employee Select(string m_ApplicationOrg, string m_EmployeeID)
        {
            return dal.Select(m_ApplicationOrg, m_EmployeeID);
        }
        /// <summary>
        /// 查询单条Employee
        /// </summary>
        /// <param name="m_ApplicationOrg">ApplicationOrg</param>
        /// <param name="m_EmployeeID">EmployeeID</param>
        /// <returns>Employee</returns>
        public DataTable Select(string m_ApplicationOrg, string[] m_EmployeeID)
        {
            return dal.Select(m_ApplicationOrg, m_EmployeeID);
        }
        /// <summary>
        /// 查询该部门下的员工信息
        /// </summary>
        /// <param name="m_ApplicationOrg">ApplicationOrg</param>
        /// <param name="m_DepartmentCode">部门代码</param>
        /// <returns></returns>
        public DataTable SearchByDepartmentCode(string m_ApplicationOrg, string m_DepartmentCode)
        {
            return dal.SearchByDepartmentCode(m_ApplicationOrg, m_DepartmentCode);
        }
        /// <summary>
        /// 查询Employee
        /// </summary>
        /// <param name="m_ApplicationOrg">ApplicationOrg</param>
        /// <param name="m_EmployeeID">EmployeeID</param>
        /// <returns>Employee</returns>
        public DataTable Search(string m_ApplicationOrg, string m_EmployeeID)
        {
            return dal.Search(m_ApplicationOrg, m_EmployeeID);
        }
        /// <summary>
        /// 增加Employee
        /// </summary>
        /// <param name="m_Employee">Employee</param>
        /// <returns>返回增加结果</returns>
        public bool Add(Model.HRM.Employee m_Employee)
        {
            return dal.Add(m_Employee);
        }
        /// <summary>
        /// 更新Employee
        /// </summary>
        /// <param name="m_Employee">Employee</param>
        /// <returns>返回更新结果</returns>
        public bool Update(Model.HRM.Employee m_Employee)
        {
            return dal.Update(m_Employee);
        }
        /// <summary>
        /// 删除员工信息
        /// </summary>
        /// <param name="m_id">主键</param>
        /// <param name="m_DeleteUserID">删除人</param>
        /// <returns>返回删除结果</returns>
        public bool Delete(long m_id, string m_DeleteUserID)
        {
            return dal.Delete(m_id, m_DeleteUserID);
        }
        /// <summary>
        /// 获得员工信息中的m_FieldName的值
        /// </summary>
        /// <param name="m_ApplicationOrg">ApplicationOrg</param>
        /// <param name="m_EmployeeID">EmployeeID</param>
        /// <param name="m_FieldName">FieldName</param>
        /// <returns>FieldValue</returns>
        public DataTable GetFieldValue(string m_ApplicationOrg, string m_EmployeeID, string m_FieldName)
        {
            return dal.GetFieldValue(m_ApplicationOrg, m_EmployeeID, m_FieldName);
        }
        /// <summary>
        /// 获得员工信息中的m_FieldName的值
        /// </summary>
        /// <param name="m_ApplicationOrg">ApplicationOrg</param>
        /// <param name="m_EmployeeID">EmployeeID</param>
        /// <param name="m_FieldName">FieldName</param>
        /// <returns>FieldValue</returns>
        public DataTable GetFieldValue(string m_ApplicationOrg, string[] m_EmployeeID, string m_FieldName)
        {
            return dal.GetFieldValue(m_ApplicationOrg, m_EmployeeID, m_FieldName);
        }
        /// <summary>
        /// 是否已经存在该员工信息
        /// </summary>
        /// <param name="m_ApplicationOrg">应用组织</param>
        /// <param name="m_EmployeeID">工号</param>
        /// <returns>返回结果</returns>
        public bool IsExist(string m_ApplicationOrg, string m_EmployeeID)
        {
            return dal.IsExist(m_ApplicationOrg, m_EmployeeID);
        }
    }
}
