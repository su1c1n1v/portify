using FastEndpoints;
using PortifyAPI.Infrastructure.Trading212;

namespace PortifyAPI.Features.Positions.GetPositions;

public class GetPositionsEndpoint(ITrading212Client trading212Client) : EndpointWithoutRequest<string>
{
    public override void Configure()
    {
        Get("/api/positions");
        AllowAnonymous();
    }
    public override async Task HandleAsync(CancellationToken ct)
    {
        var positions = await trading212Client.GetPositionsAsync(ct);
        await Send.OkAsync(positions, ct);
    }
}