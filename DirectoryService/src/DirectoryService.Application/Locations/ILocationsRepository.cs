using CSharpFunctionalExtensions;
using DirectoryService.Core.Locations;
using Shared;

namespace DirectoryService.Application.Locations
{
    public interface ILocationsRepository
    {
        Task<Result<Guid, Error>> Add(Location location, CancellationToken cancellationToken);

        Task<Result<string, Error>> SearchLocationName(string locationName, CancellationToken cancellationToken);

        Task<Result<Address, Error>> SearchAddress(Address address, CancellationToken cancellationToken);
    }
}
