

using System.Net;

namespace DirectoryService.API.ResponseExtensions
{
    public class SuccessResult<TValue> : IResult
    {
        private readonly TValue value;

        public SuccessResult(TValue value)
        {
            this.value = value;
        }

        public Task ExecuteAsync(HttpContext httpContext)
        {
            ArgumentNullException.ThrowIfNull(httpContext);
            
            var envelope = Envelope.Ok(value);

            httpContext.Response.StatusCode = (int)HttpStatusCode.OK;

            return httpContext.Response.WriteAsJsonAsync(envelope);
        }                
    }
}
