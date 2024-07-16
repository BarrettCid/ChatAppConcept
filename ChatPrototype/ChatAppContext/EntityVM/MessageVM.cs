using ChatAppContext.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppContext.EntityVM
{
    public class MessageVM
    {
        public int MessageId { get; set; }
        public Guid ChannelId { get; set; }
        public int UserId { get; set; }
        public string MessageData { get; set; }
        public DateTime DateCreated { get; set; }
        public ChannelVM Channel { get; set; }
        public UserVM User { get; set; }
    }
}
