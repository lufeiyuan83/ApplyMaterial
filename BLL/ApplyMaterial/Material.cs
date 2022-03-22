using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BLL.ApplyMaterial
{
    public class Material
    {
        private readonly IDAL.ApplyMaterial.IMaterial dal = DALFactory.DALFactory.GetInstance().GetMaterial();
        /// <summary>
        /// 查询Material
        /// </summary>
        /// <returns></returns>
        public DataTable Select()
        {
            return dal.Select();
        }
        /// <summary>
        /// 查询Material
        /// </summary>
        /// <param name="m_Id">Id</param>
        /// <returns></returns>
        public Model.ApplyMaterial.Material Select(long m_Id)
        {
            return dal.Select(m_Id);
        }
        /// <summary>
        /// 新增料号信息
        /// <param name="m_Material">Material</param>
        /// </summary>
        /// <returns></returns>
        public bool Add(Model.ApplyMaterial.Material m_Material)
        {
            return dal.Add(m_Material);
        }
        /// <summary>
        /// 是否已经存在该物料信息
        /// </summary>
        /// <param name="m_MaterialId">物料编码</param>
        /// <returns></returns>
        public bool IsExist(string m_MaterialId)
        {
            return dal.IsExist(m_MaterialId);
        }
    }
}
