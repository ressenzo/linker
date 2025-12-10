using System.Net;
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
            .Produces<Result<CreateLinkResponse>>((int)HttpStatusCode.Created)
            .Produces<Result>((int)HttpStatusCode.BadRequest);

        return group;
    }

    private static async Task<IResult> CreateLink(
        [FromBody] CreateLinkRequest request,
        [FromServices] ICreateLinkUseCase createLinkUseCase,
        CancellationToken cancellationToken)
    {
        Application.CreateLink.CreateLinkRequest createLinkRequest = request;
        var response = await createLinkUseCase.CreateLink(
            createLinkRequest,
            cancellationToken);
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
