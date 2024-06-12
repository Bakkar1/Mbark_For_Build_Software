using MediatR;
using Microsoft.AspNetCore.Mvc;
using BusinessLogicLayer.Features.Queries.Get;

namespace BsoftWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConstructionSiteReadController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ConstructionSiteReadController(IMediator mediator)
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
    }
}
