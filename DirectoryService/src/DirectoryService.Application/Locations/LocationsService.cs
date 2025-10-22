using DirectoryService.Contracts;
using DirectoryService.Core.Locations;
using FluentValidation;
using Microsoft.Extensions.Logging;
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
            IValidator<CreateLocationDto> validationRules)
        {
            this.locationRepository = locationRepository;
            this.logger = logger;
            this.validator = validationRules;
        }
        public async Task<Guid> Create(CreateLocationDto createLocationDto, CancellationToken cancellationToken)
        {
            var valudate = await validator.ValidateAsync(createLocationDto);

            var locationName = LocationName.Create(createLocationDto.Name);
            var address = Address.Create(createLocationDto.Address.Name,
                createLocationDto.Address.City,
                createLocationDto.Address.Street,
                createLocationDto.Address.HouseNumber,
                createLocationDto.Address.Room,
                createLocationDto.Address.ZipCode);
            var timezone = Timezone.Create(createLocationDto.Timezone);

            var location = Location.Create(locationName, address, timezone, true);

            var locationId = await locationRepository.Add(location, cancellationToken);

            return locationId;
        }
    }
}
