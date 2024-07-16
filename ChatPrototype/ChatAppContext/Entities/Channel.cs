using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppContext.Entities
{
    public class Channel
    {
        public Guid ChannelId { get; set; }
        public string Name { get; set; }
        public ICollection<ServerChannel> ServerChannels { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}
