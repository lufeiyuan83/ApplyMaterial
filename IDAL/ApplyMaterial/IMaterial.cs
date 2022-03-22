using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace IDAL.ApplyMaterial
{
    public interface IMaterial
    {
        /// <summary>
        /// 查询Material
        /// </summary>
        /// <returns></returns>
        DataTable Select();
        /// <summary>
        /// 查询Material
        /// </summary>
        /// <param name="m_Id">Id</param>
        /// <returns></returns>
        Model.ApplyMaterial.Material Select(long m_Id);
        /// <summary>
        /// 新增料号信息
        /// <param name="m_Material">Material</param>
        /// </summary>
        /// <returns></returns>
        bool Add(Model.ApplyMaterial.Material m_Material);
        /// <summary>
        /// 是否已经存在该物料信息
        /// </summary>
        /// <param name="m_MaterialId">物料编码</param>
        /// <returns></returns>
        bool IsExist(string m_MaterialId);
    }
}
