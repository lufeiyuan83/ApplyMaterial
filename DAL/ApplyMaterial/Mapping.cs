using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DAL;
using SQLHelp;

namespace DAL.ApplyMaterial
{
    public class Mapping : IDAL.ApplyMaterial.IMapping
    {
        private const string tableName = "PLM.dbo.Mapping";

        /// <summary>
        /// 查询Mapping
        /// </summary>
        /// <returns>Mapping</returns>
        public DataTable Select()
        {
            using (DataAccess da = new DataAccess())
            {
                return da.GetDataTable("Select * From " + tableName + " Where IsDeleted=0 Order By MapGroup,MapCode,SortNumber");
            }
        }
        /// <summary>
        /// 查询Mapping
        /// </summary>
        /// <param name="m_MapGroup">MapGroup</param>
        /// <returns>Mapping</returns>
        public DataTable Select(string m_MapGroup)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@MapGroup", m_MapGroup.Trim().ToUpper()));
                return da.GetDataTable("Select * From " + tableName + " Where Upper(MapGroup)=@MapGroup And IsDeleted=0 Order By MapCode,SortNumber", da.sqlParameters);
            }
        }        
        /// <summary>
        /// 查询Mapping
        /// </summary>
        /// <param name="m_MapGroup">MapGroup</param>
        /// <param name="m_MapCode">MapCode</param>
        /// <returns>Mapping</returns>
        public DataTable Select(string m_MapGroup, string m_MapCode)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@MapGroup", m_MapGroup.Trim().ToUpper()));
                da.sqlParameters.Add(da.CreateSqlParameter("@MapCode", m_MapCode.Trim().ToUpper()));
                return da.GetDataTable("Select * From " + tableName + " Where Upper(MapGroup)=@MapGroup And Upper(MapCode)=@MapCode And IsDeleted=0 Order By MapCode,SortNumber", da.sqlParameters);
            }
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
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@MapGroup", m_MapGroup.Trim().ToUpper()));
                da.sqlParameters.Add(da.CreateSqlParameter("@MapCode", m_MapCode.Trim().ToUpper()));
                if (!string.IsNullOrEmpty(m_MappingValue1))
                {
                    da.sqlParameters.Add(da.CreateSqlParameter("@MappingValue1", m_MappingValue1.Trim().ToUpper()));
                    return da.GetDataTable("Select * From " + tableName + " Where Upper(MapGroup)=@MapGroup And Upper(MapCode)=@MapCode And Upper(MappingValue1)=@MappingValue1 And IsDeleted=0 Order By MapCode,SortNumber", da.sqlParameters);
                }
                else
                    return da.GetDataTable("Select * From " + tableName + " Where Upper(MapGroup)=@MapGroup And Upper(MapCode)=@MapCode And IsDeleted=0 Order By MapCode,SortNumber", da.sqlParameters);
            }
        }
        /// <summary>
        /// 查询Mapping的扩展子项
        /// </summary>
        /// <param name="m_MappingId">MappingId</param>
        /// <returns>Mapping</returns>
        public DataTable GetExtendItems(long m_MappingId)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@MappingId", m_MappingId));
                
                return da.GetDataTable("Select * From " + tableName + " Where MappingID In (select * from PLM.dbo.F_SUBSTRINGINTARRAY((Select ExtendID From PLM.dbo.Mapping Where MappingId = @MappingId),',')) Order By SortNumber", da.sqlParameters);
            }
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
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@MapGroup", m_MapGroup.Trim().ToUpper()));
                da.sqlParameters.Add(da.CreateSqlParameter("@MapCode", m_MapCode.Trim().ToUpper()));
                da.sqlParameters.Add(da.CreateSqlParameter("@MappingValue1", m_MappingValue1.Trim().ToUpper()));
                return da.GetDataTable("Select * From " + tableName + " Where MappingID In (select * from PLM.dbo.F_SUBSTRINGINTARRAY((Select ExtendID From " + tableName + " Where Upper(MapGroup)=@MapGroup And Upper(MapCode)=@MapCode And Upper(MappingValue1)=@MappingValue1 And IsDeleted=0),',')) Order By SortNumber", da.sqlParameters);
            }
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
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                string strWhere = " Where IsDeleted=0";
                if (!string.IsNullOrEmpty(m_MapGroup))
                {
                    da.sqlParameters.Add(da.CreateSqlParameter("@MapGroup", m_MapGroup.Trim().ToUpper()));
                    strWhere += " And Upper(MapGroup)=@MapGroup";
                }
                if (!string.IsNullOrEmpty(m_MapCode))
                {
                    da.sqlParameters.Add(da.CreateSqlParameter("@MapCode", m_MapCode.Trim().ToUpper()));
                    strWhere += " And Upper(MapCode)=@MapCode";
                }
                if (!string.IsNullOrEmpty(m_MappingValue1))
                {
                    da.sqlParameters.Add(da.CreateSqlParameter("@MappingValue1", m_MappingValue1.Trim().ToUpper()));
                    strWhere += " And Upper(MappingValue1)=@MappingValue1";
                }
                if (!string.IsNullOrEmpty(m_MappingValue2))
                {
                    da.sqlParameters.Add(da.CreateSqlParameter("@MappingValue2", m_MappingValue2.Trim().ToUpper()));
                    strWhere += " And Upper(MappingValue2)=@MappingValue2";
                }
                strWhere += " And IsDeleted=0 Order By MapCode,SortNumber";
                return da.GetDataTable("Select * From " + tableName + strWhere, da.sqlParameters);
            }
        }
        /// <summary>
        /// 查询单条Mapping
        /// </summary>
        /// <param name="m_MappingID">MappingID</param>
        /// <returns>Mapping</returns>
        public Model.ApplyMaterial.Mapping Select(long m_MappingID)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@MappingID", m_MappingID));
                DataRow dr = da.GetDataRow("Select * From " + tableName + " Where MappingID=@MappingID And IsDeleted=0", da.sqlParameters);
                if (dr != null)
                {
                    Model.ApplyMaterial.Mapping mapping = new Model.ApplyMaterial.Mapping();
                    mapping.MappingID = Convert.ToInt64(dr["MappingID"]);
                    mapping.MapGroup = dr["MapGroup"].ToString().Trim();                    
                    mapping.MapCode = dr["MapCode"].ToString().Trim();                   
                    mapping.MapCodeDescription = dr["MapCodeDescription"].ToString().Trim();                   
                    mapping.MappingName1 = dr["MappingName1"].ToString().Trim();                   
                    mapping.MappingAlias1 = dr["MappingAlias1"].ToString().Trim();                   
                    mapping.MappingValue1 = dr["MappingValue1"].ToString().Trim();                 
                    mapping.Delimiter1 = dr["Delimiter1"].ToString().Trim();               
                    mapping.MappingType1 = dr["MappingType1"].ToString().Trim();            
                    mapping.MappingValue1Description = dr["MappingValue1Description"].ToString().Trim();
                    mapping.MappingName2 = dr["MappingName2"].ToString().Trim();                   
                    mapping.MappingAlias2 = dr["MappingAlias2"].ToString().Trim();                   
                    mapping.MappingValue2 = dr["MappingValue2"].ToString().Trim();                 
                    mapping.Delimiter2 = dr["Delimiter2"].ToString().Trim();               
                    mapping.MappingType2 = dr["MappingType2"].ToString().Trim();            
                    mapping.MappingValue2Description = dr["MappingValue2Description"].ToString().Trim();
                    mapping.ExtendID = dr["ExtendID"].ToString().Trim();
                    mapping.IsDefault = Convert.ToBoolean(dr["IsDefault"]);
                    mapping.IsNotNull = Convert.ToBoolean(dr["IsNotNull"]);
                    mapping.SortNumber = Convert.ToInt32(dr["SortNumber"]);
                    mapping.Remark = dr["Remark"].ToString().Trim();
                    return mapping;
                }
                else
                    return null;
            }
        }
    }
}
