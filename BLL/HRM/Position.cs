using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BLL.HRM
{
    public class Position
    {
        private readonly IDAL.HRM.IPosition dal = DALFactory.DALFactory.GetInstance().GetPosition();
        /// <summary>
        /// 查询Position
        /// </summary>
        /// <returns>Position</returns>
        public DataTable Select()
        {
            return dal.Select();
        }
        /// <summary>
        /// 查询单条Position
        /// </summary>
        /// <param name="m_id">主键</param>
        /// <returns>Position</returns>
        public Model.HRM.Position Select(long m_id)
        {
            return dal.Select(m_id);
        }
        /// <summary>
        /// 查询单条Position
        /// </summary>
        /// <param name="m_ApplicationOrg">组织代码</param>
        /// <returns>Position</returns>
        public DataTable Select(string m_ApplicationOrg)
        {
            return dal.Select(m_ApplicationOrg);
        }
        /// <summary>
        /// 查询单条Position
        /// </summary>
        /// <param name="m_ApplicationOrg">组织代码</param>
        /// <param name="m_DepartmentId">部门主键</param>
        /// <returns>Position</returns>
        public DataTable Select(string m_ApplicationOrg, long m_DepartmentId)
        {
            return dal.Select(m_ApplicationOrg, m_DepartmentId);
        }
        /// <summary>
        /// 查询Position
        /// </summary>
        /// <param name="m_ApplicationOrg">组织代码</param>
        /// <returns></returns>
        public DataTable Select(List<string> m_ApplicationOrg)
        {
            return dal.Select(m_ApplicationOrg);
        }
        /// <summary>
        /// 查询Position
        /// </summary>
        /// <param name="m_id">主键</param>
        /// <returns></returns>
        public DataTable Select(List<long> m_id)
        {
            return dal.Select(m_id);
        }
        /// <summary>
        /// 增加Position
        /// </summary>
        /// <param name="m_Position">Position</param>
        /// <returns>返回增加结果</returns>
        public bool Add(Model.HRM.Position m_Position)
        {
            return dal.Add(m_Position);
        }
        /// <summary>
        /// 更新Position
        /// </summary>
        /// <param name="m_Position">Position</param>
        /// <returns>返回更新结果</returns>
        public bool Update(Model.HRM.Position m_Position)
        {
            return dal.Update(m_Position);
        }
        /// <summary>
        /// 删除Position
        /// </summary>
        /// <param name="m_id">主键</param>
        /// <param name="m_DeleteUserID">删除人</param>
        /// <returns>返回删除结果</returns>
        public bool Delete(long m_id, string m_DeleteUserID)
        {
            return dal.Delete(m_id, m_DeleteUserID);
        }
        /// <summary>
        /// 是否已经存在该组织
        /// </summary>
        /// <param name="m_ApplicationOrg">组织代码</param>
        /// <param name="m_DepartmentId">部门主键</param>
        /// <param name="m_PositionCode">岗位代码</param>
        /// <returns>返回结果</returns>
        public bool IsExist(string m_ApplicationOrg, long m_DepartmentId, string m_PositionCode)
        {
            return dal.IsExist(m_ApplicationOrg, m_DepartmentId, m_PositionCode);
        }
        /// <summary>
        /// 获得岗位信息中的m_FieldName的值
        /// </summary>
        /// <param name="m_ApplicationOrg">组织代码</param>
        /// <param name="m_DepartmentId">部门主键</param>
        /// <param name="m_PositionCode">岗位代码</param>
        /// <param name="m_FieldName">字段名称，以逗号分隔</param>
        /// <returns>字段值</returns>
        public DataTable GetFieldValue(string m_ApplicationOrg, long m_DepartmentId, string m_PositionCode, string m_FieldName)
        {
            return dal.GetFieldValue(m_ApplicationOrg,m_DepartmentId, m_PositionCode, m_FieldName);
        }
        /// <summary>
        /// 获得岗位信息中的m_FieldName的值
        /// </summary>
        /// <param name="m_ApplicationOrg">组织代码</param>
        /// <param name="m_DepartmentId">部门主键</param>
        /// <param name="m_PositionCode">岗位代码</param>
        /// <param name="m_FieldName">字段名称，以逗号分隔</param>
        /// <returns>字段值</returns>
        public DataTable GetFieldValue(List<string> m_ApplicationOrg, long m_DepartmentId, string m_PositionCode, string m_FieldName)
        {
            return dal.GetFieldValue(m_ApplicationOrg, m_DepartmentId, m_PositionCode, m_FieldName);
        }
    }
}
