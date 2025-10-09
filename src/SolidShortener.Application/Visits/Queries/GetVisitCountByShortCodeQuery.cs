namespace SolidShortener.Application.Visits.Queries;

public class GetVisitCountByShortCodeQuery
{
    public string ShortCode { get; set; } = string.Empty;
    public Guid UserId { get; set; }
}
