using SolidShortener.Api.Filters;

namespace SolidShortener.Api.Extensions;

public static class ControllersExtensions
{
    public static IServiceCollection AppApiControllers(this IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            options.Filters.Add<ValidationFilter>();
        });

        return services;
    }
}
