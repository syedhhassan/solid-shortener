using SolidShortener.Application.Interfaces.Repositories;
using SolidShortener.Application.Urls.Queries;
using SolidShortener.Application.Visits.Commands;
using SolidShortener.Application.Visits.DTOs;
using SolidShortener.Application.Visits.Queries;
using SolidShortener.Application.Visits.Services.Interfaces;

namespace SolidShortener.Application.Visits.Services.Implementations;

public class VisitService : IVisitService
{
    private readonly IVisitRepository _visitRepository;
    private readonly IUrlRepository _urlRepository;

    public VisitService(IVisitRepository visitRepository, IUrlRepository urlRepository)
    {
        _visitRepository = visitRepository;
        _urlRepository = urlRepository;
    }

    public async Task<int> GetVisitCountByShortCodeAsync(GetVisitCountByShortCodeQuery query)
    {
        return await _visitRepository.GetVisitCountByShortCodeAsync(query);
    }

    public async Task<IEnumerable<VisitDTO>> GetVisitsByShortCodeAsync(GetVisitsByShortCodeQuery query)
    {
        var visits = await _visitRepository.GetVisitsByShortCodeAsync(query);
        return visits.Select(VisitMapper.ToDTO);
    }

    public async Task LogVisitAsync(LogVisitCommand command)
    {
        var url = await _urlRepository.GetUrlByShortCodeAsync(new GetUrlByShortCodeQuery { ShortCode = command.ShortCode });

        if (url == null) throw new Exception("Url not found");

        url.IncrementVisitsCount();
        await _urlRepository.UpdateAsync(url);

        await _visitRepository.AddAsync(new Visit
                    (
                        url.Id,
                        command.IpAddress,
                        command.UserAgent
                    ));
    }
}
