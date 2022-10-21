using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using teslacamviewer.web.Contracts;
using teslacamviewer.web.Data.Repositories;
using teslacamviewer.web.Helpers;

namespace teslacamviewer.web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TeslaFolderV2Controller : ControllerBase
    {
        private readonly ITeslaFolderRepository _teslaFolderRepository;

        public TeslaFolderV2Controller(ITeslaFolderRepository teslaFolderRepository)
        {
            _teslaFolderRepository = teslaFolderRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetTeslaFolders()
        {
            return Ok(await _teslaFolderRepository.GetTeslaFolders());
        }

        [HttpGet, Route("{folderType}/{folderName}")]
        public async Task<IActionResult> GetTeslaFolder(string folderType, string folderName)
        {
            return Ok(await _teslaFolderRepository.GetTeslaFolder(folderName, folderType));
        }
    }
}
