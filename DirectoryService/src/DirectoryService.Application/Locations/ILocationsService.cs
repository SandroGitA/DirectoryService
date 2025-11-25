using CSharpFunctionalExtensions;
using DirectoryService.Contracts;
using Shared;

namespace DirectoryService.Application.Locations
{
    public interface ILocationsService
    {
        Task<Result<Guid, Errors>> Create(CreateLocationDto createLocationDto, CancellationToken cancellationToken);
    }
}
