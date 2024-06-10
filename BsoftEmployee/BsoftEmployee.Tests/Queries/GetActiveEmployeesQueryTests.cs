using BusinessLogicLayer.Features.Queries.Get;
using DataAccessLayer.Data;
using DataAccessLayer.Enums;
using DataAccessLayer.Model;
using Microsoft.Extensions.DependencyInjection;

namespace BsoftEmployee.Tests.Queries;

public class GetActiveEmployeesQueryHandlerTests : TestBase
{
    [Fact]
    public async Task Handle_ReturnsActiveEmployees()
    {
        // Arrange
        using (var scope = _serviceProvider.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            await SeedData(context);

            var handler = new GetActiveEmployeesQueryHandler(context);
            var query = new GetActiveEmployeesQuery();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("John Doe", result.First().Name);
            Assert.Equal("Metselaar", result.First().Role);
        }
    }
    [Fact]
    public async Task Handle_ReturnsNoEmployees()
    {
        // Arrange
        using (var scope = _serviceProvider.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var handler = new GetActiveEmployeesQueryHandler(context);
            var query = new GetActiveEmployeesQuery();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result is null || !result.Any());
        }
    }
    private async Task SeedData(AppDbContext context)
    {
        var constructionSite = new ConstructionSite
        {
            Name = "Active Site",
            StartDate = DateTime.Now.AddDays(-1),
            Status = ConstructionSiteStatus.InProgress
        };

        var employee = new Employee
        {
            FirstName = "John Doe",
            Role = EmployeeRole.Mason
        };

        context.ConstructionSites.Add(constructionSite);
        context.Employees.Add(employee);
        await context.SaveChangesAsync();

        context.ConstructionSiteEmployees.Add(new ConstructionSiteEmployee
        {
            ConstructionSiteId = constructionSite.ConstructionSiteId,
            EmployeeId = employee.Id
        });

        await context.SaveChangesAsync();
    }
}


