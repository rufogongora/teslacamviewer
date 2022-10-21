using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace teslacamviewer.web.Data.DataModels
{
    public class TeslaFolder
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ActualPath { get; set; }
        public bool Thumbnail { get; set; }
        public IEnumerable<TeslaClipsGroup> TeslaClipGroups { get; set; }
        public TeslaEvent TeslaEvent { get; set; }
        public bool SoftDeleted { get; set; }
        public bool HardDeleted { get; set; }
        public string FolderType { get; set; }

    }
}
