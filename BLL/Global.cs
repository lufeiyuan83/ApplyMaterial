using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SQLHelp;

namespace BLL
{
    public class Global
    {
        /// <summary>
        /// 获得系统时间
        /// </summary>
        /// <returns>系统时间</returns>
        public static DateTime GetSystemTime()
        {
            using (DataAccess da = new DataAccess())
            {
                return da.GetSysTime();
            }
        }
        /// <summary>
        /// 获得该表结构信息
        /// </summary>
        /// <param name="m_Database">数据库</param>
        /// <param name="m_TableName">表名</param>
        /// <returns></returns>
        public static DataTable GetTableInfo(string m_Database, string m_TableName)
        {

            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@TableName", m_TableName.Trim()));
                return da.GetDataTable("select * from " + m_Database + ".information_schema.columns where TABLE_NAME=@TableName order by ORDINAL_POSITION", da.sqlParameters);
            }
        }
        /// <summary>
        /// 比较版本，=1：输入版本大于FIS版本；=0：相等；=-1:输入版本小于FIS版本
        /// </summary>
        /// <param name="m_InputRev">InputRev</param>
        /// <param name="m_FISRev">FISRev</param>
        /// <returns>返回比较结果</returns>
        public static int CompareRev(string m_InputRev, string m_FISRev)
        {
            return m_InputRev.CompareTo(m_FISRev);
        }
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="m_Exception">Exception</param>
        /// <param name="m_Type">Type</param>
        /// <param name="m_Description">Description</param>
        /// <param name="m_CreateUserID">CreateUserID</param>
        /// <param name="m_ComputerName">ComputerName</param>
        /// <param name="m_Remark">Remark</param>
        public static void WriteLog(Exception m_Exception, string m_CSFileName, string m_Type, string m_Description, string m_CreateUserID, string m_ComputerName, string m_Remark)
        {
            BLL.PLM.Log m_Log = new BLL.PLM.Log();
            Model.PLM.Log log = new Model.PLM.Log();
            log.CSFileName = m_CSFileName;
            log.Type = m_Type;
            if (m_Exception != null)
            {
                log.Source = m_Exception.Source == null ? "" : m_Exception.Source;
                log.Message = m_Exception.Message == null ? "" : m_Exception.Message;
                log.StackTrace = m_Exception.StackTrace == null ? "" : m_Exception.StackTrace;
            }
            log.UserDescription = m_Description == null ? "" : m_Description; 
            log.CreateDateTime = GetSystemTime();
            log.CreateUserID = m_CreateUserID;
            log.ComputerName = m_ComputerName;
            log.Remark = m_Remark;
            m_Log.Add(log);
        }
        /// <summary>
        /// 更新版本日志
        /// </summary>
        /// <param name="m_Description">Description</param>
        /// <param name="m_Rev">版本</param>
        /// <param name="m_ComputerName">ComputerName</param>
        /// <param name="m_UserName">操作人</param>
        public static void SaveSystemVersionLog(string m_Description, string m_Rev, string m_ComputerName, string m_UserName)
        {
            BLL.PLM.Log m_Log = new BLL.PLM.Log();
            Model.PLM.Log log = m_Log.Select(Util.LogType.SystemVersion.ToString(), m_ComputerName);
            if (log != null)
            {
                log.UserDescription = "SystemVersion:" + m_Rev;
                log.ComputerName = m_ComputerName;
            }
            else
            {
                log = new Model.PLM.Log();
                log.CSFileName = "";
                log.Type = Util.LogType.SystemVersion.ToString();
                log.Source = "";
                log.Message = "";
                log.StackTrace = "";
                log.UserDescription = "SystemVersion:" + m_Rev;
                log.CreateDateTime = GetSystemTime();
                log.CreateUserID = m_UserName;
                log.ComputerName = m_ComputerName;
                log.Remark = "";
            }
            m_Log.Save(log);
        }
    }
}
