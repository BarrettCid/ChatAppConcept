using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppContext.Entities
{
    public class LogEntry
    {
        public int LogEntryId { get; set; }
        public int UserId { get; set; }
        public string Action { get; set; }
        public string Message { get; set; }
        public DateTime DateLogged { get; set; }
        public User User { get; set; }
    }
}
