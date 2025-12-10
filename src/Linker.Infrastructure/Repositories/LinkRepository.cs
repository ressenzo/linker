using Linker.Application.Repositories;
using Linker.Domain.Entities;

namespace Linker.Infrastructure.Repositories;

internal sealed class LinkRepository : ILinkRepository
{
    private static List<Link> _links = [];

    public Task CreateLink(
        Link link,
        CancellationToken cancellationToken)
    {
        _links.Add(link);
        return Task.CompletedTask;
    }

    public async Task<IEnumerable<Link>?> GetLinks(
        string userId,
        CancellationToken cancellationToken)
    {
        return _links.FindAll(x => x.UserId == userId);
    }
}
