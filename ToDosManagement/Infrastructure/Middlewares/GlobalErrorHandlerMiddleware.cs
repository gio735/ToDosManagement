using Serilog;
using System.Net;
using System.Text.Json;
using ToDosManagement.API.Infrastructure.Localizations;
using ToDosManagement.Infrastructure;

namespace ToDosManagement.API.Infrastructure.Middlewares
{
    public class GlobalErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                string result;
                response.ContentType = "application/json";

                switch (error)
                {
                    case InexistentEntityException:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        result = JsonSerializer.Serialize(new { message = ErrorMessages.NotFound });
                        break;
                    case InvalidLoginInputException:
                        response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        result = JsonSerializer.Serialize(new { message = ErrorMessages.InvalidAuthentication });
                        break;
                    case AttemptToUseDeletedEntityException:
                    case UnauthorizedAccessException:
                        response.StatusCode = (int)HttpStatusCode.Forbidden;
                        result = JsonSerializer.Serialize(new { message = ErrorMessages.Forbidden });
                        break;
                    case ExistingUsernameException:
                        response.StatusCode = (int)HttpStatusCode.Conflict;
                        result = JsonSerializer.Serialize(new { message = ErrorMessages.ExistingUsername });
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        result = JsonSerializer.Serialize(new { message = ErrorMessages.InternalError });
                        break;
                };
                await response.WriteAsync(result);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
