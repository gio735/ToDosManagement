using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDosManagement.Application.Subtasks.Responses
{
    public class SubtaskResponseModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ToDoId { get; set; }
    }
}
