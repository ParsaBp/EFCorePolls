using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCorePolls.DTO
{
    public class ShowQuestionsDto
    {
        public int PollId { get; set; }
        public int QuestionId { get; set; }

        public string QuestionText { get; set; }
    }
}
