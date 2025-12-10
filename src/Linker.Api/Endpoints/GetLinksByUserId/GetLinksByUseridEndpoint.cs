using System.Net;
using Linker.Application.Commons;
using Linker.Application.GetLinksByUserId;
using Linker.Domain.Entities;
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
        CancellationToken cancellationToken)
    {
        return await Task.FromResult(Results.Ok(userId));
    }
}
