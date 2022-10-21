using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using teslacamviewer.web.Data.DataModels;

namespace teslacamviewer.web.Data.Repositories
{
    public interface ITeslaFolderRepository
    {
        Task<IEnumerable<TeslaFolder>> GetTeslaFolders();
        Task DeleteTeslaFolders(IEnumerable<TeslaFolder> teslaFolders);
        Task AddTeslaFolders(IEnumerable<TeslaFolder> teslaFolders);
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
            return await _dbContext.TeslaFolders
                .Include(tf => tf.TeslaClipGroups)
                    .ThenInclude(tf => tf.TeslaClips)
                .Include(tf => tf.TeslaEvent)
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
    }
}
