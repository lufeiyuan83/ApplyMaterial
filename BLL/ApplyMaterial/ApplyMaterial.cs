﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BLL.ApplyMaterial
{
    public class ApplyMaterial
    {
        private readonly IDAL.ApplyMaterial.IApplyMaterial dal = DALFactory.DALFactory.GetInstance().GetApplyMaterial();
        /// <summary>
        /// 查询ApplyMaterial
        /// </summary>
        /// <returns></returns>
        public DataTable Select()
        {
            return dal.Select();
        }
        /// <summary>
        /// 查询ApplyMaterial
        /// </summary>
        /// <param name="m_Id">Id</param>
        /// <returns></returns>
        public Model.ApplyMaterial.ApplyMaterial Select(long m_Id)
        {
            return dal.Select(m_Id);
        }
        /// <summary>
        /// 通过物料编码查询物料信息
        /// </summary>
        /// <param name="m_MaterialId">MaterialId</param>
        /// <returns></returns>
        public DataTable Select(string m_MaterialId)
        {
            return dal.Select(m_MaterialId);
        }
        /// <summary>
        /// 通过物料编码查询物料信息
        /// </summary>
        /// <param name="m_MaterialId">MaterialId</param>
        /// <returns></returns>
        public List<Model.ApplyMaterial.ApplyMaterial> Select(List<string> m_MaterialId)
        {
            return dal.Select(m_MaterialId);
        }
        /// <summary>
        /// 通过物料编码和申请人查询物料信息
        /// </summary>
        /// <param name="m_Bucode">TipTop资料库</param>
        /// <param name="m_MaterialId">MaterialId</param>
        /// <param name="m_CreateUserID">CreateUserID</param>
        /// <returns></returns>
        public DataTable Select(string m_Bucode, string m_MaterialId, string m_CreateUserID)
        {
            return dal.Select(m_Bucode,m_MaterialId, m_CreateUserID);
        }
        /// <summary>
        /// 获得该审批状态的的数据
        /// </summary>
        /// <param name="m_MaterialId">物料编码</param>
        /// <param name="m_Status">审批状态</param>
        /// <returns></returns>
        public DataTable GetStatusData(string m_MaterialId, string m_Status)
        {
            return dal.GetStatusData(m_MaterialId, m_Status);
        }
        /// <summary>
        /// 增加物料信息
        /// </summary>
        /// <param name="m_ApplyMaterial">物料信息</param>
        /// <param name="m_TipTopParameter">抛送TipTop的参数</param>
        /// <param name="m_MaterialId">料号</param>
        /// <returns>返回增加结果</returns>
        public bool Add(Model.ApplyMaterial.ApplyMaterial m_ApplyMaterial, List<Model.ApplyMaterial.TipTopParameter> m_TipTopParameter, out string m_MaterialId)
        {
            return dal.Add(m_ApplyMaterial,m_TipTopParameter,out m_MaterialId);
        }
        /// <summary>
        /// 更新申请料号信息
        /// <param name="m_ApplyMaterial">ApplyMaterial</param>
        /// </summary>
        /// <returns></returns>
        public bool Update(Model.ApplyMaterial.ApplyMaterial m_ApplyMaterial)
        {
            return dal.Update(m_ApplyMaterial);
        }
        /// <summary>
        /// 更新申请料号信息
        /// <param name="m_ApplyMaterial">ApplyMaterial</param>
        /// </summary>
        /// <returns></returns>
        public bool Update(List<Model.ApplyMaterial.ApplyMaterial> m_ApplyMaterial)
        {
            return dal.Update(m_ApplyMaterial);
        }
    }
}
