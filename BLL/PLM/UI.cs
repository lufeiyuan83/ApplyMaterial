using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BLL.PLM
{
    public class UI
    {
        private readonly IDAL.PLM.IUI dal = DALFactory.DALFactory.GetInstance().GetUI();
        /// <summary>
        /// 查询UI
        /// </summary>
        /// <returns></returns>
        public DataTable Select()
        {
            return dal.Select();
        }
        /// <summary>
        /// 查询UI
        /// </summary>
        /// <param name="m_UIId">UIId</param>
        /// <returns></returns>
        public Model.PLM.UI Select(long m_UIId)
        {
            return dal.Select(m_UIId);
        }
        /// <summary>
        /// 通过页面名称查询UI信息
        /// </summary>
        /// <param name="m_UIForm">页面名称</param>
        /// <returns></returns>
        public DataTable Select(string m_UIForm)
        {
            return dal.Select(m_UIForm);
        }
    }
}
