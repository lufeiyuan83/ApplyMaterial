using SQLHelp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DAL.PLM
{
    public class UI : IDAL.PLM.IUI
    {
        private const string tableName = "PLM.dbo.UI";
        /// <summary>
        /// 查询UI
        /// </summary>
        /// <returns></returns>
        public DataTable Select()
        {
            using (DataAccess da = new DataAccess())
            {
                return da.GetDataTable("Select * From " + tableName+" Order by UIForm,SortNumber");
            }
        }
        /// <summary>
        /// 查询UI
        /// </summary>
        /// <param name="m_UIId">UIId</param>
        /// <returns></returns>
        public Model.PLM.UI Select(long m_UIId)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@UIId", m_UIId));
                DataRow dr = da.GetDataRow("Select * From " + tableName + " Where UIId=@UIId", da.sqlParameters);
                if (dr != null)
                {
                    Model.PLM.UI ui = new Model.PLM.UI();
                    ui.UIId = Convert.ToInt64(dr["UIId"]);
                    ui.UIForm = dr["UIForm"].ToString().Trim();
                    ui.Type = dr["Type"].ToString().Trim();
                    ui.ID = dr["ID"].ToString().Trim();
                    ui.Layout = dr["Layout"].ToString().Trim();
                    ui.Title = dr["Title"].ToString().Trim();
                    ui.ShowBorder = Convert.ToBoolean(dr["ShowBorder"].ToString().Trim());
                    ui.ShowHeader = Convert.ToBoolean(dr["ShowHeader"].ToString().Trim());
                    ui.Width = dr["Width"].ToString().Trim();
                    ui.BodyPadding = dr["BodyPadding"].ToString().Trim();
                    ui.EmptyText = dr["EmptyText"].ToString().Trim();
                    ui.Label = dr["Label"].ToString().Trim();
                    ui.LabelAlign = dr["LabelAlign"].ToString().Trim();
                    ui.DataValueField = dr["DataValueField"].ToString().Trim();
                    ui.DataTextField = dr["DataTextField"].ToString().Trim();
                    ui.Required = Convert.ToBoolean(dr["Required"].ToString().Trim());
                    ui.ShowRedStar = Convert.ToBoolean(dr["ShowRedStar"].ToString().Trim());
                    ui.AutoPostBack = Convert.ToBoolean(dr["AutoPostBack"].ToString().Trim());
                    ui.AutoSelectFirstItem = Convert.ToBoolean(dr["AutoSelectFirstItem"].ToString().Trim());
                    ui.Enabled = Convert.ToBoolean(dr["Enabled"].ToString().Trim());
                    ui.SortNumber = Convert.ToInt32(dr["SortNumber"].ToString().Trim());
                    ui.ChildId = dr["ChildId"].ToString().Trim();
                    return ui;
                }
                else
                    return null;
            }
        }
        /// <summary>
        /// 通过页面名称查询UI信息
        /// </summary>
        /// <param name="m_UIForm">页面名称</param>
        /// <returns></returns>
        public DataTable Select(string m_UIForm)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@UIForm", m_UIForm.Trim().ToUpper()));
                return da.GetDataTable("Select * From " + tableName + " Where Upper(UIForm)=@UIForm Order by UIForm,SortNumber", da.sqlParameters);
            }
        }
    }
}
