using DataAccessLayer.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccessLayer.Data.Default
{
    public static class DataSeeder
    {
        private static readonly ConstructionSite ConstructionSiteA = new ConstructionSite { Name = "Site A", StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(3)};
        private static readonly ConstructionSite ConstructionSiteB = new ConstructionSite { Name = "Site B", StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(3) };
        private static readonly Employee Employee1 = new Employee { UserName = "john@example.com", Email = "john@example.com", FirstName = "John Doe" };
        private static readonly Employee Employee2 = new Employee { UserName = "jane@example.com", Email = "jane@example.com", FirstName = "Jane Smith" };

        public static async Task SeedDataAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Employee>>();

            if (!context.Employees.Any())
            {
                await SeedEmployeesAsync(userManager);
                await SeedConstructionSitesAsync(context);
                await SeedConstructionSiteEmployeesAsync(context);
            }
        }

        private static async Task SeedEmployeesAsync(UserManager<Employee> userManager)
        {
            if (await userManager.FindByEmailAsync(Employee1.Email) == null)
            {
                await userManager.CreateAsync(Employee1, "Password1");
            }

            if (await userManager.FindByEmailAsync(Employee2.Email) == null)
            {
                await userManager.CreateAsync(Employee2, "Password2");
            }
        }

        private static async Task SeedConstructionSitesAsync(AppDbContext context)
        {
            context.ConstructionSites.AddRange(ConstructionSiteA, ConstructionSiteB);
            await context.SaveChangesAsync();
        }

        private static async Task SeedConstructionSiteEmployeesAsync(AppDbContext context)
        {
            var employee1 = await context.Employees.FirstOrDefaultAsync(e => e.UserName == Employee1.UserName);
            var employee2 = await context.Employees.FirstOrDefaultAsync(e => e.UserName == Employee2.UserName);

            if (employee1 != null && employee2 != null)
            {
                context.ConstructionSiteEmployees.AddRange(
                    new ConstructionSiteEmployee { EmployeeId = employee1.Id, ConstructionSiteId = ConstructionSiteA.ConstructionSiteId },
                    new ConstructionSiteEmployee { EmployeeId = employee2.Id, ConstructionSiteId = ConstructionSiteB.ConstructionSiteId }
                );
                await context.SaveChangesAsync();
            }
        }
    }
}
