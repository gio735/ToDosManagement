using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDosManagement.Application.Subtasks.Requests;
using ToDosManagement.Domain.Models;

namespace ToDosManagement.Application.Subtasks
{
    public interface ISubtaskService
    {
        public Task AddAsync(CancellationToken cancellationToken, SubtaskCreateRequest entity);
        public Task UpdateAsync(CancellationToken cancellationToken, SubtaskUpdateRequest entity);
        public Task UpdatePatchAsync(CancellationToken cancellationToken, int id, JsonPatchDocument<Subtask> patchDocument);
        public Task<Subtask> GetByIdAsync(CancellationToken cancellationToken, params object[] key);
        public Task<IEnumerable<Subtask>> GetAll(CancellationToken cancellationToken);
        public Task DeleteAsync(CancellationToken cancellationToken, params object[] key);
    }
}
