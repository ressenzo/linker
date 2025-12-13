using Linker.Link.Application.Commons;
using Linker.Link.Application.Repositories;
using Microsoft.Extensions.Logging;

namespace Linker.Link.Application.UpdateLink;

internal sealed class UpdateLinkUseCase(
    ILogger<UpdateLinkUseCase> logger,
    ILinkRepository linkRepository
) : IUpdateLinkUseCase
{
    public async Task<Result> UpdateLink(
        UpdateLinkRequest request,
        CancellationToken cancellationToken
    )
    {
        var link = await linkRepository.GetById(
            request.Id,
            cancellationToken
        );
        
        if (link is null)
        {
            logger.LogInformation(
                "Link with {Id} was not found",
                request.Id    
            );
            return Result.NotFound("Link not found");
        }
        link.Update(request.Name, request.Url);
        link.Validate();
        if (!link.IsValid)
        {
            logger.LogInformation("Link update is not valid");
            return Result.ValidationError(link.Errors);
        }

        await linkRepository.UpdateLink(
            link,
            cancellationToken
        );
        return Result.Success();
    }
}
