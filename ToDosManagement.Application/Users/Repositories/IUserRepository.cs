using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDosManagement.Application.Users.Requests;
using ToDosManagement.Domain.Models;

namespace ToDosManagement.Application.Users.Repositories
{
    public interface IUserRepository
    {
        public bool Exists(User entity);
        public Task AddAsync(CancellationToken cancellationToken, User entity);
        public Task UpdateAsync(CancellationToken cancellationToken, User entity);
        public Task UpdatePatchAsync(CancellationToken cancellationToken, int id, JsonPatchDocument<User> patchDocument);
        public Task<User> GetByIdAsync(CancellationToken cancellationToken, params object[] key);
        public Task DeleteAsync(CancellationToken cancellationToken, params object[] key);
        public Task<User> AuthenticateAsync(CancellationToken cancellationToken, UserLoginRequest entity);


    }
}
