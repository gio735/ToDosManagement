using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDosManagement.Application.Subtasks.Repositories;
using ToDosManagement.Application.Subtasks.Requests;
using ToDosManagement.Application.ToDos.Repositories;
using ToDosManagement.Application.Users.Repositories;
using ToDosManagement.Domain.Models;

namespace ToDosManagement.Application.Subtasks
{
    public class SubtaskService : ISubtaskService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IToDoRepository _toDoRepository;
        private readonly ISubtaskRepository _subtaskRepository;
        private readonly string _userId;
        public SubtaskService(IHttpContextAccessor httpContextAccessor, IToDoRepository toDoRepository, ISubtaskRepository subtaskRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _toDoRepository = toDoRepository;
            _subtaskRepository = subtaskRepository;
            _userId = _httpContextAccessor.HttpContext.User.Claims?.FirstOrDefault(x => x.Type == "UserId")?.Value;
        }
        private Task<bool> Authorized(int userId)
        {
            return Task.FromResult(_userId == userId.ToString());
        }
        public async Task AddAsync(CancellationToken cancellationToken, SubtaskCreateRequest entity)
        {
            var toDo = await _toDoRepository.GetByIdAsync(cancellationToken, entity.ToDoId);
            if (!await Authorized(toDo.Owner.Id)) throw new UnauthorizedAccessException("Attempt to make action that current user is not authorized for.");
            var entityToAdd = entity.Adapt<Subtask>();
            entityToAdd.ToDoItem = toDo;
            await _subtaskRepository.AddAsync(cancellationToken, entityToAdd);
        }

        public async Task DeleteAsync(CancellationToken cancellationToken, params object[] key)
        {
            var target = await GetByIdAsync(cancellationToken, key);
            if (!await Authorized(target.ToDoItem.Owner.Id)) throw new UnauthorizedAccessException("Attempt to make action that current user is not authorized for.");
            await _subtaskRepository.DeleteAsync(cancellationToken, key);
        }

        public async Task<IEnumerable<Subtask>> GetAll(CancellationToken cancellationToken)
        {
            return await _subtaskRepository.GetAll(cancellationToken, int.Parse(_userId));
        }

        public async Task<Subtask> GetByIdAsync(CancellationToken cancellationToken, params object[] key)
        {
            var target = await _subtaskRepository.GetByIdAsync(cancellationToken, key);
            if (!await Authorized(target.ToDoItem.Owner.Id)) throw new UnauthorizedAccessException("Attempt to make action that current user is not authorized for.");
            return target;
        }

        public async Task UpdateAsync(CancellationToken cancellationToken, SubtaskUpdateRequest entity)
        {
            var target = await _subtaskRepository.GetByIdAsync(cancellationToken, entity.Id);
            if (!await Authorized(target.ToDoItem.Owner.Id)) throw new UnauthorizedAccessException("Attempt to make action that current user is not authorized for.");
            await _subtaskRepository.UpdateAsync(cancellationToken, entity.Adapt<Subtask>());
        }

        public async Task UpdatePatchAsync(CancellationToken cancellationToken, int id, JsonPatchDocument<Subtask> patchDocument)
        {
            patchDocument.Operations.RemoveAll(x => x.path.Contains("/ToDoItem"));
            var target = await _subtaskRepository.GetByIdAsync(cancellationToken, id);
            if (!await Authorized(target.ToDoItem.Owner.Id)) throw new UnauthorizedAccessException("Attempt to make action that current user is not authorized for.");

        }
    }
}
