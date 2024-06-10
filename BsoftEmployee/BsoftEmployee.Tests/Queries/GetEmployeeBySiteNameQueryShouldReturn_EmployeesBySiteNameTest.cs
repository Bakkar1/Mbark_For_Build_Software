using BusinessLogicLayer.Features.Queries.Get;
using DataAccessLayer.Data;
using Microsoft.Extensions.DependencyInjection;

namespace BsoftEmployee.Tests.Queries
{
    public class GetEmployeeBySiteNameQueryShouldReturn_EmployeesBySiteNameTest : TestBase
    {
        [Fact]
        public async Task GetEmployeeBySiteNameQuery_Should_Return_Employees_By_SiteName()
        {
            // Arrange
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                await SeedEmployees(context);
                await SeedConstructionSites(context);
                await SeedConstructionSiteEmployees(context);
                var query = new GetEmployeeBySiteNameQuery { SiteName = "Site A" };

                // Act
                var handler = new GetEmployeeBySiteNameQueryHandler(context);
                var result = await handler.Handle(query, CancellationToken.None);

                // Assert
                Assert.NotNull(result);
                Assert.NotEmpty(result);
            }
        }
    }
}
