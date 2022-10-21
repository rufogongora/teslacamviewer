using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using teslacamviewer.web.Data;
using teslacamviewer.web.Data.DataModels;
using teslacamviewer.web.Models;


namespace teslacamviewer.web.Services
{
    public interface ITeslaFolderScannerService
    {
        Task ScanTeslaFolders();
    }

    public class TeslaFolderScannerService : ITeslaFolderScannerService
    {
        private readonly ITeslaFolderRepository _teslaFolderRepository;
        private readonly TeslaContext _dbContext;

        //initialize constructor
        public TeslaFolderScannerService(
            ITeslaFolderRepository teslaFolderRepository,
            TeslaContext dbContext)
        {
            _teslaFolderRepository = teslaFolderRepository;
            _dbContext = dbContext;
        }

        public async Task ScanTeslaFolders()
        {
            // first retrieve the physical folders
            var physicalFolders = _teslaFolderRepository.GetTeslaFolders();

            // now retrieve the folders that exist in the db
            var dbFolders = await _dbContext.TeslaFolders.ToListAsync();

            // get the list of folders that have not been persisted
            var newFolders = GetNewlyCreatedFolders(dbFolders, physicalFolders);
            
            // get newly added folders ready for insertion
            var newFoldersForInsertion = newFolders.Select(x =>
                new TeslaFolder
                {
                    Name = x.Name,
                    ActualPath = x.ActualPath,
                    HardDeleted = false,
                    SoftDeleted = false,
                    TeslaClips = x.TeslaClips.Select(y => PhysicalTeslaClipToTeslaClip(y)).ToList(),
                    TeslaEvent = x.TeslaEvent != null ? PhysicalTeslaEventToTeslaEvent(x.TeslaEvent) : null,
                    Thumbnail = x.Thumbnail
                });

            // insert the newly added folders
            _dbContext.TeslaFolders.AddRange(newFoldersForInsertion);

            // get the list of physical folders that have been deleted from the drive
            var deletedFolders = GetHardDeletedFolders(dbFolders, physicalFolders);

            await _dbContext.SaveChangesAsync();
        }


        private static TeslaEvent PhysicalTeslaEventToTeslaEvent(PhysicalTeslaEvent physicalTeslaEvent)
        {
            return new TeslaEvent
            {              
                TimeStamp = physicalTeslaEvent.TimeStamp,
                City = physicalTeslaEvent.City,
                Est_Lat = physicalTeslaEvent.Est_Lat,
                Est_Lon = physicalTeslaEvent.Est_Lon,
                Reason = physicalTeslaEvent.Reason
            };
        }
        
        private static TeslaClip PhysicalTeslaClipToTeslaClip(PhysicalTeslaClip physicalTeslaClip)
        {
            return new TeslaClip
            {
                Name = physicalTeslaClip.Name,
                ActualPath = physicalTeslaClip.ActualPath,
                DateTime = physicalTeslaClip.DateTime,
                Side = physicalTeslaClip.Side,
            };
        }

        public static IEnumerable<TeslaFolder> GetHardDeletedFolders(IEnumerable<TeslaFolder> dbFolders, IEnumerable<PhysicalTeslaFolder> physicalFolders)
        {
            return dbFolders.Where(dbFolder => !physicalFolders.Any(physicalFolder => dbFolder.Name == physicalFolder.Name));
        }

        public static IEnumerable<PhysicalTeslaFolder> GetNewlyCreatedFolders(IEnumerable<TeslaFolder> dbFolders, IEnumerable<PhysicalTeslaFolder> physicalFolders)
        {
            return physicalFolders.Where(physicalFolder => !dbFolders.Any(dbFolder => dbFolder.Name == physicalFolder.Name));
        }
    }
}
