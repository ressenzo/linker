using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace Linker.Api.Endpoints.CreateLinkEndpoint;

internal static class CreateEndpoint
{
    public static RouteGroupBuilder MapCreateLinkEndpoint(this RouteGroupBuilder group)
    {
        group
            .MapPost("/", CreateLink)
            .WithDisplayName("Create Link")
            .WithSummary("Create new link to user")
            .WithName("CreateLink")
            .Produces<CreateLinkResponse>((int)HttpStatusCode.Created);

        return group;
    }

    private static async Task<IResult> CreateLink(
        [FromBody] CreateLinkRequest linkRequest,
        CancellationToken cancellationToken)
    {
        return await Task.FromResult(Results.Ok(linkRequest));
    }
}
