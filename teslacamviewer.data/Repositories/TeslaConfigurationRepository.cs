using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using teslacamviewer.data.Context;
using teslacamviewer.data.Models;

namespace teslacamviewer.data.Repositories
{
    public interface ITeslaConfigurationRepository
    {
        Task<TeslaConfig> GetPublicConfig();
        Task<TeslaConfig> SaveConfig(TeslaConfig config);
        Task<bool> Login(string password);
        Task<bool> ChangePassword(string currentPassword, string newPassword);
        Task DeleteConfigs();
    }
    public class TeslaConfigurationRepository : ITeslaConfigurationRepository
    {
        private readonly TeslaContext _context;
        private readonly IConfiguration _configuration;

        public TeslaConfigurationRepository(
            TeslaContext teslaContext,
            IConfiguration configuration
            )
        {
            _context = teslaContext;
            _configuration = configuration;
        }
        public async Task<TeslaConfig> GetPublicConfig()
        {
            var dbConfig = await _context.TeslaConfigs.FirstOrDefaultAsync();
            return dbConfig;
        }

        public async Task<TeslaConfig> SaveConfig(TeslaConfig config)
        {
            var exists = await GetConfig();
            if (exists != null)
            {
                throw new Exception("A config already exists.");
            }
            var salt = GenerateSalt();
            config.Salt = Convert.ToBase64String(salt);
            config.Password = Hash(config.Password, salt);
            _context.TeslaConfigs.Add(config);
            await _context.SaveChangesAsync();
            return config;
        }

        public async Task<bool> Login(string password)
        {
            return await isValidPassword(password);

        }

        public async Task<bool> ChangePassword(string currentPassword, string newPassword)
        {
            if (!await isValidPassword(currentPassword))
            {
                return false;
            }
            var newSalt = GenerateSalt();
            var config = await _context.TeslaConfigs.FirstOrDefaultAsync();
            config.Password = Hash(newPassword, newSalt);
            config.Salt = Convert.ToBase64String(newSalt);
            await _context.SaveChangesAsync();
            return true;
        }
        
        public async Task DeleteConfigs()
        {
            _context.TeslaConfigs.RemoveRange(_context.TeslaConfigs);
            await _context.SaveChangesAsync();
        }

        private byte[] GenerateSalt()
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        private async Task<TeslaConfig> GetConfig()
        {
            return await _context.TeslaConfigs.FirstOrDefaultAsync();
        }

        private string Hash(string password, byte[] salt)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA1,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));
        }

        private async Task<bool> isValidPassword(string password)
        {
            var config = await GetConfig();
            if (config == null)
            {
                throw new Exception("Config has not been created");
            }
            var hashedProvidedPassword = Hash(password, Convert.FromBase64String(config.Salt));
            return (config.Password == hashedProvidedPassword);
        }
    }
}