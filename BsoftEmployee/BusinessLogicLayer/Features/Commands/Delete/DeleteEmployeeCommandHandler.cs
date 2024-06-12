using BusinessLogicLayer.Helper;
using DataAccessLayer.Data;
using DataAccessLayer.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogicLayer.Features.Commands.Delete;

public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, BsoftResult>
{
    private readonly AppDbContext _context;
    public DeleteEmployeeCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<BsoftResult> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await _context
            .Users
            .FindAsync(request.EmployeeId);

        if (employee is null)
        {
            return new BsoftResult()
            {
                Succeeded = false,
                Errors = new List<string>() { $"No Employee was found with the id : {request.EmployeeId}" }
            };
        }
        else if(await IsEmployeeActive(employee.Id))
        {
            return new BsoftResult()
            {
                Succeeded = false,
                Errors = new List<string>() { $"The Employee Is Currently Active On At Least One Site" }
            };
        }

        _context.Users.Remove(employee);
        var result = await _context.SaveChangesAsync(cancellationToken);

        if(result > 0)
        {
            return new BsoftResult()
            {
                Succeeded = true,
                Message = "Employee Is Removed"
            };
        }
        else
        {
            return new BsoftResult()
            {
                Succeeded = false,
                Message = "Employee Is Not Removed"
            };
        }
    }

    private async Task<bool> IsEmployeeActive(string employeeId)
    {
        return await _context
                .ConstructionSiteEmployees
                .Include(cse => cse.Employee)
                .Include(cse => cse.ConstructionSite)
                .Where(cse => 
                    cse.ConstructionSite != null &&
                    cse.ConstructionSite.StartDate < DateTime.Now && 
                    cse.ConstructionSite.Status == ConstructionSiteStatus.InProgress
                    )
                .AnyAsync(e => e.EmployeeId == employeeId);
    }
}