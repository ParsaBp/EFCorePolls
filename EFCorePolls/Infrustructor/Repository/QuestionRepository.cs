using EFCorePolls.Contract.IRepozitory;
using EFCorePolls.DTO;
using EFCorePolls.Entity;
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

        public void CreateQuestio(Question question)
        {
            _appDb.Questions.Add(question);
            _appDb.SaveChanges();
        }

        public List<Option> GetOptionsFromQuestion(int questionId)
        {
            return _appDb.Questions
                .Where(q => q.Id == questionId)
                .SelectMany(q => q.Options)
                .ToList();
        }


    }
}
