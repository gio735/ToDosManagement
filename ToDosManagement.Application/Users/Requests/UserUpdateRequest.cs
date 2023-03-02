using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDosManagement.Application.Users.Requests
{
    public class UserUpdateRequest //username is identifier so can't be changed
    {
        public int Id { get; set; }
        public string Password { get; set; }
    }
}
