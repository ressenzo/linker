using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace Linker.Api.Endpoints.CreateLinkEndpoint;

internal static class CreateEndpoint
{
    public static RouteGroupBuilder MapCreateLinkEndpoint(this RouteGroupBuilder group)
    {
        group
            .MapPost("/", async ([FromBody] CreateLinkRequest link) =>
            {
                return await Task.FromResult(Results.Ok(link));
            })
            .WithDisplayName("Create Link")
            .WithSummary("Create link to user")
            .WithName("CreateLink")
            .Produces<CreateLinkResponse>((int)HttpStatusCode.Created);

        return group;
    }
}
