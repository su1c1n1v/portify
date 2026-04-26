using FastEndpoints;
using PortifyAPI.Features.Positions.GetPosition;
using PortifyAPI.Features.Shared.Mediator;

namespace PortifyAPI.Features.Positions.GetPositions;

public class GetPositionsEndpoint(IMediator mediator, ILogger<GetPositionsEndpoint> logger) : EndpointWithoutRequest<IEnumerable<Position>>
{
    public override void Configure()
    {
        Get("/api/positions");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var positions = await mediator.Send(new GetPositionsCommand(), ct);

        logger.LogDebug("Retrieved {Count} positions", positions.Count());

        await Send.OkAsync(positions, ct);
    }
}