using DataAccessLayer.Data;
using DataAccessLayer.DTOs;
using DataAccessLayer.Extension;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogicLayer.Features.Queries.Get;

public class GetAllConstructionSitesQuery : IRequest<List<ConstructionSiteDTO>?>
{
}

public class GetAllConstructionSitesQueryHandler : IRequestHandler<GetAllConstructionSitesQuery, List<ConstructionSiteDTO>?>
{
    private readonly AppDbContext _context;

    public GetAllConstructionSitesQueryHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<ConstructionSiteDTO>?> Handle(GetAllConstructionSitesQuery request, CancellationToken cancellationToken)
    {
        return await _context
            .ConstructionSites
            .Select(cs => new ConstructionSiteDTO()
            {
                ConstructionSiteId = cs.ConstructionSiteId,
                Name = cs.Name,
                StartDate = cs.StartDate.ToLongDateString(),
                EndDate = cs.EndDate.ToLongDateString(),
                Status = cs.Status.GetDisplayName()
            })
            .ToListAsync(cancellationToken);
    }
}