using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace IDAL.PLM
{
    public interface IUI
    {
        /// <summary>
        /// 查询UI
        /// </summary>
        /// <returns></returns>
        DataTable Select();
        /// <summary>
        /// 查询UI
        /// </summary>
        /// <param name="m_UIId">UIId</param>
        /// <returns></returns>
        Model.PLM.UI Select(long m_UIId);
        /// <summary>
        /// 通过页面名称查询UI信息
        /// </summary>
        /// <param name="m_UIForm">页面名称</param>
        /// <returns></returns>
        DataTable Select(string m_UIForm);
    }
}
