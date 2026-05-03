using PortifyAPI.Entities;
using PortifyAPI.Features.Shared.Mediator;
using PortifyAPI.Features.Shared.Results;
using PortifyAPI.Infrastructure.Trading212;

namespace PortifyAPI.Features.Pies.GetPieById;

public class GetPieByIdCommand : IRequest<Result<PieDetails>>
{
    public int Id { get; set; }
}

public class GetPieByIdHandler(ITrading212Client trading212Client, ILogger<GetPieByIdHandler> logger) : RequestHandlerBase<GetPieByIdCommand, Result<PieDetails>>
{
    public override async Task<Result<PieDetails>> Handle(GetPieByIdCommand request, CancellationToken ct)
    {
        try
        {
            var pie = await trading212Client.GetPieByIdAsync(request.Id, ct);

            if (pie == null)
            {
                logger.LogWarning("No pie found with id {Id}", request.Id);
                return new NotFoundError($"No pie found with id {request.Id}");
            }

            logger.LogDebug("Successfully retrieved pie with id {Id}", request.Id);
            return Result<PieDetails>.Ok(pie);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error retrieving pie with id {Id}: {Message}", request.Id, ex.Message);
            return new InternalServerError($"Error retrieving pie with id {request.Id}: {ex.Message}");
        }
    }
}