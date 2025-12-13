using Linker.Link.Application.Commons;
using Linker.Link.Application.Repositories;
using Microsoft.Extensions.Logging;

namespace Linker.Link.Application.GetLinkById;

internal sealed class GetLinkByIdUseCase(
    ILogger<GetLinkByIdUseCase> logger,
    ILinkRepository linkRepository
) : IGetLinkByIdUseCase
{
    public async Task<Result<GetLinkByIdResult>> GetLinkById(
        string id,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            logger.LogInformation("Invalid id");
            return Result<GetLinkByIdResult>
                .ValidationError(["Invalid id"]);
        }

        logger.LogInformation("Getting link with id: {LinkId}", id);

        var link = await linkRepository.GetById(id, cancellationToken);

        if (link is null)
        {
            logger.LogWarning("Link with id {LinkId} not found", id);
            return Result<GetLinkByIdResult>
                .NotFound("Link not found");
        }

        var result = GetLinkByIdResult.FromDomainEntity(link);
        return Result<GetLinkByIdResult>.Success(result);
    }
}
