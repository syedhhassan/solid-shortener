using SolidShortener.Api.Services;
using SolidShortener.Application.Urls.Commands;
using SolidShortener.Application.Urls.Queries;
using SolidShortener.Application.Urls.Services.Interfaces;

namespace SolidShortener.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UrlController : ControllerBase
{
    private readonly IUrlService _urlService;
    private readonly ICurrentUserService _currentUserService;

    public UrlController(IUrlService urlService, ICurrentUserService currentUserService)
    {
        _urlService = urlService;
        _currentUserService = currentUserService;
    }

    [Authorize]
    [HttpPost("shorten")]
    public async Task<IActionResult> Shorten([FromBody] ShortenUrlRequest request)
    {
        var userId = _currentUserService.UserId;
        if (userId == Guid.Empty) return BadRequest("Invalid user id");

        var response = await _urlService.ShortenUrlAsync(new ShortenUrlCommand { UserId = userId, LongUrl = request.LongUrl, ExpiresAt = request.ExpiresAt });
        return Ok(response);
    }

    [HttpGet("{code}")]
    public async Task<IActionResult> GetUrl(string code)
    {
        var response = await _urlService.GetUrlByShortCodeAsync(new GetUrlByShortCodeQuery { ShortCode = code });
        if (response is null) return NotFound();
        return RedirectPermanent(response.LongUrl);
    }

    [Authorize]
    [HttpGet()]
    public async Task<IActionResult> GetUrlsByUser()
    {
        var userId = _currentUserService.UserId;
        if (userId == Guid.Empty) return Unauthorized();

        var urls = await _urlService.GetUrlsByUserAsync(new GetUrlsByUserQuery { UserId = userId });
        return Ok(urls);
    }

    [Authorize]
    [HttpDelete("{code}")]
    public async Task<IActionResult> DeleteUrl(string code)
    {
        var userId = _currentUserService.UserId;
        if (userId == Guid.Empty) return Unauthorized();

        await _urlService.DeleteUrlAsync(new DeleteUrlCommand { UserId = userId, ShortCode = code });
        return NoContent();
    }
}
