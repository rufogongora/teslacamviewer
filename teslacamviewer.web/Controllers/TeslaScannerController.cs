using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using teslacamviewer.web.Services;

namespace teslacamviewer.web.web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeslaScannerController: ControllerBase
    {
        private readonly ITeslaFolderScannerService _teslaScannerService;
        public TeslaScannerController(ITeslaFolderScannerService teslaScannerService) {
            _teslaScannerService = teslaScannerService;
        }

        [HttpPost] 
        public async Task<IActionResult> Scan() {
            var result = await _teslaScannerService.ScanTeslaFolders();
            return Ok(result);
        }
    }
}