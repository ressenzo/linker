using Linker.Application.Commons;

namespace Linker.Application.GetLinksByUserId;

public interface IGetLinksByUserIdUseCase
{
    Task<Result<GetLinksByUserIdResult>> GetLinksByUserId(
        string userId,
        CancellationToken cancellationToken);
}
