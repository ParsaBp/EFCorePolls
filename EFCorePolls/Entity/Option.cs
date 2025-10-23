using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCorePolls.Entity
{
    public class Option
    {
        public int Id { get; set; }
        public string Text { get; set; }

        // Each Option belongs to a Question
        public int QuestionId { get; set; }
        public Question Question { get; set; }

        // Remove PollId — Option no longer directly belongs to Poll
        // public int PollId { get; set; }
        // public Poll Poll { get; set; }

        // Each Option can have many Votes
        public List<Vote> Votes { get; set; }
    }
}
