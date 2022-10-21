using System.Collections;
using System.Collections.Generic;

namespace teslacamviewer.web.Data.DataModels
{
    public class TeslaFolder
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ActualPath { get; set; }
        public bool Thumbnail { get; set; }
        public IEnumerable<TeslaClip> TeslaClips { get; set; }
        public TeslaEvent TeslaEvent { get; set; }
        public bool SoftDeleted { get; set; }
        public bool HardDeleted { get; set; }
        
    }
}
