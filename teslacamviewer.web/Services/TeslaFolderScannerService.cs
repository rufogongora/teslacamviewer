using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using teslacamviewer.web.Contracts;
using teslacamviewer.web.Data;
using teslacamviewer.web.Data.DataModels;
using teslacamviewer.web.Data.Repositories;
using teslacamviewer.web.Models;


namespace teslacamviewer.web.Services
{
    public interface ITeslaFolderScannerService
    {
        Task<ScanningResultContract> ScanTeslaFolders();
    }

    public class TeslaFolderScannerService : ITeslaFolderScannerService
    {
        private readonly ITeslaPhysicalFolderRepository _physicalTeslaFolderRepository;
        private readonly ITeslaFolderRepository _teslaFolderRepository;
        private readonly ITeslaClipsRepository _teslaClipsRepository;

        //initialize constructor
        public TeslaFolderScannerService(
            ITeslaPhysicalFolderRepository physicalTeslaFolderRepository,
            ITeslaClipsRepository teslaClipsRepository,
            ITeslaFolderRepository teslaFolderRepository)
        {
            _physicalTeslaFolderRepository = physicalTeslaFolderRepository;
            _teslaClipsRepository = teslaClipsRepository;
            _teslaFolderRepository = teslaFolderRepository;
        }

        public async Task<ScanningResultContract> ScanTeslaFolders()
        {
            // first retrieve the physical folders
            var physicalFolders = _physicalTeslaFolderRepository.GetTeslaFolders();

            // now retrieve the folders that exist in the db
            var dbFolders = await _teslaFolderRepository.GetTeslaFolders();

            // get the list of folders that have not been persisted
            var newFolders = GetNewlyCreatedFolders(dbFolders, physicalFolders);

            // get newly added folders ready for insertion
            var newFoldersForInsertion = newFolders.Select(x => PhysicalTeslaFolderToTeslaFolder(x));
            await _teslaFolderRepository.AddTeslaFolders(newFoldersForInsertion);

            // get the list of physical folders that have been deleted from the drive
            var deletedFolders = GetHardDeletedFolders(dbFolders, physicalFolders);

            var ids = deletedFolders.SelectMany(df => df?.TeslaClipGroups).SelectMany(tcg => tcg.TeslaClips).Select(tc => tc.Id);

            await _teslaClipsRepository.DeleteClipsFromTeslaFolder(deletedFolders);
            await _teslaFolderRepository.DeleteTeslaFolders(deletedFolders);

            return new ScanningResultContract { FoldersAdded = newFolders.Count(), FoldersRemoved = deletedFolders.Count() };
        }

        private TeslaFolder PhysicalTeslaFolderToTeslaFolder(PhysicalTeslaFolder physicalTeslaFolder)
        {
            return new TeslaFolder
            {
                Name = physicalTeslaFolder.Name,
                ActualPath = physicalTeslaFolder.ActualPath,
                HardDeleted = false,
                SoftDeleted = false,
                TeslaClipGroups = GroupTeslaClipsByDate(physicalTeslaFolder.TeslaClips).ToList(),
                TeslaEvent = physicalTeslaFolder.TeslaEvent != null ? PhysicalTeslaEventToTeslaEvent(physicalTeslaFolder.TeslaEvent) : null,
                Thumbnail = physicalTeslaFolder.Thumbnail,
                FolderType = physicalTeslaFolder.FolderType
            };
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

        private static IEnumerable<TeslaClipsGroup> GroupTeslaClipsByDate(IEnumerable<PhysicalTeslaClip> physicalTeslaClips)
        {
            return physicalTeslaClips.GroupBy(tc => tc.DateTime)
                .Select(grouping => new TeslaClipsGroup
                {
                    Name = grouping.Key?.ToString("yyyy-MM-dd"),
                    TeslaClips = grouping.ToList().Select(physicalClip => PhysicalTeslaClipToTeslaClip(physicalClip)).ToList()
                });
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
