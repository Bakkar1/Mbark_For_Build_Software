using BusinessLogicLayer.Helper;
using DataAccessLayer.Data;
using DataAccessLayer.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogicLayer.Features.Commands.Add;

public class AddEmployeeToSiteCommandHandler : IRequestHandler<AddEmployeeToSiteCommand, BsoftResult>
{
    private readonly AppDbContext _context;

    public AddEmployeeToSiteCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<BsoftResult> Handle(AddEmployeeToSiteCommand request, CancellationToken cancellationToken)
    {
        var constructionSite = await _context.ConstructionSites
            .Include(cs => cs.ConstructionSiteEmployees)
            .TagWith("Get ConstructionSites")
            .SingleOrDefaultAsync(cs => cs.ConstructionSiteId == request.ConstructionSiteId, cancellationToken);

        if (constructionSite is null)
        {
            return new BsoftResult()
            {
                Succeeded = false,
                Errors = new List<string>() { $"No ConstructionSite was found with the id : {request.ConstructionSiteId}" }
            };
        }

        var employee = await _context
            .Employees
            .TagWith("Get Employee")
            .SingleOrDefaultAsync(e => e.Id == request.EmployeeId, cancellationToken);

        if (employee is null)
        {
            return new BsoftResult()
            {
                Succeeded = false,
                Errors = new List<string>() { $"No Employee was found with the id : {request.EmployeeId}" }
            };
        }

        // check conflict
        bool conflict = await _context
            .ConstructionSiteEmployees
            .TagWith("Get ConstructionSiteEmployees")
            .Where(cse => cse.EmployeeId == employee.Id && cse.ConstructionSiteId == constructionSite.ConstructionSiteId )
            .AnyAsync();

        if (conflict)
        {
            return new BsoftResult()
            {
                Succeeded = false,
                Errors = new List<string>() { $"The Employee with Id : {request.EmployeeId} Is Alreadt exist in the ConstructionSiteId with id : {request.ConstructionSiteId}" }
            };
        }

        var constructionSiteEmployee = new ConstructionSiteEmployee
        {
            ConstructionSiteId = request.ConstructionSiteId,
            EmployeeId = employee.Id
        };
        _context.ConstructionSiteEmployees.Add(constructionSiteEmployee);

        await _context.SaveChangesAsync(cancellationToken);

        return new BsoftResult()
        {
            Succeeded = true,
            Message = "Employee Is Added To The Site"
        };
    }
}
