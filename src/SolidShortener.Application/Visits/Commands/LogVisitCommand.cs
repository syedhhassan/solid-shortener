namespace SolidShortener.Application.Visits.Commands;

public class LogVisitCommand
{
    public Guid UserId { get; set; }
    public string ShortCode { get; set; } = string.Empty;
    public string IpAddress { get; set; } = string.Empty;
    public string UserAgent { get; set; } = string.Empty;
}
