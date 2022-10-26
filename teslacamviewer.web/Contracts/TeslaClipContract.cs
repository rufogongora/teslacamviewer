using System;
using teslacamviewer.data.Enums;

namespace teslacamviewer.web.Contracts
{
    public class TeslaClipContract
    {
        public string Name {get;set;}
        public string ActualPath {get;set;}
        public DateTime DateTime {get;set;}
        public SideEnum Side {get;set;}
    }
}