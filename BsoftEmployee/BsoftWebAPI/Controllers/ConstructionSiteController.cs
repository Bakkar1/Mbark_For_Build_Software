using MediatR;
using Microsoft.AspNetCore.Mvc;
using BusinessLogicLayer.Features.Queries;
using BusinessLogicLayer.Features.Commands.Add;
using BusinessLogicLayer.Features.Commands.Delete;
using BusinessLogicLayer.Features.Commands.Update;
using DataAccessLayer.Model;

namespace BsoftWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConstructionSiteController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ConstructionSiteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllConstructionSites()
        {
            var query = new GetAllConstructionSitesQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetConstructionSiteById(int id)
        {
            var query = new GetConstructionSiteByIdQuery { GetConstructionSiteId = id };
            var result = await _mediator.Send(query);
            if (result is null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateConstructionSite(CreateConstructionSiteCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetConstructionSiteById), new { id = result.ConstructionSiteId }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateConstructionSite(int id, UpdateConstructionSiteCommand command)
        {
            if (id != command.ConstructionSiteId)
            {
                return BadRequest();
            }

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
