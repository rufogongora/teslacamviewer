using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using teslacamviewer.web.Data.DataModels;

namespace teslacamviewer.web.Data.Repositories
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
