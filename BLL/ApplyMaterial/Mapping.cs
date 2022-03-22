using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BLL.ApplyMaterial
{
    public class Mapping
    {
        private readonly IDAL.ApplyMaterial.IMapping dal = DALFactory.DALFactory.GetInstance().GetMapping();

        /// <summary>
        /// 查询Mapping
        /// </summary>
        /// <returns>Mapping</returns>
        public DataTable Select()
        {
            return dal.Select();
        }
        /// <summary>
        /// 查询Mapping
        /// </summary>
        /// <param name="m_MapGroup">MapGroup</param>
        /// <returns>Mapping</returns>
        public DataTable Select(string m_MapGroup)
        {
            return dal.Select(m_MapGroup);
        }
        /// <summary>
        /// 查询Mapping
        /// </summary>
        /// <param name="m_MapGroup">MapGroup</param>
        /// <param name="m_MapCode">MapCode</param>
        /// <returns>Mapping</returns>
        public DataTable Select(string m_MapGroup, string m_MapCode)
        {
            return dal.Select(m_MapGroup, m_MapCode);
        }
        /// <summary>
        /// 查询Mapping
        /// </summary>
        /// <param name="m_MapGroup">MapGroup</param>
        /// <param name="m_MapCode">MapCode</param>
        /// <param name="m_MappingValue1">MappingValue1</param>
        /// <returns>Mapping</returns>
        public DataTable Select(string m_MapGroup, string m_MapCode, string m_MappingValue1)
        {
            return dal.Select(m_MapGroup, m_MapCode, m_MappingValue1);
        }
        /// <summary>
        /// 查询Mapping的扩展子项
        /// </summary>
        /// <param name="m_MappingId">MappingId</param>
        /// <returns>Mapping</returns>
        public DataTable GetExtendItems(long m_MappingId)
        {
            return dal.GetExtendItems(m_MappingId);
        }
        /// <summary>
        /// 查询Mapping的扩展子项
        /// </summary>
        /// <param name="m_MapGroup">MapGroup</param>
        /// <param name="m_MapCode">MapCode</param>
        /// <param name="m_MappingValue1">MappingValue1</param>
        /// <returns>Mapping</returns>
        public DataTable GetExtendItems(string m_MapGroup, string m_MapCode, string m_MappingValue1)
        {
            return dal.GetExtendItems(m_MapGroup, m_MapCode, m_MappingValue1);
        }
        /// <summary>
        /// 查询Mapping
        /// </summary>
        /// <param name="m_MapGroup">MapGroup</param>
        /// <param name="m_MapCode">MapCode</param>
        /// <param name="m_MappingValue1">MappingValue1</param>
        /// <param name="m_MappingValue2">MappingValue2</param>
        /// <returns>Mapping</returns>
        public DataTable Select(string m_MapGroup, string m_MapCode, string m_MappingValue1, string m_MappingValue2)
        {
            return dal.Select(m_MapGroup, m_MapCode, m_MappingValue1, m_MappingValue2);
        }
        /// <summary>
        /// 查询单条Mapping
        /// </summary>
        /// <param name="m_MappingID">MappingID</param>
        /// <returns>Mapping</returns>
        public Model.ApplyMaterial.Mapping Select(long m_MappingID)
        {
            return dal.Select(m_MappingID);
        }
    }
}
