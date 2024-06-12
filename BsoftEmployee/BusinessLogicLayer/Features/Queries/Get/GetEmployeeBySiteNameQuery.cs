using DataAccessLayer.Data;
using DataAccessLayer.DTOs;
using DataAccessLayer.Extension;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogicLayer.Features.Queries.Get
{
    public class GetEmployeeBySiteNameQuery : IRequest<List<EmployeeDTO>?>
    {
        [Required]
        public string? SiteName { get; set; }
    }

    public class GetEmployeeBySiteNameQueryHandler : IRequestHandler<GetEmployeeBySiteNameQuery, List<EmployeeDTO>?>
    {
        private readonly AppDbContext _context;

        public GetEmployeeBySiteNameQueryHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<EmployeeDTO>?> Handle(GetEmployeeBySiteNameQuery request, CancellationToken cancellationToken)
        {
            string? siteName = request.SiteName?.ToLower();
            if (string.IsNullOrWhiteSpace(siteName))
            {
                return null;
            }
            var employees = await _context
                .Employees
                .Include(e => e.ConstructionSiteEmployees)
                .Where(e => e.ConstructionSiteEmployees != null
                    &&
                    e.ConstructionSiteEmployees.Any(cse => cse.ConstructionSite.Name.ToLower().Contains(siteName))
                    )
                .Select(e => new EmployeeDTO
                {
                    EmployeeId = e.Id,
                    Name = e.FirstName,
                    Role = e.Role.GetDisplayName(),
                    Sites = e.ConstructionSiteEmployees.Select(cse => cse.ConstructionSite.Name).ToList()
                })
                .TagWith("Get Employees By Site Name")
                .ToListAsync();

            return employees;
        }
    }
}
