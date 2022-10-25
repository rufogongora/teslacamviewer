using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using teslacamviewer.web.Data.DataModels;

namespace teslacamviewer.web.Data.Repositories
{
    public interface IFavoritesRepository 
    {
        Task ToggleClipFavorite(string name);

        Task ToggleFolderFavorite(string name);

        Task<IEnumerable<TeslaClip>> GetFavoriteClips();

        Task<IEnumerable<TeslaFolder>> GetFavoriteFolders();
    }
    public class FavoritesRepository : IFavoritesRepository
    {
        private readonly TeslaContext _context;
        public FavoritesRepository(TeslaContext context) 
        {
            _context = context;
        }

        public async Task ToggleClipFavorite(string name)
        {
            var clip = await _context.TeslaClips.FirstOrDefaultAsync(tc => tc.Name == name);
            clip.Favorite = !clip.Favorite;
            await _context.SaveChangesAsync();
        }
        public async Task ToggleFolderFavorite(string name)
        {
            var folder = await _context.TeslaFolders.FirstOrDefaultAsync(tf => tf.Name == name);
            folder.Favorite = !folder.Favorite;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TeslaClip>> GetFavoriteClips() 
        {
            return await _context.TeslaClips
                .Where(c => c.Favorite)
                .ToListAsync();
        }

        public async Task<IEnumerable<TeslaFolder>> GetFavoriteFolders()
        {
            return await _context.TeslaFolders
                .Include(tf => tf.TeslaClipGroups)
                    .ThenInclude(tf => tf.TeslaClips)
                .Include(tf => tf.TeslaEvent)                 
                .Where(f => f.Favorite)
                .ToListAsync();
        }
    }
}