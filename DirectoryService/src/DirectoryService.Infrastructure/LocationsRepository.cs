using DirectoryService.Application.Locations;
using DirectoryService.Core.Locations;
using Microsoft.Extensions.Logging;

namespace DirectoryService.Infrastructure
{
    public class LocationsRepository : ILocationsRepository
    {
        public readonly ILogger<LocationsRepository> logger;
        public readonly DirectoryServiceDbContext dbContext;

        public LocationsRepository(ILogger<LocationsRepository> logger, DirectoryServiceDbContext directoryServiceDbContext)
        {
            this.logger = logger;
            this.dbContext = directoryServiceDbContext;
        }

        public async Task<Guid> CreateLocation(Location location, CancellationToken cancellationToken)
        {
            var result = await dbContext.AddAsync(location);
            await dbContext.SaveChangesAsync();

            return location.Id;
        }
    }
}
