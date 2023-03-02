using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDosManagement.Domain.Models
{
    public class ToDo : EntityModel
    {
        public string Title { get; set; }
        public ToDoState ToDoState { get; set; }
        public DateTime Deadline { get; set; }
        public List<Subtask> Subtasks { get; set; }
        public User Owner { get; set; }
    }
}
