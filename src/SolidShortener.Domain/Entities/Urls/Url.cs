using SolidShortener.Domain.Entities.Users;
using SolidShortener.Domain.Entities.Visits;

namespace SolidShortener.Domain.Entities.Urls;

public class Url : BaseEntity
{
    public long Id { get; private set; }
    public Guid UserId { get; private set; }
    public string LongUrl { get; private set; } = string.Empty;
    public string ShortCode { get; private set; } = string.Empty;
    public int VisitsCount { get; private set; }
    public DateTime? ExpiresAt { get; private set; }

    public ICollection<Visit> Visits { get; private set; } = new List<Visit>();

    public User? User { get; private set; } // Navigation property    

    private Url() { } // EF Core

    public Url(Guid userId, string longUrl, string shortCode, DateTime? expiresAt = null)
    {
        UserId = userId;
        LongUrl = longUrl;
        ShortCode = shortCode;
        VisitsCount = 0;
        ExpiresAt = expiresAt;
    }
    public void SetShortCode(string shortCode) => ShortCode = shortCode;

    public void IncrementVisitsCount()
    {
        VisitsCount++;
        UpdatedAt = DateTime.UtcNow;
    }

    public void MarkAsDeleted()
    {
        IsDeleted = true;
        UpdatedAt = DateTime.UtcNow;
    }

    public void SetExpiresAt(DateTime? expiresAt)
    {
        ExpiresAt = expiresAt;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UnDelete()
    {
        IsDeleted = false;
        UpdatedAt = DateTime.UtcNow;
    }
}
