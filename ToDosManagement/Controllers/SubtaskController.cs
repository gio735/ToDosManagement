using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ToDosManagement.Application.Subtasks;
using ToDosManagement.Application.Subtasks.Requests;
using ToDosManagement.Application.Subtasks.Responses;
using ToDosManagement.Application.ToDos.Responses;
using ToDosManagement.Domain.Models;

namespace ToDosManagement.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class SubtaskController : Controller
    {
        private readonly ISubtaskService _subtaskService;

        public SubtaskController(ISubtaskService subtaskService)
        {
            _subtaskService = subtaskService;
        }
        [HttpGet("{id}")]
        public async Task<SubtaskResponseModel> Get(CancellationToken cancellationToken, int id)
        {
            return (await _subtaskService.GetByIdAsync(cancellationToken, id)).Adapt<SubtaskResponseModel>();
        }
        [HttpGet]
        public async Task<List<SubtaskResponseModel>> GetAll(CancellationToken cancellationToken)
        {
            return (await _subtaskService.GetAll(cancellationToken)).Adapt<List<SubtaskResponseModel>>();
        }

        [HttpPost]
        public async Task Add(CancellationToken cancellation, SubtaskCreateRequest user)
        {
            await _subtaskService.AddAsync(cancellation, user);
        }
        [HttpPut]
        public async Task Update(CancellationToken cancellationToken, [FromBody] SubtaskUpdateRequest request)
        {
            await _subtaskService.UpdateAsync(cancellationToken, request);
        }
        [HttpPatch]
        public async Task UpdatePatch(CancellationToken cancellationToken, int id, [FromBody] JsonPatchDocument<Subtask> request)
        {
            await _subtaskService.UpdatePatchAsync(cancellationToken, id, request);
        }
        [HttpDelete("{id}")]
        public async Task Delete(CancellationToken cancellationToken, int id)
        {
            await _subtaskService.DeleteAsync(cancellationToken, id);
        }
    }
}
