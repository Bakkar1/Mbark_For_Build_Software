using DataAccessLayer.DTOs;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogicLayer.Features.Commands.Update;
public class UpdateConstructionSiteCommand : IRequest<ConstructionSiteDTO>
{
    public int ConstructionSiteId { get; set; }
    [Required]
    public ConstructionSiteDTO? ConstructionSite { get; set; }
}
