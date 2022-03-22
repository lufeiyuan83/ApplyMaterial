using SQLHelp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DAL.CodeRule
{
    public class CodeRule : IDAL.CodeRule.ICodeRule
    {
        private const string tableName = "PLM.dbo.CodeRule";
        /// <summary>
        /// 查询编码原则
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
        /// 查询编码原则
        /// </summary>
        /// <param name="m_Id">Id</param>
        /// <returns></returns>
        public Model.CodeRule.CodeRule Select(long m_Id)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@Id", m_Id));
                DataRow dr = da.GetDataRow("Select * From " + tableName + " Where Id=@Id", da.sqlParameters);
                if (dr != null)
                {
                    Model.CodeRule.CodeRule codeRule = new Model.CodeRule.CodeRule();
                    codeRule.Id = Convert.ToInt64(dr["Id"]);
                    codeRule.Code = dr["Code"].ToString().Trim();
                    codeRule.CodeRuleClass = dr["CodeRuleClass"].ToString().Trim(); 
                    codeRule.BindPanelId = dr["BindPanelId"].ToString().Trim();
                    codeRule.Name = dr["Name"].ToString().Trim();
                    codeRule.Rule = dr["Rule"].ToString().Trim();
                    codeRule.RuleEN = dr["RuleEN"].ToString().Trim();
                    codeRule.Rev = Convert.ToInt32(dr["Rev"]);
                    codeRule.Schema = dr["Schema"].ToString().Trim();
                    codeRule.TableName = dr["TableName"].ToString().Trim();
                    codeRule.Field = dr["Field"].ToString().Trim();
                    codeRule.IsActive = Convert.ToBoolean(dr["IsActive"].ToString().Trim());
                    codeRule.IsDeleted = Convert.ToBoolean(dr["IsDeleted"].ToString().Trim());
                    codeRule.CreateDateTime = Convert.ToDateTime(dr["CreateDateTime"]);
                    codeRule.CreateUserID = dr["CreateUserID"].ToString().Trim();
                    codeRule.LastUpdateUserID = dr["LastUpdateUserID"].ToString().Trim();
                    codeRule.LastUpdateDateTime = Convert.ToDateTime(dr["LastUpdateDateTime"]);
                    codeRule.Remark = dr["Remark"].ToString().Trim();
                    return codeRule;
                }
                else
                    return null;
            }
        }
        /// <summary>
        /// 通过名称查询编码原则信息
        /// </summary>
        /// <param name="m_Code">编码原则代码</param>
        /// <returns></returns>
        public DataTable Select(string m_Code)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@Code", m_Code.Trim().ToUpper()));
                return da.GetDataTable("Select * From " + tableName + " Where Upper(Code)=@Code And IsDeleted=0", da.sqlParameters);
            }
        }
        /// <summary>
        /// 通过名称查询编码原则信息
        /// </summary>
        /// <param name="m_Code">编码原则代码</param>
        /// <param name="m_Code">编码原则对应类别</param>
        /// <returns></returns>
        public DataTable Select(string m_Code, string m_CodeRuleClass)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@Code", m_Code.Trim().ToUpper()));
                da.sqlParameters.Add(da.CreateSqlParameter("@CodeRuleClass", m_CodeRuleClass.Trim().ToUpper()));
                return da.GetDataTable("Select * From " + tableName + " Where Upper(Code)=@Code And Upper(CodeRuleClass)=@CodeRuleClass And IsDeleted=0", da.sqlParameters);
            }
        }
        /// <summary>
        /// 增加编码原则信息
        /// </summary>
        /// <param name="m_CodeRule">编码原则</param>
        /// <returns>返回增加结果</returns>
        public bool Add(Model.CodeRule.CodeRule m_CodeRule)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@Code", m_CodeRule.Code.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@CodeRuleClass", m_CodeRule.CodeRuleClass.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@BindPanelId", m_CodeRule.BindPanelId.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@Name", m_CodeRule.Name.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@Rule", m_CodeRule.Rule.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@RuleEN", m_CodeRule.RuleEN.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@Rev", m_CodeRule.Rev));
                da.sqlParameters.Add(da.CreateSqlParameter("@IsActive", m_CodeRule.IsActive));
                da.sqlParameters.Add(da.CreateSqlParameter("@CreateUserID", m_CodeRule.CreateUserID.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@LastUpdateUserID", m_CodeRule.LastUpdateUserID.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@Remark", m_CodeRule.Remark.Trim()));
                return da.ExecuteCommand("Insert Into " + tableName + "(Code,CodeRuleClass,BindPanelId,Name,[Rule],RuleEN,Rev,IsActive,CreateUserID,CreateDateTime,LastUpdateUserID,LastUpdateDateTime,Remark) Values(@Code,@CodeRuleClass,@BindPanelId,@Name,@Rule,@RuleEN,@Rev,@IsActive,@CreateUserID,GetDate(),@LastUpdateUserID,GetDate(),@Remark)", da.sqlParameters) != 0 ? true : false;
            }
        }
        /// <summary>
        /// 更新编码原则信息
        /// <param name="m_CodeRule">编码原则</param>
        /// </summary>
        /// <returns></returns>
        public bool Update(Model.CodeRule.CodeRule m_CodeRule)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@Id", m_CodeRule.Id));
                da.sqlParameters.Add(da.CreateSqlParameter("@Code", m_CodeRule.Code.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@CodeRuleClass", m_CodeRule.CodeRuleClass.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@BindPanelId", m_CodeRule.BindPanelId.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@Name", m_CodeRule.Name.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@Rule", m_CodeRule.Rule.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@RuleEN", m_CodeRule.RuleEN.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@Rev", m_CodeRule.Rev));
                da.sqlParameters.Add(da.CreateSqlParameter("@Schema", m_CodeRule.Schema.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@TableName", m_CodeRule.TableName.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@Field", m_CodeRule.Field.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@IsActive", m_CodeRule.IsActive));
                da.sqlParameters.Add(da.CreateSqlParameter("@LastUpdateUserID", m_CodeRule.LastUpdateUserID.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@Remark", m_CodeRule.Remark.Trim()));
                return da.ExecuteCommand("Update " + tableName + " Set Code=@Code,CodeRuleClass=@CodeRuleClass,BindPanelId=@BindPanelId,Name=@Name,[Rule]=@Rule,RuleEN=@RuleEN,Rev=@Rev,[Schema]=@Schema,TableName=@TableName,Field=@Field,IsActive=@IsActive,LastUpdateUserID=@LastUpdateUserID,LastUpdateDateTime=GetDate(),Remark=@Remark Where Id=@Id", da.sqlParameters) != 0 ? true : false;
            }
        }
    }
}
