using PortifyAPI.Entities;
using PortifyAPI.Features.Shared.Cache;
using PortifyAPI.Features.Shared.Mapper;
using PortifyAPI.Features.Shared.Mediator;
using PortifyAPI.Features.Shared.Results;
using PortifyAPI.Infrastructure.Trading212;

namespace PortifyAPI.Features.Pies.GetPies;

public class GetPiesCommand : IRequest<Result<IEnumerable<PieSummary>>> { }

public class GetPiesHandler(ITrading212Client trading212Client, ILogger<GetPiesHandler> logger, ICache cache, IMappingFacade mapper) : RequestHandlerBase<GetPiesCommand, Result<IEnumerable<PieSummary>>>
{
    public override async Task<Result<IEnumerable<PieSummary>>> Handle(GetPiesCommand request, CancellationToken ct)
    {
        try
        {
            var cachedPies = cache.Get<IEnumerable<PieSummary>>("piesSummary");
            if (cachedPies != null)
            {
                logger.LogDebug("Returning cached pies");
                return Result<IEnumerable<PieSummary>>.Ok(cachedPies);
            }

            var positions = trading212Client.GetPositionsAsync(ct);
            var pies = await trading212Client.GetPiesAsync(ct);

            if (pies == null)
            {
                logger.LogWarning("No pies found");
                return new NotFoundError("No pies found");
            }

            var pieSummaries = new List<PiePosition>();

            var positionsResult = mapper.Map<IEnumerable<Position>, PositionsSummary>(await positions);

            foreach (var pie in pies)
            {
                var pieDetails = await trading212Client.GetPieByIdAsync(pie.Id, ct);
                var timeout = Task.Delay(TimeSpan.FromSeconds(5), ct);

                pieSummaries.Add(new PiePosition
                {
                    Instruments = positionsResult.Stocks.Where(s => pieDetails.Instruments.Any(i => i.Ticker == s.Ticker)).ToList(),
                    Settings = pieDetails.Settings
                });

                await timeout;
            }

            var mappedPieSummaries = mapper.Map<IEnumerable<PiePosition>, IEnumerable<PieSummary>>(pieSummaries);

            logger.LogDebug("Successfully retrieved pies");
            cache.Set("piesSummary", mappedPieSummaries);

            return Result<IEnumerable<PieSummary>>.Ok(mappedPieSummaries);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error retrieving pies: {Message}", ex.Message);
            return new InternalServerError(ex.Message);
        }
    }
}