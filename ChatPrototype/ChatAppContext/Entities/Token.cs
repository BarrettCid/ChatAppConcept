using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ChatAppContext.Entities
{
    public class Token
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid TokenId { get; set; }
        public int UserId { get; set; }
        public DateTime IssueDate { get; set; }
        public User User { get; set; }
    }
}
