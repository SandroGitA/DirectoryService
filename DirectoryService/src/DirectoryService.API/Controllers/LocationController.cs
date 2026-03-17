using DirectoryService.API.ResponseExtensions;
using DirectoryService.Application.Abstractions;
using DirectoryService.Application.Locations;
using DirectoryService.Application.Locations.CreateLocation;
using DirectoryService.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace DirectoryService.API.Controllers
{
    [ApiController]
    [Route("api/")]
    public class LocationController : ControllerBase
    {
        private readonly ICommandHandler<Guid, CreateLocationCommand> createLocationCommandHandler;
        private readonly ILogger<LocationController> logger;

        public LocationController(ILogger<LocationController> logger)
        {
            this.logger = logger;
        }

        [HttpPost]
        [Route("locations")]
        [ProducesResponseType<Envelope<string>>(200)]
        [ProducesResponseType<Envelope>(400)]
        [ProducesResponseType<Envelope>(405)]
        [ProducesResponseType<Envelope>(500)]
        public async Task<EndpointResult<Guid>> Create(
            [FromServices] ICommandHandler<Guid, CreateLocationCommand> handler,
            CreateLocationDto request,
            CancellationToken cancellationToken)
        {
            var command = new CreateLocationCommand(request);
            return await handler.Handle(command, cancellationToken);
        }
    }
}