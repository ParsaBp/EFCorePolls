using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCorePolls.Entity
{
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; }

        // Each Question belongs to a Poll
        public int PollId { get; set; }
        public Poll Poll { get; set; }

        // Each Question has many Options
        public List<Option> Options { get; set; }

        // Votes can be optional here (via Options), so you could remove
        // public List<Vote> Votes { get; set; } // optional
    }
}
