using SolidShortener.Domain.Entities.Urls;

namespace SolidShortener.Domain.Entities.Visits;

public class Visit
{
    public long Id { get; set; }
    public long UrlId { get; private set; }
    public DateTime VisitedAt { get; private set; } = DateTime.UtcNow;
    public string IpAddress { get; private set; } = string.Empty;
    public string UserAgent { get; private set; } = string.Empty;

    public Url? Url { get; private set; } // Navigation property    

    private Visit() { } // EF Core

    public Visit(long urlId, string ipAddress, string userAgent)
    {
        UrlId = urlId;
        IpAddress = ipAddress;
        UserAgent = userAgent;
    }
}
