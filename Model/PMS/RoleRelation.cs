using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.PMS
{
    public class RoleRelation
    {
        public long Id
        { get; set; }
        public string ApplicationOrg
        { get; set; }
        public long RoleId
        { get; set; }
        public string EmployeeID
        { get; set; }
    }
}
