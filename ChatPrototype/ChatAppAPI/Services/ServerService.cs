using AutoMapper;
using ChatAppAPI.Services.Interfaces;
using ChatAppContext.Context;
using ChatAppContext.DTO;
using ChatAppContext.Entities;
using ChatAppContext.EntityVM;
using Microsoft.EntityFrameworkCore;
using PusherServer;
using System.Collections.Generic;

namespace ChatAppAPI.Services
{

    public class ServerService : IServerService
    {
        private ChatAppDBContext _dbContext;
        private IPusherService _pusherService;
        private IMapper _mapper;
        public ServerService(ChatAppDBContext dBContext, IMapper mapper, IPusherService pusherService)
        {
            this._dbContext = dBContext;
            this._mapper = mapper;
            this._pusherService = pusherService;
        }

        public async Task<List<ServerVM>> GetAll()
        {
            return this._mapper.Map<List<ServerVM>>(await this._dbContext.Servers.ToListAsync()); 
        }

        public async Task<List<ChannelVM>> GetChannelsByServer(string serverId)
        {
            Guid serverGuid = new Guid(serverId);
            ICollection<Channel> channels = await this._dbContext.Channels.Where(c => c.ServerChannels.Any(sc => sc.ServerId == serverGuid)).ToListAsync();
            return this._mapper.Map<List<ChannelVM>>(channels);
        }

        public async Task<List<ServerVM>> GetServersByUsername(string username)
        {
            ICollection<Server> servers = await this._dbContext.Servers.Where(s => s.UserServers.Any(us => us.User.EmailAddress == username))
                .OrderByDescending(s => s.DateCreated).ToListAsync();
            return this._mapper.Map<List<ServerVM>>(servers);
        }

        public async Task<Message> SaveServerMessage(Message message)
        {
            this._dbContext.Messages.Add(message);
            await this._dbContext.SaveChangesAsync();
            return message;
        }

        public async Task<bool> SendMessage(MessageDTO message)
        {
            try
            {
                Message chatAppMessage = new Message();
                chatAppMessage.User = await this._dbContext.Users.Where(u => u.EmailAddress == message.Username).FirstOrDefaultAsync();
                chatAppMessage.Channel = await this._dbContext.Channels.Where(c => c.ChannelId == new Guid(message.ChannelId)).FirstOrDefaultAsync();
                chatAppMessage.MessageData = message.Message;
                chatAppMessage.DateCreated = DateTime.Now;
                chatAppMessage = await this.SaveServerMessage(chatAppMessage);
                //if we successfully inserted
                if (chatAppMessage.MessageId > 0)
                {
                    var result = await this._pusherService.SendPusherMessageByMessageEntity(chatAppMessage, "my-channel", "my-event");

                    return result;
                }
                throw new Exception("Unable to save message. Please try again.");
                
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
