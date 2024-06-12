using MediatR;
using Microsoft.AspNetCore.Mvc;
using BusinessLogicLayer.Features.Commands.Delete;
using BusinessLogicLayer.Features.Commands.Add;

namespace BsoftWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConstructionSiteMangeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ConstructionSiteMangeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateConstructionSite(CreateConstructionSiteCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConstructionSite(int id)
        {
            var command = new DeleteConstructionSiteCommand { ConstructionSiteId = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
