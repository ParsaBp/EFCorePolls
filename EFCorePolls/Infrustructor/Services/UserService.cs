using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFCorePolls.Contract.IRepozitory;
using EFCorePolls.Contract.IService;
using EFCorePolls.DTO;
using EFCorePolls.Entity;
using EFCorePolls.Enums;
using EFCorePolls.Infrustructor.Repository;

namespace EFCorePolls.Infrustructor.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService()
        {
            _userRepository = new UserRepository();
        }
        public LoginDto login(string username, string password)
        {

            if (username == null)
            {
                return new LoginDto { IsSuccess = false, Message = " Username can not be empty ." };
            }
            if (password == null)
            {
                return new LoginDto { IsSuccess = false, Message = " Password can not be empty ." };
            }

            var userinfo = _userRepository.GetUserInfo(username, password);


            if (userinfo == null)
            {
                return new LoginDto { IsSuccess = false, Message = "Your Username or Password is wrong" };
            }
            else
            {
                return new LoginDto { IsSuccess = true, Message = "Wellcome", Role = userinfo.Role, Id = userinfo.Id };
            }
        }

        public ResultDto Register(string username, string password, UserEnum Role)
        {
            if (username == null)
            {
                return new ResultDto { IsSuccess = false, Message = " Username can not be empty ." };
            }
            if (password == null)
            {
                return new ResultDto { IsSuccess = false, Message = " Password can not be empty ." };
            }

            var Check = _userRepository.CheckUsername(username);


            if (!Check)
            {
                return new ResultDto { IsSuccess = false, Message = "Your Username already in use ! " };
            }

            var User = new User
            {
                UserName = username,
                Password = password,
                Role = Role,

            };
            _userRepository.Register(User);
            return new ResultDto { IsSuccess = true, Message = "Registration is Successful ." };
        }
    }

}
