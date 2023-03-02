using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDosManagement.Application.Subtasks.Repositories;
using ToDosManagement.Domain;
using ToDosManagement.Domain.Models;

namespace ToDosManagement.Infrastructure.Repositories
{
    public class SubtaskRepository : RepositoryBase<Subtask>, ISubtaskRepository
    {
        public SubtaskRepository(DbContext dbContext) : base(dbContext)
        {
        }
        public async Task<IEnumerable<Subtask>> GetAll(CancellationToken cancellationToken, int id)
        {
            return (await GetAll(cancellationToken)).Where(e => e.ToDoItem.Owner.Id == id && (e as EntityModel).State != State.Deleted);
        }
    }
}
