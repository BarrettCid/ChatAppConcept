using ChatAppAPI.Services;
using ChatAppAPI.Services.Interfaces;
using ChatAppContext.EntityVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Formats.Asn1;

namespace ChatAppAPI.Controllers
{
    [Authorize("Token")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private IUserService _userService;
        private IServerService _serverService;

        public UserController(
            IUserService userService,
            IServerService serverService

            )
        {
            _userService = userService;
            _serverService = serverService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login(string username, string password)
        {
            try
            {
                return Json(await this._userService.Login(username, password));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> Register([FromBody] UserVM newUser)
        {
            try
            {
                return Json(await this._userService.Register(newUser));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [Route("getServers/{username}")]
        public async Task<ActionResult> GetServers(string username)
        {
            return Json(await this._serverService.GetServersByUsername(username));
        }

        [HttpPost]
        [Route("getUserIdByUsername")]
        public async Task<ActionResult> getUserIdByUsername([FromBody] string username)
        {
            return Json(await this._userService.GetUserIdByUsername(username));
        }

    }
}
