using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolidShortener.Domain.Entities.Users;

namespace SolidShortener.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> entity)
    {
        entity.ToTable("users");

        entity.HasKey(e => e.Id);
        entity.Property(e => e.Id)
              .ValueGeneratedNever();

        entity.Property(e => e.Name)
              .IsRequired()
              .HasMaxLength(100);

        entity.HasIndex(e => e.Email).IsUnique();
        entity.Property(e => e.Email)
              .IsRequired()
              .HasMaxLength(255);

        entity.Property(e => e.PasswordHash)
              .IsRequired()
              .HasMaxLength(255);

        entity.Property(e => e.CreatedAt)
              .IsRequired();

        entity.Property(e => e.UpdatedAt)
              .IsRequired();

        entity.Property(e => e.IsDeleted)
              .IsRequired();
    }
}
