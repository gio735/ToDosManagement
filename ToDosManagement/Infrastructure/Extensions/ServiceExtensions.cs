using Microsoft.EntityFrameworkCore;
using System.Net;
using ToDosManagement.Application.Subtasks;
using ToDosManagement.Application.Subtasks.Repositories;
using ToDosManagement.Application.ToDos;
using ToDosManagement.Application.ToDos.Repositories;
using ToDosManagement.Application.Users;
using ToDosManagement.Application.Users.Repositories;
using ToDosManagement.Domain.Models;
using ToDosManagement.Infrastructure.Repositories;
using ToDosManagement.Persistence;

namespace ToDosManagement.API.Infrastructure.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IToDoRepository, ToDoRepository>();
            services.AddScoped<IToDoService, ToDoService>();

            services.AddScoped<ISubtaskRepository, SubtaskRepository>();
            services.AddScoped<ISubtaskService, SubtaskService>();
            services.AddScoped<DbContext, DataContext>();
            return services;
        }
    }
}
