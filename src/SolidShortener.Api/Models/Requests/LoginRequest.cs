using System.ComponentModel.DataAnnotations;

namespace SolidShortener.Api.Models.Requests;

public class LoginRequest
{
    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required, MinLength(8)]
    public string Password { get; set; } = string.Empty;
}
