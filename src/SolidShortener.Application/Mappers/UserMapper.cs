using SolidShortener.Application.Users.DTOs;

namespace SolidShortener.Application.Mappers;

public static class UserMapper
{
    public static UserDTO ToDto(User user) =>
        new UserDTO
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            CreatedAt = user.CreatedAt
        };
}
