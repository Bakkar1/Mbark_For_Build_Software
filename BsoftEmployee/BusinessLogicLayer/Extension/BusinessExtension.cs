using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DataAccessLayer.Extension;
using MediatR;
using DataAccessLayer.Model;
using BusinessLogicLayer.Features.Queries;
using BusinessLogicLayer.Features.Commands.Add;
using DataAccessLayer.DTOs;
using BusinessLogicLayer.Features.Commands.Delete;
using BusinessLogicLayer.Features.Commands.Update;
using System.Reflection;

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
            services.AddTransient<IRequestHandler<CreateConstructionSiteCommand, ConstructionSiteDTO>, CreateConstructionSiteCommandHandler>();
            services.AddTransient<IRequestHandler<DeleteConstructionSiteCommand>, DeleteConstructionSiteCommandHandler>();
            services.AddTransient<IRequestHandler<UpdateConstructionSiteCommand, ConstructionSiteDTO>, UpdateConstructionSiteCommandHandler>();
            services.AddTransient<IRequestHandler<AddEmployeeToSiteCommand, bool>, AddEmployeeToSiteCommandHandler>();

            // Queries
            services.AddTransient<IRequestHandler<GetAllConstructionSitesQuery, List<ConstructionSiteDTO>?>, GetAllConstructionSitesQueryHandler>();
            services.AddTransient<IRequestHandler<GetConstructionSiteByIdQuery, ConstructionSiteDTO?>, GetConstructionSiteByIdQueryHandler>();
            services.AddTransient<IRequestHandler<GetEmployeeByIdQuery, Employee?>, GetEmployeeByIdQueryHandler>();
            services.AddTransient<IRequestHandler<GetEmployeeQuery, List<Employee>?>, GetEmployeeQueryHandler>();
            return services;
        }
    }
}
