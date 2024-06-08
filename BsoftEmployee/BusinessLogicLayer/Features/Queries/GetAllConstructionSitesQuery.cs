using AutoMapper;
using DataAccessLayer.Data;
using DataAccessLayer.DTOs;
using DataAccessLayer.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogicLayer.Features.Queries;

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
        var result = await _context.ConstructionSites.ToListAsync(cancellationToken);

        return _mapper.Map<List<ConstructionSiteDTO>>(result);
    }
}