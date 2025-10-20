using Microsoft.Extensions.DependencyInjection;

namespace SolidShortener.Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUrlService, UrlService>();
        services.AddScoped<IVisitService, VisitService>();
    }
}
