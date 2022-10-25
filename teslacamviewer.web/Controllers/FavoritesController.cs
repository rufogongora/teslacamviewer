using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using teslacamviewer.data.Repositories;
using teslacamviewer.web.Helpers;
using teslacamviewer.web.Services;
using teslacamviewer.web.ViewModels;

namespace teslacamviewer.web.Controllers
{
    [Route("api/[controller]")]
    public class FavoritesController: Controller
    {
        private readonly IFavoritesRepository _favoritesRepo;
        private readonly ITeslaPhysicalFolderRepository _teslaFolderRepository;
        private readonly IMapper _mapper;

        public FavoritesController(
            IFavoritesRepository favoritesRepo,
            ITeslaPhysicalFolderRepository teslaFolderRepository,
            IMapper mapper
            ) 
        {
            _favoritesRepo = favoritesRepo;
            _teslaFolderRepository = teslaFolderRepository;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet, Route("folders")]
        public async Task<IActionResult> GetFavoriteFolders() 
        {
            return Ok(await _favoritesRepo.GetFavoriteFolders());
        }
        [Authorize]
        [HttpGet, Route("clips")]
        public async Task<IActionResult> GetFavoriteClips()
        {
            return Ok(await _favoritesRepo.GetFavoriteClips());
        }

        [Authorize]
        [HttpPost, Route("folders")]
        public async Task<IActionResult> PostFavoriteFolder([FromBody] ToggleFavoriteViewModel favorite) 
        {
            await _favoritesRepo.ToggleFolderFavorite(favorite.Name);
            return Ok();
        }

        [Authorize]
        [HttpPost, Route("clips")]
        public async Task<IActionResult> PostFavoriteClip([FromBody] ToggleFavoriteViewModel favorite)
        {
            await _favoritesRepo.ToggleClipFavorite(favorite.Name);
            return Ok();
        }
    }
}