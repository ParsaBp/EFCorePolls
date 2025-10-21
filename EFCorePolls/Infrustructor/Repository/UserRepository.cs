using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFCorePolls.Contract.IRepozitory;
using EFCorePolls.DTO;
using EFCorePolls.Entity;
using Microsoft.EntityFrameworkCore;

namespace EFCorePolls.Infrustructor.Repository
{
  public  class UserRepository : IUserRepository
  {
      private readonly AppDbContext _appDb;

      public UserRepository()
      {
          _appDb = new AppDbContext();
      }
        public bool CheckUsername(string username)
        {
            return _appDb.Users.Any(u => u.UserName == username);
        }

        public LoginDto GetUserInfo(string username, string password)
        {
            return _appDb.Users
                .Where(u => u.UserName == username && u.Password == password)
                .Select(u => new LoginDto { Id = u.Id, Role = u.Role })
                .FirstOrDefault();
        }

        public void Register(User user)
        {

            _appDb.Users.Add(user);
            _appDb.SaveChanges();
        }

        public bool CheckUserId(int userId)
        {
            return _appDb.Users.Any(u => u.Id == userId);
        }
    }
}
