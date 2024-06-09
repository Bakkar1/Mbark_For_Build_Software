using BusinessLogicLayer.Features.Commands.DeleteUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BsoftWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RemoveEmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RemoveEmployeeController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(string id)
        {
            var command = new DeleteEmployeeCommand { EmployeeId = id };
            var isDeleted = await _mediator.Send(command);
            if (isDeleted)
            {
                return Ok("Employee Is Deleted");
            }
            return BadRequest("Employee Is Not Deleted");
        }

    }
}
