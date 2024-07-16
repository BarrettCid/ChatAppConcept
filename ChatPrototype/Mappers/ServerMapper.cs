using AutoMapper;
using ChatAppContext.Entities;
using ChatAppContext.EntityVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppMappers
{
    internal class ServerMapper : Profile
    {
        public ServerMapper()
        {
            this.CreateMap<Server, ServerVM>();
            this.CreateMap<Server, ServerVM>().ReverseMap();

            this.CreateMap<Channel, ChannelVM>();
            this.CreateMap<Channel, ChannelVM>().ReverseMap();

            this.CreateMap<ServerChannel, ServerChannelVM>();
            this.CreateMap<ServerChannel, ServerChannelVM>().ReverseMap();

            this.CreateMap<Message, MessageVM>();
            this.CreateMap<Message, MessageVM>().ReverseMap();
        }
    }
}
