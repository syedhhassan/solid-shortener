using SolidShortener.Application.Interfaces.Repositories;
using SolidShortener.Application.Interfaces.Services;
using SolidShortener.Application.Urls.Commands;
using SolidShortener.Application.Urls.DTOs;
using SolidShortener.Application.Urls.Queries;
using SolidShortener.Application.Urls.Services.Interfaces;

namespace SolidShortener.Application.Urls.Services.Implementations;

public class UrlService : IUrlService
{
    private readonly IUrlRepository _urlRepository;
    private readonly IShortCodeGenerator _shortCodeGenerator;

    public UrlService(IUrlRepository urlRepository, IShortCodeGenerator shortCodeGenerator)
    {
        _urlRepository = urlRepository;
        _shortCodeGenerator = shortCodeGenerator;
    }

    public async Task<UrlDTO> ShortenUrlAsync(ShortenUrlCommand command)
    {
        var existing = await _urlRepository.GetByUserAndLongUrlAsync(new GetUrlByUserAndLongUrlQuery { UserId = command.UserId, LongUrl = command.LongUrl });

        if (existing != null)
        {
            if (existing.IsDeleted) existing.UnDelete();

            existing.SetExpiresAt(command.ExpiresAt);

            await _urlRepository.UpdateAsync(existing);
            return UrlMapper.ToDTO(existing);
        }

        var url = new Url(command.UserId, command.LongUrl, string.Empty, command.ExpiresAt);
        await _urlRepository.AddAsync(url);

        var shortCode = _shortCodeGenerator.Generate(url.Id);
        url.SetShortCode(shortCode);
        await _urlRepository.UpdateAsync(url);

        return UrlMapper.ToDTO(url);
    }

    public async Task<UrlDTO?> GetUrlByShortCodeAsync(GetUrlByShortCodeQuery query)
    {
        var url = await _urlRepository.GetUrlByShortCodeAsync(query);

        if (url is null) return null;

        if (url.ExpiresAt.HasValue && url.ExpiresAt.Value < DateTime.UtcNow) return null;

        return UrlMapper.ToDTO(url);
    }

    public async Task<IEnumerable<UrlDTO>> GetUrlsByUserAsync(GetUrlsByUserQuery query)
    {
        var urls = await _urlRepository.GetUrlsByUserAsync(query);
        return urls.Where(u => !u.IsDeleted).Select(UrlMapper.ToDTO);
    }

    public async Task DeleteUrlAsync(DeleteUrlCommand command)
    {
        var url = await _urlRepository.GetUrlByShortCodeAsync(new GetUrlByShortCodeQuery { ShortCode = command.ShortCode });

        if (url == null)
            throw new KeyNotFoundException($"URL with short code {command.ShortCode} not found.");

        if (url.UserId != command.UserId)
            throw new UnauthorizedAccessException("You do not have permission to delete this URL.");

        url.MarkAsDeleted();
        await _urlRepository.UpdateAsync(url);
    }
}
