using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDosManagement.Application.Subtasks.Requests
{
    public class SubtaskCreateRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int ToDoId { get; set; }
    }
}
