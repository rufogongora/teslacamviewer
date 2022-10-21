using System.Linq;
using AutoMapper;
using teslacamviewer.web.Data.DataModels;
using teslacamviewer.web.Models;

namespace teslacamviewer.web.Contracts
{
    public class MappingProfile: Profile
    {
        public MappingProfile() {
            CreateMap<PhysicalTeslaFolder, TeslaFolderContract>()
            .AfterMap(
                (src, dest) => {
                     dest.TeslaClipsGroupedByDate = dest.TeslaClips.GroupBy(tc => tc.DateTime);
                });

            CreateMap<PhysicalTeslaEvent, TeslaEventContract>();
            CreateMap<PhysicalTeslaClip, TeslaClipContract>();
            CreateMap<TeslaConfig, TeslaConfigPublicContract>();
        }
    }
}