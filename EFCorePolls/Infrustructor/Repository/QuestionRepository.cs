using EFCorePolls.Contract.IRepozitory;
using EFCorePolls.DTO;
using EFCorePolls.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCorePolls.Infrustructor.Repository
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly AppDbContext _appDb;

        public QuestionRepository()
        {
            _appDb = new AppDbContext();
        }

        public void CreateQuestion(Question question)
        {
            _appDb.Questions.Add(question);
            _appDb.SaveChanges();
        }

        public List<Option> GetOptionsFromQuestion(int questionId)
        {
            return _appDb.Questions
                .Where(q => q.Id == questionId)
                .SelectMany(q => q.Options)
                .Include(o => o.Question)
                .ToList();
        }

        public List<ShowQuestionTextDto> GetQuestion(int pollId)
        {
            return _appDb.Questions.Where(q => q.PollId == pollId).Select(q => new ShowQuestionTextDto
            {
                Text = q.Text
            }).ToList();

        }
    }
}
