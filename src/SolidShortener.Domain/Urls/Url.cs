using SolidShortener.Domain.Users;
using SolidShortener.Domain.Visits;

namespace SolidShortener.Domain.Urls;

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

    public void IncrementVisitsCount()
    {
        VisitsCount++;
    }
}
