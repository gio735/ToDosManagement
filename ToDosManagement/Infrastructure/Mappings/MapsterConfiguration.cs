using Mapster;
using ToDosManagement.Application.Subtasks.Requests;
using ToDosManagement.Application.Subtasks.Responses;
using ToDosManagement.Application.ToDos.Requests;
using ToDosManagement.Application.ToDos.Responses;
using ToDosManagement.Application.Users.Requests;
using ToDosManagement.Application.Users.Responses;
using ToDosManagement.Domain;
using ToDosManagement.Domain.Models;

namespace ToDosManagement.API.Infrastructure.Mappings
{
    public static class MapsterConfiguration
    {
        public static IServiceCollection RegisterMaps(this IServiceCollection services)
        {
            //Users
            TypeAdapterConfig<UserCreateRequest, User>
            .NewConfig();
            TypeAdapterConfig<UserUpdateRequest, User>
            .NewConfig();
            TypeAdapterConfig<User, UserResponseModel>
            .NewConfig()
            .Map(dest => dest.ToDos, src => GetIds(src.ToDos));

            //ToDos
            TypeAdapterConfig<ToDoCreateRequest, ToDo>
            .NewConfig();
            TypeAdapterConfig<ToDoUpdateRequest, User>
            .NewConfig();
            TypeAdapterConfig<ToDo, ToDoResponseModel>
            .NewConfig()
            .Map(dest => dest.Subtasks, src => GetIds(src.Subtasks));

            //SubTasks
            TypeAdapterConfig<SubtaskCreateRequest, Subtask>
            .NewConfig();
            TypeAdapterConfig<SubtaskUpdateRequest, Subtask>
            .NewConfig();
            TypeAdapterConfig<Subtask, SubtaskResponseModel>
            .NewConfig()
            .Map(dest => dest.ToDoId, src => src.ToDoItem.Id);
            return services;
        }
                
        private static List<int> GetIds<T>(List<T> values) where T : EntityModel
        {
            List<int> result = new();
            if (values == null) return result;
            foreach (var value in values)
            {
                if (value.State == State.Deleted) continue;
                result.Add(value.Id);
            }
            return result;
        }
    }
}
