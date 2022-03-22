using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.HRM
{
    public class Organization
    {
        public long Id
        { get; set; }
        public string OrgCode
        { get; set; }
        public string OrgName
        { get; set; }
        public string OrgShortName
        { get; set; }
        public string OrgDescription
        { get; set; }
        public long OrgHierarchy
        { get; set; }
        public string Affiliation
        { get; set; }
        public string OrgType
        { get; set; }
        public bool EnableSelect
        { get; set; }
        public bool DefaultValue
        { get; set; }
        public string Phone
        { get; set; }
        public string MobilePhone
        { get; set; }
        public string Email
        { get; set; }
        public string Fax
        { get; set; }
        public string Address
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
