using PortifyAPI.Entities;
using PortifyAPI.Dtos;
using PortifyAPI.Features.Shared.Mapper;
using PortifyAPI.Features.Shared.Mediator;
using PortifyAPI.Features.Shared.Results;
using PortifyAPI.Infrastructure.Trading212;

namespace PortifyAPI.Features.Positions.GetPositions;

public class GetPositionsCommand : IRequest<Result<PositionsSummaryDto>> { }

public class GetPositionsHandler(ITrading212Client trading212Client, ILogger<GetPositionsHandler> logger, IMappingFacade mapping) : RequestHandlerBase<GetPositionsCommand, Result<PositionsSummaryDto>>
{
    public override async Task<Result<PositionsSummaryDto>> Handle(GetPositionsCommand request, CancellationToken ct)
    {
        try
        {
            var positions = await trading212Client.GetPositionsAsync(ct);

            if (positions == null)
            {
                logger.LogWarning("No positions found");
                return new NotFoundError("No positions found");
            }

            logger.LogDebug("Successfully retrieved {Count} positions", positions.Count());

            var positionsSummary = mapping.Map<IEnumerable<Position>, PositionsSummaryDto>(positions);

            return Result<PositionsSummaryDto>.Ok(positionsSummary with
            {
                Stocks = [.. positionsSummary.Stocks.OrderByDescending(s => s.Percentage)],
                Currencies = [.. positionsSummary.Currencies.OrderByDescending(c => c.Percentage)]
            });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error retrieving positions: {Message}", ex.Message);
            return new InternalServerError(ex.Message);
        }
    }
}