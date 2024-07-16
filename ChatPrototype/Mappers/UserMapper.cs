using AutoMapper;
using ChatAppContext.EntityVM;
using ChatAppContext.Entities;

namespace ChatAppMappers
{
    public class UserMapper :Profile
    {
         
        public UserMapper()
        { 
            this.CreateMap<User, UserVM>();
            this.CreateMap<User, UserVM>().ReverseMap();

            this.CreateMap<UserServer, UserServerVM>();
            this.CreateMap<UserServer, UserServerVM>().ReverseMap();
        }
    }
}
