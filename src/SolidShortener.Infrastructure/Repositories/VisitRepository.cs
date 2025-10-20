using SolidShortener.Domain.Entities.Visits;
using SolidShortener.Application.Visits.Queries;

namespace SolidShortener.Infrastructure.Repositories;

public class VisitRepository : IVisitRepository
{
    private readonly ShortenerDbContext _dbContext;

    public VisitRepository(ShortenerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Visit visit)
    {
        await _dbContext.Visits.AddAsync(visit);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Visit>> GetVisitsByShortCodeAsync(GetVisitsByShortCodeQuery query) =>
        await _dbContext.Visits
            .AsNoTracking()
            .Include(v => v.Url)
            .Where(v =>
                v.Url != null && v.Url.ShortCode == query.ShortCode &&
                v.Url.UserId == query.UserId &&
                !v.Url.IsDeleted &&
                v.Url.User != null &&
                !v.Url.User.IsDeleted
            )
            .ToListAsync();

    public async Task<int> GetVisitCountByShortCodeAsync(GetVisitCountByShortCodeQuery query) =>
        await _dbContext.Urls
            .AsNoTracking()
            .Where(u =>
                u.ShortCode == query.ShortCode &&
                u.UserId == query.UserId &&
                !u.IsDeleted &&
                u.User != null &&
                !u.User.IsDeleted
            )
            .Select(u => u.VisitsCount)
            .SingleOrDefaultAsync();
}
