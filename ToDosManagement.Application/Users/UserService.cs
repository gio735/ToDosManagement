using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ToDosManagement.Application.Users.Repositories;
using ToDosManagement.Application.Users.Requests;
using ToDosManagement.Application.Users.Responses;
using ToDosManagement.Domain.Models;

namespace ToDosManagement.Application.Users
{
    public class UserService : IUserService
    {
        private const string SECRET_KEY = "ILHYSMBORITMHBTHA";
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;
        private readonly string _userId;
        public UserService(IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
            _userId = _httpContextAccessor.HttpContext.User.Claims?.FirstOrDefault(x => x.Type == "UserId")?.Value;
        }
        private Task<bool> Authorized(int userId)
        {
            return Task.FromResult(_userId == userId.ToString());
        }

        public async Task AddAsync(CancellationToken cancellationToken, UserCreateRequest entity)
        {
            //throw new NotImplementedException();
            entity.Password = GenerateHash(entity.Password);
            await _userRepository.AddAsync(cancellationToken, entity.Adapt<User>());
        }
        public async Task<User> AuthenticateAsync(CancellationToken cancellationToken, UserLoginRequest user)
        {
            user.Password = GenerateHash(user.Password);
            return await _userRepository.AuthenticateAsync(cancellationToken, user);
        }

        public async Task DeleteAsync(CancellationToken cancellationToken, params object[] key)
        {
            if (!await Authorized((int)key[0])) throw new UnauthorizedAccessException("Attempt to make action that current user is not authorized for.");
            await _userRepository.DeleteAsync(cancellationToken, key);
        }

        public Task<IEnumerable<User>> GetAll(CancellationToken cancellationToken)
        {
            throw new UnauthorizedAccessException("Not supported");
        }

        public async Task<User> GetByIdAsync(CancellationToken cancellationToken, params object[] key)
        {
            if (!await Authorized((int)key[0])) throw new UnauthorizedAccessException("Attempt to make action that current user is not authorized for.");
            return await _userRepository.GetByIdAsync(cancellationToken, key);
        }

        public async Task UpdateAsync(CancellationToken cancellationToken, UserUpdateRequest entity)
        {
            if (!await Authorized(entity.Id)) throw new UnauthorizedAccessException("Attempt to make action that current user is not authorized for.");
            entity.Password = GenerateHash(entity.Password);
            await _userRepository.UpdateAsync(cancellationToken, entity.Adapt<User>());
        }

        public async Task UpdatePatchAsync(CancellationToken cancellationToken, JsonPatchDocument<User> patchDocument)
        {
            patchDocument.Operations.RemoveAll(x => x.path.Contains("/Username") || x.path.Contains("/ToDos"));
            var password = patchDocument.Operations.FirstOrDefault(x => x.path == "/Password");
            password.value = GenerateHash((string)password.value);
            await _userRepository.UpdatePatchAsync(cancellationToken, int.Parse(_userId), patchDocument);
        }

        private string GenerateHash(string input)
        {
            using (SHA512 sha = SHA512.Create())
            {
                byte[] bytes = Encoding.ASCII.GetBytes(input + SECRET_KEY);
                byte[] hashBytes = sha.ComputeHash(bytes);

                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }

                return sb.ToString();
            }
        }
    }
}
