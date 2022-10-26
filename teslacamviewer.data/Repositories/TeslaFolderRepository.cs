using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using teslacamviewer.data.CompositeModels;
using teslacamviewer.data.Context;
using teslacamviewer.data.Enums;
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
        Task<PaginatedResult<TeslaFolderWithoutClips>> GetTeslaFoldersWithoutClips(
            int pageNumber,
            int pageSize,
            FolderColumnEnum orderBy,
            string search);
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

        public async Task<PaginatedResult<TeslaFolderWithoutClips>> GetTeslaFoldersWithoutClips(
            int pageNumber,
            int pageSize,
            FolderColumnEnum orderBy,
            string search)
        {
            var query = _dbContext
                .TeslaFolders
                .Select(tf => new TeslaFolderWithoutClips
                {
                    Id = tf.Id,
                    Name = tf.Name,
                    ActualPath = tf.ActualPath,
                    Thumbnail = tf.Thumbnail,
                    NumberOfClips = tf.TeslaClipGroups.Count(),
                    TeslaEvent = tf.TeslaEvent,
                    SoftDeleted = tf.SoftDeleted,
                    HardDeleted = tf.HardDeleted,
                    FolderType = tf.FolderType,
                    Favorite = tf.Favorite,
                })
                .Where(tf => tf.Name.ToLower().Contains(search) || tf.TeslaEvent.City.ToLower().Contains(search) || tf.TeslaEvent.Reason.ToLower().Contains(search.ToLower()));


            switch (orderBy)
            {
                case FolderColumnEnum.Name:
                    query = query.OrderBy(tf => tf.Name);
                    break;
                case FolderColumnEnum.NumberofClips:
                    query = query.OrderBy(tf => tf.NumberOfClips);
                    break;
                case FolderColumnEnum.Reason:
                    query = query.OrderBy(tf => tf.TeslaEvent.Reason);
                    break;
                case FolderColumnEnum.Date:
                    query = query.OrderBy(tf => tf.TeslaEvent.TimeStamp);
                    break;
                case FolderColumnEnum.City:
                    query = query.OrderBy(tf => tf.TeslaEvent.City);
                    break;
                default:
                    break;
            }

            var filteredCount = await query.CountAsync();
            var data = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginatedResult<TeslaFolderWithoutClips>
            {
                Data = data,
                TotalCount = filteredCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(filteredCount / (double)pageSize)
            };
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
