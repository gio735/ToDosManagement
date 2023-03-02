using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDosManagement.Application.ToDos.Repositories;
using ToDosManagement.Application.ToDos.Requests;
using ToDosManagement.Application.Users.Repositories;
using ToDosManagement.Domain.Models;

namespace ToDosManagement.Application.ToDos
{
    public class ToDoService : IToDoService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IToDoRepository _toDoRepository;
        private readonly IUserRepository _userRepository;
        private readonly string _userId;
        public ToDoService(IHttpContextAccessor httpContextAccessor, IToDoRepository toDoRepository, IUserRepository userRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _toDoRepository = toDoRepository;
            _userRepository = userRepository;
            _userId = _httpContextAccessor.HttpContext.User.Claims?.FirstOrDefault(x => x.Type == "UserId")?.Value;
        }
        private Task<bool> Authorized(int userId)
        {
            return Task.FromResult(_userId == userId.ToString());
        }

        public async Task AddAsync(CancellationToken cancellationToken, ToDoCreateRequest entity)
        {
            var user = await _userRepository.GetByIdAsync(cancellationToken, int.Parse(_userId));
            ToDo entityToAdd = entity.Adapt<ToDo>();
            entityToAdd.Owner = user;
            await _toDoRepository.AddAsync(cancellationToken, entityToAdd);
        }

        public async Task DeleteAsync(CancellationToken cancellationToken, params object[] key)
        {
            var target = await _toDoRepository.GetByIdAsync(cancellationToken, key);
            if(!await Authorized(target.Owner.Id)) throw new UnauthorizedAccessException("Attempt to make action that current user is not authorized for.");
            await _toDoRepository.DeleteAsync(cancellationToken, key);
        }

        public async Task<IEnumerable<ToDo>> GetAll(CancellationToken cancellationToken)
        {
            return await _toDoRepository.GetAll(cancellationToken, int.Parse(_userId));
        }

        public async Task<ToDo> GetByIdAsync(CancellationToken cancellationToken, params object[] key)
        {
            var target = await _toDoRepository.GetByIdAsync(cancellationToken, key);
            if (!await Authorized(target.Owner.Id)) throw new UnauthorizedAccessException("Attempt to make action that current user is not authorized for.");
            return target;
        }

        public async Task UpdateAsync(CancellationToken cancellationToken, ToDoUpdateRequest entity)
        {
            var target = await _toDoRepository.GetByIdAsync(cancellationToken, entity.Id);
            if (!await Authorized(target.Owner.Id)) throw new UnauthorizedAccessException("Attempt to make action that current user is not authorized for.");
            await _toDoRepository.UpdateAsync(cancellationToken, entity.Adapt<ToDo>());
        }

        public async Task UpdatePatchAsync(CancellationToken cancellationToken, int id, JsonPatchDocument<ToDo> patchDocument)
        {
            patchDocument.Operations.RemoveAll(x => x.path.Contains("/Owner") || x.path.Contains("/Subtasks"));
            var target = await _toDoRepository.GetByIdAsync(cancellationToken, id);
            if (!await Authorized(target.Owner.Id)) throw new UnauthorizedAccessException("Attempt to make action that current user is not authorized for.");
            await _toDoRepository.UpdatePatchAsync(cancellationToken, id, patchDocument);
        }
    }
}
