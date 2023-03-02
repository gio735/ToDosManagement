using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDosManagement.Domain;

namespace ToDosManagement.Application.ToDos.Requests
{
    public class ToDoUpdateRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ToDoState ToDoState { get; set; }
        public DateTime Deadline { get; set; }
    }
}
