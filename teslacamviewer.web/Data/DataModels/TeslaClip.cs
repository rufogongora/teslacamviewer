using System;
using teslacamviewer.web.Enums;

namespace teslacamviewer.web.Data.DataModels
{
    public class TeslaClip
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ActualPath { get; set; }
        public DateTime? DateTime { get; set; }
        public SideEnum Side { get; set; }
    }
}
