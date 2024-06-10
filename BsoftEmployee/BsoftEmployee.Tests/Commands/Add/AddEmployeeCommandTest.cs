using BusinessLogicLayer.Features.Commands.Add;
using DataAccessLayer.Data;
using DataAccessLayer.DTOs;
using DataAccessLayer.Enums;
using DataAccessLayer.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace BsoftEmployee.Tests.Commands.Add
{
    public class AddEmployeeCommandTest : TestBase
    {
        [Fact]
        public async Task Handle_Should_Create_New_Employee()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                // Arrange
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Employee>>();
                var handler = new AddEmployeeCommandHandler(userManager);
                CreateEmployeeDTO employee = new CreateEmployeeDTO
                {
                    FirstName = "John",
                    Email = "john@example.com",
                    Password = "password",
                    Role = EmployeeRole.Mason
                };
                var command = new AddEmployeeCommand
                {
                    Employee = employee
                };

                // Act
                var result = await handler.Handle(command, CancellationToken.None);

                // Assert
                Assert.True(result.Succeeded);
                Assert.NotEmpty(context.Employees);
                var createdEmployee = context.Employees.FirstOrDefault();
                Assert.NotNull(createdEmployee);
                Assert.Equal(employee.FirstName, createdEmployee.FirstName);
                Assert.Equal(employee.Email, createdEmployee.Email);
                Assert.Equal(employee.Role, createdEmployee.Role);
            }
        }
        [Fact]
        public async Task Handle_Should_Not_Create_New_Employee_With_Short_Password()
        {
            // Arrange
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Employee>>();
                var handler = new AddEmployeeCommandHandler(userManager);
                var employee = new CreateEmployeeDTO
                {
                    FirstName = "John",
                    Email = "john@example.com",
                    Password = "ab", // Short password
                    Role = EmployeeRole.Mason
                };
                var command = new AddEmployeeCommand
                {
                    Employee = employee
                };

                // Act
                var result = await handler.Handle(command, CancellationToken.None);

                // Assert
                Assert.False(result.Succeeded); // Employee should not be created
                Assert.Empty(context.Employees); // No employees should be present in the context
            }
        }

    }
}
