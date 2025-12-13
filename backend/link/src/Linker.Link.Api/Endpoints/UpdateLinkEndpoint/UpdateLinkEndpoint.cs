using Linker.Link.Application.Commons;
using Linker.Link.Application.UpdateLink;
using Microsoft.AspNetCore.Mvc;

namespace Linker.Link.Api.Endpoints.UpdateLinkEndpoint;

internal static class UpdateLinkEndpoint
{
    public static RouteGroupBuilder MapUpdateLinkEndpoint(
        this RouteGroupBuilder group)
    {
        group
            .MapPut("/{id}", UpdateLink)
            .WithDisplayName("Update Link")
            .WithSummary("Update link")
            .WithName("UpdateLink")
            .Produces(StatusCodes.Status204NoContent)
            .Produces<Result>(StatusCodes.Status400BadRequest)
            .Produces<Result>(StatusCodes.Status404NotFound)
            .Produces<Result>(StatusCodes.Status500InternalServerError);

        return group;
    }

    private static async Task<IResult> UpdateLink(
        [FromRoute] string id,
        [FromBody] UpdateLinkRequest request,
        [FromServices] IUpdateLinkUseCase getLinksByUserIdUseCase,
        CancellationToken cancellationToken
    )
    {
        var applicationRequest = request.ToApplicationRequest(id);
        var response = await getLinksByUserIdUseCase.UpdateLink(
            applicationRequest,
            cancellationToken
        );
        return response.ResultType switch
        {
            ResultType.SUCCESS => Results.NoContent(),
            ResultType.VALIDATION_ERROR => Results.BadRequest(response),
            ResultType.NOT_FOUND => Results.NotFound(response),
            ResultType.INTERNAL_ERROR => Results.InternalServerError(response),
            _ => throw new NotImplementedException()
        };
    }
}
