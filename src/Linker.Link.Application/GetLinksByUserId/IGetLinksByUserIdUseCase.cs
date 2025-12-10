using Linker.Link.Application.Commons;

namespace Linker.Link.Application.GetLinksByUserId;

public interface IGetLinksByUserIdUseCase
{
    Task<Result<GetLinksByUserIdResult>> GetLinksByUserId(
        string userId,
        CancellationToken cancellationToken);
}
