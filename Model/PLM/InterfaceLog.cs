using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.PLM
{
    public class InterfaceLog
    {
        public long Id
        { get; set; }
        public string LogType
        { get; set; }
        public string BuCode
        { get; set; }
        public string Message
        { get; set; }
        public string Status
        { get; set; }
        public string MaterialId
        { get; set; }
        public string PostParameter
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
