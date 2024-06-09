using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogicLayer.Features.Commands.DeleteUser
{
    public class DeleteEmployeeCommand : IRequest<bool>
    {
        [Required]
        public string? EmployeeId { get; set; }
    }
}
