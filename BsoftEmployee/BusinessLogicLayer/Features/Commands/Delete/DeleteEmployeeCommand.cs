using BusinessLogicLayer.Helper;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogicLayer.Features.Commands.Delete
{
    public class DeleteEmployeeCommand : IRequest<BsoftResult>
    {
        [Required]
        public string? EmployeeId { get; set; }
    }
}
