using SolidShortener.Application.Urls.DTOs;
using SolidShortener.Application.Urls.Queries;

namespace SolidShortener.Application.Interfaces.Repositories;

public interface IUrlRepository
{
    Task AddAsync(Url url);
    Task UpdateAsync(Url url);
    Task<Url?> GetUrlByShortCodeAsync(GetUrlByShortCodeQuery query);
    Task<Url?> GetByUserAndLongUrlAsync(GetUrlByUserAndLongUrlQuery query);
    Task<IEnumerable<Url>> GetUrlsByUserAsync(GetUrlsByUserQuery query);
}
