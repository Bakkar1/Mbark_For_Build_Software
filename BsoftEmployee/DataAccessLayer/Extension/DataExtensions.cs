using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DataAccessLayer.Model;
using DataAccessLayer.Profiles;
using DataAccessLayer.Data.Default;
using Microsoft.AspNetCore.Identity;
using static System.Formats.Asn1.AsnWriter;

namespace DataAccessLayer.Extension
{
    public static class DataExtensions
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Register the AutoMapper profiles
            services.AddAutoMapper(typeof(MappingProfile));

            // Other services
            string conString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContextPool<AppDbContext>(
                        options => options.UseSqlServer(conString)
                    );



            services.AddIdentityCore<Employee>(
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

            //Ensure the database is created
            using (var serviceScope = services.BuildServiceProvider().CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
                dbContext.Database.EnsureCreated();
            }

            // Seed initial data
            using (var serviceScope = services.BuildServiceProvider().CreateScope())
            {
                _ = DataSeeder.SeedDataAsync(serviceScope.ServiceProvider);
            }

            return services;
        }
    }
}
