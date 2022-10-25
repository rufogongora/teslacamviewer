using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using teslacamviewer.data.Models;
using teslacamviewer.data.Repositories;
using teslacamviewer.web.Contracts;
using teslacamviewer.web.Helpers;
using teslacamviewer.web.ViewModels;

namespace teslacamviewer.web.Controllers
{
    [Route("api/[controller]")]
    public class ConfigurationController: Controller
    {
        private readonly ITeslaConfigurationRepository _repo;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public ConfigurationController(
            ITeslaConfigurationRepository repository,
            IMapper mapper,
            IConfiguration configuration
        ) 
        {
            _repo = repository;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> Get() 
        {
            var config = await _repo.GetPublicConfig(); 
            var result = _mapper.Map<TeslaConfigPublicContract>(config);
            if (result == null)
            {
                result = new TeslaConfigPublicContract { ConfigExists = false };
            }
            else
            {
                result.ConfigExists = true;
            }
            result.IsAuthorizationEnabled = _configuration.GetValue<bool>("authorizationEnabled");
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TeslaConfig config)
        {
            return Ok(await _repo.SaveConfig(config));
        }

        [HttpPost, Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel login) {
            var isValid = await _repo.Login(login.Password);
            if (isValid) {
                return Ok(JwtMiddleware.generateJwtToken());
            }
            return BadRequest("Invalid password.");
        }

        [Authorize]
        [HttpPatch, Route("changePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordViewModel changePassword)
        {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState.ValidationState);
            }
            var validPassword = await _repo.ChangePassword(changePassword.Currentpassword, changePassword.Newpassword);
            return validPassword ? Ok() : BadRequest("Invalid password provided.");
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteConfig()
        {
            await _repo.DeleteConfigs();
            return Ok(new { message = "Configurations have been deleted" });
        }

    }
}