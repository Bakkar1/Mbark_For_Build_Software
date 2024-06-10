using BusinessLogicLayer.Features.Queries.Get;
using DataAccessLayer.Data;
using Microsoft.Extensions.DependencyInjection;

namespace BsoftEmployee.Tests.Queries
{
    public class GetConstructionSiteByIdQueryShouldReturnConstructionSiteWithEmployeesTest : TestBase
    {
        [Fact]
        public async Task GetConstructionSiteByIdQuery_Should_Return_ConstructionSite_With_Employees()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                await SeedEmployees(context);
                await SeedConstructionSites(context);
                await SeedConstructionSiteEmployees(context);
                var query = new GetConstructionSiteByIdQuery { GetConstructionSiteId = 1 };

                // Act
                var handler = new GetConstructionSiteByIdQueryHandler(context);
                var result = await handler.Handle(query, CancellationToken.None);

                // Assert
                Assert.NotNull(result);
                Assert.NotNull(result.Employees);
                Assert.NotEmpty(result.Employees);
            }
        }
    }
}
