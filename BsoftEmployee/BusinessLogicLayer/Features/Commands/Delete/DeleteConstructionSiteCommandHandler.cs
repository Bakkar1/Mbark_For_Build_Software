using BusinessLogicLayer.Helper;
using DataAccessLayer.Data;
using MediatR;

namespace BusinessLogicLayer.Features.Commands.Delete;
public class DeleteConstructionSiteCommandHandler : IRequestHandler<DeleteConstructionSiteCommand, BsoftResult>
{
    private readonly AppDbContext _context;

    public DeleteConstructionSiteCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<BsoftResult> Handle(DeleteConstructionSiteCommand request, CancellationToken cancellationToken)
    {
        var constructionSite = await _context
            .ConstructionSites
            .FindAsync(request.ConstructionSiteId);

        if (constructionSite != null)
        {
            _context.ConstructionSites.Remove(constructionSite);
            await _context.SaveChangesAsync(cancellationToken);
            return new BsoftResult()
            {
                Succeeded = true,
                Message = "ConstructionSite Is Removed"
            };
        }
        else
        {
            return new BsoftResult()
            {
                Succeeded = false,
                Errors = new List<string>() { $"No ConstructionSite was found with the id : {request.ConstructionSiteId}" }
            };
        }
    }
}
