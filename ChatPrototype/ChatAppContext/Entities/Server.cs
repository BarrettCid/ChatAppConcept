using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppContext.Entities
{
    public class Server
    {
        public Guid ServerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public ICollection<UserServer> UserServers { get; set; }
        public ICollection<ServerChannel> ServerChannels { get; set; }
    }
}
