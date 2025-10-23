using EFCorePolls.Contract.IRepozitory;
using EFCorePolls.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFCorePolls.DTO;

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
        public void CreateOption(Option option)
        {
            _appDb.Options.Add(option);
            _appDb.SaveChanges();
        }

        public void CreateOptionList(List<Option> options)
        {
            _appDb.Options.AddRange(options);
            _appDb.SaveChanges();
        }
    }
}
