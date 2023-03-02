using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ToDosManagement.Application.ToDos;
using ToDosManagement.Application.ToDos.Requests;
using ToDosManagement.Application.ToDos.Responses;
using ToDosManagement.Application.Users.Requests;
using ToDosManagement.Application.Users.Responses;
using ToDosManagement.Domain.Models;

namespace ToDosManagement.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class ToDoController : Controller
    {
        private readonly IToDoService _toDoService;

        public ToDoController(IToDoService toDoService)
        {
            _toDoService = toDoService;
        }

        [HttpGet("{id}")]
        public async Task<ToDoResponseModel> Get(CancellationToken cancellationToken, int id)
        {
            return (await _toDoService.GetByIdAsync(cancellationToken, id)).Adapt<ToDoResponseModel>();
        }
        [HttpGet]
        public async Task<List<ToDoResponseModel>> GetAll(CancellationToken cancellationToken)
        {
            return (await _toDoService.GetAll(cancellationToken)).Adapt<List<ToDoResponseModel>>();
        }
        [HttpPost]
        public async Task Add(CancellationToken cancellation, ToDoCreateRequest user)
        {
            await _toDoService.AddAsync(cancellation, user);
        }
        [HttpPut]
        public async Task Update(CancellationToken cancellationToken, [FromBody] ToDoUpdateRequest request)
        {
            await _toDoService.UpdateAsync(cancellationToken, request);
        }
        [HttpPatch]
        public async Task UpdatePatch(CancellationToken cancellationToken, int id,[FromBody] JsonPatchDocument<ToDo> request)
        {
            await _toDoService.UpdatePatchAsync(cancellationToken, id, request);
        }
        [HttpDelete("{id}")]
        public async Task Delete(CancellationToken cancellationToken, int id)
        {
            await _toDoService.DeleteAsync(cancellationToken, id);
        }
    }
}
