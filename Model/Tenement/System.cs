using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Tenement
{
    public class System
    {
        public long Id
        { get; set; }
        public string SystemCode
        { get; set; }
        public string SystemName
        { get; set; }
        public string SystemDescription
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
