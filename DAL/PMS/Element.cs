using SQLHelp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DAL.PMS
{
    public class Element : IDAL.PMS.IElement
    {
        private const string tableName = "PMS.dbo.Element";
        /// <summary>
        /// 查询Element
        /// </summary>
        /// <returns>Element</returns>
        public DataTable Select()
        {
            using (DataAccess da = new DataAccess())
            {
                return da.GetDataTable("Select * From " + tableName + " Order By Id");
            }
        }
        /// <summary>
        /// 查询单条Element
        /// </summary>
        /// <param name="m_Id">主键</param>
        /// <returns></returns>
        public Model.PMS.Element Select(long m_Id)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@Id", m_Id));
                DataRow dr = da.GetDataRow("Select * From " + tableName + " Where Id=@Id", da.sqlParameters);
                if (dr != null)
                {
                    Model.PMS.Element element = new Model.PMS.Element();
                    element.Id = Convert.ToInt64(dr["Id"]);
                    element.SysMenuId = Convert.ToInt64(dr["SysMenuId"]);                    
                    element.Code = dr["Code"].ToString().Trim();
                    element.Name = dr["Name"].ToString().Trim();
                    return element;
                }
                else
                    return null;
            }
        }
        /// <summary>
        /// 查询该系统菜单下的Element
        /// </summary>
        /// <param name="m_SysMenuId">系统菜单主键</param>
        /// <returns></returns>
        public DataTable SearchBySysMenuId(long m_SysMenuId)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@SysMenuId", m_SysMenuId));
                return da.GetDataTable("Select * From " + tableName + " Where SysMenuId=@SysMenuId", da.sqlParameters);
            }
        }
        /// <summary>
        /// 增加Element
        /// </summary>
        /// <param name="m_Element">Element</param>
        /// <returns>返回增加结果</returns>
        public bool Add(Model.PMS.Element m_Element)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@SysMenuId", m_Element.SysMenuId));
                da.sqlParameters.Add(da.CreateSqlParameter("@Code", m_Element.Code.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@Name", m_Element.Name.Trim()));
                return da.ExecuteCommand("Insert Into " + tableName + "(SysMenuId,Code,Name) Values(@SysMenuId,@Code,@Name)", da.sqlParameters) != 0 ? true : false;
            }
        }
        /// <summary>
        /// 更新Element
        /// </summary>
        /// <param name="m_Element">Element</param>
        /// <returns>返回更新结果</returns>
        public bool Update(Model.PMS.Element m_Element)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@Id", m_Element.Id));
                da.sqlParameters.Add(da.CreateSqlParameter("@SysMenuId", m_Element.SysMenuId));
                da.sqlParameters.Add(da.CreateSqlParameter("@Code", m_Element.Code.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@Name", m_Element.Name.Trim()));
                return da.ExecuteCommand("Update " + tableName + " Set SysMenuId=@SysMenuId,Code=@Code,Name=@Name Where Id=@Id", da.sqlParameters) != 0 ? true : false;
            }
        }
        /// <summary>
        /// 删除Element
        /// </summary>
        /// <param name="m_Id">主键</param>
        /// <returns>返回删除结果</returns>
        public bool Delete(long m_Id)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@Id", m_Id));
                return da.ExecuteCommand("Delete From " + tableName + " Where Id=@Id", da.sqlParameters) != 0 ? true : false;
            }
        }
        /// <summary>
        /// 是否已经存在该Element
        /// </summary>
        /// <param name="m_SysMenuId">系统菜单主键</param>
        /// <param name="m_Code">角色代码</param>
        /// <returns>返回结果</returns>
        public bool IsExist(long m_SysMenuId,string m_Code)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@Code", m_SysMenuId));
                da.sqlParameters.Add(da.CreateSqlParameter("@Code", m_Code.Trim().ToUpper()));
                object isExist = da.GetScalar("Select Count(1) From " + tableName + " Where SysMenuId=@SysMenuId And Upper(Code)=@Code", da.sqlParameters);
                if (isExist != null)
                    return Convert.ToInt32(isExist) > 0;
                else
                {
                    Exception ex = new Exception("Not find SysMenuId=" + m_SysMenuId + ",Code=" + m_Code + " information");
                    ex.Source = "Class name:" + tableName + ",Error method:public bool IsExist(m_SysMenuId=" + m_SysMenuId + ",m_Code=" + m_Code + ")";
                    throw ex;
                }
            }
        }
    }
}
