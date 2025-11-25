using DirectoryService.Application.Exceptions;
using Shared;
using System.Text.Json;

namespace DirectoryService.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleware> logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            this.next = next ?? throw new ArgumentNullException(nameof(next));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionContext(httpContext, ex);
            }
        }

        private async Task HandleExceptionContext(HttpContext httpContext, Exception exception)
        {
            logger.LogError(exception, exception.Message);

            var (code, errors) = exception switch
            {
                BadRequestException => (StatusCodes.Status500InternalServerError, JsonSerializer.Deserialize<IEnumerable<Error>>(exception.Message)),

                NotFoundException => (StatusCodes.Status404NotFound, JsonSerializer.Deserialize<IEnumerable<Error>>(exception.Message)),

                _ => (StatusCodes.Status500InternalServerError, [Error.Failure(null, "Internal server error")])
            };

            httpContext.Response.StatusCode = code;
            httpContext.Response.ContentType = "application/json";

            await httpContext.Response.WriteAsJsonAsync(errors);
        }
    }

    public static class ExceptionMiddlewareExtension
    {
        public static IApplicationBuilder UseExceptionMiddleware(this WebApplication app)
        {
            return app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
