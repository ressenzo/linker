using Linker.Application.Commons;
using Linker.Domain.Entities;

namespace Linker.Application.GetLinksByUserId;

public interface IGetLinksByUserIdUseCase
{
    Task<Result<IEnumerable<Link>>> GetLinksByUserId(
        string userId,
        CancellationToken cancellationToken);
}
