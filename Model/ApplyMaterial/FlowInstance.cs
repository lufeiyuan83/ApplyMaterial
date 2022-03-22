using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.ApplyMaterial
{
    public class FlowInstance
    {
        public long Id
        { get; set; }
        public string FormID
        { get; set; }
        public string FlowName
        { get; set; }
        public string FlowDescription
        { get; set; }
        public int Step
        { get; set; }
        public string FlowNode
        { get; set; }
        public string FlowNodeName
        { get; set; }
        public string OperatorID
        { get; set; }
        public string Approver
        { get; set; }
        public string ApproverName
        { get; set; }
        public string Result
        { get; set; }
        public int RejectToStep
        { get; set; }
        public DateTime ApproveDateTime
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
