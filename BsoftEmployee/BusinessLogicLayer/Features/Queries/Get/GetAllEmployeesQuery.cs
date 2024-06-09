using DataAccessLayer.Data;
using DataAccessLayer.DTOs;
using DataAccessLayer.Extension;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogicLayer.Features.Queries.Get
{
    public class GetAllEmployeesQuery : IRequest<List<EmployeeDTO>?>
    {
    }

    public class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, List<EmployeeDTO>?>
    {
        private readonly AppDbContext _context;
        public GetAllEmployeesQueryHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<EmployeeDTO>?> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            return await _context
                .Employees
                .Select(e => new EmployeeDTO
                {
                    EmployeeId = e.Id,
                    Name = e.FirstName,
                    Role = e.Role.GetDisplayName(),
                })
                .ToListAsync(cancellationToken);
        }
    }
}
