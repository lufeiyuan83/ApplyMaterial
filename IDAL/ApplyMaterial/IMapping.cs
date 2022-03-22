using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace IDAL.ApplyMaterial
{
    public interface IMapping
    {
        /// <summary>
        /// 查询Mapping
        /// </summary>
        /// <returns>Mapping</returns>
        DataTable Select();
        /// <summary>
        /// 查询Mapping
        /// </summary>
        /// <param name="m_MapGroup">MapGroup</param>
        /// <returns>Mapping</returns>
        DataTable Select(string m_MapGroup);
        /// <summary>
        /// 查询Mapping
        /// </summary>
        /// <param name="m_MapGroup">MapGroup</param>
        /// <param name="m_MapCode">MapCode</param>
        /// <returns>Mapping</returns>
        DataTable Select(string m_MapGroup,string m_MapCode);
        /// <summary>
        /// 查询Mapping
        /// </summary>
        /// <param name="m_MapGroup">MapGroup</param>
        /// <param name="m_MapCode">MapCode</param>
        /// <param name="m_MappingValue1">MappingValue1</param>
        /// <returns>Mapping</returns>
        DataTable Select(string m_MapGroup, string m_MapCode, string m_MappingValue1);
        /// <summary>
        /// 查询Mapping的扩展子项
        /// </summary>
        /// <param name="m_MappingId">MappingId</param>
        /// <returns>Mapping</returns>
        DataTable GetExtendItems(long m_MappingId);
        /// <summary>
        /// 查询Mapping的扩展子项
        /// </summary>
        /// <param name="m_MapGroup">MapGroup</param>
        /// <param name="m_MapCode">MapCode</param>
        /// <param name="m_MappingValue1">MappingValue1</param>
        /// <returns>Mapping</returns>
        DataTable GetExtendItems(string m_MapGroup, string m_MapCode, string m_MappingValue1);
        /// <summary>
        /// 查询Mapping
        /// </summary>
        /// <param name="m_MapGroup">MapGroup</param>
        /// <param name="m_MapCode">MapCode</param>
        /// <param name="m_MappingValue1">MappingValue1</param>
        /// <param name="m_MappingValue2">MappingValue2</param>
        /// <returns>Mapping</returns>
        DataTable Select(string m_MapGroup, string m_MapCode, string m_MappingValue1, string m_MappingValue2);
        /// <summary>
        /// 查询单条Mapping
        /// </summary>
        /// <param name="m_MappingID">MappingID</param>
        /// <returns>Mapping</returns>
        Model.ApplyMaterial.Mapping Select(long m_MappingID);
    }
}
