using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.ApplyMaterial
{
    public class Mapping
    {
        public long MappingID
        { get; set; }
        public string MapGroup
        { get; set; }
        public string MapCode
        { get; set; }
        public string MapCodeDescription
        { get; set; }
        public string MappingName1
        { get; set; }
        public string MappingAlias1
        { get; set; }
        public string MappingValue1
        { get; set; }
        public string Delimiter1
        { get; set; }
        public string MappingType1
        { get; set; }
        public string MappingValue1Description
        { get; set; }
        public string MappingName2
        { get; set; }
        public string MappingValue2
        { get; set; }
        public string MappingAlias2
        { get; set; }
        public string Delimiter2
        { get; set; }
        public string MappingType2
        { get; set; }
        public string MappingValue2Description
        { get; set; }
        public string ExtendID
        { get; set; }
        public bool IsDefault
        { get; set; }
        public bool IsNotNull
        { get; set; }
        public int SortNumber
        { get; set; }
        public string Remark
        { get; set; }
    }
}