using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BLL.PLM
{
    public class Log
    {
        private readonly IDAL.PLM.ILog dal = DALFactory.DALFactory.GetInstance().GetLog();

        /// <summary>
        /// 查询Log
        /// </summary>
        /// <returns>Log</returns>
        public DataTable Select()
        {
            return dal.Select();
        }
        /// <summary>
        /// 查询单条Log
        /// </summary>
        /// <param name="m_ID">ID</param>
        /// <returns>Log</returns>
        public Model.PLM.Log Select(long m_ID)
        {
            return dal.Select(m_ID);
        }
        /// <summary>
        /// 查询该UserDescription的Log
        /// </summary>
        /// <param name="m_UserDescription">UserDescription</param>
        /// <returns></returns>
        public DataTable SelectByUserDescription(string m_UserDescription)
        {
            return dal.SelectByUserDescription(m_UserDescription);
        }
        /// <summary>
        /// 查询该类型的Log
        /// </summary>
        /// <param name="m_Type">Type</param>
        /// <param name="m_ComputerName">ComputerName</param>
        /// <returns>Log</returns>
        public Model.PLM.Log Select(string m_Type, string m_ComputerName)
        {
            return dal.Select(m_Type, m_ComputerName);
        }
        /// <summary>
        /// 查询该类型的Log
        /// </summary>
        /// <param name="m_Type">Type</param>
        /// <returns>Log</returns>
        public DataTable Select(string m_Type)
        {
            return dal.Select(m_Type);
        }
        /// <summary>
        /// 增加Log
        /// </summary>
        /// <param name="m_Log">Log</param>
        /// <returns>返回增加结果</returns>
        public bool Add(Model.PLM.Log m_Log)
        {
            return dal.Add(m_Log);
        }
        /// <summary>
        /// 保存Log
        /// </summary>
        /// <param name="m_Log">Log</param>
        /// <returns>返回保存结果</returns>
        public bool Save(Model.PLM.Log m_Log)
        {
            return dal.Save(m_Log);
        }
        /// <summary>
        /// 获得计算机名
        /// </summary>
        /// <param name="m_Type">Type</param>
        /// <returns>计算机名</returns>
        public List<string> GetComputerName(string m_Type)
        {
            return dal.GetComputerName(m_Type);
        }
    }
}
