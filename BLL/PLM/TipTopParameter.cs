using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BLL.PLM
{
    public class TipTopParameter
    {
        private readonly IDAL.PLM.ITipTopParameter dal = DALFactory.DALFactory.GetInstance().GetTipTopParameter();
        /// <summary>
        /// 查询TipTopParameter
        /// </summary>
        /// <returns>Log</returns>
        public DataTable Select()
        {
            return dal.Select();
        }
        /// <summary>
        /// 通过物料编码查询物料信息
        /// </summary>
        /// <param name="m_MaterialId">MaterialId</param>
        /// <returns>Log</returns>
        public DataTable Select(string m_MaterialId)
        {
            return dal.Select(m_MaterialId);
        }
        /// <summary>
        /// 通过物料编码查询物料信息
        /// </summary>
        /// <param name="m_MaterialId">MaterialId</param>
        /// <param name="m_Bucode">Bucode</param>
        /// <returns>Log</returns>
        public DataTable Select(string m_MaterialId, string m_Bucode)
        {
            return dal.Select(m_MaterialId, m_Bucode);
        }
        /// <summary>
        /// 更新料号抛送TipTop信息
        /// <param name="m_TipTopParameter">TipTopParameter</param>
        /// </summary>
        /// <returns></returns>
        public bool Update(Model.PLM.TipTopParameter m_TipTopParameter)
        {
            return dal.Update(m_TipTopParameter);
        }
    }
}
