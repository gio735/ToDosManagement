using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDosManagement.Application.Users.Responses
{
    public class UserResponseModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public List<int> ToDos { get; set; }
    }
}
