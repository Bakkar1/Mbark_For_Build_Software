using MediatR;
using DataAccessLayer.Data;
using DataAccessLayer.DTOs;
using DataAccessLayer.Model;

namespace BusinessLogicLayer.Features.Commands.Add;

public class CreateConstructionSiteCommandHandler : IRequestHandler<CreateConstructionSiteCommand, ConstructionSiteDTO>
{
    private readonly AppDbContext _context;

    public CreateConstructionSiteCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ConstructionSiteDTO> Handle(CreateConstructionSiteCommand request, CancellationToken cancellationToken)
    {
        if(request.ConstructionSite is null || string.IsNullOrEmpty(request.ConstructionSite?.Name))
        {
            // Handle not found
        }
        var constructionSite = new ConstructionSite
        {
            Name = request.ConstructionSite.Name,
            StartDate = request.ConstructionSite.StartDate,
            EndDate = request.ConstructionSite.EndDate,
            Status = request.ConstructionSite.Status
        };

        _context.ConstructionSites.Add(constructionSite);
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
