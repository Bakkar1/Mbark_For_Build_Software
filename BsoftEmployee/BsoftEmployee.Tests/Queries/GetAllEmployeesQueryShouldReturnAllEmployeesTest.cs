using BusinessLogicLayer.Features.Queries.Get;
using DataAccessLayer.Data;
using Microsoft.Extensions.DependencyInjection;

namespace BsoftEmployee.Tests.Queries
{
    public class GetAllEmployeesQueryShouldReturnAllEmployeesTest : TestBase
    {
        [Fact]
        public async Task GetAllEmployeesQuery_Should_Return_All_Employees()
        {
            // Arrange
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                await SeedEmployees(context);
                var query = new GetAllEmployeesQuery();

                // Act
                var handler = new GetAllEmployeesQueryHandler(context);
                var result = await handler.Handle(query, CancellationToken.None);

                // Assert
                Assert.NotNull(result);
                Assert.NotEmpty(result);
            }
        }
    }
}
