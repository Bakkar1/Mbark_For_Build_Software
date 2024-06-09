using MediatR;
using Microsoft.AspNetCore.Mvc;
using BusinessLogicLayer.Features.Commands.Delete;

namespace BsoftWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RemoveConstructionSiteController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RemoveConstructionSiteController(IMediator mediator)
        {
            _mediator = mediator;
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
