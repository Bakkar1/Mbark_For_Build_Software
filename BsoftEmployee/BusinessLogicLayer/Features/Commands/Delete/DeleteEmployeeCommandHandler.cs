using DataAccessLayer.Data;
using DataAccessLayer.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogicLayer.Features.Commands.Delete;

public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, bool>
{
    private readonly AppDbContext _context;
    public DeleteEmployeeCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await _context
            .Users
            .FindAsync(request.EmployeeId);

        if (employee is null || await IsEmployeeActive(employee.Id))
        {
            return false;
        }

        _context.Users.Remove(employee);
        var result = await _context.SaveChangesAsync(cancellationToken);
        return result > 0;
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