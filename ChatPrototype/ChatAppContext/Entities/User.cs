

namespace ChatAppContext.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; } 
        public Token Token { get; set; }
        public ICollection<LogEntry> LogEntries { get; set; }
        public ICollection<UserServer> UserServers { get; set; }

    }
}
