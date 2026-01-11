using CSharpFunctionalExtensions;
using DirectoryService.Application.Locations;
using DirectoryService.Core.Locations;
using Microsoft.Extensions.Logging;
using Shared;

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

        public async Task<Result<Guid, Error>> Add(Location location, CancellationToken cancellationToken)
        {
            var result = await dbContext.AddAsync(location);

            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Error.Failure(null, ex.Message);
            }

            return location.Id;
        }
    }
}
