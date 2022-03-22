using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BLL.Auth
{
    public class System
    {
        private readonly IDAL.Auth.ISystem dal = DALFactory.DALFactory.GetInstance().GetSystem();

        /// <summary>
        /// 查询System
        /// </summary>
        /// <returns>System</returns>
        public DataTable Select()
        {
            return dal.Select();
        }
        /// <summary>
        /// 查询单条System
        /// </summary>
        /// <param name="m_Id">主键</param>
        /// <returns>System</returns>
        public Model.Auth.System Select(long m_Id)
        {
            return dal.Select(m_Id);
        }
        /// <summary>
        /// 查询System
        /// </summary>
        /// <param name="m_Id">主键</param>
        /// <returns></returns>
        public DataTable Select(List<long> m_Id)
        {
            return dal.Select(m_Id);
        }
        /// <summary>
        /// 查询单条System
        /// </summary>
        /// <param name="m_SystemCode">系统代码</param>
        /// <returns>System</returns>
        public Model.Auth.System Select(string m_SystemCode)
        {
            return dal.Select(m_SystemCode);
        }
        /// <summary>
        /// 查询单条System
        /// </summary>
        /// <param name="m_SystemCode">系统代码</param>
        /// <param name="m_SystemName">系统名称</param>
        /// <returns>System</returns>
        public Model.Auth.System Select(string m_SystemCode, string m_SystemName)
        {
            return dal.Select(m_SystemCode, m_SystemName);
        }
        /// <summary>
        /// 查询System
        /// </summary>
        /// <param name="m_SystemCode">系统代码</param>
        /// <returns></returns>
        public DataTable Select(List<string> m_SystemCode)
        {
            return dal.Select(m_SystemCode);
        }
        /// <summary>
        /// 增加System
        /// </summary>
        /// <param name="m_System">System</param>
        /// <returns>返回增加结果</returns>
        public bool Add(Model.Auth.System m_System)
        {
            return dal.Add(m_System);
        }
        /// <summary>
        /// 更新System
        /// </summary>
        /// <param name="m_System">System</param>
        /// <returns>返回更新结果</returns>
        public bool Update(Model.Auth.System m_System)
        {
            return dal.Update(m_System);
        }
        /// <summary>
        /// 删除System
        /// </summary>
        /// <param name="m_Id">主键</param>
        /// <param name="m_DeleteUserID">删除人</param>
        /// <returns>返回删除结果</returns>
        public bool Delete(long m_Id, string m_DeleteUserID)
        {
            return dal.Delete(m_Id, m_DeleteUserID);
        }
        /// <summary>
        /// 是否已经存在该系统信息
        /// </summary>
        /// <param name="m_SystemCode">系统代码</param>
        /// <returns>返回结果</returns>
        public bool IsExist(string m_SystemCode)
        {
            return dal.IsExist(m_SystemCode);
        }
        /// <summary>
        /// 获得系统信息中的m_FieldName的值
        /// </summary>
        /// <param name="m_SystemCode">系统代码</param>
        /// <param name="m_FieldName">字段名称，以逗号分隔</param>
        /// <returns>字段值</returns>
        public DataTable GetFieldValue(string m_SystemCode, string m_FieldName)
        {
            return dal.GetFieldValue(m_SystemCode, m_FieldName);
        }
        /// <summary>
        /// 获得系统信息中的m_FieldName的值
        /// </summary>
        /// <param name="m_SystemCode">系统代码</param>
        /// <param name="m_FieldName">字段名称，以逗号分隔</param>
        /// <returns>字段值</returns>
        public DataTable GetFieldValue(List<string> m_SystemCode, string m_FieldName)
        {
            return dal.GetFieldValue(m_SystemCode, m_FieldName);
        } 
        /// <summary>
        /// 获得系统信息
        /// </summary>
        /// <param name="m_SystemName">系统名称</param>
        /// <returns>字段值</returns>
        public DataTable GetSystem(string m_SystemName)
        {
            return dal.GetSystem(m_SystemName);
        }
    }
}
