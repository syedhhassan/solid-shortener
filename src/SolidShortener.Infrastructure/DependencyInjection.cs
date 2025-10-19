using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SolidShortener.Infrastructure.Configurations;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, IConfiguration configuration)
    {
        // Postgres Database
        services.AddDbContext<ShortenerDbContext>(options =>
            options
                .UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
                .UseSnakeCaseNamingConvention());

        // Redis Caching
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("Redis");
            options.InstanceName = "SolidShortener_";
        });

        // JWT Configuration
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
        services.AddScoped<ITokenGenerator, JwtTokenGenerator>();

        // Password Hashing and Short Code Generation
        services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();
        services.AddSingleton<IShortCodeGenerator, Base62ShortCodeGenerator>();

        // Repositories
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUrlRepository, UrlRepository>();
        services.AddScoped<IVisitRepository, VisitRepository>();

        // Decorators
        services.Decorate<IUrlRepository, CachedUrlRepository>();

        return services;
    }
}
