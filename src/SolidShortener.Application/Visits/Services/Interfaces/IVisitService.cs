using SolidShortener.Application.Visits.Commands;
using SolidShortener.Application.Visits.DTOs;
using SolidShortener.Application.Visits.Queries;

namespace SolidShortener.Application.Visits.Services.Interfaces;

public interface IVisitService
{
    Task LogVisitAsync(LogVisitCommand command);
    Task<int> GetVisitCountByShortCodeAsync(GetVisitCountByShortCodeQuery query);
    Task<IEnumerable<VisitDTO>> GetVisitsByShortCodeAsync(GetVisitsByShortCodeQuery query);
}
