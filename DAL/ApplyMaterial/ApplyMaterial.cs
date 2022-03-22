using SQLHelp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DAL.ApplyMaterial
{
    public class ApplyMaterial :IDAL.ApplyMaterial.IApplyMaterial
    {
        private const string tableName = "PLM.dbo.ApplyMaterial";

        /// <summary>
        /// 查询ApplyMaterial
        /// </summary>
        /// <returns>Log</returns>
        public DataTable Select()
        {
            using (DataAccess da = new DataAccess())
            {
                return da.GetDataTable("Select * From " + tableName +" Where IsDeleted=0");
            }
        }
        /// <summary>
        /// 查询ApplyMaterial
        /// </summary>
        /// <param name="m_Id">Id</param>
        /// <returns>Log</returns>
        public Model.ApplyMaterial.ApplyMaterial Select(long m_Id)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@Id", m_Id));
                DataRow dr = da.GetDataRow("Select * From " + tableName + " Where Id=@Id", da.sqlParameters);
                if (dr != null)
                {
                    Model.ApplyMaterial.ApplyMaterial applyMaterial = new Model.ApplyMaterial.ApplyMaterial();
                    applyMaterial.Id = Convert.ToInt64(dr["Id"]);
                    applyMaterial.MaterialId = dr["MaterialId"].ToString().Trim();
                    applyMaterial.ProductionName = dr["ProductionName"].ToString().Trim();
                    applyMaterial.Specification = dr["Specification"].ToString().Trim();
                    applyMaterial.BuCode = dr["BuCode"].ToString().Trim(); 
                    applyMaterial.Status = dr["Status"].ToString().Trim();
                    applyMaterial.IsDeleted = Convert.ToBoolean(dr["IsDeleted"].ToString().Trim());
                    applyMaterial.CreateDateTime = Convert.ToDateTime(dr["CreateDateTime"]);
                    applyMaterial.CreateUserID = dr["CreateUserID"].ToString().Trim();
                    applyMaterial.LastUpdateUserID = dr["LastUpdateUserID"].ToString().Trim();
                    applyMaterial.LastUpdateDateTime = Convert.ToDateTime(dr["LastUpdateDateTime"]);
                    applyMaterial.Remark = dr["Remark"].ToString().Trim();
                    return applyMaterial;
                }
                else
                    return null;
            }
        }
        /// <summary>
        /// 通过物料编码查询物料信息
        /// </summary>
        /// <param name="m_MaterialId">MaterialId</param>
        /// <returns></returns>
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
        /// <returns></returns>
        public List<Model.ApplyMaterial.ApplyMaterial> Select(List<string> m_MaterialId)
        {
            using (DataAccess da = new DataAccess())
            {
                string strWhere = " Where IsDeleted=0";
                if(m_MaterialId.Count>0)
                {
                    strWhere += " And MaterialId in(";
                    for (int i=0;i< m_MaterialId.Count;i++)
                    {
                        strWhere += "'"+ m_MaterialId[i].Trim() + "',";
                    }
                    strWhere = strWhere.Substring(0, strWhere.Length - 1) + ")";
                }
                da.sqlParameters.Clear();
                DataTable dt= da.GetDataTable("Select * From " + tableName + strWhere, da.sqlParameters);
                if (dt != null && dt.Rows.Count>0)
                {
                    List<Model.ApplyMaterial.ApplyMaterial> lstApplyMaterial = new List<Model.ApplyMaterial.ApplyMaterial>();
                    for (int i=0;i< dt.Rows.Count;i++)
                    {
                        Model.ApplyMaterial.ApplyMaterial applyMaterial = new Model.ApplyMaterial.ApplyMaterial();
                        applyMaterial.Id = Convert.ToInt64(dt.Rows[i]["Id"]);
                        applyMaterial.MaterialId = dt.Rows[i]["MaterialId"].ToString().Trim();
                        applyMaterial.ProductionName = dt.Rows[i]["ProductionName"].ToString().Trim();
                        applyMaterial.Specification = dt.Rows[i]["Specification"].ToString().Trim();
                        applyMaterial.BuCode = dt.Rows[i]["BuCode"].ToString().Trim();
                        applyMaterial.Status = dt.Rows[i]["Status"].ToString().Trim();
                        applyMaterial.IsDeleted = Convert.ToBoolean(dt.Rows[i]["IsDeleted"].ToString().Trim());
                        applyMaterial.CreateDateTime = Convert.ToDateTime(dt.Rows[i]["CreateDateTime"]);
                        applyMaterial.CreateUserID = dt.Rows[i]["CreateUserID"].ToString().Trim();
                        applyMaterial.LastUpdateUserID = dt.Rows[i]["LastUpdateUserID"].ToString().Trim();
                        applyMaterial.LastUpdateDateTime = Convert.ToDateTime(dt.Rows[i]["LastUpdateDateTime"]);
                        applyMaterial.Remark = dt.Rows[i]["Remark"].ToString().Trim();
                        lstApplyMaterial.Add(applyMaterial);
                    }
                    return lstApplyMaterial;
                }
                else
                    return null;
            }
        }
        /// <summary>
        /// 通过物料编码和申请人查询物料信息
        /// </summary>
        /// <param name="m_Bucode">TipTop资料库</param>
        /// <param name="m_MaterialId">MaterialId</param>
        /// <param name="m_CreateUserID">CreateUserID</param>
        /// <returns></returns>
        public DataTable Select(string m_Bucode, string m_MaterialId,string m_CreateUserID)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                string strWhere = " Where 1=1";
                if (!String.IsNullOrWhiteSpace(m_Bucode))
                {
                    strWhere += " And Upper(a.BuCode) Like '" + m_Bucode.Trim().ToUpper() + "%'";
                }
                if (!String.IsNullOrWhiteSpace(m_MaterialId))
                { 
                    strWhere += " And Upper(a.MaterialId) Like '" + m_MaterialId.Trim().ToUpper() + "%'";
                }
                if (!String.IsNullOrWhiteSpace(m_CreateUserID))
                {
                    da.sqlParameters.Add(da.CreateSqlParameter("@CreateUserID", m_CreateUserID.Trim().ToUpper()));
                    strWhere += " And Upper(a.CreateUserID)=@CreateUserID";
                }
                return da.GetDataTable("Select DENSE_RANK() OVER(ORDER BY a.Id) SequenceNumber,(Select EmployeeName From [HRM].[dbo].[Employee] Where IsDeleted=0 And EmployeeID=a.CreateUserID) EmployeeName,a.* From [PLM].[dbo].[vApplyMaterial] a " + strWhere + " Order by CreateDateTime", da.sqlParameters);
            }
        }
        /// <summary>
        /// 获得该审批状态的的数据
        /// </summary>
        /// <param name="m_MaterialId">物料编码</param>
        /// <param name="m_Status">审批状态</param>
        /// <returns></returns>
        public DataTable GetStatusData(string m_MaterialId,string m_Status)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@MaterialId", m_MaterialId.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@Status", m_Status.Trim()));
                return da.GetDataTable("Select * From " + tableName + " Where MaterialId=@MaterialId And Status=@Status And IsDeleted=0", da.sqlParameters);
            }
        }        
        /// <summary>
        /// 增加物料信息
        /// </summary>
        /// <param name="m_ApplyMaterial">物料信息</param>
        /// <param name="m_TipTopParameter">抛送TipTop的参数</param>
        /// <param name="m_MaterialId">料号</param>
        /// <returns>返回增加结果</returns>
        public bool Add(Model.ApplyMaterial.ApplyMaterial m_ApplyMaterial, List<Model.ApplyMaterial.TipTopParameter> m_TipTopParameter,out string m_MaterialId)
        {
            using (DataAccess da = new DataAccess())
            {
                try
                {
                    m_MaterialId = "";
                    da.BeginTransaction();

                    #region 获得当前的物料SN信息
                    da.sqlParameters.Clear();
                    object objMaxMaterialId = da.GetScalar("Select Max(MaterialId) From " + tableName + " Where MaterialId Like '"+ m_ApplyMaterial.MaterialId.Trim().Replace("*", "") + "%'", da.sqlParameters);
                    if (objMaxMaterialId != null)
                    {
                        int intStartStarIndex = m_ApplyMaterial.MaterialId.Trim().IndexOf('*');
                        int intEndStarIndex = m_ApplyMaterial.MaterialId.Trim().LastIndexOf('*');
                        string strSN = "";
                        string strMaxMaterialId = "";
                        if (objMaxMaterialId.ToString() == "")
                        {
                            strMaxMaterialId = m_ApplyMaterial.MaterialId.Trim();
                            strSN = Convert.ToInt32(m_TipTopParameter[0].StartSN).ToString("0".PadLeft(intEndStarIndex - intStartStarIndex + 1, '0'));
                        }
                        else
                        {
                            strMaxMaterialId = objMaxMaterialId.ToString();
                            strSN = (Convert.ToInt32(strMaxMaterialId.Substring(intStartStarIndex, intEndStarIndex - intStartStarIndex + 1)) + 1).ToString("0".PadLeft(intEndStarIndex - intStartStarIndex + 1, '0'));
                        }
                        strMaxMaterialId = strMaxMaterialId.Substring(0, intStartStarIndex) + strSN + strMaxMaterialId.Substring(intEndStarIndex, strMaxMaterialId.Length - intEndStarIndex - 1);
                        m_MaterialId = strMaxMaterialId;
                        #region 新增料号信息
                        da.sqlParameters.Clear();
                        da.sqlParameters.Add(da.CreateSqlParameter("@MaterialId", strMaxMaterialId));
                        da.sqlParameters.Add(da.CreateSqlParameter("@ProductionName", m_ApplyMaterial.ProductionName.Trim()));
                        da.sqlParameters.Add(da.CreateSqlParameter("@Specification", m_ApplyMaterial.Specification.Trim()));
                        da.sqlParameters.Add(da.CreateSqlParameter("@BuCode", m_ApplyMaterial.BuCode.Trim())); 
                        da.sqlParameters.Add(da.CreateSqlParameter("@Status", m_ApplyMaterial.Status.Trim()));
                        da.sqlParameters.Add(da.CreateSqlParameter("@CreateUserID", m_ApplyMaterial.CreateUserID.Trim()));
                        da.sqlParameters.Add(da.CreateSqlParameter("@LastUpdateUserID", m_ApplyMaterial.LastUpdateUserID.Trim()));
                        da.sqlParameters.Add(da.CreateSqlParameter("@Remark", m_ApplyMaterial.Remark.Trim()));
                        da.ExecuteCommand("Insert Into " + tableName + "(MaterialId,ProductionName,Specification,BuCode,Status,CreateDateTime,CreateUserID,LastUpdateUserID,LastUpdateDateTime,Remark) Values(@MaterialId,@ProductionName,@Specification,@BuCode,@Status,GetDate(),@CreateUserID,@LastUpdateUserID,GetDate(),@Remark)", da.sqlParameters);

                        for (int i = 0; i < m_TipTopParameter.Count; i++)
                        {
                            da.sqlParameters.Clear();
                            da.sqlParameters.Add(da.CreateSqlParameter("@MaterialId", strMaxMaterialId)); 
                            da.sqlParameters.Add(da.CreateSqlParameter("@CodeRule", m_TipTopParameter[i].CodeRule.Trim()));
                            da.sqlParameters.Add(da.CreateSqlParameter("@TopClass", m_TipTopParameter[i].TopClass.Trim()));
                            da.sqlParameters.Add(da.CreateSqlParameter("@MiddleClass", m_TipTopParameter[i].MiddleClass.Trim()));
                            da.sqlParameters.Add(da.CreateSqlParameter("@SubClass", m_TipTopParameter[i].SubClass.Trim()));
                            da.sqlParameters.Add(da.CreateSqlParameter("@StartSN", m_TipTopParameter[i].StartSN.Trim()));
                            da.sqlParameters.Add(da.CreateSqlParameter("@SN", strSN)); 
                            da.sqlParameters.Add(da.CreateSqlParameter("@SourceCode", m_TipTopParameter[i].SourceCode.Trim()));
                            da.sqlParameters.Add(da.CreateSqlParameter("@SourceCode2", m_TipTopParameter[i].SourceCode2.Trim()));
                            da.sqlParameters.Add(da.CreateSqlParameter("@GroupCode", m_TipTopParameter[i].GroupCode.Trim()));
                            da.sqlParameters.Add(da.CreateSqlParameter("@FourthGroupCode", m_TipTopParameter[i].FourthGroupCode.Trim()));
                            da.sqlParameters.Add(da.CreateSqlParameter("@ABCCode", m_TipTopParameter[i].ABCCode.Trim()));
                            da.sqlParameters.Add(da.CreateSqlParameter("@IsBonded", m_TipTopParameter[i].IsBonded.Trim()));
                            da.sqlParameters.Add(da.CreateSqlParameter("@MaterialClass", m_TipTopParameter[i].MaterialClass.Trim()));
                            da.sqlParameters.Add(da.CreateSqlParameter("@MainUnit", m_TipTopParameter[i].MainUnit.Trim()));
                            da.sqlParameters.Add(da.CreateSqlParameter("@BuCode", m_TipTopParameter[i].BuCode.Trim()));
                            da.sqlParameters.Add(da.CreateSqlParameter("@IsConsumed", m_TipTopParameter[i].IsConsumed.Trim()));
                            da.sqlParameters.Add(da.CreateSqlParameter("@ReplenishmentStrategyCode", m_TipTopParameter[i].ReplenishmentStrategyCode.Trim()));
                            da.sqlParameters.Add(da.CreateSqlParameter("@IsSoftwareObject", m_TipTopParameter[i].IsSoftwareObject.Trim()));
                            da.sqlParameters.Add(da.CreateSqlParameter("@MultiLocation", m_TipTopParameter[i].MultiLocation.Trim()));
                            da.sqlParameters.Add(da.CreateSqlParameter("@MPSMRPInventoryQty", m_TipTopParameter[i].MPSMRPInventoryQty.Trim()));
                            da.sqlParameters.Add(da.CreateSqlParameter("@InavailableInventoryQty", m_TipTopParameter[i].InavailableInventoryQty.Trim()));
                            da.sqlParameters.Add(da.CreateSqlParameter("@AvailableInventoryQty", m_TipTopParameter[i].AvailableInventoryQty.Trim()));
                            da.sqlParameters.Add(da.CreateSqlParameter("@IsEngineeringMaterial", m_TipTopParameter[i].IsEngineeringMaterial.Trim()));
                            da.sqlParameters.Add(da.CreateSqlParameter("@LowCode", m_TipTopParameter[i].LowCode));
                            da.sqlParameters.Add(da.CreateSqlParameter("@UnitWeight", m_TipTopParameter[i].UnitWeight.Trim()));
                            da.sqlParameters.Add(da.CreateSqlParameter("@CostLevel", m_TipTopParameter[i].CostLevel));
                            da.sqlParameters.Add(da.CreateSqlParameter("@AveragePurchaseQty", m_TipTopParameter[i].AveragePurchaseQty));
                            da.sqlParameters.Add(da.CreateSqlParameter("@Ima110", m_TipTopParameter[i].Ima110));
                            da.sqlParameters.Add(da.CreateSqlParameter("@Ima137", m_TipTopParameter[i].Ima137.Trim()));
                            da.sqlParameters.Add(da.CreateSqlParameter("@SNContorol", m_TipTopParameter[i].SNContorol.Trim()));
                            da.sqlParameters.Add(da.CreateSqlParameter("@Ima146", m_TipTopParameter[i].Ima146.Trim()));
                            da.sqlParameters.Add(da.CreateSqlParameter("@IsMassProductionMaterial", m_TipTopParameter[i].IsMassProductionMaterial.Trim()));
                            da.sqlParameters.Add(da.CreateSqlParameter("@Ima901", m_TipTopParameter[i].Ima901.ToString("yyyy-MM-dd")));
                            da.sqlParameters.Add(da.CreateSqlParameter("@Ima908", m_TipTopParameter[i].Ima908.Trim()));
                            da.sqlParameters.Add(da.CreateSqlParameter("@CreateUserID", m_ApplyMaterial.CreateUserID.Trim()));
                            da.sqlParameters.Add(da.CreateSqlParameter("@LastUpdateUserID", m_ApplyMaterial.LastUpdateUserID.Trim()));
                            da.sqlParameters.Add(da.CreateSqlParameter("@Remark", m_ApplyMaterial.Remark.Trim()));
                            da.ExecuteCommand("Insert Into PLM.dbo.TipTopParameter(MaterialId,CodeRule,TopClass,MiddleClass,SubClass,StartSN,SN,SourceCode,SourceCode2,GroupCode,FourthGroupCode,ABCCode,IsBonded,MaterialClass,MainUnit,BuCode,IsConsumed,ReplenishmentStrategyCode,IsSoftwareObject,MultiLocation,MPSMRPInventoryQty,InavailableInventoryQty,AvailableInventoryQty,IsEngineeringMaterial,LowCode,UnitWeight,CostLevel,AveragePurchaseQty,Ima110,Ima137,SNContorol,Ima146,IsMassProductionMaterial,Ima901,Ima908,CreateDateTime,CreateUserID,LastUpdateUserID,LastUpdateDateTime,Remark) Values(@MaterialId,@CodeRule,@TopClass,@MiddleClass,@SubClass,@StartSN,@SN,@SourceCode,@SourceCode2,@GroupCode,@FourthGroupCode,@ABCCode,@IsBonded,@MaterialClass,@MainUnit,@BuCode,@IsConsumed,@ReplenishmentStrategyCode,@IsSoftwareObject,@MultiLocation,@MPSMRPInventoryQty,@InavailableInventoryQty,@AvailableInventoryQty,@IsEngineeringMaterial,@LowCode,@UnitWeight,@CostLevel,@AveragePurchaseQty,@Ima110,@Ima137,@SNContorol,@Ima146,@IsMassProductionMaterial,@Ima901,@Ima908,GetDate(),@CreateUserID,@LastUpdateUserID,GetDate(),@Remark)", da.sqlParameters);
                        }
                        #endregion
                    }
                    #endregion

                    da.CommitTransaction();
                    return true;
                }
                catch (Exception ex)
                {
                    da.RollbackTransaction();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// 更新申请料号信息
        /// <param name="m_ApplyMaterial">ApplyMaterial</param>
        /// </summary>
        /// <returns></returns>
        public bool Update(Model.ApplyMaterial.ApplyMaterial m_ApplyMaterial)
        {

            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@id", m_ApplyMaterial.Id));
                da.sqlParameters.Add(da.CreateSqlParameter("@Status", m_ApplyMaterial.Status));
                da.sqlParameters.Add(da.CreateSqlParameter("@LastUpdateUserID", m_ApplyMaterial.LastUpdateUserID));
                return da.ExecuteCommand("Update " + tableName + " Set Status=@Status,LastUpdateUserID=@LastUpdateUserID,LastUpdateDateTime=GetDate() Where id=@id", da.sqlParameters) != 0;
            }
        }
        /// <summary>
        /// 更新申请料号信息
        /// <param name="m_ApplyMaterial">ApplyMaterial</param>
        /// </summary>
        /// <returns></returns>
        public bool Update(List<Model.ApplyMaterial.ApplyMaterial> m_ApplyMaterial)
        {

            using (DataAccess da = new DataAccess())
            {
                try
                {
                    da.BeginTransaction();
                
                    for(int i=0;i< m_ApplyMaterial.Count;i++)
                    { 
                        da.sqlParameters.Clear();
                        da.sqlParameters.Add(da.CreateSqlParameter("@id", m_ApplyMaterial[i].Id));
                        da.sqlParameters.Add(da.CreateSqlParameter("@Status", m_ApplyMaterial[i].Status));
                        da.sqlParameters.Add(da.CreateSqlParameter("@LastUpdateUserID", m_ApplyMaterial[i].LastUpdateUserID));
                        da.ExecuteCommand("Update " + tableName + " Set Status=@Status,LastUpdateUserID=@LastUpdateUserID,LastUpdateDateTime=GetDate() Where id=@id", da.sqlParameters);
                    }
                    da.CommitTransaction();
                    return true;
                }
                catch (Exception ex)
                {
                    da.RollbackTransaction();
                    throw ex;
                }
            }
        }
    }
}
