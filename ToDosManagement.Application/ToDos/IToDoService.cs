using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDosManagement.Application.ToDos.Requests;
using ToDosManagement.Application.Users.Requests;
using ToDosManagement.Domain.Models;

namespace ToDosManagement.Application.ToDos
{
    public interface IToDoService
    {
        public Task AddAsync(CancellationToken cancellationToken, ToDoCreateRequest entity);
        public Task UpdateAsync(CancellationToken cancellationToken, ToDoUpdateRequest entity);
        public Task UpdatePatchAsync(CancellationToken cancellationToken, int id, JsonPatchDocument<ToDo> patchDocument);
        public Task<ToDo> GetByIdAsync(CancellationToken cancellationToken, params object[] key);
        public Task<IEnumerable<ToDo>> GetAll(CancellationToken cancellationToken);
        public Task DeleteAsync(CancellationToken cancellationToken, params object[] key);
    }
}
