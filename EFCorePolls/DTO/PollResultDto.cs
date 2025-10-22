using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCorePolls.DTO
{
   public class PollResultDto
   {
       public bool IsSucces { get; set; }
       public string Message { get; set; }
       public int PollId { get; set; } 

    }
}
