using SQLHelp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DAL.ApplyMaterial
{
    public class TipTopParameter : IDAL.ApplyMaterial.ITipTopParameter
    {
        private const string tableName = "PLM.dbo.TipTopParameter";

        /// <summary>
        /// 查询TipTopParameter
        /// </summary>
        /// <returns>Log</returns>
        public DataTable Select()
        {
            using (DataAccess da = new DataAccess())
            {
                return da.GetDataTable("Select * From " + tableName + " Where IsDeleted=0");
            }
        }
        /// <summary>
        /// 通过物料编码查询物料信息
        /// </summary>
        /// <param name="m_MaterialId">MaterialId</param>
        /// <returns>Log</returns>
        public DataTable Select(string m_MaterialId)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@MaterialId", m_MaterialId.Trim().ToUpper()));
                return da.GetDataTable("Select * From " + tableName + " Where Upper(MaterialId)=@MaterialId And IsDeleted=0", da.sqlParameters);
            }
        }
        /// <summary>
        /// 通过物料编码查询物料信息
        /// </summary>
        /// <param name="m_MaterialId">MaterialId</param>
        /// <param name="m_Bucode">Bucode</param>
        /// <returns>Log</returns>
        public DataTable Select(string m_MaterialId,string m_Bucode)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@MaterialId", m_MaterialId.Trim().ToUpper()));
                da.sqlParameters.Add(da.CreateSqlParameter("@Bucode", m_Bucode.Trim().ToUpper()));
                return da.GetDataTable("Select * From " + tableName + " Where Upper(MaterialId)=@MaterialId And Upper(Bucode)=@Bucode And IsDeleted=0", da.sqlParameters);
            }
        }
        /// <summary>
        /// 更新料号抛送TipTop信息
        /// <param name="m_TipTopParameter">TipTopParameter</param>
        /// </summary>
        /// <returns></returns>
        public bool Update(Model.ApplyMaterial.TipTopParameter m_TipTopParameter)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@MaterialId", m_TipTopParameter.MaterialId));
                da.sqlParameters.Add(da.CreateSqlParameter("@BuCode", m_TipTopParameter.BuCode));
                da.sqlParameters.Add(da.CreateSqlParameter("@IsPostSuccess", m_TipTopParameter.IsPostSuccess));
                da.sqlParameters.Add(da.CreateSqlParameter("@LastUpdateUserID", m_TipTopParameter.LastUpdateUserID));
                return da.ExecuteCommand("Update " + tableName + " Set IsPostSuccess=@IsPostSuccess,LastUpdateUserID=@LastUpdateUserID,LastUpdateDateTime=GetDate() Where MaterialId=@MaterialId And Upper(BuCode)=@BuCode And IsDeleted=0", da.sqlParameters) != 0;
            }
        }
    }
}
