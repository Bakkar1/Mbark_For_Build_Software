using DataAccessLayer.Data;
using DataAccessLayer.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogicLayer.Features.Commands.Add;

public class AddEmployeeToSiteCommandHandler : IRequestHandler<AddEmployeeToSiteCommand, bool>
{
    private readonly AppDbContext _context;

    public AddEmployeeToSiteCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(AddEmployeeToSiteCommand request, CancellationToken cancellationToken)
    {
        var constructionSite = await _context.ConstructionSites
            .Include(cs => cs.ConstructionSiteEmployees)
            .SingleOrDefaultAsync(cs => cs.ConstructionSiteId == request.ConstructionSiteId, cancellationToken);

        if (constructionSite is null)
        {
            return false;
        }

        var employee = await _context.Employees.SingleOrDefaultAsync(e => e.Id == request.EmployeeId, cancellationToken);

        if (employee is null)
        {
            return false;
        }

        // check conflict
        bool conflict = await _context
            .ConstructionSiteEmployees
            .Where(cse => cse.EmployeeId == employee.Id && cse.ConstructionSiteId == constructionSite.ConstructionSiteId )
            .AnyAsync();

        if (conflict)
        {
            return false;
        }

        var constructionSiteEmployee = new ConstructionSiteEmployee
        {
            ConstructionSiteId = request.ConstructionSiteId,
            EmployeeId = employee.Id
        };
        _context.ConstructionSiteEmployees.Add(constructionSiteEmployee);

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
