using SolidShortener.Domain.Urls;

namespace SolidShortener.Domain.Visits;

public class Visit
{
    public long Id { get; private set; }
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
