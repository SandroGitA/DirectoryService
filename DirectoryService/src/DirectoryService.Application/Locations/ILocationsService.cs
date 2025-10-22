using DirectoryService.Contracts;

namespace DirectoryService.Application.Locations
{
    public interface ILocationsService
    {
        Task<Guid> Create(CreateLocationDto createLocationDto, CancellationToken cancellationToken);
    }
}
