using Linker.Link.Application.Commons;
using Linker.Link.Application.Repositories;
using Microsoft.Extensions.Logging;

namespace Linker.Link.Application.CreateLink;

internal sealed class CreateLinkUseCase(
    ILogger<CreateLinkUseCase> logger,
    ILinkRepository linkRepository
) : ICreateLinkUseCase
{
    public async Task<Result<CreateLinkResult>> CreateLink(
        CreateLinkRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            Domain.Entities.Link link = request;
            link.Validate();

            if (!link.IsValid)
            {
                logger.LogError("Link validation failed: {Errors}", new string("aaa"));
                return Result<CreateLinkResult>.ValidationError(link.Errors);
            }

            await linkRepository.CreateLink(
                link,
                cancellationToken);
            CreateLinkResult result = link;
            return Result<CreateLinkResult>.Success(result);
        }
        catch (Exception exception)
        {
            logger.LogError(
                exception,
                "Error: {Message}",
                exception.Message);
            return Result<CreateLinkResult>.InternalError();
        }
    }
}
