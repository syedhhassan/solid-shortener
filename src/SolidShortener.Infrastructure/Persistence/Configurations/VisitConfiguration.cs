using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolidShortener.Domain.Entities.Visits;

namespace SolidShortener.Infrastructure.Persistence.Configurations;

public class VisitConfiguration : IEntityTypeConfiguration<Visit>
{
    public void Configure(EntityTypeBuilder<Visit> entity)
    {
        entity.ToTable("visits");

        entity.HasKey(e => e.Id);

        entity.Property(e => e.Id)
              .ValueGeneratedOnAdd();

        entity.Property(e => e.UrlId)
              .IsRequired();

        entity.Property(e => e.IpAddress)
              .IsRequired()
              .HasMaxLength(45);

        entity.Property(e => e.UserAgent)
              .IsRequired()
              .HasMaxLength(512);

        entity.Property(e => e.VisitedAt)
              .IsRequired();

        entity.HasOne(e => e.Url)
              .WithMany(u => u.Visits)
              .HasForeignKey(e => e.UrlId)
              .OnDelete(DeleteBehavior.Cascade);
    }
}
