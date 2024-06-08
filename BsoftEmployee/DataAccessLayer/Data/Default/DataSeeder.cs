using DataAccessLayer.Model;
using Microsoft.AspNetCore.Identity;

namespace DataAccessLayer.Data.Default;

public static class DataSeeder
{
    private static ConstructionSite ConstructionSiteA = new ConstructionSite { Name = "Site A", /* other properties */ };
    private static ConstructionSite ConstructionSiteB = new ConstructionSite { Name = "Site B", /* other properties */ };
    private static Employee Employee1 = new Employee { UserName = "john@example.com", Email = "john@example.com", FirstName = "John Doe", /* other properties */ };
    private static Employee Employee2 = new Employee {UserName = "jane@example.com", Email = "jane@example.com", FirstName = "Jane Smith", /* other properties */ };
    
    public static void SeedData(AppDbContext context, UserManager<Employee> userManager)
    {
        if (!context.Employees.Any())
        {
            SeedEmployees(userManager);
            SeedConstructionSites(context);
            SeedConstructionSiteEmployees(context);
        }
    }

    private static async Task SeedEmployees(UserManager<Employee> userManager)
    {
        await userManager.CreateAsync(Employee1, "YourPassword1");
        await userManager.CreateAsync(Employee2, "YourPassword2");
    }

    private static async Task SeedConstructionSites(AppDbContext context)
    {
        context.ConstructionSites.AddRange(
            ConstructionSiteA,
            ConstructionSiteB
        );
        await context.SaveChangesAsync();
    }

    private static async Task SeedConstructionSiteEmployees(AppDbContext context)
    {
        context.ConstructionSiteEmployees.AddRange(
            new ConstructionSiteEmployee { EmployeeId = Employee1.Id, ConstructionSiteId = ConstructionSiteA.ConstructionSiteId },
            new ConstructionSiteEmployee { EmployeeId = Employee2.Id, ConstructionSiteId = ConstructionSiteB.ConstructionSiteId }
        );
        await context.SaveChangesAsync();
    }
}
