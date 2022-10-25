using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using teslacamviewer.data.Models;

namespace teslacamviewer.data.Context
{
    public class TeslaContext : DbContext
    {
        private readonly string _connectionString;
        public DbSet<TeslaConfig> TeslaConfigs {get;set;}
        public DbSet<Favorite> Favorites {get;set;}
        public DbSet<TeslaClip> TeslaClips { get; set; }
        public DbSet<TeslaEvent> TeslaEvents { get; set; }
        public DbSet<TeslaFolder> TeslaFolders { get; set; }
        public DbSet<TeslaClipsGroup> TeslaClipsGroups { get; set; }
        public DbSet<TeslaData> TeslaDatas { get; set; }
        public TeslaContext(DbContextOptions options) : base(options)
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite(_connectionString);
        //}
    }
}