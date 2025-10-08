using SolidShortener.Application.Visits.DTOs;

namespace SolidShortener.Application.Mappers;

public static class VisitMapper
{
    public static VisitDTO ToDTO(Visit visit)
    {
        if (visit.Url is null)
            throw new InvalidOperationException("Visit.Url is null. Include Url or project ShortCode.");

        return new VisitDTO
        {
            ShortCode = visit.Url.ShortCode,
            IpAddress = visit.IpAddress,
            UserAgent = visit.UserAgent,
            VisitedAt = visit.VisitedAt
        };
    }
}
