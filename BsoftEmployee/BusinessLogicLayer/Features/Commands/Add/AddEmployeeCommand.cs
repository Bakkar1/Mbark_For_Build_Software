using DataAccessLayer.DTOs;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogicLayer.Features.Commands.Add
{
    public class AddEmployeeCommand : IRequest<IdentityResult>
    {
        [Required]
        public CreateEmployeeDTO? Employee { get; set; }
    }
}
