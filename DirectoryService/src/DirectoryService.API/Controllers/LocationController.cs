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
        public async Task<Guid> Create(CreateLocationDto createLocationDto, CancellationToken cancellationToken)
        {
            var locationId = await locationService.Create(createLocationDto, cancellationToken);
            return locationId;
        }
    }
}
