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
                return new ResultDto { IsSuccess = true, Message = "The poll deleted successfully" };
            }
            return new ResultDto { IsSuccess = false, Message = "The poll Not deleted " };
        }

        public List<PollResultDto> ShowPollResult(int pollId)
        {
            return _appDb.Options
                .Include(o => o.Votes)
                .Include(o => o.Question)
                    .ThenInclude(q => q.Poll)
                        .Where(o => o.Question.PollId == pollId)
                .Select(o => new PollResultDto
                {
                    QuestionText = o.Question.Text,
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
                .Include(q => q.Poll)
                .Select(q => new ShowQuestionsDto
                {
                    PollId = q.Poll.Id,
                    QuestionId = q.Id,
                    QuestionText = q.Text
                })
                .ToList();
        }

        public bool PollExists(int pollId)
        {
            return _appDb.Polls.Any(p => p.Id == pollId);
        }

        public Poll GetPollByTitle(string title)
        {
            return _appDb.Polls.FirstOrDefault(p => p.Title == title);
        }
    }
}
