using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogicLayer.Features.Commands.Add;

public class AddEmployeeToSiteCommand : IRequest<bool>
{
    [Required]
    public int ConstructionSiteId { get; set; }

    [Required]
    public string? EmployeeId { get; set; }
}