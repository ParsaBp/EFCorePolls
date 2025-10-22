using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFCorePolls.Contract.IRepozitory;
using EFCorePolls.DTO;
using EFCorePolls.Entity;

namespace EFCorePolls.Infrustructor.Repository
{
   public class PollRepository : IPollRepository
   {
       private readonly AppDbContext _appDb;

       public PollRepository()
       {
           _appDb = new AppDbContext();
       }
        public void CreatePoll(Poll poll)
        {
            _appDb.Polls.Add(poll);
            _appDb.SaveChanges();
            
        }

        public ResultDto Delete(int pollId)
        {
           var poll = _appDb.Polls.FirstOrDefault(p=>p.Id == pollId);
            if (poll != null)
            {
                _appDb.Polls.Remove(poll);
                  _appDb.SaveChanges();
                  new ResultDto { IsSuccess = true, Message = "The poll deleted successfully" };
            }
          
            return new ResultDto { IsSuccess = false, Message = "The poll Not deleted " };

        }

        public PollResultDto ShowPollResult(int pollId)
        {
            _appDb.Polls.Where(p=>p.Id == pollId)

        }
    }
}
