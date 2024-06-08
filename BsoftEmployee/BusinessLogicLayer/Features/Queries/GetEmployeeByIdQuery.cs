using DataAccessLayer.Data;
using DataAccessLayer.Model;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogicLayer.Features.Queries;

public class GetEmployeeByIdQuery : IRequest<Employee?>
{
    [Required]
    public string? EmployeeId { get; set; }
}

public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, Employee?>
{
    private readonly AppDbContext _context;
    public GetEmployeeByIdQueryHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Employee?> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
    {
        return await _context.Users.FindAsync(request.EmployeeId);
    }
}

