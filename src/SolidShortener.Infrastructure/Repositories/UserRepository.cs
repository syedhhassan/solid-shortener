using SolidShortener.Domain.Entities.Users;

namespace SolidShortener.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ShortenerDbContext _dbContext;

    public UserRepository(ShortenerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(User user)
    {
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<User?> GetUserByIdAsync(Guid id) =>
        await _dbContext.Users.AsNoTracking().SingleOrDefaultAsync(u => u.Id == id && !u.IsDeleted);

    public async Task<User?> GetUserByEmailAsync(string email) =>
        await _dbContext.Users.AsNoTracking().SingleOrDefaultAsync(u => u.Email == email && !u.IsDeleted);
}
