using ChatAppContext.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppContext.EntityVM
{
    public class UserServerVM
    {
        public int UserId { get; set; }
        public UserVM User { get; set; }

        public Guid ServerId { get; set; }
        public ServerVM Server { get; set; }
    }
}
