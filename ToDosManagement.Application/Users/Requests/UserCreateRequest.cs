using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDosManagement.Application.Users.Requests
{
    public class UserCreateRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
