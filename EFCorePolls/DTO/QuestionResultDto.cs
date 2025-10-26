using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCorePolls.DTO
{
    public class QuestionResultDto
    {
        public string QuestionText { get; set; }
        public List<OptionResultDto> Options { get; set; }
    }
}
