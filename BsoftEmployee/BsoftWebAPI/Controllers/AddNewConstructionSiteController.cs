using MediatR;
using Microsoft.AspNetCore.Mvc;
using BusinessLogicLayer.Features.Commands.Add;

namespace BsoftWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddNewConstructionSiteController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AddNewConstructionSiteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateConstructionSite(CreateConstructionSiteCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
