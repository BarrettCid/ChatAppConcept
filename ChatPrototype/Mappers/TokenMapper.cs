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
    internal class TokenMapper : Profile
    {
        public TokenMapper()
        {
            this.CreateMap<Token, TokenVM>();
            this.CreateMap<Token, TokenVM>().ReverseMap();
        }
    }
}
