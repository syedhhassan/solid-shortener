using SolidShortener.Application.Urls.DTOs;

namespace SolidShortener.Application.Mappers;

public class UrlMapper
{
    public static UrlDTO ToDTO(Url url)
    {
        return new UrlDTO
        {
            UserId = url.UserId,
            LongUrl = url.LongUrl,
            ShortCode = url.ShortCode,
            VisitsCount = url.VisitsCount,
            ExpiresAt = url.ExpiresAt,
            CreatedAt = url.CreatedAt,
        };
    }
}
