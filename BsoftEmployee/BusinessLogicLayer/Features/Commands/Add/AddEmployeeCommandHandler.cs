using DataAccessLayer.Model;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BusinessLogicLayer.Features.Commands.Add;

public class AddEmployeeCommandHandler : IRequestHandler<AddEmployeeCommand, IdentityResult>
{
    private UserManager<Employee> _userManager;
    public AddEmployeeCommandHandler(UserManager<Employee> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IdentityResult> Handle(AddEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = request.Employee;
        var user = new Employee
        {
            UserName = employee.Email,
            Email = employee.Email,
            FirstName = employee.FirstName,
            Role = employee.Role
        };

        return await _userManager.CreateAsync(user, employee.Password);
    }
}
