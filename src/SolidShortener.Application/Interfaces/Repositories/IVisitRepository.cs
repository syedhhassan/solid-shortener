using SolidShortener.Application.Visits.Queries;

namespace SolidShortener.Application.Interfaces.Repositories;

public interface IVisitRepository
{
    Task AddAsync(Visit visit);
    Task<IEnumerable<Visit>> GetVisitsByShortCodeAsync(GetVisitsByShortCodeQuery query);
    Task<int> GetVisitCountByShortCodeAsync(GetVisitCountByShortCodeQuery query);
}
