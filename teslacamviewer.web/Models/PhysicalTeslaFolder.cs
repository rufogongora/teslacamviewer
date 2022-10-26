using System.Collections.Generic;

namespace teslacamviewer.web.Models
{
    public class PhysicalTeslaFolder
    {
        public string Name {get;set;}
        public string ActualPath {get;set;}
        public PhysicalTeslaEvent TeslaEvent {get;set;}
        public bool Thumbnail {get;set;}
        public IEnumerable<PhysicalTeslaClip> TeslaClips {get;set;} = new List<PhysicalTeslaClip>();
        public string FolderType {get;set;}
    }
}