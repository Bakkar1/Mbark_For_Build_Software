using BusinessLogicLayer.Features.Commands.Add;
using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BsoftEmployee.Tests.Commands.Add
{
    public class AddEmployeeToSiteCommandTest : TestBase
    {
        [Fact]
        public async Task Handle_Should_Add_Employee_To_Site()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                // Arrange
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                await SeedEmployees(context);
                await SeedConstructionSites(context);

                var handler = new AddEmployeeToSiteCommandHandler(context);
                var command = new AddEmployeeToSiteCommand
                {
                    ConstructionSiteId = 1,
                    EmployeeId = "1"
                };

                // Act
                var result = await handler.Handle(command, CancellationToken.None);

                // Assert
                Assert.True(result.Succeeded);
                Assert.Equal("Employee Is Added To The Site", result.Message);
                var constructionSiteEmployee = await context.ConstructionSiteEmployees
                    .FirstOrDefaultAsync(cse => cse.ConstructionSiteId == command.ConstructionSiteId && cse.EmployeeId == command.EmployeeId);
                Assert.NotNull(constructionSiteEmployee);
            }
        }

        [Fact]
        public async Task Handle_Should_Return_Error_If_ConstructionSite_Not_Found()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                // Arrange
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                await SeedEmployees(context);

                var handler = new AddEmployeeToSiteCommandHandler(context);
                var command = new AddEmployeeToSiteCommand
                {
                    ConstructionSiteId = 99, // Non-existing site
                    EmployeeId = "1"
                };

                // Act
                var result = await handler.Handle(command, CancellationToken.None);

                // Assert
                Assert.False(result.Succeeded);
                Assert.Contains($"No ConstructionSite was found with the id : {command.ConstructionSiteId}", result.Errors);
            }
        }

        [Fact]
        public async Task Handle_Should_Return_Error_If_Employee_Not_Found()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                // Arrange
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                await SeedConstructionSites(context);

                var handler = new AddEmployeeToSiteCommandHandler(context);
                var command = new AddEmployeeToSiteCommand
                {
                    ConstructionSiteId = 1,
                    EmployeeId = "99" // Non-existing employee
                };

                // Act
                var result = await handler.Handle(command, CancellationToken.None);

                // Assert
                Assert.False(result.Succeeded);
                Assert.Contains($"No Employee was found with the id : {command.EmployeeId}", result.Errors);
            }
        }

        [Fact]
        public async Task Handle_Should_Return_Error_If_Employee_Already_In_Site()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                // Arrange
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                await SeedEmployees(context);
                await SeedConstructionSites(context);
                await SeedConstructionSiteEmployees(context);

                var handler = new AddEmployeeToSiteCommandHandler(context);
                var command = new AddEmployeeToSiteCommand
                {
                    ConstructionSiteId = 1,
                    EmployeeId = "1" // Employee already in site
                };

                // Act
                var result = await handler.Handle(command, CancellationToken.None);

                // Assert
                Assert.False(result.Succeeded);
                Assert.Contains($"The Employee with Id : {command.EmployeeId} Is Alreadt exist in the ConstructionSiteId with id : {command.ConstructionSiteId}", result.Errors);
            }
        }
    }
}

