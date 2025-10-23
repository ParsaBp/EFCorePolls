using EFCorePolls.DTO;
using EFCorePolls.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCorePolls.Contract.IRepozitory
{
    public interface IQuestionRepository
    {
        public void CreateQuestion(Question question);
        public List<Option> GetOptionsFromQuestion(int questionId);
        ShowQuestionTextDto GetQuestion(int pollId);

    }
}
