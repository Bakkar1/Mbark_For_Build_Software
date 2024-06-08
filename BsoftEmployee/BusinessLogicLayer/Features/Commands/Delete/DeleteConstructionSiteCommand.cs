using MediatR;

namespace BusinessLogicLayer.Features.Commands.Delete;
public class DeleteConstructionSiteCommand : IRequest
{
    public int ConstructionSiteId { get; set; }
}