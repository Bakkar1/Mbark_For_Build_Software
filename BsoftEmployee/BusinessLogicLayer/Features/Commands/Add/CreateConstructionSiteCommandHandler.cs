using MediatR;
using DataAccessLayer.Data;
using DataAccessLayer.DTOs;
using DataAccessLayer.Model;
using AutoMapper;

namespace BusinessLogicLayer.Features.Commands.Add;

public class CreateConstructionSiteCommandHandler : IRequestHandler<CreateConstructionSiteCommand, ConstructionSiteDTO>
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    public CreateConstructionSiteCommandHandler(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ConstructionSiteDTO> Handle(CreateConstructionSiteCommand request, CancellationToken cancellationToken)
    {
        if(request.ConstructionSite is null || string.IsNullOrEmpty(request.ConstructionSite?.Name))
        {
            // Handle not found
        }
        var constructionSite = _mapper.Map<ConstructionSite>(request.ConstructionSite);

        _context.ConstructionSites.Add(constructionSite);
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<ConstructionSiteDTO>(constructionSite);
    }
}
