using DataAccessLayer.Data;
using DataAccessLayer.DTOs;
using DataAccessLayer.Enums;
using DataAccessLayer.Extension;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogicLayer.Features.Queries.Get
{
    public class GetActiveEmployeesQuery : IRequest<List<EmployeeDTO>?>
    {
    }

    public class GetActiveEmployeesQueryHandler : IRequestHandler<GetActiveEmployeesQuery, List<EmployeeDTO>?>
    {
        private readonly AppDbContext _context;

        public GetActiveEmployeesQueryHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<EmployeeDTO>?> Handle(GetActiveEmployeesQuery request, CancellationToken cancellationToken)
        {
            var now = DateTime.Now;
            var employees = await _context
                .Employees
                .Include(e => e.ConstructionSiteEmployees)
                .Where(e => e.ConstructionSiteEmployees != null 
                    && 
                    e.ConstructionSiteEmployees.Any(cse => cse.ConstructionSite != null &&
                        cse.ConstructionSite.StartDate < DateTime.Now && cse.ConstructionSite.Status == ConstructionSiteStatus.InProgress))
                .Select( e => new EmployeeDTO
                {
                    EmployeeId = e.Id,
                    Name = e.FirstName,
                    Role = e.Role.GetDisplayName(),
                })
                .TagWith("Get Active Employees")
                .ToListAsync();

            return employees;
        }
    }
}
