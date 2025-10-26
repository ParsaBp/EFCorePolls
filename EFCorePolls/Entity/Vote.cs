using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCorePolls.Entity
{
    public class Vote
    {
        public int Id { get; set; }
        public DateTime VotedAt { get; set; }

        public int OptionId { get; set; }
        public Option Option { get; set; }

        public int? UserId { get; set; }
        public string UserName { get; set; }
        public User? User { get; set; }
    }
}
