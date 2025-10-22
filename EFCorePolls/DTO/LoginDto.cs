using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFCorePolls.Enums;

namespace EFCorePolls.DTO
{
   public class LoginDto
    {
        public int Id { get; set; }
        public UserEnum Role { get; set; }

        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string UserName { get; set; }

    }
}
