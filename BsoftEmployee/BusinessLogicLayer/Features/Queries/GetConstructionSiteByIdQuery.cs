using AutoMapper;
using DataAccessLayer.Data;
using DataAccessLayer.DTOs;
using DataAccessLayer.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogicLayer.Features.Queries
{
    public class GetConstructionSiteByIdQuery : IRequest<ConstructionSiteDTO?>
    {
        public int GetConstructionSiteId { get; set; }
    }

    public class GetConstructionSiteByIdQueryHandler : IRequestHandler<GetConstructionSiteByIdQuery, ConstructionSiteDTO?>
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public GetConstructionSiteByIdQueryHandler(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ConstructionSiteDTO?> Handle(GetConstructionSiteByIdQuery request, CancellationToken cancellationToken)
        {
            var constructionSite = await _context
                .ConstructionSites
                .FirstOrDefaultAsync(cs =>  cs.ConstructionSiteId == request.GetConstructionSiteId,cancellationToken);
            return _mapper.Map<ConstructionSiteDTO>(constructionSite);
        }
    }
}
