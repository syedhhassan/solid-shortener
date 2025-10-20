using System.ComponentModel.DataAnnotations;

namespace SolidShortener.Api.Models.Requests;

public class ShortenUrlRequest
{
    [Required, Url]
    public string LongUrl { get; set; } = string.Empty;

    public DateTime? ExpiresAt { get; set; }
}
