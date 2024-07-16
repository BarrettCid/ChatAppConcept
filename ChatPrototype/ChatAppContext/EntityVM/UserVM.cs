using ChatAppContext.Entities;

namespace ChatAppContext.EntityVM
{
    public class UserVM
    {
        public int UserId { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public Token Token { get; set; }
        public ICollection<UserServerVM> UserServers { get; set; }
    }
}
