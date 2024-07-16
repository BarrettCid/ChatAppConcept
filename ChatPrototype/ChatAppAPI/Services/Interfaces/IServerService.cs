using ChatAppContext.DTO;
using ChatAppContext.Entities;
using ChatAppContext.EntityVM;

namespace ChatAppAPI.Services.Interfaces
{
    public interface IServerService
    {
        Task<List<ServerVM>> GetAll();
        
        Task<List<ServerVM>> GetServersByUsername(string username);

        Task<List<ChannelVM>> GetChannelsByServer(string serverId);

        Task<Message> SaveServerMessage(Message message);

        Task<bool> SendMessage(MessageDTO message);
    }
}
