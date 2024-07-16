using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppContext.Entities
{
    [Table("UserServer", Schema = "Chat")]
    public class UserServer
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public Guid ServerId { get; set; }
        public Server Server { get; set; }
    }
}
