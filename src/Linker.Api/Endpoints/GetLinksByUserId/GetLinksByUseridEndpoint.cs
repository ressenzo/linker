using System.Net;
using Linker.Application.Commons;
using Linker.Application.GetLinksByUserId;
using Microsoft.AspNetCore.Mvc;

namespace Linker.Api.Endpoints.GetLinksByUserId;

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
            .Produces<Result<GetLinksByUserIdResult>>((int)HttpStatusCode.OK)
            .Produces<Result>((int)HttpStatusCode.BadRequest)
            .Produces<Result>((int)HttpStatusCode.NotFound);

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
            ResultType.NOT_FOUND => Results.NotFound(response),
            ResultType.VALIDATION_ERROR => Results.BadRequest(response),
            ResultType.INTERNAL_ERROR => Results.InternalServerError(response),
            _ => throw new NotImplementedException()
        };
    }
}
