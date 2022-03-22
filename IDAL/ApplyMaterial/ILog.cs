using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace IDAL.ApplyMaterial
{
    public interface ILog
    {
        /// <summary>
        /// 查询Log
        /// </summary>
        /// <returns>Log</returns>
        DataTable Select();
        /// <summary>
        /// 查询单条Log
        /// </summary>
        /// <param name="m_ID">ID</param>
        /// <returns>Log</returns>
        Model.ApplyMaterial.Log Select(long m_ID);
        /// <summary>
        /// 查询该UserDescription的Log
        /// </summary>
        /// <param name="m_UserDescription">UserDescription</param>
        /// <returns></returns>
        DataTable SelectByUserDescription(string m_UserDescription);
        /// <summary>
        /// 查询该类型的Log
        /// </summary>
        /// <param name="m_Type">Type</param>
        /// <param name="m_ComputerName">ComputerName</param>
        /// <returns>Log</returns>
        Model.ApplyMaterial.Log Select(string m_Type, string m_ComputerName);
        /// <summary>
        /// 查询该类型的Log
        /// </summary>
        /// <param name="m_Type">Type</param>
        /// <returns>Log</returns>
        DataTable Select(string m_Type);
        /// <summary>
        /// 增加Log
        /// </summary>
        /// <param name="m_Log">Log</param>
        /// <returns>返回增加结果</returns>
        bool Add(Model.ApplyMaterial.Log m_Log);
        /// <summary>
        /// 保存Log
        /// </summary>
        /// <param name="m_Log">Log</param>
        /// <returns>返回保存结果</returns>
        bool Save(Model.ApplyMaterial.Log m_Log);
        /// <summary>
        /// 获得计算机名
        /// </summary>
        /// <param name="m_Type">Type</param>
        /// <returns>计算机名</returns>
        List<string> GetComputerName(string m_Type);
    }
}
