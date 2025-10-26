using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using EFCorePolls.Enums;

namespace EFCorePolls.Entity
{
   public  class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public UserEnum Role { get; set; }
        public List<Vote> Votes { get; set; }
    }

}
