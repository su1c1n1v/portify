using PortifyAPI.Entities;
using PortifyAPI.Features.Shared.Mediator;
using PortifyAPI.Features.Shared.Results;
using PortifyAPI.Infrastructure.Trading212;

namespace PortifyAPI.Features.Pies.GetPies;

public class GetPiesCommand : IRequest<Result<IEnumerable<Pie>>> { }

public class GetPiesHandler(ITrading212Client trading212Client, ILogger<GetPiesHandler> logger) : RequestHandlerBase<GetPiesCommand, Result<IEnumerable<Pie>>>
{
    public override async Task<Result<IEnumerable<Pie>>> Handle(GetPiesCommand request, CancellationToken ct)
    {
        try
        {
            var pies = await trading212Client.GetPiesAsync(ct);

            if (pies == null)
            {
                logger.LogWarning("No pies found");
                return new NotFoundError("No pies found");
            }

            logger.LogDebug("Successfully retrieved pies");
            return Result<IEnumerable<Pie>>.Ok(pies);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error retrieving pies: {Message}", ex.Message);
            return new InternalServerError(ex.Message);
        }
    }
}