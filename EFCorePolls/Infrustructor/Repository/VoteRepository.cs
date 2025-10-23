using EFCorePolls.Contract.IRepozitory;
using EFCorePolls.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCorePolls.Infrustructor.Repository
{
    public class VoteRepository:IVoteRepository
    {
        private readonly AppDbContext _appDb;

        public VoteRepository()
        {
            _appDb = new AppDbContext();
        }
      public bool AnyUserHasVoted(int pollId)
        {
            return _appDb.Votes
                         .Include(v => v.Option)
                         .ThenInclude(o => o.Question)
                         .Any(v => v.Option.Question.PollId == pollId);
        }
        public void AddVote(Vote vote)
        {
            _appDb.Votes.Add(vote);
            _appDb.SaveChanges();
        }

        public bool UserHasVoted(int pollId, int userId)
         {
            return _appDb.Votes
              .Include(v => v.Option)
              .ThenInclude(o => o.Question)
              .Any(v => v.Option.Question.PollId == pollId && v.UserId == userId);

         }
    }
}
