using EFCorePolls.Contract.IRepozitory;
using EFCorePolls.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCorePolls.Infrustructor.Repository
{
    public class VoteRepository : IVoteRepository
    {
        private readonly AppDbContext _appDb;

        public VoteRepository()
        {
            _appDb = new AppDbContext();
        }
        public bool UserHasVoted(int pollId, string userName)
        {
            return _appDb.Votes.Any(v => v.PollId == pollId && v.UserName == userName);
        }
        public void AddVote(Vote vote)
        {
            _appDb.Votes.Add(vote);
            _appDb.SaveChanges();
        }
    }
}
