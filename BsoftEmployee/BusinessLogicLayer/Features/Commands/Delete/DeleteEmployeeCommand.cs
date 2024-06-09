using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogicLayer.Features.Commands.Delete
{
    public class DeleteEmployeeCommand : IRequest<bool>
    {
        [Required]
        public string? EmployeeId { get; set; }
    }
}
