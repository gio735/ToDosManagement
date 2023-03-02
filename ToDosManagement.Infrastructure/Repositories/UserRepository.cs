using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDosManagement.Application.ToDos.Repositories;
using ToDosManagement.Application.Users.Repositories;
using ToDosManagement.Application.Users.Requests;
using ToDosManagement.Domain;
using ToDosManagement.Domain.Models;

namespace ToDosManagement.Infrastructure.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        private readonly IToDoRepository _toDoRepository;
        public UserRepository(DbContext dbContext, IToDoRepository toDoRepository) : base(dbContext) 
        {
            _toDoRepository = toDoRepository;
        }
        public async Task<bool> Exists(string username)
        {
            var result = _dbSet.FirstOrDefault(x => x.Username.ToLower() == username.ToLower());
            return result != null;
        }
        public override async Task DeleteAsync(CancellationToken cancellationToken, params object[] key)
        {
            var target = await GetByIdAsync(cancellationToken, key);
            await _toDoRepository.DeleteRangeAsync(cancellationToken, target.ToDos);
            await base.DeleteAsync(cancellationToken, key);
        }
        public override async Task AddAsync(CancellationToken cancellationToken, User entity)
        {
            if(await Exists(entity.Username)) throw new ExistingUsernameException();
            await base.AddAsync(cancellationToken, entity);
        }

        public Task<User> AuthenticateAsync(CancellationToken cancellationToken, UserLoginRequest entity)
        {
            var result = _dbSet.FirstOrDefault(e => e.Username.ToLower() == entity.Username.ToLower() && e.Password == entity.Password);
            if (result == null) throw new InvalidLoginInputException();
            if ((result as EntityModel).State == State.Deleted) throw new AttemptToUseDeletedEntityException();
            return Task.FromResult(result);
        }
    }
}