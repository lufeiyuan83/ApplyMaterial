using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.HRM
{
    public class Employee
    {
        public long Id
        { get; set; }
        public string ApplicationOrg
        { get; set; }
        public string EmployeeID
        { get; set; }
        public string EmployeeName
        { get; set; }
        public string EmployeeEnglishName
        { get; set; }
        public string EmpoyeeGender
        { get; set; }
        public string EmployeeEmail
        { get; set; }
        public string EmployeePhone
        { get; set; }
        public string DepartmentCode
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
