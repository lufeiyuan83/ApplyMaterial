using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.PMS
{
    public class Element
    {
        public long Id
        { get; set; }
        public long SysMenuId
        { get; set; }        
        public string Code
        { get; set; }
        public string Name
        { get; set; }
    }
}
