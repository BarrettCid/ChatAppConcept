using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppContext.EntityVM
{
    public class ServerVM
    {
        public Guid ServerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public ICollection<UserServerVM> UserServers { get; set; }
        public ICollection<ServerChannelVM> ServerChannels { get; set; }
    }
}
