using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http.Metadata;
using Shared;
using System.Reflection;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace DirectoryService.API.ResponseExtensions
{
    public class EndpointResult<TValue> : IResult, IEndpointMetadataProvider
    {
        private readonly IResult result;

        public EndpointResult(Result<TValue, Error> result)
        {
            this.result = result.IsSuccess
                ? new SuccessResult<TValue>(result.Value)
                : new ErrorsResults(result.Error);
        }

        public EndpointResult(Result<TValue, Errors> result)
        {
            this.result = result.IsSuccess
                ? new SuccessResult<TValue>(result.Value)
                : new ErrorsResults(result.Error);
        }

        public Task ExecuteAsync(HttpContext httpContext)
            => result.ExecuteAsync(httpContext);

        public static implicit operator EndpointResult<TValue>(Result<TValue, Error> result) => new(result);

        public static implicit operator EndpointResult<TValue>(Result<TValue, Errors> result) => new(result);

        public static void PopulateMetadata(MethodInfo method, EndpointBuilder builder)
        {
            ArgumentNullException.ThrowIfNull(method);
            ArgumentNullException.ThrowIfNull(builder);

            builder.Metadata.Add(new ProducesResponseTypeMetadata(StatusCodes.Status200OK, typeof(Envelope<TValue>), ["application/json"]));
            builder.Metadata.Add(new ProducesResponseTypeMetadata(400, typeof(Envelope<TValue>), ["application/json"]));
            builder.Metadata.Add(new ProducesResponseTypeMetadata(405, typeof(Envelope<TValue>), ["application/json"]));
            builder.Metadata.Add(new ProducesResponseTypeMetadata(500, typeof(Envelope<TValue>), ["application/json"]));
        }
    }
}
