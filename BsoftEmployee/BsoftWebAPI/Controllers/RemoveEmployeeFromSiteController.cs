using BusinessLogicLayer.Features.Commands.Delete;
using DataAccessLayer.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BsoftWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RemoveEmployeeFromSiteController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RemoveEmployeeFromSiteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveEmployeeFromSite([FromBody] RemoveEmployeeFromSiteCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _mediator.Send(command);

            if (!result)
            {
                return NotFound("Construction site or employee not found.");
            }

            return Ok("Employee removed from construction site.");
        }
        [HttpDelete("remove-employees")]
        public async Task<IActionResult> RemoveEmployeesFromSite([FromBody] RemoveEmployeesFromSiteDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else if(model.EmployeeIds is null || !model.EmployeeIds.Any())
            {
                return BadRequest();
            }

            RemoveEmployeeFromSiteCommand command;
            int constructionSiteId = model.ConstructionSiteId;

            foreach (var employeeId in model.EmployeeIds)
            {
                command = new RemoveEmployeeFromSiteCommand()
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

            return Ok("Employee removed from construction site.");
        }
    }
}
