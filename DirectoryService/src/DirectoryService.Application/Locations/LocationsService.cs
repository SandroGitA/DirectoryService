using CSharpFunctionalExtensions;
using DirectoryService.Application.Extensions;
using DirectoryService.Contracts;
using DirectoryService.Core.Locations;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Shared;
using Address = DirectoryService.Core.Locations.Address;
using Timezone = DirectoryService.Core.Locations.Timezone;

namespace DirectoryService.Application.Locations
{
    public class LocationsService : ILocationsService
    {
        private readonly ILocationsRepository locationRepository;
        private readonly ILogger<LocationsService> logger;
        private readonly IValidator<CreateLocationDto> validator;

        public LocationsService(
            ILocationsRepository locationRepository,
            ILogger<LocationsService> logger,
            IValidator<CreateLocationDto> validator)
        {
            this.locationRepository = locationRepository;
            this.logger = logger;
            this.validator = validator;
        }
        public async Task<Result<Guid, Errors>> Create(CreateLocationDto createLocationDto, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(createLocationDto, cancellationToken);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.ToErrors();
                logger.LogError("Errors occurred: {@Errors}", errors);
                
                return errors;
            }

            var locationNameResult = LocationName.Create(createLocationDto.Name);

            if (!locationNameResult.IsSuccess)
            {
                var errors = locationNameResult.Error.ToErrors();
                logger.LogError("Errors occurred: {@Errors}", errors);
                
                return errors;
            }

            var addressResult = Address.Create(createLocationDto.Address.Name,
                createLocationDto.Address.City,
                createLocationDto.Address.Street,
                createLocationDto.Address.HouseNumber,
                createLocationDto.Address.Room,
                createLocationDto.Address.ZipCode);

            if (!addressResult.IsSuccess)
            {
                var errors = addressResult.Error.ToErrors();
                logger.LogError("Errors occurred: {@Errors}", errors);
                
                return errors;
            }

            var timezoneResult = Timezone.Create(createLocationDto.Timezone);

            if (!timezoneResult.IsSuccess)
            {
                var errors = timezoneResult.Error.ToErrors();
                logger.LogError("Errors occurred: {@Errors}", errors);
                
                return errors;
            }

            var location = Location.Create(locationNameResult.Value, addressResult.Value, timezoneResult.Value, true);

            var locationId = await locationRepository.Add(location, cancellationToken);

            if (!locationId.IsSuccess)
            {
                var errors = locationId.Error.ToErrors();
                logger.LogError("Errors occurred: {@Errors}", errors);
                
                return errors;
            }

            logger.LogInformation("Created location: {@Location}", location);
            
            return locationId.Value;
        }
    }
}
