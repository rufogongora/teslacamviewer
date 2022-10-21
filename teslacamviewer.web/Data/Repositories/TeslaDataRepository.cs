using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using teslacamviewer.web.Data.DataModels;

namespace teslacamviewer.web.Data.Repositories
{
    public interface ITeslaDataRepository
    {
        Task<TeslaData> GetData();
        Task UpdateData(TeslaData teslaData);
    }
    public class TeslaDataRepository : ITeslaDataRepository
    {
        private readonly TeslaContext _teslaContext;

        public TeslaDataRepository(TeslaContext teslaContext)
        {
            _teslaContext = teslaContext;
        }

        public async Task<TeslaData> GetData()
        {
            return await _teslaContext.TeslaDatas.FirstOrDefaultAsync();
        }

        public async Task UpdateData(TeslaData teslaData)
        {
            var existingData = await GetData();
            if (existingData != null)
            {
                existingData.LastRun = teslaData.LastRun;
            } else
            {
                _teslaContext.Add(teslaData);
            }
            await _teslaContext.SaveChangesAsync();
        }
    }
}
