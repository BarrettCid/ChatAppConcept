using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppContext.DTO
{
    public class MessageDTO
    {
        public string Username { get; set; }
        public string ChannelId { get; set; }
        public string MessageId { get; set; }
        public string Message { get; set; }
        public string DateCreated { get; set; }
    }
}
