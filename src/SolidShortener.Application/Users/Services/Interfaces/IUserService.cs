using SolidShortener.Application.Users.Commands;
using SolidShortener.Application.Users.DTOs;
using SolidShortener.Application.Users.Queries;

namespace SolidShortener.Application.Users.Services.Interfaces;

public interface IUserService
{
    Task<UserDTO> RegisterUserAsync(RegisterUserCommand command);
    Task<UserDTO?> GetUserByIdAsync(Guid id);
    Task<UserDTO?> GetUserByEmailAsync(GetUserByEmailQuery query);
    Task<AuthResultDTO?> AuthenticateAsync(AuthenticateUserQuery query);
}
