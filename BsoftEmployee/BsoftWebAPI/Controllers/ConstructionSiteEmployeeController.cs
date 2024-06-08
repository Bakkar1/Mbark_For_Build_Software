using BusinessLogicLayer.Features.Commands.Add;
using DataAccessLayer.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BsoftWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConstructionSiteEmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ConstructionSiteEmployeeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployeeToSite([FromBody] AddEmployeeToSiteDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var command = new AddEmployeeToSiteCommand()
            {
                ConstructionSiteId = model.ConstructionSiteId,
                EmployeeId = model.EmployeeId
            };

            var result = await _mediator.Send(command);

            if (!result)
            {
                return NotFound("Construction site or one or more employees not found.");
            }

            return Ok("Employee added to construction site.");
        }

        [HttpPost("add-employees")]
        public async Task<IActionResult> AddEmployeesToSite([FromBody] AddEmployeesToSiteDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AddEmployeeToSiteCommand command;
            int constructionSiteId = model.ConstructionSiteId;

            foreach (var employeeId in model.EmployeeIds)
            {
                command = new AddEmployeeToSiteCommand()
                {
                    ConstructionSiteId = constructionSiteId,
                    EmployeeId = employeeId
                };

                var result = await _mediator.Send(command);
                if (!result)
                {
                    return NotFound("Construction site or one or more employees not found.");
                }
            }

            return Ok("Employees added to construction site.");
        }
    }
}
