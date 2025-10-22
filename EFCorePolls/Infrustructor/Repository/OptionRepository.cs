using EFCorePolls.Contract.IRepozitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCorePolls.Infrustructor.Repository
{
    public class OptionRepository : IOptionRepository
    {
        private readonly AppDbContext _appDb;

        public OptionRepository()
        {
            _appDb = new AppDbContext();
        }
        public bool OptionExists(int optionId)
        {
            return _appDb.Options.Any(o => o.Id == optionId);
        }
    }
}
