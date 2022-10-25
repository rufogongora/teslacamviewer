using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using teslacamviewer.data.Context;
using teslacamviewer.data.Models;

namespace teslacamviewer.data.Repositories
{
    public interface ITeslaFolderRepository
    {
        Task<IEnumerable<TeslaFolder>> GetTeslaFolders();
        Task DeleteTeslaFolders(IEnumerable<TeslaFolder> teslaFolders);
        Task AddTeslaFolders(IEnumerable<TeslaFolder> teslaFolders);
        Task<TeslaFolder> GetTeslaFolder(string folderName, string folderType);
        Task ToggleFavorite(string folderName, string folderType);
        Task<IEnumerable<TeslaFolder>> GetFavorites();
    }
    public class TeslaFolderRepository : ITeslaFolderRepository
    {
        private readonly TeslaContext _dbContext;

        public TeslaFolderRepository(TeslaContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<IEnumerable<TeslaFolder>> GetTeslaFolders()
        {
            return await GetTeslaFoldersWithChildrenAttached()
                .ToListAsync();
        }

        public async Task<TeslaFolder> GetTeslaFolder(string folderName, string folderType)
        {
            return await GetTeslaFoldersWithChildrenAttached()
                .FirstOrDefaultAsync(tf => tf.Name == folderName && tf.FolderType == folderType);
        }

        public async Task ToggleFavorite(string folderName, string folderType)
        {
            var teslaFolder = await GetTeslaFolder(folderName, folderType);
            teslaFolder.Favorite = !teslaFolder.Favorite;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<TeslaFolder>> GetFavorites()
        {
            return await GetTeslaFoldersWithChildrenAttached()
                .Where(tf => tf.Favorite)
                .ToListAsync();
        }

        public async Task DeleteTeslaFolders(IEnumerable<TeslaFolder> teslaFolders)
        {
            _dbContext.RemoveRange(teslaFolders.Where(df => df.TeslaEvent != null).Select(df => df.TeslaEvent));
            _dbContext.RemoveRange(teslaFolders);
            await _dbContext.SaveChangesAsync();

        }

        public async Task AddTeslaFolders(IEnumerable<TeslaFolder> teslaFolders)
        {
            _dbContext.TeslaFolders.AddRange(teslaFolders);
            await _dbContext.SaveChangesAsync();
        }

        private IIncludableQueryable<TeslaFolder, TeslaEvent> GetTeslaFoldersWithChildrenAttached()
        {
            return _dbContext.TeslaFolders
                .Include(tf => tf.TeslaClipGroups)
                    .ThenInclude(tf => tf.TeslaClips)
                .Include(tf => tf.TeslaEvent);
        }
    }
}
