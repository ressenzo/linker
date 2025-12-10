using Linker.Application.Commons.Results;
using Linker.Domain.Entities;

namespace Linker.Application.GetLinksByUserId;

public record GetLinksByUserIdResult
{
    private readonly List<LinkResult> _links;

    public IEnumerable<LinkResult> Links => _links;

    public GetLinksByUserIdResult(IEnumerable<Link> links)
    {
        _links = [];
        _links.AddRange(
            links.Select(
                link => new LinkResult(
                    link.Id,
                    link.Name,
                    link.Url,
                    link.UserId)
            )
        );
    }
}
