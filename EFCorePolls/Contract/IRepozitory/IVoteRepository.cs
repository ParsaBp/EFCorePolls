using EFCorePolls.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCorePolls.Contract.IRepozitory
{
    public interface IVoteRepository
    {
        public bool AnyUserHasVoted(int pollId);
        public void AddVote(Vote vote);
        public bool UserHasVoted(int pollId, int userId);
    }
}
