using BusinessLogicLayer.Helper;
using MediatR;

namespace BusinessLogicLayer.Features.Commands.Delete;
public class DeleteConstructionSiteCommand : IRequest<BsoftResult>
{
    public int ConstructionSiteId { get; set; }
}