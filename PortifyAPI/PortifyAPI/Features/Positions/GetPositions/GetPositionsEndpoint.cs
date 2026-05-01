using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using PortifyAPI.Entities;
using PortifyAPI.Features.Shared.Mediator;
using FluentValidation.Results;
using PortifyAPI.Features.Shared.Results;

namespace PortifyAPI.Features.Positions.GetPositions;

public class GetPositionsEndpoint(IMediator mediator, ILogger<GetPositionsEndpoint> logger) :
     EndpointWithoutRequest<Results<Ok<PositionsSummary>, NotFound, ProblemDetails>>
{
    public override void Configure()
    {
        Get("/api/positions");
        AllowAnonymous();
    }

    public override async Task<Results<Ok<PositionsSummary>, NotFound, ProblemDetails>> ExecuteAsync(CancellationToken ct)
    {
        logger.LogInformation("Received request to get all positions");

        var result = await mediator.Send(new GetPositionsCommand(), ct);

        logger.LogInformation("Finished processing request to get all positions with success: {Success}", result.Success);

        if (!result.Success)
        {
            if (result.Error is NotFoundError)
                return TypedResults.NotFound();

            logger.LogError("Failed to retrieve positions: {ErrorMessage}", result.Error.Message);

            return new ProblemDetails(
                new List<ValidationFailure>(),
                "Error retrieving positions",
                result.Error.Message,
                result.Error.StatusCode);
        }

        return TypedResults.Ok(result.Value);
    }
}