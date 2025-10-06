namespace SolidShortener.Application.Users.DTOs;

public class AuthResultDTO
{
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
}
