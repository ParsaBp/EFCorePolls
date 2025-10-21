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

        public int QuestionId { get; set; }
        public Question Question { get; set; }

        public int OptionId { get; set; }
        public Option Option { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int PollId { get; set; }
        public Poll Poll { get; set; }


        

    }
}
