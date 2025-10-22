using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCorePolls.DTO
{
   public class PollResultDto
   {
        public string OptionText { get; set; }
        public int VoteCount { get; set; }
        public List<string> Participants { get; set; }
    }
}
