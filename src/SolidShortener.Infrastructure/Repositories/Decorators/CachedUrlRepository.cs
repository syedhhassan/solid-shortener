using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using SolidShortener.Application.Urls.Queries;
using SolidShortener.Domain.Entities.Urls;

namespace SolidShortener.Infrastructure.Repositories.Decorators;

public class CachedUrlRepository : IUrlRepository
{
    private readonly IUrlRepository _innerRepository;
    private readonly IDistributedCache _cache;

    public CachedUrlRepository(IUrlRepository innerRepository, IDistributedCache cache)
    {
        _innerRepository = innerRepository;
        _cache = cache;
    }

    private static string BuildCacheKey(string shortCode) => $"url:{shortCode}";

    public async Task AddAsync(Url url)
    {
        await _innerRepository.AddAsync(url);
        await _cache.RemoveAsync(BuildCacheKey(url.ShortCode));
    }

    public async Task UpdateAsync(Url url)
    {
        await _innerRepository.UpdateAsync(url);
        await _cache.RemoveAsync(BuildCacheKey(url.ShortCode));
    }

    public async Task<Url?> GetUrlByShortCodeAsync(GetUrlByShortCodeQuery query)
    {
        Url? url;
        var cacheKey = BuildCacheKey(query.ShortCode);
        var cachedUrl = await _cache.GetStringAsync(cacheKey);

        if (!string.IsNullOrWhiteSpace(cachedUrl))
        {
            try
            {
                url = JsonSerializer.Deserialize<Url>(cachedUrl);
                if (url is not null) return url;
            }
            catch (JsonException)
            {
                await _cache.RemoveAsync(cacheKey);
            }
        }

        url = await _innerRepository.GetUrlByShortCodeAsync(query);

        if (url is not null)
        {
            var urlData = JsonSerializer.Serialize(url);
            await _cache.SetStringAsync(
                cacheKey,
                urlData,
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
                });
        }

        return url;
    }

    public Task<Url?> GetByUserAndLongUrlAsync(GetUrlByUserAndLongUrlQuery query) =>
        _innerRepository.GetByUserAndLongUrlAsync(query);

    public Task<IEnumerable<Url>> GetUrlsByUserAsync(GetUrlsByUserQuery query) =>
        _innerRepository.GetUrlsByUserAsync(query);
}
