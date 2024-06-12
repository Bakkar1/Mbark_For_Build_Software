using BusinessLogicLayer.Features.Commands.Add;
using BusinessLogicLayer.Features.Commands.Delete;
using BusinessLogicLayer.Features.Commands.Update;
using BusinessLogicLayer.Features.Queries.Get;
using BusinessLogicLayer.Helper;
using DataAccessLayer.Data;
using DataAccessLayer.DTOs;
using DataAccessLayer.Enums;
using DataAccessLayer.Model;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BsoftEmployee.Tests
{
    public abstract class TestBase
    {
        protected readonly ServiceProvider _serviceProvider;

        public TestBase()
        {
            var serviceCollection = new ServiceCollection();

            // Add DbContext with in-memory database
            serviceCollection.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase($"InMemoryDb-{Guid.NewGuid()}"));

            serviceCollection.AddIdentityCore<Employee>(
                options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequiredLength = 4;
                    options.User.RequireUniqueEmail = true;
                }
             )
            .AddEntityFrameworkStores<AppDbContext>();

            // Add MediatR
            serviceCollection.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            // Commmands
            //Add
            serviceCollection.AddTransient<IRequestHandler<CreateConstructionSiteCommand, ConstructionSiteDTO>, CreateConstructionSiteCommandHandler>();
            serviceCollection.AddTransient<IRequestHandler<AddEmployeeToSiteCommand, BsoftResult>, AddEmployeeToSiteCommandHandler>();
            serviceCollection.AddTransient<IRequestHandler<AddEmployeeCommand, IdentityResult>, AddEmployeeCommandHandler>();

            //Update
            serviceCollection.AddTransient<IRequestHandler<UpdateConstructionSiteCommand, ConstructionSiteDTO>, UpdateConstructionSiteCommandHandler>();

            //Delete
            serviceCollection.AddTransient<IRequestHandler<RemoveEmployeeFromSiteCommand, BsoftResult>, RemoveEmployeeFromSiteCommandHandler>();
            serviceCollection.AddTransient<IRequestHandler<DeleteConstructionSiteCommand, BsoftResult>, DeleteConstructionSiteCommandHandler>();
            serviceCollection.AddTransient<IRequestHandler<DeleteEmployeeCommand, BsoftResult>, DeleteEmployeeCommandHandler>();

            // Queries

            //Get
            serviceCollection.AddTransient<IRequestHandler<GetAllConstructionSitesQuery, List<ConstructionSiteDTO>?>, GetAllConstructionSitesQueryHandler>();
            serviceCollection.AddTransient<IRequestHandler<GetConstructionSiteByIdQuery, ConstructionSiteDTO?>, GetConstructionSiteByIdQueryHandler>();
            serviceCollection.AddTransient<IRequestHandler<GetEmployeeByIdQuery, EmployeeDTO?>, GetEmployeeByIdQueryHandler>();
            serviceCollection.AddTransient<IRequestHandler<GetEmployeeBySiteNameQuery, List<EmployeeDTO>?>, GetEmployeeBySiteNameQueryHandler>();
            serviceCollection.AddTransient<IRequestHandler<GetActiveEmployeesQuery, List<EmployeeDTO>?>, GetActiveEmployeesQueryHandler>();
            serviceCollection.AddTransient<IRequestHandler<GetAllEmployeesQuery, List<EmployeeDTO>?>, GetAllEmployeesQueryHandler>();


            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        public async Task SeedEmployees(AppDbContext context)
        {
            context.Employees.Add(new Employee { Id = "1", FirstName = "John", Role = EmployeeRole.Mason });
            context.Employees.Add(new Employee { Id = "2", FirstName = "Jane", Role = EmployeeRole.Carpenter });
            await context.SaveChangesAsync();
        }

        public async Task SeedConstructionSites(AppDbContext context)
        {
            context.ConstructionSites.Add(new ConstructionSite { ConstructionSiteId = 1, Name = "Site A", StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(1), Status = ConstructionSiteStatus.InProgress });
            context.ConstructionSites.Add(new ConstructionSite { ConstructionSiteId = 2, Name = "Site B", StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(1), Status = ConstructionSiteStatus.InProgress });
            await context.SaveChangesAsync();
        }

        public async Task SeedConstructionSiteEmployees(AppDbContext context)
        {
            context.ConstructionSiteEmployees.Add(new ConstructionSiteEmployee { ConstructionSiteId = 1, EmployeeId = "1" });
            context.ConstructionSiteEmployees.Add(new ConstructionSiteEmployee { ConstructionSiteId = 2, EmployeeId = "2" });
            await context.SaveChangesAsync();
        }
    }
}
