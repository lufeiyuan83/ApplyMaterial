using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace IDAL.ApplyMaterial
{
    public interface IInterfaceLog
    {
        /// <summary>
        /// 查询InterfaceLog
        /// </summary>
        /// <returns></returns>
        DataTable Select();
        /// <summary>
        /// 查询InterfaceLog
        /// </summary>
        /// <param name="m_Id">Id</param>
        /// <returns></returns>
        Model.ApplyMaterial.InterfaceLog Select(long m_Id);
        /// <summary>
        /// 新增接口日志信息
        /// <param name="m_InterfaceLog">InterfaceLog</param>
        /// </summary>
        /// <returns></returns>
        bool Add(Model.ApplyMaterial.InterfaceLog m_InterfaceLog);
    }
}
