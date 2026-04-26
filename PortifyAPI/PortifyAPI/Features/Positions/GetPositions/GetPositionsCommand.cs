using PortifyAPI.Features.Positions.GetPosition;
using PortifyAPI.Features.Shared.Mediator;
using PortifyAPI.Infrastructure.Trading212;

namespace PortifyAPI.Features.Positions.GetPositions;

public class GetPositionsCommand : IRequest<IEnumerable<Position>> { }

public class GetPositionsHandler(ITrading212Client trading212Client, ILogger<GetPositionsHandler> logger) : RequestHandlerBase<GetPositionsCommand, IEnumerable<Position>>
{
    // TODO: Implement option result to handle errors gracefully instead of throwing exceptions
    public override async Task<IEnumerable<Position>> Handle(GetPositionsCommand request, CancellationToken ct)
    {
        try
        {
            var positions = await trading212Client.GetPositionsAsync(ct);
            logger.LogInformation("Successfully retrieved {Count} positions", positions.Count());
            return positions;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error retrieving positions: {Message}", ex.Message);
            throw;
        }
    }
}