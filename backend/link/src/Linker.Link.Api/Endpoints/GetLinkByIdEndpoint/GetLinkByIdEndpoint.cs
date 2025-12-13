using Linker.Link.Application.Commons;
using Linker.Link.Application.GetLinkById;
using Microsoft.AspNetCore.Mvc;

namespace Linker.Link.Api.Endpoints.GetLinkByIdEndpoint;

internal static class GetLinkByIdEndpoint
{
    public static RouteGroupBuilder MapGetLinkByIdEndpoint(
        this RouteGroupBuilder group
    )
    {
        group
            .MapGet("/{id}", GetLinkById)
            .WithDisplayName("Get Links By Id")
            .WithSummary("Get link by Id")
            .WithName("GetLinkById")
            .Produces<Result<GetLinkByIdResult>>(StatusCodes.Status200OK)
            .Produces<Result>(StatusCodes.Status400BadRequest)
            .Produces<Result>(StatusCodes.Status404NotFound)
            .Produces<Result>(StatusCodes.Status500InternalServerError);

        return group;
    }

    private static async Task<IResult> GetLinkById(
        [FromRoute] string id,
        [FromServices] IGetLinkByIdUseCase getLinksByUserIdUseCase,
        CancellationToken cancellationToken)
    {
        var result = await getLinksByUserIdUseCase.GetLinkById(
            id,
            cancellationToken);
        return result.ResultType switch
        {
            ResultType.SUCCESS => Results.Ok(result),
            ResultType.VALIDATION_ERROR => Results.BadRequest(result),
            ResultType.NOT_FOUND => Results.NotFound(result),
            ResultType.INTERNAL_ERROR => Results.InternalServerError(result),
            _ => throw new NotImplementedException()
        };
    }
}
