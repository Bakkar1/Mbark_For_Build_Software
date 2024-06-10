using BusinessLogicLayer.Features.Queries.Get;
using DataAccessLayer.Data;
using DataAccessLayer.Enums;
using DataAccessLayer.Model;
using Microsoft.Extensions.DependencyInjection;

namespace BsoftEmployee.Tests.Queries
{
    public class GetAllConstructionSitesQueryTests : TestBase
    {
        [Fact]
        public async Task Handle_ReturnsAllConstructionSites()
        {
            // Arrange
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                await SeedData(context);

                var handler = new GetAllConstructionSitesQueryHandler(context);
                var query = new GetAllConstructionSitesQuery();

                // Act
                var result = await handler.Handle(query, CancellationToken.None);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(2, result.Count); // Assuming you seeded two construction sites
            }
        }
        private async Task SeedData(AppDbContext context)
        {
            var constructionSites = new List<ConstructionSite>
            {
                new ConstructionSite { Name = "Site A", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(30), Status = ConstructionSiteStatus.Created },
                new ConstructionSite { Name = "Site B", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(30), Status = ConstructionSiteStatus.Approved }
            };

            await context.ConstructionSites.AddRangeAsync(constructionSites);
            await context.SaveChangesAsync();
        }
    }
}
