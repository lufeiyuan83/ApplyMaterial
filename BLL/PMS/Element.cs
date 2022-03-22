using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BLL.PMS
{
    public class Element
    {
        private readonly IDAL.PMS.IElement dal = DALFactory.DALFactory.GetInstance().GetElement();
        /// <summary>
        /// 查询Element
        /// </summary>
        /// <returns>Element</returns>
        public DataTable Select()
        {
            return dal.Select();
        }
        /// <summary>
        /// 查询单条Element
        /// </summary>
        /// <param name="m_Id">主键</param>
        /// <returns></returns>
        public Model.PMS.Element Select(long m_Id)
        {
            return dal.Select(m_Id);
        }
        /// <summary>
        /// 查询该系统菜单下的Element
        /// </summary>
        /// <param name="m_SysMenuId">系统菜单主键</param>
        /// <returns></returns>
        public DataTable SearchBySysMenuId(long m_SysMenuId)
        {
            return dal.SearchBySysMenuId(m_SysMenuId);
        }
        /// <summary>
        /// 增加Element
        /// </summary>
        /// <param name="m_Element">Element</param>
        /// <returns>返回增加结果</returns>
        public bool Add(Model.PMS.Element m_Element)
        {
            return dal.Add(m_Element);
        }
        /// <summary>
        /// 更新Element
        /// </summary>
        /// <param name="m_Element">Element</param>
        /// <returns>返回更新结果</returns>
        public bool Update(Model.PMS.Element m_Element)
        {
            return dal.Update(m_Element);
        }
        /// <summary>
        /// 删除Element
        /// </summary>
        /// <param name="m_Id">主键</param>
        /// <returns>返回删除结果</returns>
        public bool Delete(long m_Id)
        {
            return dal.Delete(m_Id);
        }
        /// <summary>
        /// 是否已经存在该Element
        /// </summary>
        /// <param name="m_SysMenuId">系统菜单主键</param>
        /// <param name="m_Code">角色代码</param>
        /// <returns>返回结果</returns>
        public bool IsExist(long m_SysMenuId, string m_Code)
        {
            return dal.IsExist(m_SysMenuId,m_Code);
        }
    }
}
