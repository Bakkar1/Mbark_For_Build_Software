using DataAccessLayer.Data;
using DataAccessLayer.DTOs;
using MediatR;

namespace BusinessLogicLayer.Features.Commands.Update;
public class UpdateConstructionSiteCommandHandler : IRequestHandler<UpdateConstructionSiteCommand, ConstructionSiteDTO>
{
    private readonly AppDbContext _context;

    public UpdateConstructionSiteCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ConstructionSiteDTO> Handle(UpdateConstructionSiteCommand request, CancellationToken cancellationToken)
    {
        var constructionSite = await _context.ConstructionSites.FindAsync(request.ConstructionSiteId);

        if (constructionSite is null)
        {
            // Handle not found
        }

        // Update properties of constructionSite with request.ConstructionSite

        await _context.SaveChangesAsync(cancellationToken);

        return new ConstructionSiteDTO
        {
            ConstructionSiteId = constructionSite.ConstructionSiteId,
            Name = constructionSite.Name,
            StartDate = constructionSite.StartDate,
            EndDate = constructionSite.EndDate,
            Status = constructionSite.Status
        };
    }
}
