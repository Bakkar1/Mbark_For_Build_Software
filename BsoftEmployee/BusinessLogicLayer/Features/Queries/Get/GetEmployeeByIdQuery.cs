using DataAccessLayer.Data;
using DataAccessLayer.DTOs;
using DataAccessLayer.Extension;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogicLayer.Features.Queries.Get;

public class GetEmployeeByIdQuery : IRequest<EmployeeDTO?>
{
    [Required]
    public string? EmployeeId { get; set; }
}

public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, EmployeeDTO?>
{
    private readonly AppDbContext _context;

    public GetEmployeeByIdQueryHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<EmployeeDTO?> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
    {
        return await _context
            .Users
            .Where(e => e.Id == request.EmployeeId)
            .Select(e => new EmployeeDTO
            {
                EmployeeId = e.Id,
                Name = e.FirstName,
                Role = e.Role.GetDisplayName(),
                Sites = e.ConstructionSiteEmployees.Select(cse => cse.ConstructionSite.Name).ToList()
            })
            .TagWith("Get Employee By Id")
            .SingleOrDefaultAsync();
    }
}

