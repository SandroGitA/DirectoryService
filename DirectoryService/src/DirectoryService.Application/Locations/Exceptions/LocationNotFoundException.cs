using DirectoryService.Application.Exceptions;
using Shared;

namespace DirectoryService.Application.Locations.Exceptions
{
    public class LocationNotFoundException : NotFoundException
    {
        public LocationNotFoundException(IEnumerable<Error> errors)
            : base(errors) { }
    }
}
