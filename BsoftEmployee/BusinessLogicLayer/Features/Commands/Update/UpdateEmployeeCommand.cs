using BusinessLogicLayer.Helper;
using DataAccessLayer.Data;
using DataAccessLayer.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogicLayer.Features.Commands.Update;

public class UpdateUserCommand : IRequest<BsoftResult>
{
    [Required]
    public Employee? Employee { get; set; }
}

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, BsoftResult>
{
    private readonly AppDbContext _context;
    public UpdateUserCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<BsoftResult> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        if(request.Employee is null)
        {
            return new BsoftResult()
            {
                Succeeded = false,
                Errors = new List<string>() { $"Please Provied An Employee" }
            };
        }

        var employee = await _context.Users.SingleOrDefaultAsync(e => e.Id == request.Employee.Id);

        if(employee is null)
        {
            return new BsoftResult()
            {
                Succeeded = false,
                Errors = new List<string>() { $"No Employee was found with the id : {request.Employee.Id}" }
            };
        }

        employee.FirstName = request.Employee.FirstName;
        employee.Role = request.Employee.Role;

        _context.Users.Update(employee);
        var result = await _context.SaveChangesAsync(cancellationToken);
        if (result > 0)
        {
            return new BsoftResult()
            {
                Succeeded = true,
                Message = "Employee Is Updated"
            };
        }
        else
        {
            return new BsoftResult()
            {
                Succeeded = false,
                Message = "Employee Is Not Updated"
            };
        }
    }
}
