using Linker.Domain.Entities;

namespace Linker.Application.Repositories;

public interface ILinkRepository
{
    Task CreateLink(Link link, CancellationToken cancellationToken);

    Task<IEnumerable<Link>?> GetLinks(
        string userId,
        CancellationToken cancellationToken);
}
