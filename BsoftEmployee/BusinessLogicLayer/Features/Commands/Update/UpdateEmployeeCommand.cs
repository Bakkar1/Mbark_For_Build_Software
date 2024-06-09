using DataAccessLayer.Data;
using DataAccessLayer.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogicLayer.Features.Commands.Update;

public class UpdateUserCommand : IRequest<bool>
{
    [Required]
    public Employee? Employee { get; set; }
}

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
{
    private readonly AppDbContext _context;
    public UpdateUserCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        if(request.Employee is null)
        {
            return false;
        }

        var employee = await _context.Users.SingleOrDefaultAsync(e => e.Id == request.Employee.Id);

        if(employee is null) { return false; }

        employee.FirstName = request.Employee.FirstName;
        employee.Role = request.Employee.Role;

        _context.Users.Update(employee);
        var result = await _context.SaveChangesAsync(cancellationToken);
        return result > 0;
    }
}
