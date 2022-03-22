using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.PLM
{
    public class WorkList
    {
        public long Id
        { get; set; }
        public string TableName
        { get; set; }
        public string FormId
        { get; set; }
        public string SystemCode
        { get; set; }
        public string Title
        { get; set; }
        public string URL
        { get; set; }
        public string FlowName
        { get; set; }
        public int FlowGroup
        { get; set; }        
        public string Approver
        { get; set; }
        public bool Status
        { get; set; }
        public string CreateUserID
        { get; set; }
        public DateTime CreateDateTime
        { get; set; }
    }
}
