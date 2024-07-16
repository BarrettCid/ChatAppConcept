using ChatAppContext.DTO;
using ChatAppContext.EntityVM;
using Microsoft.AspNetCore.Mvc;

namespace ChatAppAPI.Services.Interfaces
{
    public interface IChannelService
    {
        Task<ICollection<MessageDTO>> GetMessagesByChannel(string channelId);
    }
}
