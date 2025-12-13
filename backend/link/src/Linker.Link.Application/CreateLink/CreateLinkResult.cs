using Linker.Link.Application.Commons.Results;

namespace Linker.Link.Application.CreateLink;

public record CreateLinkResult : LinkResult
{
    public CreateLinkResult(
        string Id,
        string Name,
        string Url,
        string UserId) : base(
            Id,
            Name,
            Url,
            UserId)
    { }

    public static CreateLinkResult FromDomainEntity(
        Domain.Entities.Link link
    ) =>
        new(
            link.Id,
            link.Name,
            link.Url,
            link.UserId);
}
