namespace Linker.Link.Application.Repositories;

public interface ILinkRepository
{
    Task CreateLink(Domain.Entities.Link link, CancellationToken cancellationToken);

    Task<IEnumerable<Domain.Entities.Link>?> GetLinks(
        string userId,
        CancellationToken cancellationToken);
}
