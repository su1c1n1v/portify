using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using PortifyAPI.Dtos;
using PortifyAPI.Features.Shared.Mediator;
using FluentValidation.Results;
using PortifyAPI.Features.Shared.Results;

namespace PortifyAPI.Features.Accounts.GetAccount;

public class GetAccountsEndpoint(IMediator mediator, ILogger<GetAccountsEndpoint> logger) :
     EndpointWithoutRequest<Results<Ok<AccountDto>, NotFound, ProblemDetails>>
{
    public override void Configure()
    {
        Get("/api/accounts");
        AllowAnonymous();
    }

    public override async Task<Results<Ok<AccountDto>, NotFound, ProblemDetails>> ExecuteAsync(CancellationToken ct)
    {
        logger.LogInformation("Received request to get account");

        var result = await mediator.Send(new GetAccountCommand(), ct);

        logger.LogInformation("Finished processing request to get account with success: {Success}", result.Success);

        if (!result.Success)
        {
            if (result.Error is NotFoundError)
                return TypedResults.NotFound();

            logger.LogError("Failed to retrieve account: {ErrorMessage}", result.Error.Message);

            return new ProblemDetails(
                [],
                "Error retrieving account",
                result.Error.Message,
                result.Error.StatusCode);
        }

        return TypedResults.Ok(result.Value);
    }
}