using Linker.Link.Application.Commons;
using Linker.Link.Application.CreateLink;
using Microsoft.AspNetCore.Mvc;

namespace Linker.Link.Api.Endpoints.CreateLinkEndpoint;

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
            .Produces<Result<CreateLinkResult>>(StatusCodes.Status201Created)
            .Produces<Result>(StatusCodes.Status400BadRequest)
            .Produces<Result>(StatusCodes.Status500InternalServerError);

        return group;
    }

    private static async Task<IResult> CreateLink(
        [FromBody] CreateLinkRequest request,
        [FromServices] ICreateLinkUseCase createLinkUseCase,
        CancellationToken cancellationToken)
    {
        var applicationRequest = request.ToApplicationRequest();
        var response = await createLinkUseCase.CreateLink(
            applicationRequest,
            cancellationToken
        );
        return response.ResultType switch
        {
            ResultType.SUCCESS => Results.Created(
                $"/api/links/{response!.Content!.Id}",
                response),
            ResultType.VALIDATION_ERROR => Results.BadRequest(response),
            ResultType.INTERNAL_ERROR => Results.InternalServerError(response),
            _ => throw new NotImplementedException()
        };
    }
}
