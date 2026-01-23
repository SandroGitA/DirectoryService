using CSharpFunctionalExtensions;
using DirectoryService.Application.Locations;
using DirectoryService.Core.Locations;
using Microsoft.Extensions.Logging;
using Shared;

namespace DirectoryService.Infrastructure
{
    public class LocationsRepository : ILocationsRepository
    {
        private readonly ILogger<LocationsRepository> logger;
        private readonly DirectoryServiceDbContext directoryServiceDbContext;

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
    }
}
