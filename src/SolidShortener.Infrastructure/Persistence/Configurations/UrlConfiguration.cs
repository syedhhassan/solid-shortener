using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolidShortener.Domain.Entities.Urls;

namespace SolidShortener.Infrastructure.Persistence.Configurations;

public class UrlConfiguration : IEntityTypeConfiguration<Url>
{
    public void Configure(EntityTypeBuilder<Url> entity)
    {
        entity.HasKey(e => e.Id);
        entity.HasIndex(e => e.ShortCode).IsUnique();

        entity.Property(e => e.Id)
              .ValueGeneratedOnAdd();

        entity.Property(e => e.UserId)
              .IsRequired();

        entity.Property(e => e.LongUrl)
              .IsRequired()
              .HasMaxLength(2048);

        entity.Property(e => e.ShortCode)
              .IsRequired()
              .HasMaxLength(10);

        entity.Property(e => e.VisitsCount)
              .IsRequired();

        entity.Property(e => e.ExpiresAt);

        entity.Property(e => e.CreatedAt)
                .IsRequired();

        entity.Property(e => e.UpdatedAt)
                .IsRequired();

        entity.Property(e => e.IsDeleted)
                .IsRequired();

        entity.HasOne(e => e.User)
              .WithMany(u => u.Urls)
              .HasForeignKey(e => e.UserId)
              .OnDelete(DeleteBehavior.Cascade);
    }

}
