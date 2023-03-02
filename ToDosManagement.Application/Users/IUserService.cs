using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDosManagement.Application.Users.Requests;
using ToDosManagement.Domain.Models;

namespace ToDosManagement.Application.Users
{
    public interface IUserService
    {
        public Task AddAsync(CancellationToken cancellationToken, UserCreateRequest entity);
        public Task UpdateAsync(CancellationToken cancellationToken, UserUpdateRequest entity);
        public Task UpdatePatchAsync(CancellationToken cancellationToken, JsonPatchDocument<User> patchDocument);
        public Task<User> GetByIdAsync(CancellationToken cancellationToken, params object[] key);
        public Task<IEnumerable<User>> GetAll(CancellationToken cancellationToken);
        public Task DeleteAsync(CancellationToken cancellationToken, params object[] id);
        public Task<User> AuthenticateAsync(CancellationToken cancellationToken, UserLoginRequest entity);

    }
}
