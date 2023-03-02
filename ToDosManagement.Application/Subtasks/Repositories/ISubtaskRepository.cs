using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDosManagement.Domain.Models;

namespace ToDosManagement.Application.Subtasks.Repositories
{
    public interface ISubtaskRepository
    {
        public bool Exists(Subtask entity);
        public Task AddAsync(CancellationToken cancellationToken, Subtask entity);
        public Task UpdateAsync(CancellationToken cancellationToken, Subtask entity);
        public Task UpdatePatchAsync(CancellationToken cancellationToken, int id, JsonPatchDocument<Subtask> patchDocument);
        public Task<Subtask> GetByIdAsync(CancellationToken cancellationToken, params object[] key);
        public Task<IEnumerable<Subtask>> GetAll(CancellationToken cancellationToken, int id);
        public Task DeleteAsync(CancellationToken cancellationToken, params object[] key);
        public Task DeleteRangeAsync(CancellationToken cancellationToken, List<Subtask> entities);
    }
}
