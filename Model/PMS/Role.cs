using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.PMS
{
    public class Role
    {
        public long Id
        { get; set; }
        public string RoleCode
        { get; set; }
        public string RoleName
        { get; set; }
        public string RoleDescription
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
