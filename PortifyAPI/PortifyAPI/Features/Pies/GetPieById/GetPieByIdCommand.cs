using PortifyAPI.Entities;
using PortifyAPI.Dtos;
using PortifyAPI.Features.Shared.Mapper;
using PortifyAPI.Features.Shared.Mediator;
using PortifyAPI.Features.Shared.Results;
using PortifyAPI.Infrastructure.Trading212;

namespace PortifyAPI.Features.Pies.GetPieById;

public class GetPieByIdCommand : IRequest<Result<PieDetailsDto>>
{
    public int Id { get; set; }
}

public class GetPieByIdHandler(ITrading212Client trading212Client, ILogger<GetPieByIdHandler> logger, IMappingFacade mapper) : RequestHandlerBase<GetPieByIdCommand, Result<PieDetailsDto>>
{
    public override async Task<Result<PieDetailsDto>> Handle(GetPieByIdCommand request, CancellationToken ct)
    {
        try
        {
            var pie = await trading212Client.GetPieByIdAsync(request.Id, ct);

            if (pie == null)
            {
                logger.LogWarning("No pie found with id {Id}", request.Id);
                return new NotFoundError($"No pie found with id {request.Id}");
            }
            var pieDto = mapper.Map<PieDetails, PieDetailsDto>(pie);

            logger.LogDebug("Successfully retrieved pie with id {Id}", request.Id);

            return Result<PieDetailsDto>.Ok(pieDto);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error retrieving pie with id {Id}: {Message}", request.Id, ex.Message);
            return new InternalServerError($"Error retrieving pie with id {request.Id}: {ex.Message}");
        }
    }
}