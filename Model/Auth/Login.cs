using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Auth
{
    public class Login
    {
        public string UserName
        { get; set; }
        public string Password
        { get; set; }
        public int LoginCount
        { get; set; }
        public DateTime LastLoginDate
        { get; set; }
        public bool IsLock
        { get; set; }
        public int Validity
        { get; set; }
        public DateTime ValidDate
        { get; set; }
        public bool IsChange
        { get; set; }
        public int LoginErrorCount
        { get; set; }
        public string LoginIP
        { get; set; }
        public string LoginComputer
        { get; set; }
    }
}