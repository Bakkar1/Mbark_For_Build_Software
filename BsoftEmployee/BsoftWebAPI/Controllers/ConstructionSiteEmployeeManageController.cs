using BusinessLogicLayer.Features.Commands.Add;
using BusinessLogicLayer.Features.Commands.Delete;
using DataAccessLayer.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BsoftWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConstructionSiteEmployeeManageController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ConstructionSiteEmployeeManageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region Add
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

            if (!result.Succeeded)
            {
                if(result.Errors is not null)
                {
                    return NotFound(string.Join('\n', result.Errors));
                }
                else
                {
                    return NotFound("Something Went Wrong!");
                }
            }

            return Ok(result.Message);
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
                if (!result.Succeeded)
                {
                    if (result.Errors is not null)
                    {
                        return NotFound(string.Join('\n', result.Errors));
                    }
                    else
                    {
                        return NotFound("Something Went Wrong!");
                    }
                }
            }

            return Ok("Employee are added to the construction site");
        }
        #endregion

        #region Delete

        [HttpDelete]
        public async Task<IActionResult> RemoveEmployeeFromSite([FromBody] RemoveEmployeeFromSiteCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _mediator.Send(command);

            if (!result.Succeeded)
            {
                if (result.Errors is not null)
                {
                    return NotFound(string.Join('\n', result.Errors));
                }
                else
                {
                    return NotFound("Something Went Wrong!");
                }
            }

            return Ok(result.Message);
        }

        [HttpDelete("remove-employees")]
        public async Task<IActionResult> RemoveEmployeesFromSite([FromBody] RemoveEmployeesFromSiteDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else if (model.EmployeeIds is null || !model.EmployeeIds.Any())
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
                if (!result.Succeeded)
                {
                    if (result.Errors is not null)
                    {
                        return NotFound(string.Join('\n', result.Errors));
                    }
                    else
                    {
                        return NotFound("Something Went Wrong!");
                    }
                }
            }

            return Ok("Employees are removed from construction site.");
        }
        #endregion
    }
}
