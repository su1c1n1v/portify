using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using PortifyAPI.Dtos;
using PortifyAPI.Features.Shared.Mediator;
using FluentValidation.Results;
using PortifyAPI.Features.Shared.Results;

namespace PortifyAPI.Features.Pies.GetPies;

public class GetPiesEndpoint(IMediator mediator, ILogger<GetPiesEndpoint> logger) :
     EndpointWithoutRequest<Results<Ok<IEnumerable<PieSummaryDto>>, NotFound, ProblemDetails>>
{
    public override void Configure()
    {
        Get("/api/pies");
        AllowAnonymous();
    }

    public override async Task<Results<Ok<IEnumerable<PieSummaryDto>>, NotFound, ProblemDetails>> ExecuteAsync(CancellationToken ct)
    {
        logger.LogInformation("Received request to get pies");

        var result = await mediator.Send(new GetPiesCommand(), ct);

        logger.LogInformation("Finished processing request to get pies with success: {Success}", result.Success);

        if (!result.Success)
        {
            if (result.Error is NotFoundError)
                return TypedResults.NotFound();

            logger.LogError("Failed to retrieve pies: {ErrorMessage}", result.Error.Message);

            return new ProblemDetails(
                new List<ValidationFailure>(),
                "Error retrieving pies",
                result.Error.Message,
                result.Error.StatusCode);
        }

        return TypedResults.Ok(result.Value);
    }
}