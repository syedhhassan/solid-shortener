namespace SolidShortener.Application.Visits.DTOs;

public class VisitDTO
{
    public string ShortCode { get; set; } = string.Empty;
    public string IpAddress { get; set; } = string.Empty;
    public string UserAgent { get; set; } = string.Empty;
    public DateTime VisitedAt { get; set; }
}
