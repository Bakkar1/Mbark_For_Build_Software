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
    public class EmployeeMangeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeeMangeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddNewEmployee(CreateEmployeeDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var command = new AddEmployeeCommand { Employee = model };
            var result = await _mediator.Send(command);
            if (!result.Succeeded)
            {
                return NotFound(result.Errors);
            }

            return Ok("Employee added.");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(string id)
        {
            var command = new DeleteEmployeeCommand { EmployeeId = id };
            var result = await _mediator.Send(command);
            if (result.Succeeded)
            {
                return Ok(result.Message);
            }

            if (result.Errors is not null)
            {
                return BadRequest(string.Join('\n', result.Errors));
            }
            else
            {
                return BadRequest("Something Went Wrong!");
            }
        }
    }
}
