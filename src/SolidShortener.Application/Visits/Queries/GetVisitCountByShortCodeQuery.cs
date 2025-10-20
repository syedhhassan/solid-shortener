namespace SolidShortener.Application.Visits.Queries;

public class GetVisitCountByShortCodeQuery
{
    public Guid UserId { get; set; }
    public string ShortCode { get; set; } = string.Empty;
}
