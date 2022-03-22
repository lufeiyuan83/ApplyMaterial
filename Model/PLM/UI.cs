using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.PLM
{
    public class UI
    {
        public long UIId
        { get; set; }
        public string UIForm
        { get; set; }
        public string Type
        { get; set; }
        public string ID
        { get; set; }
        public string Layout
        { get; set; }
        public string Title
        { get; set; }
        public bool ShowBorder
        { get; set; }
        public bool ShowHeader
        { get; set; }
        public string Width
        { get; set; }
        public string BodyPadding
        { get; set; }
        public string EmptyText
        { get; set; }
        public string Label
        { get; set; }
        public string LabelAlign
        { get; set; }
        public string DataValueField
        { get; set; }
        public string DataTextField
        { get; set; }
        public bool Required
        { get; set; }
        public bool ShowRedStar
        { get; set; }
        public bool AutoPostBack
        { get; set; }
        public bool AutoSelectFirstItem
        { get; set; }
        public bool Enabled
        { get; set; }
        public int SortNumber
        { get; set; }
        public string ChildId
        { get; set; }
    }
}
