using BusinessLogicLayer.Helper;
using DataAccessLayer.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogicLayer.Features.Commands.Delete;

public class RemoveEmployeeFromSiteCommandHandler : IRequestHandler<RemoveEmployeeFromSiteCommand, BsoftResult>
{
    private readonly AppDbContext _context;

    public RemoveEmployeeFromSiteCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<BsoftResult> Handle(RemoveEmployeeFromSiteCommand request, CancellationToken cancellationToken)
    {
        var constructionSiteEmployee = await _context.ConstructionSiteEmployees
            .Where(cse => cse.EmployeeId == request.EmployeeId && cse.ConstructionSiteId == request.ConstructionSiteId)
            .SingleOrDefaultAsync(cancellationToken);

        if (constructionSiteEmployee == null)
        {
            return new BsoftResult()
            {
                Succeeded = false,
                Errors = new List<string>() { $"No ConstructionSite was found with the id : {request.ConstructionSiteId}" }
            };
        }


        _context.ConstructionSiteEmployees.Remove(constructionSiteEmployee);
        await _context.SaveChangesAsync(cancellationToken);

        return new BsoftResult()
        {
            Succeeded = true,
            Message = "Employee Is Removed From The Site"
        };
    }
}