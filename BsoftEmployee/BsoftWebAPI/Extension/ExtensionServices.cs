using BusinessLogicLayer.Extension;

namespace BsoftWebAPI.Extension;
public static class ExtensionServices
{
    public static IServiceCollection AddExtensionServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddBusinessServices(configuration);

        return services;
    }
}