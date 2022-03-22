using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.ApplyMaterial
{
    public class Material
    {
        public long Id
        { get; set; }
        public string MaterialId
        { get; set; }
        public string ProductionName
        { get; set; }
        public string Specification
        { get; set; }  
        public bool IsDeleted
        { get; set; }
        public string CreateUserID
        { get; set; }
        public DateTime CreateDateTime
        { get; set; }
        public string LastUpdateUserID
        { get; set; }
        public DateTime LastUpdateDateTime
        { get; set; }
        public string Remark
        { get; set; }
    }
}
