using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDosManagement.Application.Subtasks.Repositories;
using ToDosManagement.Application.ToDos.Repositories;
using ToDosManagement.Domain;
using ToDosManagement.Domain.Models;

namespace ToDosManagement.Infrastructure.Repositories
{
    public class ToDoRepository : RepositoryBase<ToDo>, IToDoRepository
    {
        private readonly ISubtaskRepository _subtaskRepository;
        public ToDoRepository(DbContext dbContext, ISubtaskRepository subtaskRepository) : base(dbContext)
        {
            _subtaskRepository = subtaskRepository;
        }

        public override async Task DeleteAsync(CancellationToken cancellationToken, params object[] key)
        {
            var target = await GetByIdAsync(cancellationToken, key);
            await _subtaskRepository.DeleteRangeAsync(cancellationToken, target.Subtasks);
            await base.DeleteAsync(cancellationToken, key);
        }
        public override async Task DeleteRangeAsync(CancellationToken cancellationToken, List<ToDo> entities)
        {
            foreach(var entity in entities)
            {
                await _subtaskRepository.DeleteRangeAsync(cancellationToken, entity.Subtasks);
            }
            await base.DeleteRangeAsync(cancellationToken, entities);
        }
        public async Task<IEnumerable<ToDo>> GetAll(CancellationToken cancellationToken, int id)
        {
            return (await GetAll(cancellationToken)).Where(e => e.Owner.Id == id && (e as EntityModel).State != State.Deleted);
        }
    }
}
