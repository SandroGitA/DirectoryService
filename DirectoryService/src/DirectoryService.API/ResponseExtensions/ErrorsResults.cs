
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace DirectoryService.API.ResponseExtensions
{
    public class ErrorsResults : IResult
    {
        private readonly Errors errors;

        public ErrorsResults(Error error)
        {
            errors = error.ToErrors();
        }

        public ErrorsResults(Errors errors)
        {
            this.errors = errors;
        }

        public Task ExecuteAsync(HttpContext httpContext)
        {
            ArgumentNullException.ThrowIfNull(httpContext);

            if (!errors.Any())
            {
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

                return httpContext.Response.WriteAsJsonAsync(errors);
            }

            var statusTypes = errors
                .Select(x => x.TypeError)
                .Distinct()
                .ToList();

            int statusCode = statusTypes.Count > 1
                ? StatusCodes.Status500InternalServerError
                : GetStatusCodeFromErrorType(statusTypes.First());

            var envelope = Envelope.Fail(errors);
            httpContext.Response.StatusCode = statusCode;

            return httpContext.Response.WriteAsJsonAsync(envelope);
        }

        private static int GetStatusCodeFromErrorType(TypeError typeError)
        {
            return typeError switch
            {
                TypeError.NOT_FOUND => StatusCodes.Status400BadRequest,
                TypeError.VALIDATION => StatusCodes.Status405MethodNotAllowed,
                TypeError.FAILURE => StatusCodes.Status500InternalServerError,
                _ => StatusCodes.Status500InternalServerError,
            };
        }
    }
}
