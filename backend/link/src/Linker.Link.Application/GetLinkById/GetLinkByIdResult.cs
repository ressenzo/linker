using Linker.Link.Application.Commons.Results;

namespace Linker.Link.Application.GetLinkById;

public record GetLinkByIdResult : LinkResult
{
    public GetLinkByIdResult(
        string Id,
        string Name,
        string Url,
        string UserId) : base(
            Id,
            Name,
            Url,
            UserId)
    {
    }

    public static GetLinkByIdResult FromDomainEntity(
        Domain.Entities.Link link
    ) =>
        new(
            link.Id,
            link.Name,
            link.Url,
            link.UserId);
}
