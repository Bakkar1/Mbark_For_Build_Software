using BsoftWebAPI.AuthorizeAttribute;
using BusinessLogicLayer.Features.Queries.Get;
using BusinessLogicLayer.Features.Queries.GetUsers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BsoftWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetEmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GetEmployeeController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [ApiKeyAuthorize]
        public async Task<IActionResult> GetAllEmployees()
        {
            var query = new GetAllEmployeesQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [HttpGet("ActiveEmployees")]
        public async Task<IActionResult> GetActiveEmployees()
        {
            var query = new GetActiveEmployeesQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(string id)
        {
            var query = new GetEmployeeByIdQuery { EmployeeId = id };
            var result = await _mediator.Send(query);
            if (result is null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpGet("BySiteName/{siteName}")]
        public async Task<IActionResult> GetEmployeeByName(string siteName)
        {
            var query = new GetEmployeeBySiteNameQuery { SiteName = siteName };
            var result = await _mediator.Send(query);
            if (result is null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
