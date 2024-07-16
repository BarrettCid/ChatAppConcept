using ChatAppContext.Entities;

namespace ChatAppAPI.Services.Interfaces
{
    public interface IPusherService
    {

        Task<bool> SendPusherMessageByMessageEntity(Message messageEntity, string pusherChannel, string pusherEvent);
    }
}
