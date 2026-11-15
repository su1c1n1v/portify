using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using PortifyAPI.Dtos;
using PortifyAPI.Features.Shared.Mediator;
using FluentValidation.Results;
using PortifyAPI.Features.Shared.Results;

namespace PortifyAPI.Features.Pies.GetPieById;

public class GetPieByIdRequest
{
    public int Id { get; set; }
}

public class GetPieByIdEndpoint(IMediator mediator, ILogger<GetPieByIdEndpoint> logger) :
     Endpoint<GetPieByIdRequest, Results<Ok<PieDetailsDto>, NotFound, BadRequest, ProblemDetails>>
{
    public override void Configure()
    {
        Get("/api/pies/{id}");
        AllowAnonymous();
    }

    public override async Task<Results<Ok<PieDetailsDto>, NotFound, BadRequest, ProblemDetails>> ExecuteAsync(GetPieByIdRequest request, CancellationToken ct)
    {
        logger.LogInformation("Received request to get pie by id {Id}", request.Id);

        var result = await mediator.Send(new GetPieByIdCommand { Id = request.Id }, ct);

        logger.LogInformation("Finished processing request to get pie by id {Id} with success: {Success}", request.Id, result.Success);

        if (!result.Success)
        {
            if (result.Error is NotFoundError)
                return TypedResults.NotFound();

            logger.LogError("Failed to retrieve pie by id {Id}: {ErrorMessage}", request.Id, result.Error.Message);

            return new ProblemDetails(
                new List<ValidationFailure>(),
                "Error retrieving pie by id",
                result.Error.Message,
                result.Error.StatusCode);
        }

        return TypedResults.Ok(result.Value);
    }
}