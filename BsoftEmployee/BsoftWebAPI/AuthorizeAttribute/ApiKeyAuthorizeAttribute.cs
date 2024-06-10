using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using BsoftWebAPI.Helper;

namespace BsoftWebAPI.AuthorizeAttribute;

public class ApiKeyAuthorizeAttribute : Attribute, IAsyncActionFilter
{

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.HttpContext.Request.Headers.TryGetValue(ApiHelper.ApiKeyName, out var extractedApiKey))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
        var apiKey = configuration.GetValue<string>(ApiHelper.ApiKeyName);

        if (!apiKey.Equals(extractedApiKey))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        await next();
    }
}

