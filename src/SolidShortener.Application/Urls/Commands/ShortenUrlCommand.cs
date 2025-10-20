namespace SolidShortener.Application.Urls.Commands;

public class ShortenUrlCommand
{
    public Guid UserId { get; set; }
    public string LongUrl { get; set; } = string.Empty;
    public DateTime? ExpiresAt { get; set; }
}
