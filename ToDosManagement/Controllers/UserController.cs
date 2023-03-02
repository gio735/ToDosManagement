using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Serilog;
using System.Security.Claims;
using ToDosManagement.API.Infrastructure.Auth.JWT;
using ToDosManagement.Application.Users;
using ToDosManagement.Application.Users.Requests;
using ToDosManagement.Application.Users.Responses;
using ToDosManagement.Domain.Models;

namespace ToDosManagement.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IOptions<JWTConfiguration> _options;
        public UserController(IUserService userService, IOptions<JWTConfiguration> options)
        {
            _userService = userService;
            _options = options;
        }
        [HttpGet]
        public async Task<UserResponseModel> Get(CancellationToken cancellationToken)
        {
            var id = HttpContext.User.Claims?.FirstOrDefault(x => x.Type == "UserId")?.Value;
            return (await _userService.GetByIdAsync(cancellationToken, int.Parse(id))).Adapt<UserResponseModel>();
        }

        [Route("Register")]
        [HttpPost]
        [AllowAnonymous]
        public async Task Register(CancellationToken cancellation, UserCreateRequest user)
        {
            await _userService.AddAsync(cancellation, user);
        }

        [Route("LogIn")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<string> LogIn(CancellationToken cancellation, UserLoginRequest request)
        {
            var result = await _userService.AuthenticateAsync(cancellation, request);
            return JWTHelper.GenerateSecurityToken(result, _options);
        }

        [HttpPut]
        public async Task Update(CancellationToken cancellationToken, [FromBody] UserUpdateRequest request)
        {
            await _userService.UpdateAsync(cancellationToken, request);
        }
        [HttpPatch]
        public async Task UpdatePatch(CancellationToken cancellationToken, [FromBody] JsonPatchDocument<User> request)
        {
            await _userService.UpdatePatchAsync(cancellationToken, request);
        }
        [HttpDelete("{id}")]
        public async Task Delete(CancellationToken cancellationToken, int id)
        {
            await _userService.DeleteAsync(cancellationToken, id);
        }
    }
}
