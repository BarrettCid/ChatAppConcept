using AutoMapper;
using AutoMapper.Internal;
using ChatAppAPI.Services.Interfaces;
using ChatAppContext.Context;
using ChatAppContext.DTO;
using ChatAppContext.Entities;
using ChatAppContext.EntityVM;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ChatAppAPI.Services
{
    public class ChannelService :  IChannelService
    {
        private ChatAppDBContext _dbContext;
        private IMapper _mapper;
        public ChannelService(ChatAppDBContext dBContext, IMapper mapper)
        {
            this._dbContext = dBContext;
            this._mapper = mapper;
        }
        public async Task<ICollection<MessageDTO>> GetMessagesByChannel(string channelId)
        {
            Guid channelGuid = new Guid(channelId);
            ICollection <Message> messages = await this._dbContext.Messages.Include(m => m.User).Where(m => m.ChannelId == channelGuid)
           .OrderByDescending(m => m.DateCreated).ToListAsync();
            var messageDTO = messages.Select(x => new MessageDTO
            {
                Username = x.User.EmailAddress,
                ChannelId = x.ChannelId.ToString(),
                MessageId = x.MessageId.ToString(),
                Message = x.MessageData,
                DateCreated = x.DateCreated.ToString("MM/dd/yy HH:mm")
            }).ToList();
            return messageDTO;
        }
    }
}
