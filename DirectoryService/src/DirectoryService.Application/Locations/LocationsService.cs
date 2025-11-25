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
        public readonly ILocationsRepository locationRepository;
        public readonly ILogger<LocationsService> logger;
        public readonly IValidator<CreateLocationDto> validator;

        public LocationsService(ILocationsRepository locationRepository,
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
                return errors;
            }

            var locationNameResult = LocationName.Create(createLocationDto.Name);

            if (!locationNameResult.IsSuccess)
            {
                return locationNameResult.Error.ToErrors();
            }

            var addressResult = Address.Create(createLocationDto.Address.Name,
                createLocationDto.Address.City,
                createLocationDto.Address.Street,
                createLocationDto.Address.HouseNumber,
                createLocationDto.Address.Room,
                createLocationDto.Address.ZipCode);

            if (!addressResult.IsSuccess)
            {
                return addressResult.Error.ToErrors();
            }

            var timezoneResult = Timezone.Create(createLocationDto.Timezone);

            if (!timezoneResult.IsSuccess)
            {
                return locationNameResult.Error.ToErrors();
            }

            var location = Location.Create(locationNameResult.Value, addressResult.Value, timezoneResult.Value, true);

            var locationId = await locationRepository.Add(location, cancellationToken);

            return locationId;
        }
    }
}
