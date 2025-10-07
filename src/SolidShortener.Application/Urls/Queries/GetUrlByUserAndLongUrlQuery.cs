namespace SolidShortener.Application.Urls.Queries;

public class GetUrlByUserAndLongUrlQuery
{
    public Guid UserId { get; set; }
    public string LongUrl { get; set; } = string.Empty;
}
