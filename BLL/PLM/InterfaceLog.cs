using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BLL.PLM
{
    public class InterfaceLog
    {
        private readonly IDAL.PLM.IInterfaceLog dal = DALFactory.DALFactory.GetInstance().GetInterfaceLog();
        /// <summary>
        /// 查询InterfaceLog
        /// </summary>
        /// <returns></returns>
        public DataTable Select()
        {
            return dal.Select();
        }
        /// <summary>
        /// 查询InterfaceLog
        /// </summary>
        /// <param name="m_Id">Id</param>
        /// <returns></returns>
        public Model.PLM.InterfaceLog Select(long m_Id)
        {
            return dal.Select(m_Id);
        }
        /// <summary>
        /// 新增接口日志信息
        /// <param name="m_InterfaceLog">InterfaceLog</param>
        /// </summary>
        /// <returns></returns>
        public bool Add(Model.PLM.InterfaceLog m_InterfaceLog)
        {
            return dal.Add(m_InterfaceLog);
        }
    }
}
