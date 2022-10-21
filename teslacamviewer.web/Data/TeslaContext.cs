using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using teslacamviewer.web.Data.DataModels;

namespace teslacamviewer.web.Data
{
    public class TeslaContext : DbContext
    {
        private readonly IConfiguration _config;
        public DbSet<TeslaConfig> TeslaConfigs {get;set;}
        public DbSet<Favorite> Favorites {get;set;}
        public DbSet<TeslaClip> TeslaClips { get; set; }
        public DbSet<TeslaEvent> TeslaEvents { get; set; }
        public DbSet<TeslaFolder> TeslaFolders { get; set; }
        public TeslaContext(IConfiguration config) {
            _config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            var useSqlite = _config.GetValue<bool>("useSqlite");
            if (useSqlite) {
                optionsBuilder.UseSqlite(_config["connectionString"]);
            } else {
                optionsBuilder.UseSqlServer(_config["connectionString"]);
            }
        }
    }
}