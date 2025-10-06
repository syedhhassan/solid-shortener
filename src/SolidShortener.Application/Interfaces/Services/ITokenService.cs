using SolidShortener.Application.Users.DTOs;

namespace SolidShortener.Application.Interfaces.Services;

public interface ITokenService
{
    string GenerateToken(UserDTO user);
    DateTime GetTokenExpirationTime(string token);
}
