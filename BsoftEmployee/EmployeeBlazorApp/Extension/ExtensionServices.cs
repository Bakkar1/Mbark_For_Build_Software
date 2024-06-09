using BusinessLogicLayer.Extension;

namespace EmployeeBlazorApp.Extension;
public static class ExtensionServices
{
    public static IServiceCollection AddExtensionServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddBusinessServices(configuration);
        services.AddHttpClient("ApiHttpClient", client =>
        {
            client.BaseAddress = new Uri(configuration["ApiBaseUrl"]);
            client.DefaultRequestHeaders.Add("API-KEY", configuration["API-KEY"]);
        });

        return services;
    }
}