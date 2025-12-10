using Linker.Link.Api.Endpoints.CreateLinkEndpoint;
using Linker.Link.Api.Endpoints.GetLinksByUserId;

namespace Linker.Link.Api.Endpoints;

internal static class LinkEndpoints
{
    public static void MapEndpoints(this WebApplication app)
    {
        var group = app
            .MapGroup("/api/links")
            .WithTags("Link");

        group
            .MapCreateLinkEndpoint()
            .MapGetLinksByUserIdEndpoint();
    }
}
