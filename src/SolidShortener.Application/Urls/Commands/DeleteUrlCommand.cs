namespace SolidShortener.Application.Urls.Commands;

public class DeleteUrlCommand
{
    public string ShortCode { get; set; } = string.Empty;
    public Guid UserId { get; set; }
}
