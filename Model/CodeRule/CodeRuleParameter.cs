using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.CodeRule
{
    public class CodeRuleParameter
    {
        public long Id
        { get; set; }
        public long CodeRuleId
        { get; set; }
        public string ParameterCode
        { get; set; }
        public string ParameterName
        { get; set; }
        public string ParameterValue
        { get; set; }
        public string ParameterType
        { get; set; }
        public string ParameterDescription
        { get; set; }
        public long ParentId
        { get; set; }
        public bool IsDefault
        { get; set; }
        public int SortNo
        { get; set; }
        public int GroupNo
        { get; set; }
        public int Level
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
