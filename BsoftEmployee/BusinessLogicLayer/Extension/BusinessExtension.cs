using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DataAccessLayer.Extension;
using MediatR;
using DataAccessLayer.Model;
using BusinessLogicLayer.Features.Commands.Add;
using DataAccessLayer.DTOs;
using BusinessLogicLayer.Features.Commands.Delete;
using BusinessLogicLayer.Features.Commands.Update;
using System.Reflection;
using Microsoft.AspNetCore.Identity;
using BusinessLogicLayer.Features.Commands.DeleteUser;
using BusinessLogicLayer.Features.Queries.Get;
using BusinessLogicLayer.Features.Queries.GetUsers;

namespace BusinessLogicLayer.Extension
{
    public static class BusinessExtension
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Add MediatR
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddDataServices(configuration);

            // Commmands
            //Add
            services.AddTransient<IRequestHandler<CreateConstructionSiteCommand, ConstructionSiteDTO>, CreateConstructionSiteCommandHandler>();
            services.AddTransient<IRequestHandler<AddEmployeeToSiteCommand, bool>, AddEmployeeToSiteCommandHandler>();
            services.AddTransient<IRequestHandler<AddEmployeeCommand, IdentityResult>, AddEmployeeCommandHandler>();

            //Update
            services.AddTransient<IRequestHandler<UpdateConstructionSiteCommand, ConstructionSiteDTO>, UpdateConstructionSiteCommandHandler>();

            //Delete
            services.AddTransient<IRequestHandler<RemoveEmployeeFromSiteCommand, bool>, RemoveEmployeeFromSiteCommandHandler>();
            services.AddTransient<IRequestHandler<DeleteConstructionSiteCommand>, DeleteConstructionSiteCommandHandler>();
            services.AddTransient<IRequestHandler<DeleteEmployeeCommand, bool>, DeleteEmployeeCommandHandler>();

            // Queries

            //Get
            services.AddTransient<IRequestHandler<GetAllConstructionSitesQuery, List<ConstructionSiteDTO>?>, GetAllConstructionSitesQueryHandler>();
            services.AddTransient<IRequestHandler<GetConstructionSiteByIdQuery, ConstructionSiteDTO?>, GetConstructionSiteByIdQueryHandler>();
            services.AddTransient<IRequestHandler<GetEmployeeByIdQuery, EmployeeDTO?>, GetEmployeeByIdQueryHandler>();
            services.AddTransient<IRequestHandler<GetEmployeeBySiteNameQuery, List<EmployeeDTO>?>, GetEmployeeBySiteNameQueryHandler>();
            services.AddTransient<IRequestHandler<GetActiveEmployeesQuery, List<EmployeeDTO>?>, GetActiveEmployeesQueryHandler>();
            services.AddTransient<IRequestHandler<GetAllEmployeesQuery, List<EmployeeDTO>?>, GetAllEmployeesQueryHandler>();


            return services;
        }
    }
}
