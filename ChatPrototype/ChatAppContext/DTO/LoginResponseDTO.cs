using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppContext.DTO
{
    public class LoginResponseDTO
    {
        public string EmailAddress { get; set; }
        public Guid TokenId { get; set; }
        public DateTime IssueDate { get; set; }
    }
}
