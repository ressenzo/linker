using Linker.Link.Application.Commons;
using Linker.Link.Application.Repositories;
using Linker.Link.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Linker.Link.Application.GetLinksByUserId;

internal sealed class GetLinksByUserIdUseCase(
    ILogger<GetLinksByUserIdUseCase> logger,
    ILinkRepository linkRepository
) : IGetLinksByUserIdUseCase
{
    public async Task<Result<GetLinksByUserIdResult>> GetLinksByUserId(
        string userId,
        CancellationToken cancellationToken)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                logger.LogInformation($"{nameof(userId)} is null or empty");
                return Result<GetLinksByUserIdResult>
                    .ValidationError(["User ID cannot be null or empty"]);
            }

            var links = await linkRepository.GetLinks(
                userId,
                cancellationToken);

            if (links == null || !links.Any())
            {
                logger.LogInformation("No links found for the user");
                return Result<GetLinksByUserIdResult>
                    .NotFound("No links found for the user");
            }

            var result = new GetLinksByUserIdResult(links);
            return Result<GetLinksByUserIdResult>
                .Success(result);
        }
        catch (Exception exception)
        {
            logger.LogError(
                exception,
                "Error: {Message}",
                exception.Message);
            return Result<GetLinksByUserIdResult>
                .InternalError();
        }
    }
}
