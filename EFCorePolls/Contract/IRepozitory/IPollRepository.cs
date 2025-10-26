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
        public void CreatePoll(Poll poll);

        public ResultDto Delete(int pollId);

        public List<PollResultDto> ShowPollResult(int pollId);

        public List<ShowQuestionsDto> ShowPolls();

        public bool PollExists(int pollId);

        public Poll GetPollByTitle(string title);

    }
}
