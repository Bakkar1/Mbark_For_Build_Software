using AutoMapper;
using DataAccessLayer.Data;
using DataAccessLayer.DTOs;
using MediatR;

namespace BusinessLogicLayer.Features.Commands.Update;
public class UpdateConstructionSiteCommandHandler : IRequestHandler<UpdateConstructionSiteCommand, ConstructionSiteDTO>
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public UpdateConstructionSiteCommandHandler(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
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

        return _mapper.Map<ConstructionSiteDTO>(constructionSite);
    }
}
