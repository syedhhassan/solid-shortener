using SolidShortener.Application.Urls.Commands;
using SolidShortener.Application.Urls.DTOs;
using SolidShortener.Application.Urls.Queries;

namespace SolidShortener.Application.Urls.Services.Interfaces;

public interface IUrlService
{
    Task<UrlDTO> ShortenUrlAsync(ShortenUrlCommand command);
    Task<UrlDTO?> GetUrlByShortCodeAsync(GetUrlByShortCodeQuery query);
    Task<IEnumerable<UrlDTO>> GetUrlsByUserAsync(GetUrlsByUserQuery query);
    Task DeleteUrlAsync(DeleteUrlCommand command);
}
