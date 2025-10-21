using DirectoryService.Contracts;

namespace DirectoryService.Application.Locations
{
    public interface ILocationsService
    {
        Task<Guid> CreateLocation(CreateLocationDto createLocationDto, CancellationToken cancellationToken);
    }
}
