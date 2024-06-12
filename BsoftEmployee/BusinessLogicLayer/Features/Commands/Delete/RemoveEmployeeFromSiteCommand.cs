using BusinessLogicLayer.Helper;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogicLayer.Features.Commands.Delete;

public class RemoveEmployeeFromSiteCommand : IRequest<BsoftResult>
{
    [Required]
    public int ConstructionSiteId { get; set; }

    [Required]
    public string? EmployeeId { get; set; }
}
