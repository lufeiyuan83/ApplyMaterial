using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace IDAL.Auth
{
    public interface ISysMenu
    {
        /// <summary>
        /// 查询系统菜单
        /// </summary>
        /// <returns></returns>
        DataTable Select();
        /// <summary>
        /// 查询系统菜单
        /// </summary>
        /// <param name="m_SystemCode">系统代码</param>
        /// <returns></returns>
        DataTable Select(string m_SystemCode);
        /// <summary>
        /// 获得最大Id
        /// </summary>
        /// <returns></returns>
        int GetMaxId();
        /// <summary>
        /// 获得最大SortNo
        /// </summary>
        /// <param name="m_SystemCode">系统代码</param>
        /// <param name="m_ParentId"></param>
        /// <returns></returns>
        int GetMaxSortNo(string m_SystemCode, int m_ParentId);
        /// <summary>
        /// 查询该id的系统菜单
        /// <param name="m_Id"></param>
        /// </summary>
        /// <returns></returns>
        Model.Auth.SysMenu Select(long m_Id);
        /// <summary>
        /// 增加系统菜单
        /// </summary>
        /// <param name="m_SysMenu">SysMenu</param>
        /// <returns>返回增加结果</returns>
        bool Add(Model.Auth.SysMenu m_SysMenu);
        /// <summary>
        /// 更新系统菜单
        /// </summary>
        /// <param name="m_SysMenu">System</param>
        /// <returns>返回更新结果</returns>
        bool Update(Model.Auth.SysMenu m_SysMenu);
        /// <summary>
        /// 删除系统菜单
        /// </summary>
        /// <param name="m_Id">主键</param>
        /// <param name="m_DeleteUserID">删除人</param>
        /// <returns>返回删除结果</returns>
        bool Delete(long m_Id, string m_DeleteUserID);
        /// <summary>
        /// 是否已经存在该系统菜单信息
        /// </summary>
        /// <param name="m_SystemCode">系统代码</param>
        /// <param name="m_NodeCode">节点代码</param>
        /// <returns>返回结果</returns>
        bool IsExist(string m_SystemCode, string m_NodeCode);
        /// <summary>
        /// 是否已经存在该系统菜单信息
        /// </summary>
        /// <param name="m_SystemCode">系统代码</param>
        /// <param name="m_ParentId">父节点编号</param>
        /// <param name="m_MenuName">菜单名称</param>
        /// <returns>返回结果</returns>
        bool IsExist(string m_SystemCode, int m_ParentId, string m_MenuName);
    }
}
