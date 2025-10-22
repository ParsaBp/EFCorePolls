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
        public void CreateQuestio(Question question);
    }
}
