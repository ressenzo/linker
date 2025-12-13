using Linker.Link.Application.Commons.Results;

namespace Linker.Link.Application.GetLinksByUserId;

public record GetLinksByUserIdResult
{
    private readonly List<LinkResult> _links;

    public IEnumerable<LinkResult> Links => _links;

    public GetLinksByUserIdResult(IEnumerable<Domain.Entities.Link> links)
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
