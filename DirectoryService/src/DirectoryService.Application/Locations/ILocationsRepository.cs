using DirectoryService.Core.Locations;

namespace DirectoryService.Application.Locations
{
    public interface ILocationsRepository
    {
        Task<Guid> Add(Location location, CancellationToken cancellationToken);
    }
}
