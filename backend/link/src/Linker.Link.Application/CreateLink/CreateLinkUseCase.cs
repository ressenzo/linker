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
        CancellationToken cancellationToken
    )
    {
        Domain.Entities.Link link = request.ToDomainEntity();
        link.Validate();

        if (!link.IsValid)
        {
            logger.LogError("Link validation failed: {Errors}", new string("aaa"));
            return Result<CreateLinkResult>.ValidationError(link.Errors);
        }

        await linkRepository.CreateLink(
            link,
            cancellationToken);
        var result = CreateLinkResult.FromDomainEntity(link);
        return Result<CreateLinkResult>.Success(result);
    }
}
