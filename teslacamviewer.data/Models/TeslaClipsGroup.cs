using System.Collections.Generic;

namespace teslacamviewer.data.Models
{
    public class TeslaClipsGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<TeslaClip> TeslaClips { get; set; }
    }
}
