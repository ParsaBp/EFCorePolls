using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCorePolls.Entity
{
   public class Poll
    {
        public int Id { get; set; }
        public string Question { get; set; }

        public List<Option> Options { get; set; } 
        public List<Vote> Votes { get; set; } 
        public List<Question> Questions { get; set; }
    }
}
