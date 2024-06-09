using AutoMapper;
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
    private readonly IMapper _mapper;

    public GetAllConstructionSitesQueryHandler(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
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