using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BLL.PMS
{
    public class Role
    {
        private readonly IDAL.PMS.IRole dal = DALFactory.DALFactory.GetInstance().GetRole();

        /// <summary>
        /// 查询Role
        /// </summary>
        /// <returns>Role</returns>
        public DataTable Select()
        {
            return dal.Select();
        }
        /// <summary>
        /// 查询单条Role
        /// </summary>
        /// <param name="m_Id">主键</param>
        /// <returns>Role</returns>
        public Model.PMS.Role Select(long m_Id)
        {
            return dal.Select(m_Id);
        }
        /// <summary>
        /// 查询Role
        /// </summary>
        /// <param name="m_Id">主键</param>
        /// <returns></returns>
        public DataTable Select(List<long> m_Id)
        {
            return dal.Select(m_Id);
        }
        /// <summary>
        /// 查询单条Role
        /// </summary>
        /// <param name="m_RoleCode">角色代码</param>
        /// <returns>Role</returns>
        public Model.PMS.Role Select(string m_RoleCode)
        {
            return dal.Select(m_RoleCode);
        }
        /// <summary>
        /// 查询单条Role
        /// </summary>
        /// <param name="m_RoleCode">角色代码</param>
        /// <param name="m_RoleName">角色名称</param>
        /// <returns>Role</returns>
        public Model.PMS.Role Select(string m_RoleCode, string m_RoleName)
        {
            return dal.Select(m_RoleCode, m_RoleName);
        }
        /// <summary>
        /// 查询Role
        /// </summary>
        /// <param name="m_RoleCode">角色代码</param>
        /// <returns></returns>
        public DataTable Select(List<string> m_RoleCode)
        {
            return dal.Select(m_RoleCode);
        }
        /// <summary>
        /// 增加Role
        /// </summary>
        /// <param name="m_Role">Role</param>
        /// <returns>返回增加结果</returns>
        public bool Add(Model.PMS.Role m_Role)
        {
            return dal.Add(m_Role);
        }
        /// <summary>
        /// 更新Role
        /// </summary>
        /// <param name="m_Role">Role</param>
        /// <returns>返回更新结果</returns>
        public bool Update(Model.PMS.Role m_Role)
        {
            return dal.Update(m_Role);
        }
        /// <summary>
        /// 删除Role
        /// </summary>
        /// <param name="m_Id">主键</param>
        /// <param name="m_DeleteUserID">删除人</param>
        /// <returns>返回删除结果</returns>
        public bool Delete(long m_Id, string m_DeleteUserID)
        {
            return dal.Delete(m_Id, m_DeleteUserID);
        }
        /// <summary>
        /// 是否已经存在该角色信息
        /// </summary>
        /// <param name="m_RoleCode">角色代码</param>
        /// <returns>返回结果</returns>
        public bool IsExist(string m_RoleCode)
        {
            return dal.IsExist(m_RoleCode);
        }
        /// <summary>
        /// 获得角色信息中的m_FieldName的值
        /// </summary>
        /// <param name="m_RoleCode">角色代码</param>
        /// <param name="m_FieldName">字段名称，以逗号分隔</param>
        /// <returns>字段值</returns>
        public DataTable GetFieldValue(string m_RoleCode, string m_FieldName)
        {
            return dal.GetFieldValue(m_RoleCode, m_FieldName);
        }
        /// <summary>
        /// 获得角色信息中的m_FieldName的值
        /// </summary>
        /// <param name="m_RoleCode">角色代码</param>
        /// <param name="m_FieldName">字段名称，以逗号分隔</param>
        /// <returns>字段值</returns>
        public DataTable GetFieldValue(List<string> m_RoleCode, string m_FieldName)
        {
            return dal.GetFieldValue(m_RoleCode, m_FieldName);
        } 
        /// <summary>
        /// 获得角色信息
        /// </summary>
        /// <param name="m_RoleName">角色名称</param>
        /// <returns>字段值</returns>
        public DataTable GetRole(string m_RoleName)
        {
            return dal.GetRole(m_RoleName);
        }
    }
}
