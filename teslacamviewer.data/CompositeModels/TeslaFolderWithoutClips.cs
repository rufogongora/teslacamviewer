using System.Collections.Generic;
using teslacamviewer.data.Models;

namespace teslacamviewer.data.CompositeModels
{
    public class TeslaFolderWithoutClips
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ActualPath { get; set; }
        public bool Thumbnail { get; set; }
        public int NumberOfClips { get; set; }
        public TeslaEvent TeslaEvent { get; set; }
        public bool SoftDeleted { get; set; }
        public bool HardDeleted { get; set; }
        public string FolderType { get; set; }
        public bool Favorite { get; set; }
    }
}
