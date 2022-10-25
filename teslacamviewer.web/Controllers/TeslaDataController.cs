using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
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
            return Ok(await _teslaDataRepository.GetData());
        }
    }
}
