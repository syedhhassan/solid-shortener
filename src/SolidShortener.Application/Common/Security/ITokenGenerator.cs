namespace SolidShortener.Application.Common.Security;

public interface ITokenGenerator
{
    string GenerateToken(User user);
}
