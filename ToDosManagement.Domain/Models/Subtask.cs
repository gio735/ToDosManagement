using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDosManagement.Domain.Models
{
    public class Subtask : EntityModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public ToDo ToDoItem { get; set; }
    }
}
