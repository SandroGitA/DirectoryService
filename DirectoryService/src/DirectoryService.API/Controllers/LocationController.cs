using DirectoryService.API.ResponseExtensions;
using DirectoryService.Application.Locations;
using DirectoryService.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace DirectoryService.API.Controllers
{
    [ApiController]
    [Route("api/")]
    public class LocationController : ControllerBase
    {
        public readonly ILocationsService locationService;
        public readonly ILogger<LocationController> logger;
        public LocationController(ILocationsService locationService, ILogger<LocationController> logger)
        {
            this.locationService = locationService;
            this.logger = logger;
        }

        [HttpPost]
        [Route("locations")]
        [ProducesResponseType<Envelope<string>>(200)]
        [ProducesResponseType<Envelope>(400)]
        [ProducesResponseType<Envelope>(405)]
        [ProducesResponseType<Envelope>(500)]
        public async Task<EndpointResult<Guid>> Create(CreateLocationDto createLocationDto, CancellationToken cancellationToken)
        {
            return await locationService.Create(createLocationDto, cancellationToken);
        }
    }
}
