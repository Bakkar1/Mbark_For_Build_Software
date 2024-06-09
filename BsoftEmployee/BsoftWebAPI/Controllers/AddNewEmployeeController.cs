using BusinessLogicLayer.Features.Commands.Add;
using DataAccessLayer.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BsoftWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddNewEmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AddNewEmployeeController(IMediator mediator)
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
    }
}
