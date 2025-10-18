using SolidShortener.Application.Urls.Queries;
using SolidShortener.Domain.Entities.Urls;

namespace SolidShortener.Infrastructure.Repositories;

public class UrlRepository : IUrlRepository
{
    private readonly ShortenerDbContext _dbContext;

    public UrlRepository(ShortenerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Url url)
    {
        await _dbContext.Urls.AddAsync(url);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Url url)
    {
        _dbContext.Urls.Update(url);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Url?> GetUrlByShortCodeAsync(GetUrlByShortCodeQuery query) =>
        await _dbContext.Urls.AsNoTracking().SingleOrDefaultAsync(u => u.ShortCode == query.ShortCode && !u.IsDeleted);

    public async Task<Url?> GetByUserAndLongUrlAsync(GetUrlByUserAndLongUrlQuery query) =>
        await _dbContext.Urls.AsNoTracking().SingleOrDefaultAsync(u => u.UserId == query.UserId && u.LongUrl == query.LongUrl);

    public async Task<IEnumerable<Url>> GetUrlsByUserAsync(GetUrlsByUserQuery query) =>
        await _dbContext.Urls.AsNoTracking().Where(u => u.UserId == query.UserId && !u.IsDeleted).ToListAsync();

}
