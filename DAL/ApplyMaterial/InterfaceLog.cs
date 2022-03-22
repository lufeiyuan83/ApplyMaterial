using SQLHelp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DAL.ApplyMaterial
{
    public class InterfaceLog : IDAL.ApplyMaterial.IInterfaceLog
    {
        private const string tableName = "PLM.dbo.InterfaceLog";
        /// <summary>
        /// 查询InterfaceLog
        /// </summary>
        /// <returns></returns>
        public DataTable Select()
        {
            using (DataAccess da = new DataAccess())
            {
                return da.GetDataTable("Select * From " + tableName);
            }
        }
        /// <summary>
        /// 查询InterfaceLog
        /// </summary>
        /// <param name="m_Id">Id</param>
        /// <returns></returns>
        public Model.ApplyMaterial.InterfaceLog Select(long m_Id)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@Id", m_Id));
                DataRow dr = da.GetDataRow("Select * From " + tableName + " Where Id=@Id", da.sqlParameters);
                if (dr != null)
                {
                    Model.ApplyMaterial.InterfaceLog interfaceLog = new Model.ApplyMaterial.InterfaceLog();
                    interfaceLog.Id = Convert.ToInt64(dr["Id"]);
                    interfaceLog.LogType = dr["LogType"].ToString().Trim();
                    interfaceLog.BuCode = dr["BuCode"].ToString().Trim();
                    interfaceLog.Message = dr["Message"].ToString().Trim();
                    interfaceLog.Status = dr["Status"].ToString().Trim();
                    interfaceLog.MaterialId = dr["MaterialId"].ToString().Trim();
                    interfaceLog.PostParameter = dr["PostParameter"].ToString().Trim();
                    interfaceLog.IsDeleted = Convert.ToBoolean(dr["IsDeleted"].ToString().Trim());
                    interfaceLog.CreateDateTime = Convert.ToDateTime(dr["CreateDateTime"]);
                    interfaceLog.CreateUserID = dr["CreateUserID"].ToString().Trim();
                    interfaceLog.LastUpdateUserID = dr["LastUpdateUserID"].ToString().Trim();
                    interfaceLog.LastUpdateDateTime = Convert.ToDateTime(dr["LastUpdateDateTime"]);
                    interfaceLog.Remark = dr["Remark"].ToString().Trim();
                    return interfaceLog;
                }
                else
                    return null;
            }
        }
        /// <summary>
        /// 新增接口日志信息
        /// <param name="m_InterfaceLog">InterfaceLog</param>
        /// </summary>
        /// <returns></returns>
        public bool Add(Model.ApplyMaterial.InterfaceLog m_InterfaceLog)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@LogType", m_InterfaceLog.LogType.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@BuCode", m_InterfaceLog.BuCode.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@Message", m_InterfaceLog.Message.Trim())); 
                da.sqlParameters.Add(da.CreateSqlParameter("@Status", m_InterfaceLog.Status.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@MaterialId", m_InterfaceLog.MaterialId.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@PostParameter", m_InterfaceLog.PostParameter.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@CreateUserID", m_InterfaceLog.CreateUserID.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@LastUpdateUserID", m_InterfaceLog.LastUpdateUserID.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@Remark", m_InterfaceLog.Remark.Trim()));
                return da.ExecuteCommand("Insert Into " + tableName + "(LogType,BuCode,Message,Status,MaterialId,PostParameter,CreateDateTime,CreateUserID,LastUpdateUserID,LastUpdateDateTime,Remark) Values(@LogType,@BuCode,@Message,@Status,@MaterialId,@PostParameter,GetDate(),@CreateUserID,@LastUpdateUserID,GetDate(),@Remark)", da.sqlParameters) != 0;
            }
        }
    }
}
