using BusinessLogicLayer.Extension;
using BusinessLogicLayer.Helper;
using Microsoft.OpenApi.Models;

namespace BsoftWebAPI.Extension;
public static class ExtensionServices
{
    public static IServiceCollection AddExtensionServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddBusinessServices(configuration);

        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Name = ApiHelper.ApiKeyName,
                Type = SecuritySchemeType.ApiKey,
                Description = "API Key needed to access the endpoints. X-API-KEY: {key}"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "ApiKey"
                        },
                        Name = "ApiKey",
                        In = ParameterLocation.Header,
                    },
                    new List<string>()
                }
            });
        });

        return services;
    }
}