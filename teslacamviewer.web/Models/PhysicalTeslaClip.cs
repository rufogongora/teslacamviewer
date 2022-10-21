using System;
using System.Collections.Generic;
using teslacamviewer.web.Enums;

namespace teslacamviewer.web.Models
{
    public class PhysicalTeslaClip
    {
        public string Name {get;set;}
        public string ActualPath {get;set;}
        public DateTime? DateTime {get;set;}
        public SideEnum Side {get;set;}
    }
}