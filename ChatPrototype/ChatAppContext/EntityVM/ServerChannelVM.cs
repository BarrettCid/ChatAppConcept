using ChatAppContext.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppContext.EntityVM
{
    public class ServerChannelVM
    {
        public Guid ServerId { get; set; }
        public ServerVM Server { get; set; }
        public Guid ChannelId { get; set; }
        public ChannelVM Channel { get; set; }
    }
}
