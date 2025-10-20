namespace SolidShortener.Application.Visits.Queries;

public class GetVisitsByShortCodeQuery
{
    public Guid UserId { get; set; }
    public string ShortCode { get; set; } = string.Empty;
}
