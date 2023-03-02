using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDosManagement.Domain;

namespace ToDosManagement.Application.ToDos.Responses
{
    public class ToDoResponseModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ToDoState State { get; set; }
        public DateTime Deadline { get; set; }
        public List<int> Subtasks { get; set; }
    }
}
