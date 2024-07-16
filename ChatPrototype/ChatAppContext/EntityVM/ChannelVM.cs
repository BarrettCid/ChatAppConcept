using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppContext.EntityVM
{
    public class ChannelVM
    {
        public Guid ChannelId { get; set; }
        public string Name { get; set; }
        public ICollection<ServerChannelVM> ServerChannels { get; set; }
    }
}
