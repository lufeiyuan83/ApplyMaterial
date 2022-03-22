using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace IDAL.PLM
{
    public interface ITipTopParameter
    {
        /// <summary>
        /// 查询TipTopParameter
        /// </summary>
        /// <returns>Log</returns>
        DataTable Select();
        /// <summary>
        /// 通过物料编码查询物料信息
        /// </summary>
        /// <param name="m_MaterialId">MaterialId</param>
        /// <returns>Log</returns>
        DataTable Select(string m_MaterialId);
        /// <summary>
        /// 通过物料编码查询物料信息
        /// </summary>
        /// <param name="m_MaterialId">MaterialId</param>
        /// <param name="m_Bucode">Bucode</param>
        /// <returns>Log</returns>
        DataTable Select(string m_MaterialId, string m_Bucode);
        /// <summary>
        /// 更新料号抛送TipTop信息
        /// <param name="m_TipTopParameter">TipTopParameter</param>
        /// </summary>
        /// <returns></returns>
        bool Update(Model.PLM.TipTopParameter m_TipTopParameter);
    }
}
