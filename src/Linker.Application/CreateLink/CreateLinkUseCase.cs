using Linker.Application.Commons;
using Linker.Application.Repositories;
using Linker.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Linker.Application.CreateLink;

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
            Link link = request;
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
