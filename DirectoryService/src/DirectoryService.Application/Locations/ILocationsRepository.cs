using CSharpFunctionalExtensions;
using DirectoryService.Core.Locations;
using Shared;

namespace DirectoryService.Application.Locations
{
    public interface ILocationsRepository
    {
        Task<Result<Guid, Error>> Add(Location location, CancellationToken cancellationToken);
    }
}
