using ChatAppAPI.Services.Interfaces;
using ChatAppContext.DTO;
using ChatAppContext.Entities;
using ChatAppContext.EntityVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PusherServer;
using System.Net;


namespace ChatAppAPI.Controllers
{
    [Authorize("Token")]
    [Route("api/[controller]")]
    [ApiController]
    public class ServerController : Controller
    {
        private IServerService _serverService;
        public ServerController(
            IServerService serverService
            )
        {
            _serverService = serverService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Json(await _serverService.GetAll());
        }

        [HttpGet]
        [Route("getChannelsByServer/{serverId}")]
        public async Task<ActionResult> GetChannelsByServer(string serverId)
        {
            return Json(await _serverService.GetChannelsByServer(serverId));
        }

        [HttpPost]
        [Route("sendMessage")]
        public async Task<ActionResult> SendMessage([FromBody] MessageDTO message)
        {
            var result = await this._serverService.SendMessage(message);
            return Json(result);
        }

    }

    public class TestClass
    {
        public string Test { get; set; }
        public string Test2 { get; set; }
    }
}
