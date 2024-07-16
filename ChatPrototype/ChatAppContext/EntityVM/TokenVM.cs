using ChatAppContext.Entities;

namespace ChatAppContext.EntityVM
{
    public class TokenVM
    {
        public Guid TokenId { get; set; }
        public int UserId { get; set; }
        public DateTime IssueDate { get; set; }
    }
}
