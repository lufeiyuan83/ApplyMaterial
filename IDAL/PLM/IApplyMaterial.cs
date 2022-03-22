﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace IDAL.PLM
{
    public interface IApplyMaterial
    {
        /// <summary>
        /// 查询ApplyMaterial
        /// </summary>
        /// <returns></returns>
        DataTable Select();
        /// <summary>
        /// 查询ApplyMaterial
        /// </summary>
        /// <param name="m_Id">Id</param>
        /// <returns></returns>
        Model.PLM.ApplyMaterial Select(long m_Id);
        /// <summary>
        /// 通过物料编码查询物料信息
        /// </summary>
        /// <param name="m_MaterialId">MaterialId</param>
        /// <returns></returns>
        DataTable Select(string m_MaterialId);
        /// <summary>
        /// 通过物料编码查询物料信息
        /// </summary>
        /// <param name="m_MaterialId">MaterialId</param>
        /// <returns></returns>
        List<Model.PLM.ApplyMaterial> Select(List<string> m_MaterialId);
        /// <summary>
        /// 通过物料编码和申请人查询物料信息
        /// </summary>
        /// <param name="m_Bucode">TipTop资料库</param>
        /// <param name="m_MaterialId">MaterialId</param>
        /// <param name="m_CreateUserID">CreateUserID</param>
        /// <returns></returns>
        DataTable Select(string m_Bucode, string m_MaterialId, string m_CreateUserID);
        /// <summary>
        /// 通过物料编码、品名、规格、申请人查询物料信息
        /// </summary>
        /// <param name="m_Bucode">TipTop资料库</param>
        /// <param name="m_MaterialId">MaterialId</param>
        /// <param name="m_ProductionName">品名</param>
        /// <param name="m_Specification">规格</param>
        /// <param name="m_CreateUserID">申请人</param>
        /// <returns></returns>
        DataTable Select(string m_Bucode, string m_MaterialId, string m_ProductionName, string m_Specification, string m_CreateUserID);
        /// <summary>
        /// 获得该审批状态的的数据
        /// </summary>
        /// <param name="m_MaterialId">物料编码</param>
        /// <param name="m_Status">审批状态</param>
        /// <returns></returns>
        DataTable GetStatusData(string m_MaterialId, string m_Status);
        /// <summary>
        /// 增加物料信息
        /// </summary>
        /// <param name="m_ApplyMaterial">物料信息</param>
        /// <param name="m_TipTopParameter">抛送TipTop的参数</param>
        /// <param name="m_MaterialId">料号</param>
        /// <returns>返回增加结果</returns>
        bool Add(Model.PLM.ApplyMaterial m_ApplyMaterial, List<Model.PLM.TipTopParameter> m_TipTopParameter, out string m_MaterialId);
        /// <summary>
        /// 更新申请料号信息
        /// <param name="m_ApplyMaterial">ApplyMaterial</param>
        /// </summary>
        /// <returns></returns>
        bool Update(Model.PLM.ApplyMaterial m_ApplyMaterial);
        /// <summary>
        /// 更新申请料号信息
        /// <param name="m_ApplyMaterial">ApplyMaterial</param>
        /// </summary>
        /// <returns></returns>
        bool Update(List<Model.PLM.ApplyMaterial> m_ApplyMaterial);
    }
}
