using DataAccessLayer.DTOs;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogicLayer.Features.Commands.Add;
public class CreateConstructionSiteCommand : IRequest<ConstructionSiteDTO>
{
    [Required]
    public CreateConstructionSiteDTO? ConstructionSite { get; set; }
}