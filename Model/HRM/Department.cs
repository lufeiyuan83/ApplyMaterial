using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.HRM
{
    public class Department
    {
        public long Id
        { get; set; }
        public string ApplicationOrg
        { get; set; }
        public string DepartmentCode
        { get; set; }
        public string DepartmentName
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
