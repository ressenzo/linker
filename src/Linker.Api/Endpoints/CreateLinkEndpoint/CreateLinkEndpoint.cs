using System.Net;
using Linker.Application.Commons;
using Microsoft.AspNetCore.Mvc;

namespace Linker.Api.Endpoints.CreateLinkEndpoint;

internal static class CreateEndpoint
{
    public static RouteGroupBuilder MapCreateLinkEndpoint(
        this RouteGroupBuilder group)
    {
        group
            .MapPost("/", CreateLink)
            .WithDisplayName("Create Link")
            .WithSummary("Create new link to user")
            .WithName("CreateLink")
            .Produces<Result<CreateLinkResponse>>((int)HttpStatusCode.Created)
            .Produces<Result>((int)HttpStatusCode.BadRequest);

        return group;
    }

    private static async Task<IResult> CreateLink(
        [FromBody] CreateLinkRequest linkRequest,
        CancellationToken cancellationToken)
    {
        return await Task.FromResult(Results.Ok(linkRequest));
    }
}
