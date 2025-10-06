namespace SolidShortener.Application.Urls.DTOs;

public class GetLongUrlDTO
{
    public string LongUrl { get; set; } = string.Empty;
    public DateTime? ExpiresAt { get; set; }
}
