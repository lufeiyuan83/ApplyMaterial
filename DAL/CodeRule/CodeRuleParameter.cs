using SQLHelp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DAL.CodeRule
{
    public class CodeRuleParameter : IDAL.CodeRule.ICodeRuleParameter
    {
        private const string tableName = "PLM.dbo.CodeRuleParameter";
        /// <summary>
        /// 查询编码原则参数信息
        /// </summary>
        /// <returns></returns>
        public DataTable Select()
        {
            using (DataAccess da = new DataAccess())
            {
                return da.GetDataTable("Select * From " + tableName + " Where IsDeleted=0 Order By GroupNo,SortNo");
            }
        }
        /// <summary>
        /// 查询编码原则参数信息
        /// </summary>
        /// <param name="m_Id">Id</param>
        /// <returns></returns>
        public Model.CodeRule.CodeRuleParameter Select(long m_Id)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@Id", m_Id));
                DataRow dr = da.GetDataRow("Select * From " + tableName + " Where Id=@Id", da.sqlParameters);
                if (dr != null)
                {
                    Model.CodeRule.CodeRuleParameter codeRuleParameter = new Model.CodeRule.CodeRuleParameter
                    {
                        Id = Convert.ToInt64(dr["Id"]),
                        CodeRuleId = Convert.ToInt64(dr["CodeRuleId"]),
                        ParameterCode = dr["ParameterCode"].ToString().Trim(),
                        ParameterName = dr["ParameterName"].ToString().Trim(),
                        ParameterValue = dr["ParameterValue"].ToString().Trim(),
                        ParameterType = dr["ParameterType"].ToString().Trim(),
                        ParameterDescription = dr["ParameterDescription"].ToString().Trim(),
                        ParentId = Convert.ToInt64(dr["ParentId"]),
                        IsDefault = Convert.ToBoolean(dr["IsDefault"].ToString().Trim()),
                        SortNo = Convert.ToInt32(dr["SortNo"].ToString().Trim()),
                        GroupNo = Convert.ToInt32(dr["GroupNo"].ToString().Trim()),
                        Level = Convert.ToInt32(dr["Level"].ToString().Trim()),
                        IsDeleted = Convert.ToBoolean(dr["IsDeleted"].ToString().Trim()),
                        CreateDateTime = Convert.ToDateTime(dr["CreateDateTime"]),
                        CreateUserID = dr["CreateUserID"].ToString().Trim(),
                        LastUpdateUserID = dr["LastUpdateUserID"].ToString().Trim(),
                        LastUpdateDateTime = Convert.ToDateTime(dr["LastUpdateDateTime"]),
                        Remark = dr["Remark"].ToString().Trim()
                    };
                    return codeRuleParameter;
                }
                else
                    return null;
            }
        }
        /// <summary>
        /// 通过编码原则主键查询编码原则参数信息
        /// </summary>
        /// <param name="m_CodeRuleId">编码原则主键</param>
        /// <returns></returns>
        public DataTable Search(long m_CodeRuleId)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@CodeRuleId", m_CodeRuleId));
                return da.GetDataTable("Select * From " + tableName + " Where CodeRuleId=@CodeRuleId And IsDeleted=0 Order By GroupNo,SortNo", da.sqlParameters);
            }
        }
        /// <summary>
        /// 增加编码原则参数信息
        /// </summary>
        /// <param name="m_CodeRuleParameter">编码原则参数信息</param>
        /// <returns>返回增加结果</returns>
        public bool Add(Model.CodeRule.CodeRuleParameter m_CodeRuleParameter)
        {
            using (DataAccess da = new DataAccess())
            {
                try
                {
                    da.BeginTransaction();
                    da.sqlParameters.Clear();
                    da.sqlParameters.Add(da.CreateSqlParameter("@CodeRuleId", m_CodeRuleParameter.CodeRuleId));
                    da.sqlParameters.Add(da.CreateSqlParameter("@ParameterCode", m_CodeRuleParameter.ParameterCode.Trim()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@ParameterName", m_CodeRuleParameter.ParameterName.Trim()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@ParameterValue", m_CodeRuleParameter.ParameterValue.Trim()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@ParameterType", m_CodeRuleParameter.ParameterType.Trim()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@ParameterDescription", m_CodeRuleParameter.ParameterDescription.Trim()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@ParentId", m_CodeRuleParameter.ParentId));
                    da.sqlParameters.Add(da.CreateSqlParameter("@IsDefault", m_CodeRuleParameter.IsDefault));
                    da.sqlParameters.Add(da.CreateSqlParameter("@SortNo", m_CodeRuleParameter.SortNo));
                    da.sqlParameters.Add(da.CreateSqlParameter("@GroupNo", m_CodeRuleParameter.GroupNo)); 
                    da.sqlParameters.Add(da.CreateSqlParameter("@Level", m_CodeRuleParameter.Level));
                    da.sqlParameters.Add(da.CreateSqlParameter("@CreateUserID", m_CodeRuleParameter.CreateUserID.Trim()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@LastUpdateUserID", m_CodeRuleParameter.LastUpdateUserID.Trim()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@Remark", m_CodeRuleParameter.Remark.Trim()));
                    da.ExecuteCommand("Insert Into " + tableName + "(CodeRuleId,ParameterCode,ParameterName,ParameterValue,ParameterType,ParameterDescription,ParentId,IsDefault,SortNo,GroupNo,Level,CreateUserID,CreateDateTime,LastUpdateUserID,LastUpdateDateTime,Remark) Values(@CodeRuleId,@ParameterCode,@ParameterName,@ParameterValue,@ParameterType,@ParameterDescription,@ParentId,@IsDefault,@SortNo,@GroupNo,@Level,@CreateUserID,GetDate(),@LastUpdateUserID,GetDate(),@Remark)", da.sqlParameters);
                    if(m_CodeRuleParameter.ParentId==0)
                        da.ExecuteCommand("Update " + tableName + " Set ParentId=id where ParentId=0");

                    da.CommitTransaction();
                    return true;
                }
                catch
                {
                    da.RollbackTransaction();
                    throw;
                }
            }
        }
        /// <summary>
        /// 更新编码原则参数信息
        /// <param name="m_CodeRuleParameter">编码原则参数信息</param>
        /// </summary>
        /// <returns></returns>
        public bool Update(Model.CodeRule.CodeRuleParameter m_CodeRuleParameter)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@Id", m_CodeRuleParameter.Id));
                da.sqlParameters.Add(da.CreateSqlParameter("@CodeRuleId", m_CodeRuleParameter.CodeRuleId));
                da.sqlParameters.Add(da.CreateSqlParameter("@ParameterCode", m_CodeRuleParameter.ParameterCode.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@ParameterName", m_CodeRuleParameter.ParameterName.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@ParameterValue", m_CodeRuleParameter.ParameterValue.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@ParameterType", m_CodeRuleParameter.ParameterType.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@ParameterDescription", m_CodeRuleParameter.ParameterDescription.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@ParentId", m_CodeRuleParameter.ParentId));
                da.sqlParameters.Add(da.CreateSqlParameter("@IsDefault", m_CodeRuleParameter.IsDefault));
                da.sqlParameters.Add(da.CreateSqlParameter("@SortNo", m_CodeRuleParameter.SortNo));
                da.sqlParameters.Add(da.CreateSqlParameter("@GroupNo", m_CodeRuleParameter.GroupNo));
                da.sqlParameters.Add(da.CreateSqlParameter("@LastUpdateUserID", m_CodeRuleParameter.LastUpdateUserID.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@Remark", m_CodeRuleParameter.Remark.Trim()));
                return da.ExecuteCommand("Update " + tableName + " Set CodeRuleId=@CodeRuleId,ParameterCode=@ParameterCode,ParameterName=@ParameterName,ParameterValue=@ParameterValue,ParameterType=@ParameterType,ParameterDescription=@ParameterDescription,ParentId=@ParentId,IsDefault=@IsDefault,SortNo=@SortNo,GroupNo=@GroupNo,LastUpdateUserID=@LastUpdateUserID,LastUpdateDateTime=GetDate(),Remark=@Remark Where Id=@Id", da.sqlParameters) != 0 ? true : false;
            }
        }
        /// <summary>
        /// 删除编码原则参数信息
        /// </summary>
        /// <param name="m_Id">主键</param>
        /// <param name="m_DeleteUserID">删除人</param>
        /// <returns>返回删除结果</returns>
        public bool Delete(long m_Id, string m_DeleteUserID)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@Id", m_Id));
                da.sqlParameters.Add(da.CreateSqlParameter("@LastUpdateUserID", m_DeleteUserID.Trim()));
                return da.ExecuteCommand("Update " + tableName + " Set IsDeleted=1,LastUpdateUserID=@LastUpdateUserID,LastUpdateDateTime=GetDate() Where id=@Id", da.sqlParameters) != 0 ? true : false;
            }
        }
        /// <summary>
        /// 获得最大SortNo
        /// </summary>
        /// <param name="m_CodeRuleId">编码原则主键</param>
        /// <param name="m_ParentId"></param>
        /// <param name="m_IsSubNodeSortNo">是否查询子节点的最大排序号</param>
        /// <param name="m_IsSubNodeSortNo">是否顶层节点</param>
        /// <returns></returns>
        public int GetMaxSortNo(long m_CodeRuleId, long m_ParentId,bool m_IsSubNodeSortNo,bool m_IsTopNode)
        {
            using (DataAccess da = new DataAccess())
            {
                if(!m_IsSubNodeSortNo)
                {
                    if (m_IsTopNode)
                    {
                        da.sqlParameters.Clear();
                        da.sqlParameters.Add(da.CreateSqlParameter("@CodeRuleId", m_CodeRuleId));
                        object obj = da.GetScalar("Select Max(SortNo) From " + tableName + " Where CodeRuleId=@CodeRuleId And ParentId=Id And IsDeleted=0", da.sqlParameters);
                        return (obj == null || obj.ToString() == "" ? 0 : Convert.ToInt32(obj));
                    }
                    else
                    {
                        da.sqlParameters.Clear();
                        da.sqlParameters.Add(da.CreateSqlParameter("@CodeRuleId", m_CodeRuleId));
                        da.sqlParameters.Add(da.CreateSqlParameter("@ParentId", m_ParentId));
                        object obj = da.GetScalar("Select Max(SortNo) From " + tableName + " Where CodeRuleId=@CodeRuleId And ParentId=@ParentId And IsDeleted=0", da.sqlParameters);
                        return (obj == null || obj.ToString() == "" ? 0 : Convert.ToInt32(obj));
                    }
                }
                else
                {
                    da.sqlParameters.Clear();
                    da.sqlParameters.Add(da.CreateSqlParameter("@CodeRuleId", m_CodeRuleId));
                    da.sqlParameters.Add(da.CreateSqlParameter("@ParentId", m_ParentId));
                    object obj = da.GetScalar("Select Max(SortNo) From " + tableName + " Where CodeRuleId=@CodeRuleId And ParentId=@ParentId And ParentId!=Id And IsDeleted=0", da.sqlParameters);
                    return (obj == null || obj.ToString() == "" ? 0 : Convert.ToInt32(obj));
                }
            }
        }
        /// <summary>
        /// 是否已经存在该编码参数信息
        /// </summary>
        /// <param name="m_CodeRuleId">编码原则主键</param>
        /// <param name="m_ParameterCode">参数代码</param>
        /// <returns>返回结果</returns>
        public bool IsExist(long m_CodeRuleId, string m_ParameterCode)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@CodeRuleId", m_CodeRuleId));
                da.sqlParameters.Add(da.CreateSqlParameter("@ParameterCode", m_ParameterCode.Trim().ToUpper()));
                object isExist = da.GetScalar("Select Count(1) From " + tableName + " Where CodeRuleId=@CodeRuleId And Upper(ParameterCode)=@ParameterCode And IsDeleted=0", da.sqlParameters);
                if (isExist != null)
                    return Convert.ToInt32(isExist) > 0;
                else
                {
                    Exception ex = new Exception("Not find CodeRuleId=" + m_CodeRuleId + ",ParameterCode=" + m_ParameterCode + " information");
                    ex.Source = "Class name:" + tableName + ",Error method:public bool IsExist(m_CodeRuleId=" + m_CodeRuleId + ",m_ParameterCode=" + m_ParameterCode + ")";
                    throw ex;
                }
            }
        }
        /// <summary>
        /// 是否已经存在该编码参数信息
        /// </summary>
        /// <param name="m_CodeRuleId">编码原则主键</param>
        /// <param name="m_ParentId">父节点编号</param>
        /// <param name="m_ParameterCode">参数代码</param>
        /// <returns>返回结果</returns>
        public bool IsExist(long m_CodeRuleId, long m_ParentId, string m_ParameterCode)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@CodeRuleId", m_CodeRuleId));
                da.sqlParameters.Add(da.CreateSqlParameter("@ParentId", m_ParentId));
                da.sqlParameters.Add(da.CreateSqlParameter("@ParameterCode", m_ParameterCode.Trim().ToUpper()));
                object isExist = da.GetScalar("Select Count(1) From " + tableName + " Where CodeRuleId=@CodeRuleId And ParentId=@ParentId And Upper(ParameterCode)=@ParameterCode And IsDeleted=0", da.sqlParameters);
                if (isExist != null)
                    return Convert.ToInt32(isExist) > 0;
                else
                {
                    Exception ex = new Exception("Not find CodeRuleId=" + m_CodeRuleId + ",ParentId=" + m_ParentId.ToString() + ",ParameterCode=" + m_ParameterCode + " information");
                    ex.Source = "Class name:" + tableName + ",Error method:public bool IsExist(m_CodeRuleId=" + m_CodeRuleId + ",m_ParentId=" + m_ParentId.ToString() + ",m_ParameterCode=" + m_ParameterCode + ")";
                    throw ex;
                }
            }
        }
    }
}
