using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace SolidShortener.Api.Middlewares;

public class RateLimitingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IDistributedCache _cache;
    private readonly ILogger<RateLimitingMiddleware> _logger;

    private const int LIMIT = 100; // requests per window
    private static readonly TimeSpan WINDOW = TimeSpan.FromMinutes(1);

    public RateLimitingMiddleware(RequestDelegate next, IDistributedCache cache, ILogger<RateLimitingMiddleware> logger)
    {
        _next = next;
        _cache = cache;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var identifier = GetClientIdentifier(context);
        if (string.IsNullOrEmpty(identifier))
        {
            await _next(context);
            return;
        }

        var cacheKey = $"ratelimit:{identifier}";
        var recordJson = await _cache.GetStringAsync(cacheKey);
        RateLimitRecord record;

        if (recordJson is null)
        {
            record = new RateLimitRecord { Count = 1, Reset = DateTime.UtcNow.Add(WINDOW) };
            await SetRecordAsync(cacheKey, record);
        }
        else
        {
            record = JsonSerializer.Deserialize<RateLimitRecord>(recordJson)!;

            if (record.Reset < DateTime.UtcNow)
            {
                // Reset the window
                record.Count = 1;
                record.Reset = DateTime.UtcNow.Add(WINDOW);
                await SetRecordAsync(cacheKey, record);
            }
            else
            {
                record.Count++;

                if (record.Count > LIMIT)
                {
                    _logger.LogWarning("Rate limit exceeded for {Key}", identifier);

                    var retryAfter = (int)(record.Reset - DateTime.UtcNow).TotalSeconds;
                    context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                    context.Response.Headers["Retry-After"] = retryAfter.ToString();

                    await context.Response.WriteAsJsonAsync(new
                    {
                        message = "Rate limit exceeded. Please wait before retrying.",
                        limit = LIMIT,
                        resetAt = record.Reset
                    });
                    return;
                }

                await SetRecordAsync(cacheKey, record);
            }
        }

        await _next(context);
    }

    private static string GetClientIdentifier(HttpContext context)
    {
        if (context.User.Identity?.IsAuthenticated == true)
        {
            var userId = context.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (!string.IsNullOrEmpty(userId))
                return $"user:{userId}";
        }

        var ip = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
        return $"ip:{ip}";
    }

    private async Task SetRecordAsync(string key, RateLimitRecord record)
    {
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = WINDOW
        };

        await _cache.SetStringAsync(key, JsonSerializer.Serialize(record), options);
    }

    private class RateLimitRecord
    {
        public int Count { get; set; }
        public DateTime Reset { get; set; }
    }
}