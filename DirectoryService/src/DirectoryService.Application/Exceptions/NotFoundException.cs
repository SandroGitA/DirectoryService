using Shared;
using System.Text.Json;

namespace DirectoryService.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        protected NotFoundException(IEnumerable<Error> errors)
            : base(JsonSerializer.Serialize(errors)) { }
    }
}
