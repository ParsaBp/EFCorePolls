using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFCorePolls.Contract.IRepozitory;
using EFCorePolls.DTO;
using EFCorePolls.Entity;
using Microsoft.EntityFrameworkCore;

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
            var poll = _appDb.Polls.FirstOrDefault(p => p.Id == pollId);
            if (poll != null)
            {
                _appDb.Polls.Remove(poll);
                _appDb.SaveChanges();
                new ResultDto { IsSuccess = true, Message = "The poll deleted successfully" };
            }

            return new ResultDto { IsSuccess = false, Message = "The poll Not deleted " };

        }

        public List<PollResultDto> ShowPollResult(int pollId)
        {
            return _appDb.Options
                           .Where(o => o.PollId == pollId)
                           .Include(o => o.Votes)
                           .Select(o => new PollResultDto
                           {
                               OptionText = o.Text,
                               VoteCount = o.Votes.Count,
                               Participants = o.Votes
                                               .Select(v => v.UserName)
                                               .ToList()
                           })
                           .ToList();
        }

        public List<ShowQuestionsDto> ShowPolls()
        {
            return _appDb.Questions
                .Select(q => new ShowQuestionsDto
            {
                Id = q.PollId,
                QuestionText = q.Text
            }).ToList(); 
        }

        public bool PollExists(int pollId)
        {
            return _appDb.Polls.Any(p => p.Id == pollId);
        }
    }
}
