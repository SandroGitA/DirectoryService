using System.Linq.Expressions;
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

        public async Task<Result<T, Error>> GetByField<T>(string field, object value, string op,
            CancellationToken cancellationToken)
        {
            var param = Expression.Parameter(typeof(Location), "l");
            var property = Expression.Property(param, field);
            
            var convertedValue = Convert.ChangeType(value, property.Type);
            var constant = Expression.Constant(convertedValue);
            
            var equal = Expression.Equal(property, constant);
            
            var lambda = Expression.Lambda<Func<Location, bool>>(equal, param);
            var queryResult = await directoryServiceDbContext.Locations.FirstOrDefaultAsync(lambda, cancellationToken);

            if (queryResult == null)
                return Error.Failure(null, $"Could not find location with field {field}");

            var result = typeof(T).GetProperty(field)?.GetValue(queryResult, null);

            return (T)result;
        }
    }
}
