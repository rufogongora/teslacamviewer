using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using teslacamviewer.web.Data.DataModels;
using teslacamviewer.web.Models;
using teslacamviewer.web.Services;
using Xunit;

namespace teslacamviewer.web.tests.Services
{
    public class TeslaFolderScannerServiceTests
    {
        [Fact]
        public void ShouldReturnOneDeletedFolder()
        {
            var dbFolders = new List<TeslaFolder> { new TeslaFolder { Name = "I have been deleted" }, new TeslaFolder { Name = "I exist both in db and physical" } };
            var physicalFolders = new List<PhysicalTeslaFolder> { new PhysicalTeslaFolder { Name = "I am a new tesla folder"}, new PhysicalTeslaFolder { Name = "I exist both in db and physical" } };
            var result = TeslaFolderScannerService.GetHardDeletedFolders(dbFolders, physicalFolders);
            result.Count().Should().Be(1);
        }

        [Fact]
        public void ShouldReturnOneNewlyAddedFolder()
        {
            var dbFolders = new List<TeslaFolder> { new TeslaFolder { Name = "I have been deleted" }, new TeslaFolder { Name = "I exist both in db and physical" } };
            var physicalFolders = new List<PhysicalTeslaFolder> { new PhysicalTeslaFolder { Name = "I am a new tesla folder" }, new PhysicalTeslaFolder { Name = "I exist both in db and physical" } };

            var result = TeslaFolderScannerService.GetNewlyCreatedFolders(dbFolders, physicalFolders);
            result.Count().Should().Be(1);

        }
    }
}
