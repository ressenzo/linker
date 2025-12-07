using Linker.Api.Endpoints.CreateLinkEndpoint;

namespace Linker.Api.Endpoints;

internal static class LinkEndpoints
{
    public static void MapEndpoints(this WebApplication app)
    {
        var group = app
            .MapGroup("/api/links")
            .WithTags("Link");

        group.MapCreateLinkEndpoint();
    }
}
