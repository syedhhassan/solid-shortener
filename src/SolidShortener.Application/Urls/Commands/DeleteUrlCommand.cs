namespace SolidShortener.Application.Urls.Commands;

public class DeleteUrlCommand
{
    public Guid UserId { get; set; }
    public string ShortCode { get; set; } = string.Empty;
}
