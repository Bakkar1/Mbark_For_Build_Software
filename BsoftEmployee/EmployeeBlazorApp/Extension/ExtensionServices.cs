﻿using BusinessLogicLayer.Extension;
using BusinessLogicLayer.Helper;

namespace BusinessLogicLayer.Extension;
public static class ExtensionServices
{
    public static IServiceCollection AddExtensionServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddBusinessServices(configuration);
        services.AddHttpClient(ApiHelper.ApiName, client =>
        {
            client.BaseAddress = new Uri(configuration[ApiHelper.ApiBaseUrlName]);
            client.DefaultRequestHeaders.Add(ApiHelper.ApiKeyName, configuration[ApiHelper.ApiKeyName]);
        });

        return services;
    }
}