using PortifyAPI.Entities;
using PortifyAPI.Dtos;
using PortifyAPI.Features.Shared.Cache;
using PortifyAPI.Features.Shared.Mapper;
using PortifyAPI.Features.Shared.Mediator;
using PortifyAPI.Features.Shared.Results;
using PortifyAPI.Infrastructure.Trading212;

namespace PortifyAPI.Features.Pies.GetPies;

public class GetPiesCommand : IRequest<Result<IEnumerable<PieSummaryDto>>> { }

public class GetPiesHandler(ITrading212Client trading212Client, ILogger<GetPiesHandler> logger, ICache cache, IMappingFacade mapper) : RequestHandlerBase<GetPiesCommand, Result<IEnumerable<PieSummaryDto>>>
{
    public override async Task<Result<IEnumerable<PieSummaryDto>>> Handle(GetPiesCommand request, CancellationToken ct)
    {
        try
        {
            var cachedPies = cache.Get<IEnumerable<PieSummaryDto>>("piesSummary");
            if (cachedPies != null)
            {
                logger.LogDebug("Returning cached pies");
                return Result<IEnumerable<PieSummaryDto>>.Ok(cachedPies);
            }

            var positionsTask = trading212Client.GetPositionsAsync(ct);
            var piesTask = trading212Client.GetPiesAsync(ct);

            var pieSummaries = new List<PiePosition>();

            var positionsResult = mapper.Map<IEnumerable<Position>, PositionsSummaryDto>(await positionsTask);

            var pies = await piesTask;

            if (pies == null)
            {
                logger.LogWarning("No pies found");
                return new NotFoundError("No pies found");
            }

            foreach (var pie in pies)
            {
                var pieDetails = await trading212Client.GetPieByIdAsync(pie.Id, ct);
                var timeout = Task.Delay(TimeSpan.FromSeconds(5), ct);

                pieSummaries.Add(new PiePosition(
                    positionsResult.Stocks.Where(s => pieDetails.Instruments.Any(i => i.Ticker == s.Ticker)).ToList(),
                    pieDetails.Settings,
                    pie.DividendDetails));

                await timeout;
            }

            var mappedPieSummaries = mapper.Map<IEnumerable<PiePosition>, IEnumerable<PieSummaryDto>>(pieSummaries);

            logger.LogDebug("Successfully retrieved pies");
            cache.Set("piesSummary", mappedPieSummaries);

            return Result<IEnumerable<PieSummaryDto>>.Ok(mappedPieSummaries);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error retrieving pies: {Message}", ex.Message);
            return new InternalServerError(ex.Message);
        }
    }
}