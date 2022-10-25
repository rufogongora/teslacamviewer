using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using teslacamviewer.data.Context;
using teslacamviewer.data.Models;

namespace teslacamviewer.data.Repositories
{
    public interface ITeslaClipsRepository
    {
        Task DeleteClipsFromTeslaFolder(IEnumerable<TeslaFolder> teslaFolders); 
    }
    public class TeslaClipsRepository : ITeslaClipsRepository
    {
        private readonly TeslaContext _context;
        public TeslaClipsRepository(TeslaContext context)
        {
            _context = context;
        }

        //delete teslaclips from a folder
        public async Task DeleteClipsFromTeslaFolder(IEnumerable<TeslaFolder> teslaFolders)
        {
            _context.TeslaClips.RemoveRange(teslaFolders.SelectMany(df => df?.TeslaClipGroups).SelectMany(tcg => tcg?.TeslaClips));
            await _context.SaveChangesAsync();
        }
    }
}
