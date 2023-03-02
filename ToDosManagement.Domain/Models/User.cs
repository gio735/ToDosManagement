﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ToDosManagement.Domain.Models
{
    public class User : EntityModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public List<ToDo> ToDos { get; set; }
    }
}
