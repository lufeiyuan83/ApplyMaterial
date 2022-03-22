using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.ApplyMaterial
{
    public class Flow
    {
        public long Id
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
        public int RejectToStep
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
