using PortifyAPI.Entities;
using PortifyAPI.Features.Shared.Mediator;
using PortifyAPI.Features.Shared.Results;
using PortifyAPI.Infrastructure.Trading212;

namespace PortifyAPI.Features.Positions.GetPositions;

public class GetPositionsCommand : IRequest<Result<IEnumerable<Position>>> { }

public class GetPositionsHandler(ITrading212Client trading212Client, ILogger<GetPositionsHandler> logger) : RequestHandlerBase<GetPositionsCommand, Result<IEnumerable<Position>>>
{
    public override async Task<Result<IEnumerable<Position>>> Handle(GetPositionsCommand request, CancellationToken ct)
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
            return Result<IEnumerable<Position>>.Ok(positions);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error retrieving positions: {Message}", ex.Message);
            return new InternalServerError(ex.Message);
        }
    }
}