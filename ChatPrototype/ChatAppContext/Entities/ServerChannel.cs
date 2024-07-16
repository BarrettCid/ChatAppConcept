using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppContext.Entities
{
    [Table("ServerChannel", Schema = "Chat")]
    public class ServerChannel
    {
        public Guid ServerId { get; set; }
        public Server Server { get; set; }
        public Guid ChannelId { get; set; }
        public Channel Channel { get; set; }
    }
}
