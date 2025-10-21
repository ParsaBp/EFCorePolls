using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFCorePolls.DTO;
using EFCorePolls.Entity;

namespace EFCorePolls.Contract.IRepozitory
{
   public interface IUserRepository
   {
       bool CheckUsername(string username);
       LoginDto GetUserInfo(string username, string password);
       void Register(User user);
       bool CheckUserId(int userId);

    }
}
