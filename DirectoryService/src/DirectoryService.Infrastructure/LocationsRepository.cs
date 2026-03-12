using CSharpFunctionalExtensions;
using DirectoryService.Application.Locations;
using DirectoryService.Core.Locations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shared;

namespace DirectoryService.Infrastructure
{
    public class LocationsRepository : ILocationsRepository
    {
        private readonly ILogger<LocationsRepository> logger;
        private readonly DirectoryServiceDbContext directoryServiceDbContext;
        private ILocationsRepository _locationsRepositoryImplementation;

        public LocationsRepository(
            ILogger<LocationsRepository> logger,
            DirectoryServiceDbContext directoryServiceDbContext)
        {
            this.logger = logger;
            this.directoryServiceDbContext = directoryServiceDbContext;
        }

        public async Task<Result<Guid, Error>> Add(Location location, CancellationToken cancellationToken)
        {
            var result = await directoryServiceDbContext.AddAsync(location);

            try
            {
                await directoryServiceDbContext.SaveChangesAsync();
                logger.LogInformation("Successfully added location {@Location}", result.Entity.Id);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occured while adding new location");
                return Error.Failure(null, ex.Message);
            }

            return location.Id;
        }

        public async Task<Result<string, Error>> SearchLocationName(string locationName,
            CancellationToken cancellationToken)
        {
            var result = await directoryServiceDbContext.Locations
                .Where(l => l.Name.Name == locationName)
                .Select(l => l.Name.Name)
                .FirstOrDefaultAsync(cancellationToken);

            if (result is null)
                return locationName;

            return Error.Validation(null, "Name already exists", "name");
        }

        public async Task<Result<Address, Error>> SearchAddress(Address address, CancellationToken cancellationToken)
        {
            var result = await directoryServiceDbContext.Locations
                .Where(a=>a.Address == address)
                .Select(a => a.Address)
                .FirstOrDefaultAsync(cancellationToken);

            if (result is null)
                return address;
            
            return Error.Validation(null, "Address already exists", "address");
        }
    }
}
