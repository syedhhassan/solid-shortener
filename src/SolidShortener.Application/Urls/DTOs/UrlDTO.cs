namespace SolidShortener.Application.Urls.DTOs;

public class UrlDTO
{
    public Guid UserId { get; set; }
    public string LongUrl { get; set; } = string.Empty;
    public string ShortCode { get; set; } = string.Empty;
    public int VisitsCount { get; set; }
    public DateTime? ExpiresAt { get; set; }
    public DateTime CreatedAt { get; set; }
}
