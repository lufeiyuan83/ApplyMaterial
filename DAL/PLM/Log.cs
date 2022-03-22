using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DAL;
using SQLHelp;

namespace DAL.PLM
{
    public class Log : IDAL.PLM.ILog
    {
        private const string tableName = "PLM.dbo.Log";

        /// <summary>
        /// 查询Log
        /// </summary>
        /// <returns>Log</returns>
        public DataTable Select()
        {
            using (DataAccess da = new DataAccess())
            {
                return da.GetDataTable("Select * From " + tableName + " Order By CreateDateTime Desc");
            }
        }
        /// <summary>
        /// 查询单条Log
        /// </summary>
        /// <param name="m_ID">ID</param>
        /// <returns>Log</returns>
        public Model.PLM.Log Select(long m_ID)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@ID", m_ID));
                DataRow dr = da.GetDataRow("Select * From " + tableName + " Where ID=@ID Order By CreateDateTime Desc",da.sqlParameters);
                if (dr != null)
                {
                    Model.PLM.Log log = new Model.PLM.Log();
                    log.ID = Convert.ToInt64(dr["ID"]);
                    log.CSFileName = dr["CSFileName"].ToString().Trim();
                    log.Type = dr["Type"].ToString().Trim();
                    log.Source = dr["Source"].ToString().Trim();
                    log.Message = dr["Message"].ToString().Trim();
                    log.StackTrace = dr["StackTrace"].ToString().Trim();
                    log.UserDescription = dr["UserDescription"].ToString().Trim();
                    log.CreateDateTime = Convert.ToDateTime(dr["CreateDateTime"]);
                    log.CreateUserID = dr["CreateUserID"].ToString().Trim();
                    log.ComputerName = dr["ComputerName"].ToString().Trim();
                    log.Remark = dr["Remark"].ToString().Trim();
                    return log;
                }
                else
                    return null;
            }
        }
        /// <summary>
        /// 查询该类型的Log
        /// </summary>
        /// <param name="m_Type">Type</param>
        /// <param name="m_ComputerName">ComputerName</param>
        /// <returns>Log</returns>
        public Model.PLM.Log Select(string m_Type, string m_ComputerName)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@Type", m_Type.Trim().ToUpper()));
                da.sqlParameters.Add(da.CreateSqlParameter("@ComputerName", m_ComputerName.Trim().ToUpper()));
                DataRow dr = da.GetDataRow("Select Top 1 * From " + tableName + " Where Upper(Type)=@Type And Upper(ComputerName)=@ComputerName", da.sqlParameters);
                if (dr != null)
                {
                    Model.PLM.Log log = new Model.PLM.Log();
                    log.ID = Convert.ToInt64(dr["ID"]);
                    log.CSFileName = dr["CSFileName"].ToString().Trim();
                    log.Type = dr["Type"].ToString().Trim();
                    log.Source = dr["Source"].ToString().Trim();
                    log.Message = dr["Message"].ToString().Trim();
                    log.StackTrace = dr["StackTrace"].ToString().Trim();
                    log.UserDescription = dr["UserDescription"].ToString().Trim();
                    log.CreateDateTime = Convert.ToDateTime(dr["CreateDateTime"]);
                    log.CreateUserID = dr["CreateUserID"].ToString().Trim();
                    log.ComputerName = dr["ComputerName"].ToString().Trim();
                    log.Remark = dr["Remark"].ToString().Trim();
                    return log;
                }
                else
                    return null;
            }
        }
        /// <summary>
        /// 查询该类型的Log
        /// </summary>
        /// <param name="m_Type">Type</param>
        /// <returns>Log</returns>
        public DataTable Select(string m_Type)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@Type", m_Type.Trim().ToUpper()));
                return da.GetDataTable("Select * From " + tableName + " Where Upper(Type)=@Type Order By CreateDateTime Desc",da.sqlParameters);
            }
        }
        /// <summary>
        /// 查询该UserDescription的Log
        /// </summary>
        /// <param name="m_UserDescription">UserDescription</param>
        /// <returns></returns>
        public DataTable SelectByUserDescription(string m_UserDescription)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@UserDescription", m_UserDescription.Trim().ToUpper()));
                return da.GetDataTable("Select a.*,(Select Name From Employee where EmployeeID = a.CreateUserID) Name From " + tableName + " a Where Upper(UserDescription)=@UserDescription", da.sqlParameters);
            }
        }        
        /// <summary>
        /// 获得该角色的所有邮件
        /// </summary>
        /// <param name="m_RoleName">RoleName</param>
        /// <param name="m_Prefix">Prefix</param>
        /// <returns>Email</returns>
        private string GetEmail(string m_RoleName, string m_Prefix)
        {
            using (DataAccess da = new DataAccess())
            {
                string strEmail = "";
                string[] arrEmployeeID = null;
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@Type", Util.MappingType.FISAccess.ToString().ToUpper()));
                da.sqlParameters.Add(da.CreateSqlParameter("@RoleName", m_RoleName));
                da.sqlParameters.Add(da.CreateSqlParameter("@Prefix", m_Prefix.Trim().ToUpper()));
                object dtEmployeeID = da.GetDataTable("Select Distinct CASE WHEN CHARINDEX('|',SUBSTRING(MappingValue2,LEN(@Prefix)+1,LEN(MappingValue2)-LEN(@Prefix)),0)=0 THEN SUBSTRING(MappingValue2,LEN(@Prefix)+1,LEN(MappingValue2)-LEN(@Prefix)) else SUBSTRING(SUBSTRING(MappingValue2,LEN(@Prefix)+1,LEN(MappingValue2)-LEN(@Prefix)),0,CHARINDEX('|',SUBSTRING(MappingValue2,LEN(@Prefix)+1,LEN(MappingValue2)-LEN(@Prefix)),0)) end As EmployeeID From Mapping Where Upper([Type])=@Type AND Upper(MappingValue1)=@RoleName And Upper(MappingValue2) LIKE @Prefix+'%' And IsDeleted=0", da.sqlParameters);
                if (dtEmployeeID != null)
                {
                    DataTable dt = dtEmployeeID as DataTable;
                    arrEmployeeID = new string[dt.Rows.Count];
                    for (int i = 0; i < dt.Rows.Count; i++)
                        arrEmployeeID[i] = dt.Rows[i]["EmployeeID"].ToString().Trim();
                    if (arrEmployeeID != null && arrEmployeeID.Length > 0)
                    {
                        for (int j = 0; j < arrEmployeeID.Length; j++)
                        {
                            da.sqlParameters.Clear();
                            da.sqlParameters.Add(da.CreateSqlParameter("@EmployeeID", arrEmployeeID[j].Trim().ToUpper()));
                            object email = da.GetScalar("Select Email From Employee Where Upper(EmployeeID)=@EmployeeID And IsPost=1 And IsDeleted=0", da.sqlParameters);
                            if (email != null)
                            {
                                if (!string.IsNullOrEmpty(email.ToString().Trim()))
                                    strEmail += email.ToString().Trim() + ";";
                            }
                        }
                    }
                }
                return strEmail.Trim().Length > 1 ? strEmail.Substring(0, strEmail.Length - 1) : "";
            }
        }
        /// <summary>
        /// 增加Log
        /// </summary>
        /// <param name="m_Log">Log</param>
        /// <returns>返回增加结果</returns>
        public bool Add(Model.PLM.Log m_Log)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@CSFileName", m_Log.CSFileName.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@Type", m_Log.Type.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@Source", m_Log.Source.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@Message", m_Log.Message.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@StackTrace",m_Log.StackTrace==null?"": m_Log.StackTrace.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@UserDescription", m_Log.UserDescription.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@CreateDateTime", m_Log.CreateDateTime));
                da.sqlParameters.Add(da.CreateSqlParameter("@CreateUserID", m_Log.CreateUserID.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@ComputerName", m_Log.ComputerName.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@Remark", m_Log.Remark.Trim()));
                return da.ExecuteCommand("Insert Into " + tableName + "(CSFileName,Type,Source,Message,StackTrace,UserDescription,CreateDateTime,CreateUserID,ComputerName,Remark) Values(@CSFileName,@Type,@Source,@Message,@StackTrace,@UserDescription,@CreateDateTime,@CreateUserID,@ComputerName,@Remark)", da.sqlParameters) != 0 ? true : false;
            }
        }
        /// <summary>
        /// 保存Log
        /// </summary>
        /// <param name="m_Log">Log</param>
        /// <returns>返回保存结果</returns>
        public bool Save(Model.PLM.Log m_Log)
        {
            using (DataAccess da = new DataAccess())
            {
                if (m_Log != null && m_Log.ID != 0)
                {
                    da.sqlParameters.Clear();
                    da.sqlParameters.Add(da.CreateSqlParameter("@ID", m_Log.ID));
                    da.sqlParameters.Add(da.CreateSqlParameter("@CSFileName", m_Log.CSFileName.Trim()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@Type", m_Log.Type.Trim()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@Source", m_Log.Source.Trim()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@Message", m_Log.Message.Trim()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@StackTrace", m_Log.StackTrace == null ? "" : m_Log.StackTrace.Trim()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@UserDescription", m_Log.UserDescription.Trim()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@ComputerName", m_Log.ComputerName.Trim()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@Remark", m_Log.Remark.Trim()));
                    return da.ExecuteCommand("Update " + tableName + " Set CSFileName=@CSFileName,Type=@Type,Source=@Source,Message=@Message,StackTrace=@StackTrace,UserDescription=@UserDescription,ComputerName=@ComputerName,Remark=@Remark Where ID=@ID", da.sqlParameters) != 0 ? true : false;
                }
                else
                {
                    da.sqlParameters.Clear();
                    da.sqlParameters.Add(da.CreateSqlParameter("@CSFileName", m_Log.CSFileName.Trim()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@Type", m_Log.Type.Trim()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@Source", m_Log.Source.Trim()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@Message", m_Log.Message.Trim()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@StackTrace", m_Log.StackTrace == null ? "" : m_Log.StackTrace.Trim()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@UserDescription", m_Log.UserDescription.Trim()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@CreateDateTime", m_Log.CreateDateTime));
                    da.sqlParameters.Add(da.CreateSqlParameter("@CreateUserID", m_Log.CreateUserID.Trim()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@ComputerName", m_Log.ComputerName.Trim()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@Remark", m_Log.Remark.Trim()));
                    return da.ExecuteCommand("Insert Into " + tableName + "(CSFileName,Type,Source,Message,StackTrace,UserDescription,CreateDateTime,CreateUserID,ComputerName,Remark) Values(@CSFileName,@Type,@Source,@Message,@StackTrace,@UserDescription,@CreateDateTime,@CreateUserID,@ComputerName,@Remark)", da.sqlParameters) != 0 ? true : false;
                }
            }
        }
        /// <summary>
        /// 获得计算机名
        /// </summary>
        /// <param name="m_Type">Type</param>
        /// <returns>计算机名</returns>
        public List<string> GetComputerName(string m_Type)
        {
            using (DataAccess da = new DataAccess())
            {
                List<string> lstComputerName = new List<string>();
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@Type", m_Type.Trim().ToUpper()));
                DataTable dtComputerName = da.GetDataTable("Select Distinct ComputerName From " + tableName + " Where Upper([Type])=@Type", da.sqlParameters);
                if (dtComputerName != null && dtComputerName.Rows.Count > 0)
                {
                    for(int i=0;i<dtComputerName.Rows.Count;i++)
                        lstComputerName.Add(dtComputerName.Rows[i]["ComputerName"].ToString().ToUpper());
                }
                return lstComputerName;                    
            }
        }
    }
}