using EFCorePolls.DTO;
using EFCorePolls.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCorePolls.Contract.IRepozitory
{
    public interface IOptionRepository
    {
        public bool OptionExists(int optionId);
        public void CreateOption(Option option);
        public void CreateOptionList(List<Option> options);

    }
}
