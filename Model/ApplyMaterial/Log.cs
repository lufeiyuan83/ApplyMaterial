using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.ApplyMaterial
{
    public class Log
    {
        public long ID
        { get; set; }
        public string CSFileName
        { get; set; }
        public string Type
        { get; set; }
        public string Source
        { get; set; }
        public string Message
        { get; set; }
        public string StackTrace
        { get; set; }
        public string UserDescription
        { get; set; }
        public DateTime CreateDateTime
        { get; set; }
        public string CreateUserID
        { get; set; }
        public string ComputerName
        { get; set; }
        public string Remark
        { get; set; }
    }
}
