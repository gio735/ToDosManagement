using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDosManagement.Domain.Models;

namespace ToDosManagement.Application.ToDos.Repositories
{
    public interface IToDoRepository
    {
        public bool Exists(ToDo entity);
        public Task AddAsync(CancellationToken cancellationToken, ToDo entity);
        public Task UpdateAsync(CancellationToken cancellationToken, ToDo entity);
        public Task UpdatePatchAsync(CancellationToken cancellationToken, int id, JsonPatchDocument<ToDo> patchDocument);
        public Task<ToDo> GetByIdAsync(CancellationToken cancellationToken, params object[] key);
        public Task<IEnumerable<ToDo>> GetAll(CancellationToken cancellationToken, int id);
        public Task DeleteAsync(CancellationToken cancellationToken, params object[] key);
        public Task DeleteRangeAsync(CancellationToken cancellationToken, List<ToDo> entities);
    }
}
