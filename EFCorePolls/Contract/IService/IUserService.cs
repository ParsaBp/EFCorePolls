using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFCorePolls.DTO;
using EFCorePolls.Enums;
using Microsoft.Identity.Client;

namespace EFCorePolls.Contract.IService
{
   public interface IUserService
   {
       LoginDto login(string username, string password);
       ResultDto Register(string username, string password, UserEnum Role);
    }
}
