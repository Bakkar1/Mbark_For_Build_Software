using DataAccessLayer.Data;
using MediatR;

namespace BusinessLogicLayer.Features.Commands.Delete;
public class DeleteConstructionSiteCommandHandler : IRequestHandler<DeleteConstructionSiteCommand>
{
    private readonly AppDbContext _context;

    public DeleteConstructionSiteCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteConstructionSiteCommand request, CancellationToken cancellationToken)
    {
        var constructionSite = await _context.ConstructionSites.FindAsync(request.ConstructionSiteId);

        if (constructionSite != null)
        {
            _context.ConstructionSites.Remove(constructionSite);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
