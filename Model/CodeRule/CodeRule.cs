using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.CodeRule
{
    public class CodeRule
    {
        public long Id
        { get; set; }
        public string Code
        { get; set; }        
        public string CodeRuleClass
        { get; set; }
        public string BindPanelId
        { get; set; }        
        public string Name
        { get; set; }
        public string Rule
        { get; set; }
        public string RuleEN
        { get; set; }
        public int Rev
        { get; set; }
        public string Schema
        { get; set; }
        public string TableName
        { get; set; }
        public string Field
        { get; set; }
        public bool IsActive
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
