using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppContext.Entities
{
    public class Message
    {
        public int MessageId { get; set; }
	    public Guid ChannelId { get; set; }
        public int UserId { get; set; }
        public string MessageData { get; set; }
        public DateTime DateCreated { get; set; }
        public Channel Channel { get; set; }
        public User User { get; set; }
    }
}
