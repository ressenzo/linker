using Linker.Application.Commons;
using Linker.Application.Repositories;
using Linker.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Linker.Application.GetLinksByUserId;

internal sealed class GetLinksByUserIdUseCase(
    ILogger<GetLinksByUserIdUseCase> logger,
    ILinkRepository linkRepository
) : IGetLinksByUserIdUseCase
{
    public async Task<Result<IEnumerable<Link>>> GetLinksByUserId(
        string userId,
        CancellationToken cancellationToken)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                logger.LogInformation($"{nameof(userId)} is null or empty");
                return Result<IEnumerable<Link>>
                    .ValidationError(["User ID cannot be null or empty"]);
            }

            var links = await linkRepository.GetLinks(
                userId,
                cancellationToken);

            if (links == null || !links.Any())
            {
                logger.LogInformation("No links found for the user");
                return Result<IEnumerable<Link>>
                    .NotFound("No links found for the user");
            }

            return Result<IEnumerable<Link>>
                .Success(links);
        }
        catch (Exception exception)
        {
            logger.LogError(
                exception,
                "Error: {Message}",
                exception.Message);
            return Result<IEnumerable<Link>>
                .InternalError();
        }
    }
}
