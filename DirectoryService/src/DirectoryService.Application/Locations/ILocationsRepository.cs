using DirectoryService.Core.Locations;

namespace DirectoryService.Application.Locations
{
    public interface ILocationsRepository
    {
        Task<Guid> CreateLocation(Location location, CancellationToken cancellationToken);
    }
}
