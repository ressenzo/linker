using System.Net;
using Linker.Link.Application.Commons;
using Linker.Link.Application.GetLinksByUserId;
using Microsoft.AspNetCore.Mvc;

namespace Linker.Link.Api.Endpoints.GetLinksByUserIdEndpoint;

internal static class CreateEndpoint
{
    public static RouteGroupBuilder MapGetLinksByUserIdEndpoint(
        this RouteGroupBuilder group)
    {
        group
            .MapGet("/user-id/{userId}", GetLinksByUserId)
            .WithDisplayName("Get Links By User Id")
            .WithSummary("Get all links from user")
            .WithName("GetLinksByUserId")
            .Produces<Result<GetLinksByUserIdResult>>(StatusCodes.Status200OK)
            .Produces<Result>(StatusCodes.Status400BadRequest)
            .Produces<Result>(StatusCodes.Status404NotFound)
            .Produces<Result>(StatusCodes.Status500InternalServerError);

        return group;
    }

    private static async Task<IResult> GetLinksByUserId(
        [FromRoute] string userId,
        [FromServices] IGetLinksByUserIdUseCase getLinksByUserIdUseCase,
        CancellationToken cancellationToken)
    {
        var response = await getLinksByUserIdUseCase.GetLinksByUserId(
            userId,
            cancellationToken);
        return response.ResultType switch
        {
            ResultType.SUCCESS => Results.Ok(response),
            ResultType.VALIDATION_ERROR => Results.BadRequest(response),
            ResultType.NOT_FOUND => Results.NotFound(response),
            ResultType.INTERNAL_ERROR => Results.InternalServerError(response),
            _ => throw new NotImplementedException()
        };
    }
}
