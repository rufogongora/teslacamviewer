using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using teslacamviewer.data.Enums;
using teslacamviewer.data.Repositories;
using teslacamviewer.web.Contracts;
using teslacamviewer.web.Helpers;
using teslacamviewer.web.ViewModels;

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

        [HttpGet, Route("paginate/{pageNumber}/{pageSize}/{orderBy}")]
        public async Task<IActionResult> GetTeslaFolders([FromRoute] int pageNumber, [FromRoute] int pageSize, [FromRoute] FolderColumnEnum orderBy, [FromQuery] string search = "")
        {
            return Ok(await _teslaFolderRepository.GetTeslaFoldersWithoutClips(pageNumber, pageSize, orderBy, search));
        }

        [HttpGet, Route("{folderType}/{folderName}")]
        public async Task<IActionResult> GetTeslaFolder(string folderType, string folderName)
        {
            return Ok(await _teslaFolderRepository.GetTeslaFolder(folderName, folderType));
        }
    }
}
