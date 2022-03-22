using SQLHelp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DAL.Auth
{
    public class SysMenu : IDAL.Auth.ISysMenu
    {
        private const string tableName = "Auth.dbo.SysMenu";
        /// <summary>
        /// 查询系统菜单
        /// </summary>
        /// <returns></returns>
        public DataTable Select()
        {
            using (DataAccess da = new DataAccess())
            {
                return da.GetDataTable("Select * From " + tableName + " Order By SortNo");
            }
        }
        /// <summary>
        /// 查询系统菜单
        /// </summary>
        /// <param name="m_SystemCode">系统代码</param>
        /// <returns></returns>
        public DataTable Select(string m_SystemCode)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@SystemCode", m_SystemCode.Trim().ToUpper()));
                return da.GetDataTable("Select * From " + tableName + " Where Upper(SystemCode)=@SystemCode Order By SortNo", da.sqlParameters);
            }
        }
        /// <summary>
        /// 获得最大Id
        /// </summary>
        /// <returns></returns>
        public int GetMaxId()
        {
            using (DataAccess da = new DataAccess())
            {
                object obj = da.GetScalar("Select Max(NodeId) From " + tableName);
                return (obj == null || obj.ToString() == "" ? 0 : Convert.ToInt32(obj));
            }
        }
        /// <summary>
        /// 获得最大SortNo
        /// </summary>
        /// <param name="m_SystemCode">系统代码</param>
        /// <param name="m_ParentId"></param>
        /// <returns></returns>
        public int GetMaxSortNo(string m_SystemCode, int m_ParentId)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                if (m_ParentId != 0)
                {
                    da.sqlParameters.Add(da.CreateSqlParameter("@SystemCode", m_SystemCode.Trim().ToUpper()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@ParentId", m_ParentId));
                    object obj = da.GetScalar("Select Max(SortNo) From " + tableName + " Where Upper(SystemCode)=@SystemCode And ParentId=@ParentId And id <> ParentId", da.sqlParameters);
                    return (obj == null || obj.ToString() == "" ? 0 : Convert.ToInt32(obj));
                }
                else
                {
                    da.sqlParameters.Add(da.CreateSqlParameter("@SystemCode", m_SystemCode.Trim().ToUpper()));
                    object obj = da.GetScalar("Select Max(SortNo) From " + tableName + " Where Upper(SystemCode)=@SystemCode", da.sqlParameters);
                    return (obj == null || obj.ToString() == "" ? 0 : Convert.ToInt32(obj));
                }
            }
        }
        /// <summary>
        /// 查询该Id的系统菜单
        /// <param name="m_Id"></param>
        /// </summary>
        /// <returns></returns>
        public Model.Auth.SysMenu Select(long m_Id)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@Id", m_Id));
                DataRow dr = da.GetDataRow("Select * From " + tableName + " Where Id=@Id", da.sqlParameters);
                if (dr != null)
                {
                    Model.Auth.SysMenu sysMenu = new Model.Auth.SysMenu();
                    sysMenu.Id = Convert.ToInt32(dr["Id"]);
                    sysMenu.SystemCode = dr["SystemCode"].ToString().Trim();
                    sysMenu.NodeCode = dr["NodeCode"].ToString().Trim();
                    sysMenu.MenuName = dr["MenuName"].ToString().Trim();
                    sysMenu.MenuNameEn = dr["MenuNameEn"].ToString().Trim();
                    sysMenu.MenuNameTh = dr["MenuNameTh"].ToString().Trim();
                    if (dr["ParentId"] != null && dr["ParentId"].ToString() != "")
                        sysMenu.ParentId = int.Parse(dr["ParentId"].ToString());
                    sysMenu.NavigateUrl = dr["NavigateUrl"].ToString().Trim();
                    sysMenu.Icon = dr["Icon"].ToString().Trim();
                    sysMenu.Expanded = Convert.ToBoolean(dr["Expanded"]);
                    sysMenu.SortNo = Convert.ToInt32(dr["SortNo"]);
                    return sysMenu;
                }
                else
                    return null;
            }
        }
        /// <summary>
        /// 增加系统菜单
        /// </summary>
        /// <param name="m_SysMenu">SysMenu</param>
        /// <returns>返回增加结果</returns>
        public bool Add(Model.Auth.SysMenu m_SysMenu)
        {
            using (DataAccess da = new DataAccess())
            {
                try
                {
                    da.BeginTransaction();
                    da.sqlParameters.Clear();
                    da.sqlParameters.Add(da.CreateSqlParameter("@SystemCode", m_SysMenu.SystemCode.Trim()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@NodeCode", m_SysMenu.NodeCode.Trim()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@MenuName", m_SysMenu.MenuName.Trim()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@MenuNameEn", m_SysMenu.MenuNameEn.Trim()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@MenuNameTh", m_SysMenu.MenuNameTh.Trim()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@NavigateUrl", m_SysMenu.NavigateUrl.Trim()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@Icon", m_SysMenu.Icon.Trim()));
                    da.sqlParameters.Add(da.CreateSqlParameter("@Expanded", m_SysMenu.Expanded));
                    da.sqlParameters.Add(da.CreateSqlParameter("@SortNo", m_SysMenu.SortNo));
                    if (m_SysMenu.ParentId !=0)
                    {
                        da.sqlParameters.Add(da.CreateSqlParameter("@ParentId", m_SysMenu.ParentId));
                        da.ExecuteCommand("Insert Into " + tableName + "(SystemCode,NodeCode,MenuName,MenuNameEn,MenuNameTh,ParentId,NavigateUrl,Icon,Expanded,SortNo) Values(@SystemCode,@NodeCode,@MenuName,@MenuNameEn,@MenuNameTh,@ParentId,@NavigateUrl,@Icon,@Expanded,@SortNo)", da.sqlParameters);
                    }
                    else
                    {
                        da.ExecuteCommand("Insert Into " + tableName + "(SystemCode,NodeCode,MenuName,MenuNameEn,MenuNameTh,ParentId,NavigateUrl,Icon,Expanded,SortNo) Values(@SystemCode,@NodeCode,@MenuName,@MenuNameEn,@MenuNameTh,null,@NavigateUrl,@Icon,@Expanded,@SortNo)", da.sqlParameters);
                        da.ExecuteCommand("Update " + tableName + " Set ParentId=Id where ParentId is null");
                    }
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
        /// 更新系统菜单
        /// </summary>
        /// <param name="m_SysMenu">System</param>
        /// <returns>返回更新结果</returns>
        public bool Update(Model.Auth.SysMenu m_SysMenu)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@Id", m_SysMenu.Id));
                da.sqlParameters.Add(da.CreateSqlParameter("@SystemCode", m_SysMenu.SystemCode.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@NodeCode", m_SysMenu.NodeCode.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@MenuName", m_SysMenu.MenuName.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@MenuNameEn", m_SysMenu.MenuNameEn.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@MenuNameTh", m_SysMenu.MenuNameTh.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@NavigateUrl", m_SysMenu.NavigateUrl.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@Icon", m_SysMenu.Icon.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@Expanded", m_SysMenu.Expanded));
                da.sqlParameters.Add(da.CreateSqlParameter("@SortNo", m_SysMenu.SortNo));
                if (m_SysMenu.ParentId != 0)
                {
                    da.sqlParameters.Add(da.CreateSqlParameter("@ParentId", m_SysMenu.ParentId));
                    return da.ExecuteCommand("Update " + tableName + " Set SystemCode=@SystemCode,NodeCode=@NodeCode,MenuName=@MenuName,MenuNameEn=@MenuNameEn,MenuNameTh=@MenuNameTh,ParentId=@ParentId,NavigateUrl=@NavigateUrl,Icon=@Icon,Expanded=@Expanded,SortNo=@SortNo Where Id=@Id", da.sqlParameters) != 0 ? true : false;
                }
                else
                    return da.ExecuteCommand("Update " + tableName + " Set SystemCode=@SystemCode,NodeCode=@NodeCode,MenuName=@MenuName,MenuNameEn=@MenuNameEn,MenuNameTh=@MenuNameTh,NavigateUrl=@NavigateUrl,Icon=@Icon,Expanded=@Expanded,SortNo=@SortNo Where Id=@Id", da.sqlParameters) != 0 ? true : false;
            }
        }
        /// <summary>
        /// 删除系统菜单
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
                return da.ExecuteCommand("Delete From " + tableName + " Where Id=@Id", da.sqlParameters) != 0 ? true : false;
            }
        }
        /// <summary>
        /// 是否已经存在该系统菜单信息
        /// </summary>
        /// <param name="m_SystemCode">系统代码</param>
        /// <param name="m_NodeCode">节点代码</param>
        /// <returns>返回结果</returns>
        public bool IsExist(string m_SystemCode, string m_NodeCode)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@SystemCode", m_SystemCode.Trim().ToUpper()));
                da.sqlParameters.Add(da.CreateSqlParameter("@NodeCode", m_NodeCode.Trim().ToUpper()));
                object isExist = da.GetScalar("Select Count(1) From " + tableName + " Where Upper(SystemCode)=@SystemCode And Upper(NodeCode)=@NodeCode", da.sqlParameters);
                if (isExist != null)
                    return Convert.ToInt32(isExist) > 0;
                else
                {
                    Exception ex = new Exception("Not find SystemCode=" + m_SystemCode + ",NodeCode=" + m_NodeCode + " information");
                    ex.Source = "Class name:" + tableName + ",Error method:public bool IsExist(m_SystemCode=" + m_SystemCode + ",m_NodeCode=" + m_NodeCode + ")";
                    throw ex;
                }
            }
        }
        /// <summary>
        /// 是否已经存在该系统菜单信息
        /// </summary>
        /// <param name="m_SystemCode">系统代码</param>
        /// <param name="m_ParentId">父节点编号</param>
        /// <param name="m_MenuName">菜单名称</param>
        /// <returns>返回结果</returns>
        public bool IsExist(string m_SystemCode, int m_ParentId, string m_MenuName)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@SystemCode", m_SystemCode.Trim().ToUpper()));
                da.sqlParameters.Add(da.CreateSqlParameter("@ParentId", m_ParentId));
                da.sqlParameters.Add(da.CreateSqlParameter("@MenuName", m_MenuName.Trim().ToUpper()));
                object isExist = da.GetScalar("Select Count(1) From " + tableName + " Where Upper(SystemCode)=@SystemCode And ParentId=@ParentId And Upper(MenuName)=@MenuName", da.sqlParameters);
                if (isExist != null)
                    return Convert.ToInt32(isExist) > 0;
                else
                {
                    Exception ex = new Exception("Not find SystemCode=" + m_SystemCode + ",ParentId=" + m_ParentId.ToString() + ",MenuName=" + m_MenuName + " information");
                    ex.Source = "Class name:" + tableName + ",Error method:public bool IsExist(m_SystemCode=" + m_SystemCode + ",m_ParentId=" + m_ParentId.ToString() + ",m_MenuName=" + m_MenuName + ")";
                    throw ex;
                }
            }
        }
    }
}
