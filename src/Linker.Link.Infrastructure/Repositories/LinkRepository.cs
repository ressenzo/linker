using Linker.Link.Application.Repositories;

namespace Linker.Link.Infrastructure.Repositories;

internal sealed class LinkRepository : ILinkRepository
{
    private static readonly List<Domain.Entities.Link> _links = [];

    public Task CreateLink(
        Domain.Entities.Link link,
        CancellationToken cancellationToken)
    {
        _links.Add(link);
        return Task.CompletedTask;
    }

    public async Task<Domain.Entities.Link?> GetById(
        string id,
        CancellationToken cancellationToken)
    {
        return _links.FirstOrDefault(x => x.Id == id);
    }

    public async Task<IEnumerable<Domain.Entities.Link>?> GetLinks(
        string userId,
        CancellationToken cancellationToken)
    {
        return _links.FindAll(x => x.UserId == userId);
    }

    public Task UpdateLink(
        Domain.Entities.Link link,
        CancellationToken cancellationToken
    )
    {
        var index = _links.IndexOf(link);
        _links[index] = link;
        return Task.CompletedTask;
    }
}
