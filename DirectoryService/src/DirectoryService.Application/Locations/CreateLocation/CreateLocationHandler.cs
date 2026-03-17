using CSharpFunctionalExtensions;
using DirectoryService.Application.Abstractions;
using DirectoryService.Application.Extensions;
using DirectoryService.Contracts;
using DirectoryService.Core.Locations;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Shared;
using Address = DirectoryService.Core.Locations.Address;
using Timezone = DirectoryService.Core.Locations.Timezone;

namespace DirectoryService.Application.Locations.CreateLocation
{
    public class CreateLocationHandler : ICommandHandler<Guid, CreateLocationCommand>
    {
        private readonly ILocationsRepository locationRepository;
        private readonly ILogger<CreateLocationHandler> logger;
        private readonly IValidator<CreateLocationDto> validator;

        public CreateLocationHandler(
            ILocationsRepository locationRepository,
            ILogger<CreateLocationHandler> logger,
            IValidator<CreateLocationDto> validator)
        {
            this.locationRepository = locationRepository;
            this.logger = logger;
            this.validator = validator;
        }

        public async Task<Result<Guid, Errors>> Handle(CreateLocationCommand command,
            CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(command.CreateLocationDto, cancellationToken);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.ToErrors();
                logger.LogError("Errors occurred: {@Errors}", errors);

                return errors;
            }

            var locationNameResult = LocationName.Create(command.CreateLocationDto.Name);

            if (!locationNameResult.IsSuccess)
            {
                var errors = locationNameResult.Error.ToErrors();
                logger.LogError("Errors occurred: {@Errors}", errors);

                return errors;
            }

            var addressResult = Address.Create(command.CreateLocationDto.Address.Name,
                command.CreateLocationDto.Address.City,
                command.CreateLocationDto.Address.Street,
                command.CreateLocationDto.Address.HouseNumber,
                command.CreateLocationDto.Address.Room,
                command.CreateLocationDto.Address.ZipCode);

            if (!addressResult.IsSuccess)
            {
                var errors = addressResult.Error.ToErrors();
                logger.LogError("Errors occurred: {@Errors}", errors);

                return errors;
            }

            var timezoneResult = Timezone.Create(command.CreateLocationDto.Timezone);

            if (!timezoneResult.IsSuccess)
            {
                var errors = timezoneResult.Error.ToErrors();
                logger.LogError("Errors occurred: {@Errors}", errors);

                return errors;
            }

            var searchLocationNameResult = await locationRepository
                .GetByField<LocationName>("Name", command.CreateLocationDto.Name, "==", cancellationToken);

            if (!searchLocationNameResult.IsSuccess)
            {
                var errors = searchLocationNameResult.Error.ToErrors();
                logger.LogError("Errors occurred: {@Errors}", errors);
                
                return errors;
            }
            
            var searchAddressResult = await locationRepository
                .GetByField<Address>("Address", command.CreateLocationDto.Address, "==", cancellationToken);

            if (!searchAddressResult.IsSuccess)
            {
                var errors = searchAddressResult.Error.ToErrors();
                logger.LogError("Errors occurred: {@Errors}", errors);
                
                return errors;
            }
            
            var location = Location.Create(searchLocationNameResult.Value, searchAddressResult.Value, timezoneResult.Value, true);
            
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