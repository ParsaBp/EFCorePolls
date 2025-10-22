using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFCorePolls.DTO;
using EFCorePolls.Entity;

namespace EFCorePolls.Contract.IRepozitory
{
   public interface IPollRepository
   {
       void CreatePoll(Poll poll);
       ResultDto Delete(int pollId);
       PollResultDto ShowPollResult(int pollId);

   }
}
