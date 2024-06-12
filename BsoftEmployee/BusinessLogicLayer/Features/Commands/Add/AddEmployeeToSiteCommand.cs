using BusinessLogicLayer.Helper;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogicLayer.Features.Commands.Add;

public class AddEmployeeToSiteCommand : IRequest<BsoftResult>
{
    [Required]
    public int ConstructionSiteId { get; set; }

    [Required]
    public string? EmployeeId { get; set; }
}