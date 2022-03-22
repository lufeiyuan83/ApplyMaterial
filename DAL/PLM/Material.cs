using SQLHelp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DAL.PLM
{
    public class Material : IDAL.PLM.IMaterial
    {
        private const string tableName = "PLM.dbo.Material";
        /// <summary>
        /// 查询Material
        /// </summary>
        /// <returns></returns>
        public DataTable Select()
        {
            using (DataAccess da = new DataAccess())
            {
                return da.GetDataTable("Select * From " + tableName + " Where IsDeleted=0");
            }
        }
        /// <summary>
        /// 查询Material
        /// </summary>
        /// <param name="m_Id">Id</param>
        /// <returns></returns>
        public Model.PLM.Material Select(long m_Id)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@Id", m_Id));
                DataRow dr = da.GetDataRow("Select * From " + tableName + " Where Id=@Id", da.sqlParameters);
                if (dr != null)
                {
                    Model.PLM.Material material = new Model.PLM.Material();
                    material.Id = Convert.ToInt64(dr["Id"]);
                    material.MaterialId = dr["MaterialId"].ToString().Trim();
                    material.Rev = dr["Rev"].ToString().Trim();
                    material.ProductionName = dr["ProductionName"].ToString().Trim();
                    material.Specification = dr["Specification"].ToString().Trim();
                    material.IsDeleted = Convert.ToBoolean(dr["IsDeleted"].ToString().Trim());
                    material.CreateDateTime = Convert.ToDateTime(dr["CreateDateTime"]);
                    material.CreateUserID = dr["CreateUserID"].ToString().Trim();
                    material.LastUpdateUserID = dr["LastUpdateUserID"].ToString().Trim();
                    material.LastUpdateDateTime = Convert.ToDateTime(dr["LastUpdateDateTime"]);
                    material.Remark = dr["Remark"].ToString().Trim();
                    return material;
                }
                else
                    return null;
            }
        }
        /// <summary>
        /// 新增料号信息
        /// <param name="m_Material">Material</param>
        /// </summary>
        /// <returns></returns>
        public bool Add(Model.PLM.Material m_Material)
        {
            using (DataAccess da = new DataAccess())
            { 
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@MaterialId", m_Material.MaterialId.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@Rev", m_Material.Rev.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@ProductionName", m_Material.ProductionName.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@Specification", m_Material.Specification.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@CreateUserID", m_Material.CreateUserID.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@LastUpdateUserID", m_Material.LastUpdateUserID.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@Remark", m_Material.Remark.Trim()));
                return da.ExecuteCommand("Insert Into " + tableName + "(MaterialId,Rev,ProductionName,Specification,CreateDateTime,CreateUserID,LastUpdateDateTime,LastUpdateUserID,Remark) Values(@MaterialId,@Rev,@ProductionName,@Specification,GetDate(),@CreateUserID,GetDate(),@LastUpdateUserID,@Remark)", da.sqlParameters)!=0;
            }
        }
        /// <summary>
        /// 是否已经存在该物料信息
        /// </summary>
        /// <param name="m_MaterialId">物料编码</param>
        /// <returns></returns>
        public bool IsExist(string m_MaterialId)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@MaterialId", m_MaterialId.Trim().ToUpper()));
                object isExist = da.GetScalar("Select Count(1) From " + tableName + " Where Upper(MaterialId)=@MaterialId And IsDeleted=0", da.sqlParameters);
                if (isExist != null)
                    return Convert.ToInt32(isExist) > 0;
                else
                {
                    Exception ex = new Exception("Not find MaterialId=" + m_MaterialId);
                    ex.Source = "Class name:" + tableName + ",Error method:public bool IsExist(m_MaterialId=" + m_MaterialId + ")";
                    throw ex;
                }
            }
        }
    }
}
