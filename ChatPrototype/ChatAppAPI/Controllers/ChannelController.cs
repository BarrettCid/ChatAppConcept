using ChatAppAPI.Services;
using ChatAppAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatAppAPI.Controllers
{
    [Authorize("Token")]
    [Route("api/[controller]")]
    [ApiController]
    public class ChannelController : Controller
    {
        private IChannelService _channelService;
        public ChannelController(
            IChannelService channelService
            )
        {
            _channelService = channelService;
        }

        [HttpGet]
        [Route("getMessagesByChannel/{channelId}")]
        public async Task<ActionResult> GetMessagesByChannel(string channelId)
        {
            var data = await _channelService.GetMessagesByChannel(channelId);
            return Json(data);
        }
    }
}
