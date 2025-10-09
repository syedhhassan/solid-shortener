using SolidShortener.Domain.Urls;

namespace SolidShortener.Domain.Users;

public class User : BaseEntity
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;

    public ICollection<Url> Urls { get; private set; } = new List<Url>();

    private User() { } // EF Core

    public User(string name, string email, string passwordHash)
    {
        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        PasswordHash = passwordHash;
    }
}
