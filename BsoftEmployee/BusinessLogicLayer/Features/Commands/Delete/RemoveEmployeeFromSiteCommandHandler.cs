using DataAccessLayer.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogicLayer.Features.Commands.Delete;

public class RemoveEmployeeFromSiteCommandHandler : IRequestHandler<RemoveEmployeeFromSiteCommand, bool>
{
    private readonly AppDbContext _context;

    public RemoveEmployeeFromSiteCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(RemoveEmployeeFromSiteCommand request, CancellationToken cancellationToken)
    {
        var constructionSiteEmployee = await _context.ConstructionSiteEmployees
            .Where(cse => cse.EmployeeId == request.EmployeeId && cse.ConstructionSiteId == request.ConstructionSiteId)
            .SingleOrDefaultAsync(cancellationToken);

        if (constructionSiteEmployee == null)
        {
            return false;
        }


        _context.ConstructionSiteEmployees.Remove(constructionSiteEmployee);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}