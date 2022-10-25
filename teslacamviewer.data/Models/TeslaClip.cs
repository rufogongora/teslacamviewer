using System;
using teslacamviewer.data.Enums;

namespace teslacamviewer.data.Models
{
    public class TeslaClip
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ActualPath { get; set; }
        public DateTime? DateTime { get; set; }
        public SideEnum Side { get; set; }
        public int TeslaClipGroupId { get; set; }
        public bool Favorite { get; set; }
    }
}
