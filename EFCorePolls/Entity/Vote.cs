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

        // Each Vote belongs to an Option
        public int OptionId { get; set; }
        public Option Option { get; set; }

        // Remove PollId and QuestionId — they can be accessed via Option
        // public int PollId { get; set; }
        // public Poll Poll { get; set; }
        // public int QuestionId { get; set; }
        // public Question Question { get; set; }

        // User information (optional)
        public int? UserId { get; set; }
        public string UserName { get; set; }
        public User? User { get; set; }
    }
}
