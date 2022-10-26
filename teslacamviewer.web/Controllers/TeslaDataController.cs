using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using teslacamviewer.data.Models;
using teslacamviewer.data.Repositories;
using teslacamviewer.web.Helpers;

namespace teslacamviewer.web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TeslaDataController : ControllerBase
    {
        private readonly ITeslaDataRepository _teslaDataRepository;

        public TeslaDataController(ITeslaDataRepository teslaDataRepository)
        {
            _teslaDataRepository = teslaDataRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _teslaDataRepository.GetData();
            if (result == null)
            {
                return Ok(new TeslaData { LastRun = DateTime.Parse("03/27/1992 07:22:16") });
            }
            return Ok(result);
        }
    }
}
