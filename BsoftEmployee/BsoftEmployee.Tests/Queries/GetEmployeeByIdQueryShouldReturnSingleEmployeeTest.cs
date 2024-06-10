using BusinessLogicLayer.Features.Queries.Get;
using DataAccessLayer.Data;
using Microsoft.Extensions.DependencyInjection;

namespace BsoftEmployee.Tests.Queries
{
    public class GetEmployeeByIdQueryShouldReturnSingleEmployeeTest : TestBase
    {
        [Fact]
        public async Task GetEmployeeByIdQuery_Should_Return_Single_Employee()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                await SeedEmployees(context);
                var query = new GetEmployeeByIdQuery { EmployeeId = "1" };

                // Act
                var handler = new GetEmployeeByIdQueryHandler(context);
                var result = await handler.Handle(query, CancellationToken.None);

                // Assert
                Assert.NotNull(result);
            }
        }
    }
}
