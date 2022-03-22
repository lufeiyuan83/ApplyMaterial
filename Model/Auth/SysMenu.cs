using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Auth
{
    public class SysMenu
    {
        public int Id
        { get; set; }
        public string SystemCode
        { get; set; }
        public int NodeId
        { get; set; }
        public string NodeCode
        { get; set; }
        public string MenuName
        { get; set; }
        public string MenuNameEn
        { get; set; }
        public string MenuNameTh
        { get; set; }
        public int ParentId
        { get; set; }
        public string NavigateUrl
        { get; set; }
        public bool Expanded
        { get; set; }
        public string Icon
        { get; set; }
        public int SortNo
        { get; set; }
    }
}
