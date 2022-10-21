using System.Collections.Generic;

namespace teslacamviewer.web.Data.DataModels
{
    public class TeslaClipsGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<TeslaClip> TeslaClips { get; set; }
    }
}
