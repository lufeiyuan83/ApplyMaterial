using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Web.Services;

namespace CodeRuleService
{
    /// <summary>
    /// WSCodeRuleasmx 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class WSCodeRule : System.Web.Services.WebService
    {
        /// <summary>
        /// 将json转换为DataTable
        /// </summary>
        /// <param name="strJson">得到的json</param>
        /// <returns></returns>
        private DataTable JsonToDataTable(string strJson)
        {
            try
            { 
                //转换json格式
                strJson = strJson.Replace(",\"", "*\"").Replace("\":", "\"#").ToString();
                //取出表名  
                var rg = new System.Text.RegularExpressions.Regex(@"(?<={)[^:]+(?=:\[)", RegexOptions.IgnoreCase);
                string strName = rg.Match(strJson).Value;
                DataTable tb = null;
                //去除表名  
                strJson = strJson.Substring(strJson.IndexOf("[") + 1);
                strJson = strJson.Substring(0, strJson.IndexOf("]"));
                //获取数据  
                rg = new System.Text.RegularExpressions.Regex(@"(?<={)[^}]+(?=})");
                System.Text.RegularExpressions.MatchCollection mc = rg.Matches(strJson);
                for (int i = 0; i < mc.Count; i++)
                {
                    string strRow = mc[i].Value;
                    string[] strRows = strRow.Split('*');
                    //创建表  
                    if (tb == null)
                    {
                        tb = new DataTable(strName);
                        foreach (string str in strRows)
                        {
                            var dc = new DataColumn();
                            string[] strCell = str.Split('#');
                            if (strCell[0].Substring(0, 1) == "\"")
                            {
                                int a = strCell[0].Length;
                                dc.ColumnName = strCell[0].Substring(1, a - 2);
                            }
                            else
                            {
                                dc.ColumnName = strCell[0];
                            }
                            tb.Columns.Add(dc);
                        }
                        tb.AcceptChanges();
                    }
                    //增加内容  
                    DataRow dr = tb.NewRow();
                    for (int r = 0; r < strRows.Length; r++)
                    {
                        dr[r] = strRows[r].Split('#')[1].Trim().Replace("，", ",").Replace("：", ":").Replace("\"", "");
                    }
                    tb.Rows.Add(dr);
                    tb.AcceptChanges();
                }
                return tb;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 依据编码规则配置信息，获得相应的业务编码信息
        /// </summary>
        /// <param name="code">编码规则代码，例如：MaterialCodeRule</param>
        /// <param name="parameters">输入的Json字符串，需要与编码规则配置信息匹配，例如：[{"编码原则":"工具","大分类":"原材料","中分类":"钻石刀","次分类":"工具部门","流水码":"流水码"}]</param>
        /// <returns>
        /// [Success]:成功，返回编码
        /// [Error]:失败，返回错误信息
        /// </returns>
        [WebMethod]        
        public string GenerateCode(string code, string parameters)
        {
            using (DataAccess da = new DataAccess())
            {
                try
                {
                    string strResult = "";
                    da.BeginTransaction();
                    da.sqlParameters.Clear();
                    da.sqlParameters.Add(da.CreateSqlParameter("@Code", code.Trim().ToUpper()));
                    DataTable dtCodeRule = da.GetDataTable("Select Top 1 * From [PLM].[dbo].[CodeRule] Where IsDeleted=0 And IsActive=1 And Code=@Code", da.sqlParameters);
                    if (dtCodeRule != null && dtCodeRule.Rows.Count > 0)
                    {
                        #region 列表化中文编码规则需要替换的参数
                        strResult = dtCodeRule.Rows[0]["Rule"].ToString();
                        string[] arrRuleEN1 = strResult.Split('[');
                        List<string> lstRuleEN = new List<string>();
                        for (int i = 0; i < arrRuleEN1.Length; i++)
                        {
                            if (arrRuleEN1[i].Trim() != "")
                            {
                                string[] arrRuleEN2 = arrRuleEN1[i].Split(']');
                                for (int j = 0; j < arrRuleEN2.Length; j++)
                                {
                                    if (arrRuleEN2[j].Trim() != "" && strResult.Contains("[" + arrRuleEN2[j] + "]"))
                                        lstRuleEN.Add(arrRuleEN2[j]);
                                }
                            }
                        }
                        #endregion
                        da.sqlParameters.Clear();
                        da.sqlParameters.Add(da.CreateSqlParameter("@CodeRuleId", dtCodeRule.Rows[0]["Id"]));
                        DataTable dtCodeRuleParameter = da.GetDataTable("Select * From [PLM].[dbo].[CodeRuleParameter] Where IsDeleted=0 And CodeRuleId=@CodeRuleId", da.sqlParameters);
                        if (dtCodeRuleParameter != null && dtCodeRuleParameter.Rows.Count > 0)
                        {
                            #region 将输入的Json参数表格化，便于进行智能匹配
                            DataTable dtParameters;
                            try
                            {
                                dtParameters = JsonToDataTable(parameters);
                            }
                            catch(Exception ex)
                            {
                                return "[Error]Invalid json paramter." + ex.Message;
                            }
                            #endregion
                            for (int i=0;i< lstRuleEN.Count;i++)
                            {
                                DataRow[] drsCodeRuleParameter;
                                if(!dtParameters.Columns.Contains(lstRuleEN[i]) || dtParameters.Rows[0][lstRuleEN[i]].ToString()== lstRuleEN[i])
                                {
                                    drsCodeRuleParameter = dtCodeRuleParameter.Select("ParameterType='" + lstRuleEN[i] + "' And ParameterName='" + lstRuleEN[i] + "'");
                                    if (drsCodeRuleParameter.Length > 0)
                                    { 
                                        if (lstRuleEN[i]== "流水码")
                                        {
                                            #region 流水码
                                            drsCodeRuleParameter = dtCodeRuleParameter.Select("ParameterType='" + lstRuleEN[i] + "' And ParameterName='" + lstRuleEN[i] + "'");
                                            if(drsCodeRuleParameter.Length>0)
                                            { 
                                                int intStartSNIndex = strResult.IndexOf("[流水码]");
                                                da.sqlParameters.Clear();
                                                object objSN = da.GetScalar("Select Max(" + dtCodeRule.Rows[0]["Field"].ToString() + ") From " + dtCodeRule.Rows[0]["Schema"].ToString() +"."+ dtCodeRule.Rows[0]["TableName"].ToString() + " Where IsDeleted=0 And " + dtCodeRule.Rows[0]["Field"].ToString() + " Like '" + strResult.Substring(0, intStartSNIndex) + "%'", da.sqlParameters);
                                                string strSN = "";
                                                if (objSN.ToString() == "")
                                                {
                                                    strSN = Convert.ToInt32(drsCodeRuleParameter[0]["ParameterValue"].ToString()).ToString("0".PadLeft(drsCodeRuleParameter[0]["ParameterValue"].ToString().Length, '0'));
                                                }
                                                else
                                                {
                                                    strSN = (Convert.ToInt32(objSN.ToString().Substring(intStartSNIndex, drsCodeRuleParameter[0]["ParameterValue"].ToString().Length)) + 1).ToString("0".PadLeft(drsCodeRuleParameter[0]["ParameterValue"].ToString().Length, '0'));
                                                }
                                                strResult = strResult.Replace("[" + lstRuleEN[i] + "]", strSN);
                                            }
                                            #endregion
                                        }
                                        else if (lstRuleEN[i] == "4位年份" || lstRuleEN[i] == "2位年份")
                                            strResult = strResult.Replace("[" + lstRuleEN[i] + "]", DateTime.Now.Year.ToString(drsCodeRuleParameter[0]["ParameterValue"].ToString()));
                                        else if (lstRuleEN[i] == "2位月份" || lstRuleEN[i] == "1位月份")
                                            strResult = strResult.Replace("[" + lstRuleEN[i] + "]", DateTime.Now.Month.ToString(drsCodeRuleParameter[0]["ParameterValue"].ToString()));
                                        else if (lstRuleEN[i] == "2位天")
                                            strResult = strResult.Replace("[" + lstRuleEN[i] + "]", DateTime.Now.Day.ToString(drsCodeRuleParameter[0]["ParameterValue"].ToString()));
                                        else if (lstRuleEN[i] == "24进制小时" || lstRuleEN[i] == "12进制小时")
                                            strResult = strResult.Replace("[" + lstRuleEN[i] + "]", DateTime.Now.Hour.ToString(drsCodeRuleParameter[0]["ParameterValue"].ToString()));
                                        else if (lstRuleEN[i] == "2位分钟")
                                            strResult = strResult.Replace("[" + lstRuleEN[i] + "]", DateTime.Now.Minute.ToString(drsCodeRuleParameter[0]["ParameterValue"].ToString()));
                                        else if (lstRuleEN[i] == "2位秒")
                                            strResult = strResult.Replace("[" + lstRuleEN[i] + "]", DateTime.Now.Second.ToString(drsCodeRuleParameter[0]["ParameterValue"].ToString()));
                                    }
                                }
                                else
                                {
                                    #region 其他非内置编码规则
                                    drsCodeRuleParameter = dtCodeRuleParameter.Select("ParameterType='" + lstRuleEN[i] + "' And ParameterName='" + dtParameters.Rows[0][lstRuleEN[i]].ToString() + "'");
                                    strResult = strResult.Replace("["+ lstRuleEN[i] + "]", drsCodeRuleParameter[0]["ParameterValue"].ToString());
                                    #endregion
                                }
                            }
                            #region 检查是否所有参数均已经替换完毕，由此验证输入参数是否于配置的编码规则一致
                            bool blReplaceFailure = false;
                            for(int i=0;i< lstRuleEN.Count; i++)
                            {
                                if (strResult.Contains("[" + lstRuleEN[i] + "]"))
                                { 
                                    blReplaceFailure = true;
                                    break;
                                }
                            }
                            if(blReplaceFailure)
                                strResult = "[Error]("+ strResult+") can not replace all fields";
                            #endregion
                        }
                        else
                        {
                            da.RollbackTransaction();
                            return "[Error]Can not find the code's parameters";
                        }
                    }
                    else
                    {
                        da.RollbackTransaction();
                        return "[Error]Can not find the code("+ code + ")";
                    }

                    da.CommitTransaction();
                    return "[Success]" + strResult;
                }
                catch (Exception ex)
                {
                    da.RollbackTransaction();
                    return "[Error]" + ex.Message;
                }
            }
        }
    }
}
