using Microsoft.EntityFrameworkCore;
using SolidShortener.Domain.Entities.Users;
using SolidShortener.Domain.Entities.Urls;
using SolidShortener.Domain.Entities.Visits;

namespace SolidShortener.Infrastructure.Persistence;

public class ShortenerDbContext : DbContext
{
    public ShortenerDbContext(DbContextOptions<ShortenerDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Url> Urls { get; set; } = null!;
    public DbSet<Visit> Visits { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ShortenerDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
